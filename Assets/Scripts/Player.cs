using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]//序列化
public class FlyEdge//飞行边界类
{
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
}

[AddComponentMenu("MyGame/Player")]
public class Player : MonoBehaviour {
    public FlyEdge border;

    public Transform m_rocket;
    private float m_life = 10;//生命值
    public GameObject boom;
    public AudioClip audioClip;
    public GameObject again;
    public GameObject quit;
    public Text leftText;
    

    private AudioSource audioSource;
    private float m_speed = 5;
    private Transform m_transform;
    private float m_timer;

    public bool isRocketProp = false;
    public float rocketPropTime = 0;

    public bool isPowerProp = false;
    public float powerPropTime = 0;

    private Rocket rocketpower;

    private void Awake()
    {
        m_transform = transform;
        rocketpower = m_rocket.GetComponent<Rocket>();
    }

    void Start ()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        m_rocket.transform.localScale = new Vector3(1, 1, 1);
        leftText.text = "生命：" + m_life.ToString();
    }
	
	void Update () {

        m_timer -= Time.deltaTime;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        //边界值获取与限定
        this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x,border.minX,border.maxX),0,Mathf.Clamp(this.transform.position.z, border.minZ, border.maxZ));

        //飞机移动
        if(h != 0 || v != 0)
        {
            m_transform.Translate(-(new Vector3(h, 0, v)) * m_speed * Time.deltaTime);
        }

        if (isRocketProp)
        {
        rocketPropTime -= Time.deltaTime;
            if (rocketPropTime <= 0)
            {
              isRocketProp = false;
            }
        }

        if (isPowerProp)
        {
            powerPropTime -= Time.deltaTime;
            if (powerPropTime <= 0)
            {
                isPowerProp = false;
                m_rocket.transform.localScale = new Vector3(1, 1, 1);
                rocketpower.SetPower(2);
            }
        }


        //鼠标控制子弹发射
        if (Input.GetMouseButtonDown(0))
        {
            
            if (isRocketProp)
            {
                Instantiate(m_rocket, transform.position, Quaternion.Euler(0, 200, 0));
                Instantiate(m_rocket, transform.position, Quaternion.Euler(0, 160, 0));
            }
            if (isPowerProp)
            {
                m_rocket.transform.localScale = new Vector3(3, 3, 3);
                Instantiate(m_rocket, transform.position, transform.rotation);
                rocketpower.SetPower(5);
            }
            else
            {
                Instantiate(m_rocket, transform.position, transform.rotation);
            }
            audioSource.PlayOneShot(audioClip);

        }
        //空格控制子弹发射
        if (m_timer <= 0)
        {
            m_timer = 0.2f;
            if (Input.GetKey(KeyCode.Space))
            {
                if (isRocketProp)
                {
                    Instantiate(m_rocket, transform.position, Quaternion.Euler(0, 200, 0));
                    Instantiate(m_rocket, transform.position, Quaternion.Euler(0, 160, 0));
                }
                if (isPowerProp)
                {
                    m_rocket.transform.localScale = new Vector3(3, 3, 3);
                    Instantiate(m_rocket, transform.position, transform.rotation);
                    rocketpower.SetPower(5);
                }
                else
                {
                    Instantiate(m_rocket, transform.position, transform.rotation);
                }
                audioSource.PlayOneShot(audioClip);
            }

        }

        

        if (m_life <= 0)
        {
            Destroy(this.gameObject);
        }
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "PlayerRocket" && other.gameObject.tag!="RocketProp" && other.gameObject.tag != "HPAdd" && other.gameObject.tag != "PowerProp")
        {
            m_life--;
            leftText.text = "生命：" + m_life.ToString();
            if (m_life <= 0)
            {
                PlayerDie();
            }
            GameManager.Instance.GameOver();
        }
        if (other.gameObject.tag == "RocketProp")
        {
            isRocketProp = true;
            rocketPropTime = 5;
        }
        if (other.gameObject.tag == "HPAdd")
        {
            m_life++;
            leftText.text = "生命：" + m_life.ToString();
        }
        if (other.gameObject.tag == "PowerProp")
        {
            isPowerProp = true;
            powerPropTime = 5;
        }
    }

    /// <summary>
    /// 玩家死亡
    /// </summary>
    private void PlayerDie()
    {
        Destroy(gameObject);
        GameObject.Instantiate(boom, transform.position, Quaternion.identity);
        again.SetActive(true);
        quit.SetActive(true);
    }
}
