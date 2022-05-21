using UnityEngine;
using System.Collections;

public class NormalProjectile : BaseProjectile
{
    private Vector3 direction;
    private bool fired;

    void Update()
    {
        if (fired)
        {
            transform.position += direction * (speedProjectile * Time.deltaTime);
        }
    }

    public override void FireProjectile(GameObject launcher, GameObject target)
    {
        if (launcher && target)
        {
            direction = (target.transform.position - launcher.transform.position).normalized;
            fired = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
    
}