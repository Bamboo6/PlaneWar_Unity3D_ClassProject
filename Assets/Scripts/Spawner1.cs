using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner1 : MonoBehaviour
{
    public GameObject enemy1;
    public GameObject enemy2;
    private float timer;
    private void Start()
    {
        timer = Random.Range(0, 3);
    }

    /// <summary>
    /// 敌人的生成
    /// </summary>
    void Update()
    {
        EnemyBorn();
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
            float index = Random.Range(0, 15);
            if (index <= 3)
            {
                Instantiate(enemy2, transform.position, transform.rotation);
            }
            else
            {
                Instantiate(enemy1, transform.position, transform.rotation);
            }
        }
    }
}
