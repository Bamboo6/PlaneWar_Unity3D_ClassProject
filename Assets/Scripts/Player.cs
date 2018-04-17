using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("MyGame/Player")]
public class Player : MonoBehaviour {

    

    public Transform m_rocket;
    public float m_life = 3;//生命值
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

    private void Awake()
    {
        m_transform = transform;
    }

    void Start ()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        leftText.text = "生命：" + m_life.ToString();
    }
	
	void Update () {

        m_timer -= Time.deltaTime;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

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
       

        //鼠标控制子弹发射
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(m_rocket,transform.position,transform.rotation);
            if (isRocketProp)
            {
                Instantiate(m_rocket, transform.position, Quaternion.Euler(0,225,0));
                Instantiate(m_rocket, transform.position, Quaternion.Euler(0, 135, 0));
                

            }
            audioSource.PlayOneShot(audioClip);

        }
        //空格控制子弹发射
        if (m_timer <= 0)
        {
            m_timer = 0.2f;
            if (Input.GetKey(KeyCode.Space))
            {
                Instantiate(m_rocket, transform.position, transform.rotation);
                if (isRocketProp)
                {
                    Instantiate(m_rocket, transform.position, Quaternion.Euler(0, 200, 0));
                    Instantiate(m_rocket, transform.position, Quaternion.Euler(0, 160, 0));


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
        if (other.gameObject.tag != "PlayerRocket"&&other.gameObject.tag!="RocketProp" && other.gameObject.tag != "HPAdd")
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
            rocketPropTime = 3;
        }
        if (other.gameObject.tag == "HPAdd")
        {
            m_life++;
            leftText.text = "生命：" + m_life.ToString();

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
