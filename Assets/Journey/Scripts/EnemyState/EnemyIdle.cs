using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdle : IEnemyState
{
    private Enemy parent;

    public void Enter(Enemy parent)
    {
        this.parent = parent;
        parent.nav.isStopped = true;

        //확인용
        Debug.Log(parent.name + "의 Idle Enter");
    }
    public void Update()
    {
        parent.nav.SetDestination(parent.target.position);

        switch (parent.name)
        {
            case "Wolf":
                //추적 범위에 있을 때
                if (parent.isFollowArea())
                    parent.ChangeState(new EnemyFollow());
                break;
            case "Chicken":
                //플레이어에게 공격 당했고, 추적 범위에 있을 때
                if (parent.isDamaged && parent.isFollowArea())
                    parent.ChangeState(new EnemyFollow());
                break;
            case "Sheep":
                //플레이어에게 공격 당했고, 추적 범위에 있을 때
                if (parent.isDamaged && parent.isFollowArea())
                    parent.ChangeState(new EnemyRunaway());
                break;
        }
        

        //확인용
        Debug.Log(parent.name + "의 Idle Update");

    }
    public void Exit()
    {
        //확인용
        Debug.Log(parent.name + "의 Idle Exit");
    }
}