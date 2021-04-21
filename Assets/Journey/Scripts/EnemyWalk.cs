using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWalk : MonoBehaviour, IEnemyState
{
    private Enemy parentEnemy;
    private EnemySpawner enemySpawner;

    public void Enter(Enemy parentEnemy)
    {
        this.parentEnemy = parentEnemy;

        parentEnemy.nav.ResetPath();
        parentEnemy.ani.SetBool("isWalk", true);

        //확인용
        Debug.Log(parentEnemy.name + "의 Idle Enter");
    }

    public void Update()
    {
        parentEnemy.nav.SetDestination(parentEnemy.DestinationPos);

        switch (parentEnemy.name)
        {
            case "Wolf":
                //추적 범위에 있을 때
                if (parentEnemy.CheckFollow())
                    parentEnemy.ChangeState(new EnemyFollow());
                break;
            case "Chicken":
                //플레이어에게 공격 당했고, 추적 범위에 있을 때
                if (parentEnemy.isDamaged && parentEnemy.CheckFollow())
                    parentEnemy.ChangeState(new EnemyFollow());
                break;
            case "Sheep":
                //플레이어에게 공격 당했고, 추적 범위에 있을 때
                if (parentEnemy.isDamaged && parentEnemy.CheckFollow())
                    parentEnemy.ChangeState(new EnemyRunaway());
                break;
        }

        //확인용
        Debug.Log(parentEnemy.name + "의 Idle Update");
    }

    public void Exit()
    {
        parentEnemy.ani.SetBool("isWalk", false);

        //확인용
        Debug.Log(parentEnemy.name + "의 Idle Exit");

    }

}
