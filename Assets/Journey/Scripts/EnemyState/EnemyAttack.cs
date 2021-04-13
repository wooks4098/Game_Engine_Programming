using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : IEnemyState
{
    //이 상태는 늑대와 닭만 사용

    private Enemy parent;

    public void Enter(Enemy parent)
    {
        this.parent = parent;

        //확인용
        Debug.Log(parent.name + "의 Attack Enter");
    }
    public void Update()
    {
        parent.nav.SetDestination(parent.target.position);
        parent.transform.LookAt(parent.EnemyLookPoint);

        //공격 딜레이가 지났다면
        if (Time.time >= parent.lastAttack + parent.delay)
        {
            parent.player.OnDamage(parent.damage,parent.critical,parent.accuracy);
            Debug.Log("적이 플레이어를 공격");
            
            parent.lastAttack = Time.time;
            parent.ani.SetTrigger("Attack");
        }

        //공격 범위에 없고, 추적 범위에 있을 때
        if (!parent.isAttackArea() && parent.isFollowArea())
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
