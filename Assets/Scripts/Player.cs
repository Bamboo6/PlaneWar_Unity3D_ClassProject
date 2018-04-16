using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("MyGame/Player")]
public class Player : MonoBehaviour {

    

    public Transform m_rocket;
    public float m_life = 3;
    public GameObject boom;
    public AudioClip audioClip;
    public GameObject again;
    public GameObject quit;
    

    private AudioSource audioSource;
    private float m_speed = 5;
    private Transform m_transform;
    private float m_timer;

    private void Awake()
    {
        m_transform = transform;
    }

    void Start ()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
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

        //鼠标控制子弹发射
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(m_rocket,transform.position,transform.rotation);
            audioSource.PlayOneShot(audioClip);

        }
        //空格控制子弹发射
        if (m_timer <= 0)
        {
            m_timer = 0.2f;
            if (Input.GetKey(KeyCode.Space))
            {
                Instantiate(m_rocket, transform.position, transform.rotation);
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
        if (other.gameObject.tag != "PlayerRocket")
        {
            PlayerDie();
            GameManager.Instance.GameOver();
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
