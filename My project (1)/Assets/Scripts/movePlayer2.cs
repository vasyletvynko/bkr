using System;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer2 : MonoBehaviour
{
    const float stickyTime = 0.05f;
    const float stickyForce = 9.6f;
    protected const float coyoteDelay = 0.1f;

    public float maxForwardSpeed = 13f;
    [Range(1, 60)]
    public float acceleration = 20.0f;

    [Range(0, 500)]
    public float maxRotateSpeed = 150f;
    public float jumpSpeed = 20f;
    public float gravity = 40f;

    public AudioClip jumpAudioClip;
    public AudioClip doubleJumpAudioClip;
    public AudioClip landAudioClip;

    [SerializeField]
    private FixedJoystick FixedJoystick;

    protected CharacterController controller;
    protected Animator animator;
    protected AudioSource audioSource;

    protected bool airborne;
    protected float airborneTime;
    protected int jumpsInAir;
    protected Vector3 directSpeed;
    protected bool exploded;
    protected bool stepped;

    protected float speed;
    protected float rotateSpeed;
    protected Vector3 moveDelta = Vector3.zero;
    protected bool stopSpecial;
    protected bool cancelSpecial;

    [SerializeField] private UIJump buttonJump;

    float externalRotation;
    Vector3 externalMotion;

    Transform groundedTransform;
    Vector3 groundedLocalPosition;
    Vector3 oldGroundedPosition;
    Quaternion oldGroundedRotation;

    protected static readonly int jumpHash = Animator.StringToHash("Jump");
    protected virtual void OnValidate()
    {
        maxForwardSpeed = Mathf.Clamp(maxForwardSpeed, 5, 30);
    }

    protected virtual void Awake()
    {
        controller = GetComponent<CharacterController>();
        if (controller.enabled)
        {
            controller.Move(Vector3.down * 0.01f);
        }
    }

    private float Horizontal()
    {
        if (FixedJoystick.Horizontal != 0)return FixedJoystick.Horizontal;
        else return Input.GetAxisRaw("Horizontal");
    }

    private float Vertical()
    {
        if (FixedJoystick.Vertical != 0) return FixedJoystick.Vertical;
        else return Input.GetAxisRaw("Vertical");
    }

    protected virtual void Update()
    {
        var right = Vector3.right;
        var forward = Vector3.forward;

        var targetSpeed = right * Horizontal();
        targetSpeed += forward * Vertical();


        if (targetSpeed.sqrMagnitude > 0.0f)
        {
            targetSpeed.Normalize();
        }
        targetSpeed *= maxForwardSpeed;

        var speedDiff = targetSpeed - directSpeed;
        if (speedDiff.sqrMagnitude < acceleration * acceleration * Time.deltaTime * Time.deltaTime)
        {
            directSpeed = targetSpeed;
        }
        else if (speedDiff.sqrMagnitude > 0.0f)
        {
            speedDiff.Normalize();

            directSpeed += speedDiff * acceleration * Time.deltaTime;
        }
        speed = directSpeed.magnitude;

        rotateSpeed = 0.0f;
        if (targetSpeed.sqrMagnitude > 0.0f)
        {
            var localTargetSpeed = transform.InverseTransformDirection(targetSpeed);
            var angleDiff = Vector3.SignedAngle(Vector3.forward, localTargetSpeed.normalized, Vector3.up);

            if (angleDiff > 0.0f)
            {
                rotateSpeed = maxRotateSpeed;
            }
            else if (angleDiff < 0.0f)
            {
                rotateSpeed = -maxRotateSpeed;
            }

            if (Mathf.Abs(rotateSpeed) > Mathf.Abs(angleDiff) / Time.deltaTime)
            {
                rotateSpeed = angleDiff / Time.deltaTime;
            }
        }

        moveDelta = new Vector3(directSpeed.x, moveDelta.y, directSpeed.z);

        if (Input.GetButtonDown("Jump")|| buttonJump.isDown)
        {
            if (!airborne || jumpsInAir > 0)
            {
                if (airborne)
                {
                    jumpsInAir--;
                }
                else
                {
                    if (jumpAudioClip)
                    {
                        audioSource.PlayOneShot(jumpAudioClip);
                    }
                }

                moveDelta.y = jumpSpeed;

                //animator.SetTrigger(jumpHash);

                airborne = true;
                airborneTime = coyoteDelay;
            }
        }

        HandleMotion();
    }

    protected void HandleMotion()
    {
        // Handle external motion.
        externalMotion = Vector3.zero;
        externalRotation = 0.0f;

        var wasGrounded = controller.isGrounded;

        if (!controller.isGrounded)
        {
            // Apply gravity.
            moveDelta.y -= gravity * Time.deltaTime;

            groundedTransform = null;

            airborneTime += Time.deltaTime;
        }
        else
        {
            // Apply external motion and rotation.
            if (groundedTransform && Time.deltaTime > 0.0f)
            {
                var newGroundedPosition = groundedTransform.TransformPoint(groundedLocalPosition);
                externalMotion = (newGroundedPosition - oldGroundedPosition) / Time.deltaTime;
                oldGroundedPosition = newGroundedPosition;

                var newGroundedRotation = groundedTransform.rotation;
                // FIXME Breaks down if rotating more than 180 degrees per frame.
                var diffRotation = newGroundedRotation * Quaternion.Inverse(oldGroundedRotation);
                var rotatedRight = diffRotation * Vector3.right;
                rotatedRight.y = 0.0f;
                if (rotatedRight.magnitude > 0.0f)
                {
                    rotatedRight.Normalize();
                    externalRotation = Vector3.SignedAngle(Vector3.right, rotatedRight, Vector3.up) / Time.deltaTime;
                }
                oldGroundedRotation = newGroundedRotation;
            }
        }

        // Move minifig - check if game object was made inactive in some callback to avoid warnings from CharacterController.Move.
        if (gameObject.activeInHierarchy)
        {
            // Use a sticky move to make the minifig stay with moving platforms.
            var stickyMove = airborneTime < stickyTime ? Vector3.down * stickyForce * Time.deltaTime : Vector3.zero;
            controller.Move((moveDelta + externalMotion) * Time.deltaTime + stickyMove);
        }

        // If becoming grounded by this Move, reset y movement and airborne time.
        if (!wasGrounded && controller.isGrounded)
        {
            // Play landing sound if landing sufficiently hard.
            if (moveDelta.y < -5.0f)
            {
                if (landAudioClip)
                {
                    audioSource.PlayOneShot(landAudioClip);
                }
            }

            moveDelta.y = 0.0f;
            airborneTime = 0.0f;
        }

        // Update airborne state.
        airborne = airborneTime >= coyoteDelay;

        // Rotate minifig.
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        transform.RotateAround(oldGroundedPosition, Vector3.up, externalRotation * Time.deltaTime);

        // Stop special if requested.
        cancelSpecial |= stopSpecial;
        stopSpecial = false;

    }
}

