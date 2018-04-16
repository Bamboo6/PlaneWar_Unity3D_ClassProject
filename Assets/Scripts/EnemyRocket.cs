using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRocket : Rocket{

    /// <summary>
    /// 敌人的子弹
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player") return;
        Destroy(gameObject);
    }
}
