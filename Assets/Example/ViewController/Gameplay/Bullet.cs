using System;
using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;

namespace ShootingEditor2D
{
    public class Bullet : ShootingEditor2DController
    {
        private Rigidbody2D m_rigidbody; 
        void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            m_rigidbody.velocity = Vector2.right * 10 * Mathf.Sign(transform.localScale.x);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                this.SendCommand<KillEnemyCommand>();
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}

