using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : IEnemyState
{
    //이 상태는 늑대와 닭만 사용
    private Enemy enemy;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;

        enemy.nav.isStopped = false;
        enemy.ani.SetBool("isRun", true);

        //확인용
        Debug.Log(enemy.name + "의 Follow Enter");
    }
    public void Update()
    {
        enemy.nav.SetDestination(enemy.playerPos.position);

        //공격 범위에 있을 때
        if (enemy.inAttackArea)
            enemy.ChangeState(new EnemyAttack());

        //공격 범위에 없고, 추적 범위에 없을 때
        else if (!enemy.CheckFollow()) 
        {
            enemy.ChangeState(new EnemyWalk());
            enemy.isDamaged = false;
        }

            //확인용
            Debug.Log(enemy.name + "의 Follow Update");
    }
    public void Exit()
    {
        enemy.ani.SetBool("isRun", false);

        //확인용
        Debug.Log(enemy.name + "의 Follow Exit");
    }
}
