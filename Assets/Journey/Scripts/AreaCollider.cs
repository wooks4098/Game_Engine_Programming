using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCollider : MonoBehaviour
{
    private Enemy enemy;
    private Player player;
    private SphereCollider col;
    private RaycastHit hit;
    private float maxDistance;

    private void Awake()
    {
        enemy = transform.parent.gameObject.GetComponent<Enemy>();
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        col = GetComponent<SphereCollider>();
        maxDistance = col.radius; //레이캐스트 거리
    }

    private void Update()
    {
        //플레이어가 추적 범위에 있을때만 레이를 쏘도록
        if (enemy.inFollowArea)
        {
            Vector3 dir = (player.transform.position - transform.position).normalized; //플레이어로 향하는 정규화 벡터

            if (Physics.Raycast(transform.position, dir, out hit, maxDistance))
            {
                //레이의 충돌체가 플레이어라면
                if (hit.transform.tag == "Player")
                    enemy.hasObstacle = false; //장애물이 없다고 판단
                else
                    enemy.hasObstacle = true; //장애물이 있다고 판단
            }
            
            //레이 시각화
            Debug.DrawRay(transform.position, dir * maxDistance, Color.red);
        }
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
            {
                enemy.inFollowArea = false;
            }
        }
    }
}
