using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWalk : IEnemyState
{
    private Enemy parentEnemy;
    private EnemySpawner enemySpawner;

    public void Enter(Enemy enemy)
    {
        this.parentEnemy = enemy;
        enemy.nav.ResetPath();

        //확인용
        Debug.Log(enemy.name + "의 Idle Enter");
    }

    public void Update()
    {
        parentEnemy.nav.SetDestination(parentEnemy.DestinationPos.position);

        switch (parentEnemy.name)
        {
            case "Wolf":
                //추적 범위에 있을 때
                if (parentEnemy.isFollowArea())
                    parentEnemy.ChangeState(new EnemyFollow());
                break;
            case "Chicken":
                //플레이어에게 공격 당했고, 추적 범위에 있을 때 
                if (parentEnemy.isDamaged && parentEnemy.isFollowArea())
                    parentEnemy.ChangeState(new EnemyFollow());
                break;
            case "Sheep":
                //플레이어에게 공격 당했고, 추적 범위에 있을 때
                if (parentEnemy.isDamaged && parentEnemy.isFollowArea())
                    parentEnemy.ChangeState(new EnemyRunaway());
                break;
        }

        //확인용
        Debug.Log(parentEnemy.name + "의 Idle Update");
    }

    public void Exit()
    {
        //확인용
        Debug.Log(parentEnemy.name + "의 Idle Exit");
    }


}