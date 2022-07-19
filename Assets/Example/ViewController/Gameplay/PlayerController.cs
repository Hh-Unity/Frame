using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingEditor2D
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D m_Rigidbody;
        private TriggerCheck triggerCheck;
        private Gun gun;
        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody2D>();
            triggerCheck = transform.Find("trigger").GetComponent<TriggerCheck>();
            gun = transform.Find("gun").GetComponent<Gun>();
        }

        private bool isJump = false;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
                isJump = true;
            if (Input.GetKeyDown(KeyCode.J))
                gun.Shoot();
        }

        private void FixedUpdate()
        {
            float horizontalMove = Input.GetAxis("Horizontal");
            m_Rigidbody.velocity = new Vector2(horizontalMove * 5, m_Rigidbody.velocity.y);

            if (isJump && triggerCheck.IsTrigger)
            {
                isJump = false;
                m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, 5);
            }
        }
    }
}

