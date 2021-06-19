using UnityEngine;

//이 상태는 늑대와 사슴만 사용
public class EnemyAttack : IEnemyState
{

    private Enemy enemy;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        enemy.state = Enemy.STATE.ATTACK;

        enemy.target = enemy.player.transform.position;
    }

    public void Update()
    {
        enemy.transform.LookAt(enemy.player.transform);

        //공격 딜레이가 지났다면
        if (Time.time >= enemy.lastAttack + enemy.delay)
        {
            enemy.player.OnDamage(enemy.damage,enemy.critical,enemy.accuracy);
            
            enemy.lastAttack = Time.time;
            enemy.ani.SetTrigger("Attack");
        }

        //공격 범위에 없고, 추적 범위에는 있을 때
        if (enemy.distance > enemy.attackBoundary && enemy.distance < enemy.followBoundary)
            enemy.ChangeState(new EnemyFollow());
    }
    public void Exit()
    {

    }
}
