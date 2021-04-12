using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : IEnemyState
{
    //이 상태는 늑대와 닭만 사용
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

        if (parent.isAttackArea()) //공격 범위에 있을 때
            parent.ChangeState(new EnemyAttack());
        else if (!parent.isFollowArea()) //공격 범위에 없고, 추적 범위에 없을 때
        {
            parent.ChangeState(new EnemyIdle());
            parent.isDamaged = false;
        }

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
