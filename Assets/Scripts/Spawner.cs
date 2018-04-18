using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject rocketProp;
    public GameObject hpAdd;
    public GameObject powerProp;
    private float timer ;
    private float rocketPropTime = 5;
    private float hpAddPropTime = 10;
    private float powerAddPropTime = 8;
    private void Start()
    {
        timer = Random.Range(0, 3);
    }

    /// <summary>
    /// 敌人的生成
    /// </summary>
    void Update () {
        EnemyBorn();
        PropBorn();
	}

    private void EnemyBorn()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            float posX = Random.Range(-7, 8);
            float posZ = 10;
            transform.position = new Vector3(posX, 0, posZ);
            timer = Random.Range(3, 7);
            float index = Random.Range(0,15);
            if (index <= 3)
            {
                Instantiate(enemy2,transform.position,transform.rotation);
            }
            else
            {
                Instantiate(enemy1, transform.position, transform.rotation);
            }
        }
    }

    private void PropBorn()
    {
        rocketPropTime -= Time.deltaTime;
        if (rocketPropTime <= 0)
        {
            float posX = Random.Range(-8, 8);
            float posZ = Random.Range(-8, 0);
            transform.position = new Vector3(posX, 0, posZ);
            rocketPropTime = Random.Range(5, 10);
            Instantiate(rocketProp, transform.position, rocketProp.transform.rotation);
        }

        hpAddPropTime -= Time.deltaTime;
        if (hpAddPropTime <= 0)
        {
            float posX = Random.Range(-8, 8);
            float posZ = Random.Range(-8, 0);
            transform.position = new Vector3(posX, 0, posZ);
            hpAddPropTime = Random.Range(10, 20);
            Instantiate(hpAdd, transform.position, hpAdd.transform.rotation);
        }

        powerAddPropTime -= Time.deltaTime;
        if (powerAddPropTime <= 0)
        {
            float posX = Random.Range(-8, 8);
            float posZ = Random.Range(-8, 0);
            transform.position = new Vector3(posX, 0, posZ);
            powerAddPropTime = Random.Range(7, 10);
            Instantiate(powerProp, transform.position, powerProp.transform.rotation);
        }
    }
}
