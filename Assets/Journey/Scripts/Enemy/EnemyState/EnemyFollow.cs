using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : IEnemyState
{
    private Enemy parent;

    public void Enter(Enemy parent)
    {
        this.parent = parent;

        parent.nav.isStopped = false;
        parent.ani.SetBool("isRun", true);

        //확인용
        Debug.Log(parent.name + "의 Follow Enter");
        parent.state = Enemy.STATE.FOLLOW;
    }
    public void Update()
    {
        parent.nav.SetDestination(parent.target.position);

        if (!parent.isFollowArea())
            parent.ChangeState(new EnemyIdle());
        else if(parent.isAttackArea())
            parent.ChangeState(new EnemyAttack());

        //확인용
        Debug.Log(parent.name + "의 Follow Update");
    }
    public void Exit()
    {
        parent.ani.SetBool("isRun", false);

        //확인용
        Debug.Log(parent.name + "의 Follow Exit");
    }
}
