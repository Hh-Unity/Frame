using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private TriggerCheck mWallCheck;
    private TriggerCheck mFallCheck;
    private TriggerCheck mGroundCheck;
  

    private Rigidbody2D mRigidbody2D;

    private void Awake()
    {
        mWallCheck = transform.Find("wallCheck").GetComponent<TriggerCheck>();
        mFallCheck = transform.Find("fallCheck").GetComponent<TriggerCheck>();
        mGroundCheck = transform.Find("groundCheck").GetComponent<TriggerCheck>();

        mRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        var scaleX = transform.localScale.x;

        if (mGroundCheck.IsTrigger && mFallCheck.IsTrigger && !mWallCheck.IsTrigger)
        {
            mRigidbody2D.velocity = new Vector2(scaleX * 10, mRigidbody2D.velocity.y);
        }
        else
        {
            // 反转
            var localScale = transform.localScale;
            localScale.x = -scaleX;
            transform.localScale = localScale;
        }
    }
}
