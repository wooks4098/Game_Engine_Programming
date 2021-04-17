using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunaway : IEnemyState
{
    //이 상태는 양만 사용

    private Enemy parent;

    public void Enter(Enemy parent)
    {
        this.parent = parent;

        parent.ani.SetBool("isRun", true);

        //확인용
        Debug.Log(parent.name + "의 Runaway Enter");
    }
    public void Update()
    {
        //새 목표지점 = 플레이어 반대 방향
        parent.nav.ResetPath();
        //parent.nav.SetDestination(parent.RunawayPoint.position);

        //추적 범위에 없을 때
        if (Vector3.Distance(parent.transform.position, parent.target.position) > parent.nav.stoppingDistance * 3)
        {
            parent.isDamaged = false;
            parent.ChangeState(new EnemyIdle());
        }

        //확인용
        Debug.Log(parent.name + "의 Runaway Update");
    }
    public void Exit()
    {
        parent.nav.ResetPath();
        parent.ani.SetBool("isRun", false);

        //확인용
        Debug.Log(parent.name + "의 Runaway Exit");
    }
}
