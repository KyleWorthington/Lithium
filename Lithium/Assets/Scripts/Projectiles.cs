using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    private Rigidbody2D m_rigidbody2D;

    private void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void LaunchProjectile(Vector2 direction, float force)
    {
        m_rigidbody2D.AddForce(direction * force);
    }
}
