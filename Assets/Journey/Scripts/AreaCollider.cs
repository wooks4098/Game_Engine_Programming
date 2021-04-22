using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCollider : MonoBehaviour
{
    Enemy enemy;
    Player player;





    private void Awake()
    {
        enemy = transform.parent.gameObject.GetComponent<Enemy>();
        player = transform.parent.gameObject.GetComponent<Player>();
    }

    private void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {
        //플레이어와 충돌했다면
        if (other.tag == "Player")
        {
            if (this.name == "AttackArea")
                enemy.inAttackArea = true;
            if (this.name == "FollowArea")
                enemy.inFollowArea = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //플레이어와 충돌했다면
        if (other.tag == "Player")
        {
            if (this.name == "AttackArea")
                enemy.inAttackArea = false;
            if (this.name == "FollowArea")
                enemy.inFollowArea = false;
        }
    }
}
