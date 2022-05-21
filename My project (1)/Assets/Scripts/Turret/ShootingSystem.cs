using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootingSystem : MonoBehaviour
{
    public float fireRate;
    public GameObject projectile;
    public GameObject target;
    public List<GameObject> projectileSpawns;
    
    List<GameObject> m_lastProjectiles = new List<GameObject>();

    float m_fireTimer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (m_lastProjectiles.Count <= 0)
        {
            float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position));

                SpawnProjectiles();
        }
        else
        {
            m_fireTimer += Time.deltaTime;

            if (m_fireTimer >= fireRate)
            {
                float angle = Quaternion.Angle(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position));

                    SpawnProjectiles();

                    m_fireTimer = 0.0f;
            }
        }
    }

    void SpawnProjectiles() 
    {
        if (!projectile)
        {
            return;
        }

        m_lastProjectiles.Clear();

        for (int i = 0; i < projectileSpawns.Count; i++)
        {
            if (projectileSpawns[i])
            {
                GameObject proj = Instantiate(projectile, projectileSpawns[i].transform.position, Quaternion.Euler(projectileSpawns[i].transform.forward)) as GameObject;
                proj.GetComponent<BaseProjectile>().FireProjectile(projectileSpawns[i], target);

                m_lastProjectiles.Add(proj);
            }
        }
    }

    void RemoveLastProjectiles()
    {
        while (m_lastProjectiles.Count > 0)
        {
            Debug.Log("qw");
            Destroy(m_lastProjectiles[0]);
            m_lastProjectiles.RemoveAt(0);
        }
    }
}