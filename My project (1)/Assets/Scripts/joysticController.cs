using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class joysticController : MonoBehaviour
{
    [SerializeField]
    private FixedJoystick fj;

    public float Horizontal()
    {
        if (fj.Horizontal != 0) return fj.Horizontal;
        else return Input.GetAxisRaw("Horizontal");
    }

    public float Vertical()
    {
        if (fj.Vertical != 0) return fj.Vertical;
        else return Input.GetAxisRaw("Vertical");
    }

    public bool jumpOnButton()
    {
        return true;
    }
}
