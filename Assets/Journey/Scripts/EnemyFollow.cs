using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이 상태는 늑대와 사슴만 사용
public class EnemyFollow : IEnemyState
{
    private Enemy enemy;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        enemy.state = Enemy.STATE.FOLLOW;

        enemy.nav.isStopped = false;
        enemy.ani.SetBool("isRun", true);
        enemy.target = enemy.player.transform.position;
    }
    public void Update()
    {
        //공격 범위에 있을 때
        if (enemy.distance < enemy.attackBoundary)
            enemy.ChangeState(new EnemyAttack());

        //공격 범위에 없고, 추적 범위에 없을 때
        else if (enemy.distance > enemy.followBoundary) 
        {
            enemy.ChangeState(new EnemyWalk());
            enemy.isDamaged = false;
        }
    }
    public void Exit()
    {
        enemy.ani.SetBool("isRun", false);
    }
}
