using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingEditor2D
{
    public class CameraController : MonoBehaviour
    {
        private Transform m_playerTransform;
        private Vector3 targetPos;
        private float m_minX = -5;
        private float m_minY = -5;
        private float m_maxX = 5;
        private float m_maxY = 5;
        void Update()
        {
            if (!m_playerTransform)
            {
                GameObject playerGameObject = GameObject.FindWithTag("Player");
                if (playerGameObject)
                    m_playerTransform = playerGameObject.transform;
                else
                    return;
            }

            Vector3 cameraPos = transform.position;
            Vector3 playerPos = m_playerTransform.position;
            float isRight = Mathf.Sign(m_playerTransform.localScale.x);
            targetPos.x = playerPos.x + 3 * isRight;
            targetPos.y = playerPos.y + 2;
            targetPos.z = -10;

            int smoothSpeed = 5;
            Vector3 position = transform.position;
            position = Vector3.Lerp(position, new Vector3(targetPos.x, targetPos.y, position.z), smoothSpeed * Time.deltaTime);
            transform.position = new Vector3(Mathf.Clamp(position.x, m_minX, m_maxX), Mathf.Clamp(position.y, m_minY, m_maxY), position.z);
        }
    }
}

