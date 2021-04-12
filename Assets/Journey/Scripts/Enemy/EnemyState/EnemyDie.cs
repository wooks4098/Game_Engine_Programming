using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDie : IEnemyState
{
    private Enemy parent;

    public void Enter(Enemy parent)
    {
        this.parent = parent;

        parent.ani.SetTrigger("Die");
        parent.nav.isStopped = true;
        parent.Die();

        //확인용
        Debug.Log(parent.name + "의 Die Enter");
        parent.state = Enemy.STATE.DIE;
    }
    public void Update()
    {
        //확인용
        Debug.Log(parent.name + "의 Die Update");
    }
    public void Exit()
    {
        //확인용
        Debug.Log(parent.name + "의 Die Exit");
    }
}