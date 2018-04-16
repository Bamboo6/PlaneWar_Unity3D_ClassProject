using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("MyGame/SuperEnemy")]
public class SuperEnemy : Enemy {

    public Transform m_rocket;
    protected float m_fireTimer = 3;
  

    /// <summary>
    /// 发射子弹，移动
    /// </summary>
    protected override void UpdateMove()
    {
        m_fireTimer -= Time.deltaTime;
        if(m_fireTimer <= 0)
        {
            m_fireTimer = 3;
            Instantiate(m_rocket, transform.position, transform.rotation);
        }
        //前进
        m_transform.Translate(new Vector3(0, 0, -m_speed * Time.deltaTime));
    }
}
