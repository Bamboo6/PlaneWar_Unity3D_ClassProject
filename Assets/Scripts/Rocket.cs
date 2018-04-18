using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

    public int m_power = 2;

    public float rocketSpeed = 10;
    private Transform m_transform;


    private void Awake()
    {
        m_transform = transform;
    }
    public void SetPower(int a)
    {
        m_power = a;
    }
    void Start () {
        Destroy(gameObject, 5f);
	}
	
	void Update () {
        //子弹飞行
        m_transform.Translate(-Vector3.forward * rocketSpeed * Time.deltaTime);
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(this.gameObject);
        }
    }
}
