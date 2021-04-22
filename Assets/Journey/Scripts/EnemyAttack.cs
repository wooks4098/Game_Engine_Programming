using UnityEngine;

public class EnemyAttack : IEnemyState
{
    //이 상태는 늑대와 닭만 사용

    private Enemy enemy;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;

        //확인용
        Debug.Log(enemy.name + "의 Attack Enter");
    }
    public void Update()
    {
        enemy.nav.SetDestination(enemy.playerPos.position);
        enemy.transform.LookAt(enemy.EnemyLookPoint);

        //공격 딜레이가 지났다면
        if (Time.time >= enemy.lastAttack + enemy.delay)
        {
            enemy.player.OnDamage(enemy.damage,enemy.critical,enemy.accuracy);
            Debug.Log("적이 플레이어를 공격");
            
            enemy.lastAttack = Time.time;
            enemy.ani.SetTrigger("Attack");
        }

        //공격 범위에 없고, 추적 범위에 있을 때
        if (!enemy.inAttackArea && enemy.CheckFollow())
            enemy.ChangeState(new EnemyFollow());

        //확인용
        Debug.Log(enemy.name + "의 Attack Update");
    }
    public void Exit()
    {
        //확인용
        Debug.Log(enemy.name + "의 Attack Exit");
    }
}
