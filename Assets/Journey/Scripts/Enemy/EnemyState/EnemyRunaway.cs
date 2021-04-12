using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunaway : IEnemyState
{
    private Enemy parent;

    public void Enter(Enemy parent)
    {
        this.parent = parent;

        parent.ani.SetBool("isRun", true);

        //확인용
        Debug.Log(parent.name + "의 Runaway Enter");
        parent.state = Enemy.STATE.RUNAWAY;
    }
    public void Update()
    {
        //새 목표지점 = 플레이어 반대 방향
        parent.nav.ResetPath();
        parent.nav.SetDestination(parent.RunawayPoint.position);

        //확인용
        Debug.Log(parent.name + "의 Runaway Update");
    }
    public void Exit()
    {
        parent.ani.SetBool("isRun", false);

        //확인용
        Debug.Log(parent.name + "의 Runaway Exit");
    }
}
