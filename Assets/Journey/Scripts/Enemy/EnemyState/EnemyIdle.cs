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

        if (parent.isFollowArea())
            parent.ChangeState(new EnemyFollow());

        //확인용
        Debug.Log(parent.name + "의 Idle Update");

    }
    public void Exit()
    {
        //확인용
        Debug.Log(parent.name + "의 Idle Exit");
    }
}
