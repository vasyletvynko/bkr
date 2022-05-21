using UnityEngine;
using System.Collections;

public abstract class BaseProjectile : MonoBehaviour
{
    [SerializeField] public float speedProjectile = 5.0f;

    public abstract void FireProjectile(GameObject launcher, GameObject target);
}