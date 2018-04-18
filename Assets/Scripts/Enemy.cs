using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("MyGame/Enemy")]
public class Enemy : MonoBehaviour
{

    public float m_life = 1;
    //速度
    public float m_speed = 1;
    public GameObject boom;

    public Rocket rocket;

    //旋转速度
    protected float m_rotSpeed = 45;
    //变向间隔时间
    protected float m_timer = 1.5f;
    protected Transform m_transform;
    

    private void Awake()
    {
        m_transform = transform;
    }
    
    void Start () {
		
	}
	
	void Update () {
        UpdateMove();
	}

    //敌人移动
    protected virtual void UpdateMove()
    {

        m_timer -= Time.deltaTime;
        if(m_timer <= 0)
        {
            m_timer = 3;
            //改变旋转方向
            m_rotSpeed = -m_rotSpeed;
        }

        //旋转方向
        m_transform.Rotate(Vector3.up, m_rotSpeed * Time.deltaTime, Space.World);
        //前进
        m_transform.Translate(new Vector3(0, 0, -m_speed * Time.deltaTime));
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerRocket")
        {
            //获取子弹的威力
            Rocket rocket = other.GetComponent<Rocket>();
            if (rocket != null)
            {
                m_life -= rocket.m_power;

                if (m_life <= 0)
                {
                    EnemyDie();
                    GameManager.Instance.AddScore();
                }

            }
        }
        else if(other.gameObject.tag == "Player")
        {
            EnemyDie();
        }
        else if (other.gameObject.tag == "Bound")
        {
            EnemyDie();
            
        }
    }

    /// <summary>
    /// 敌人死亡
    /// </summary>
    private void EnemyDie()
    {
        GameObject.Instantiate(boom, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        m_life = 0;
        
    }
}
