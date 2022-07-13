using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    private Bullet m_Bullet;
    void Start()
    {
        m_Bullet = transform.Find("bullet").GetComponent<Bullet>();
    }

    public void Shoot()
    {
        Transform bullet = Instantiate(m_Bullet.transform, m_Bullet.transform.position, m_Bullet.transform.rotation);
        bullet.localScale = m_Bullet.transform.lossyScale;
        bullet.gameObject.SetActive(true);
    }
}
