using UnityEngine;

public class EnemyFollow : IEnemyState
{
    //이 상태는 늑대와 닭만 사용
    private Enemy parentEnemy;

    public void Enter(Enemy parentEnemy)
    {
        this.parentEnemy = parentEnemy;

        parentEnemy.nav.ResetPath();
        parentEnemy.ani.SetBool("isWalk", true);

        //확인용
        Debug.Log(parentEnemy.name + "의 Follow Enter");
    }
    public void Update()
    {
        parentEnemy.nav.SetDestination(parentEnemy.playerPos.position);

        //공격 범위에 있을 때
        if (parentEnemy.inAttackArea)
            parentEnemy.ChangeState(new EnemyAttack());

        //공격 범위에 없고, 추적 범위에 없을 때
        else if (!parentEnemy.CheckFollow())
        {
            parentEnemy.ChangeState(new EnemyWalk());
            parentEnemy.isDamaged = false;
        }

        //확인용
        Debug.Log(parentEnemy.name + "의 Follow Update");
    }
    public void Exit()
    {
        parentEnemy.ani.SetBool("isWalk", false);

        //확인용
        Debug.Log(parentEnemy.name + "의 Follow Exit");
    }
}
