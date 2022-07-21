using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingEditor2D
{
    public class CameraController : MonoBehaviour
    {
        private Transform m_playerTransform;
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
            cameraPos.x = playerPos.x + 3;
            cameraPos.y = playerPos.y + 2;
            transform.position = cameraPos;
        }
    }
}

