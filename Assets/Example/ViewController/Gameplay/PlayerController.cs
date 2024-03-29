using System;
using System.Collections;
using System.Collections.Generic;
using HFrame2022;
using UnityEngine;

namespace ShootingEditor2D
{
    public class PlayerController : ShootingEditor2DController
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
            if (Input.GetKeyDown(KeyCode.R))
                gun.Reload();
            if (Input.GetKeyDown(KeyCode.P))
                this.SendCommand(new PickGunCommand("冲锋枪", 30, 100));
            if (Input.GetKeyDown(KeyCode.Q))
                this.SendCommand<ShiftGunCommand>();
        }

        private void FixedUpdate()
        {
            var horizontalMovement = Input.GetAxis("Horizontal");

            if (horizontalMovement < 0 && transform.localScale.x > 0 || 
                horizontalMovement > 0 && transform.localScale.x < 0) // +
            {
                var localScale =  transform.localScale;
                localScale.x = -localScale.x;
                transform.localScale = localScale;
          
            }

            m_Rigidbody.velocity = new Vector2(horizontalMovement * 5, m_Rigidbody.velocity.y);

            var grounded = triggerCheck.IsTrigger;
      
            if (isJump && grounded)
            {
                m_Rigidbody.velocity = new Vector2(m_Rigidbody.velocity.x, 5);
            }
      
            isJump = false;
        }
    }
}

