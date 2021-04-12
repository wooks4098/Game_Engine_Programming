using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : IEnemyState
{
    private Enemy parent;

    public void Enter(Enemy parent)
    {
        this.parent = parent;

        //확인용
        Debug.Log(parent.name + "의 Attack Enter");
        parent.state = Enemy.STATE.ATTACK;
    }
    public void Update()
    {
        parent.nav.SetDestination(parent.target.position);
        parent.transform.LookAt(parent.EnemyLookPoint); //플레이어를 바라보게

        if (Time.time >= parent.lastAttack + parent.delay) //공격 딜레이가 지났다면
        {
            //playerState.OnDamage(damage);
            parent.lastAttack = Time.time;
            parent.ani.SetTrigger("Attack");
        }

        if(!parent.isFollowArea() || parent.player.isDead)
            parent.ChangeState(new EnemyIdle());
        else if (!parent.isAttackArea())
            parent.ChangeState(new EnemyFollow());

        //확인용
        Debug.Log(parent.name + "의 Attack Update");
    }
    public void Exit()
    {
        //확인용
        Debug.Log(parent.name + "의 Attack Exit");
    }
}
