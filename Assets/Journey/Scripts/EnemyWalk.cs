using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWalk : IEnemyState
{
    private Enemy enemy;
    private EnemySpawner enemySpawner;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        enemy.state = Enemy.STATE.WANDER;

        enemy.ani.SetBool("isRun", true);
    }

    public void Update()
    {
        //무작위 벡터를 목표물의 위치에 더함
        enemy.wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * enemy.wanderJitter,
            0, Random.Range(-1.0f, 1.0f) * enemy.wanderJitter);

        enemy.wanderTarget = enemy.wanderTarget.normalized;
        enemy.wanderTarget *= enemy.wanderRadius; //정규화한 벡터에 원의 반경을 곱함

        enemy.target = enemy.transform.position + enemy.transform.TransformDirection(Vector3.forward * enemy.wanderDistance) + enemy.wanderTarget;

        switch (enemy.monster)
        {
            case Enemy.MONSTER.WOLF:
                //추적 범위에 있을 때
                if (enemy.distance < enemy.followBoundary)
                    enemy.ChangeState(new EnemyFollow());
                break;
            case Enemy.MONSTER.DEER:
                //플레이어에게 공격 당했고, 추적 범위에 있을 때 
                if (enemy.isDamaged && enemy.distance < enemy.followBoundary)
                    enemy.ChangeState(new EnemyFollow());
                break;
            case Enemy.MONSTER.RABIT:
                //플레이어에게 공격 당했고, 추적 범위에 있을 때
                if (enemy.isDamaged && enemy.distance < enemy.followBoundary)
                    enemy.ChangeState(new EnemyRunaway());
                break;
        }
    }

    public void Exit()
    {
        enemy.ani.SetBool("isRun", false);
    }


}