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
        parent.state = Enemy.STATE.IDLE;
    }
    public void Update()
    {
        parent.nav.SetDestination(parent.target.position);

        switch (parent.name)
        {
            case "Wolf":
                if (parent.isFollowArea()) //추적 범위에 있을 때
                    parent.ChangeState(new EnemyFollow());
                break;
            case "Chicken":
                if (parent.isDamaged && parent.isFollowArea()) //플레이어에게 공격 당했고, 추적 범위에 있을 때
                    parent.ChangeState(new EnemyFollow());
                break;
            case "Sheep":
                if (parent.isDamaged && parent.isFollowArea()) //플레이어에게 공격 당했고, 추적 범위에 있을 때
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

/*
switch (parent.name)
{
    case "Wolf":
        break;
    case "Chicken":
        break;
    case "Sheep":
        break;
}
*/