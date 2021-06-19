using UnityEngine;

//이 상태는 토끼만 사용
public class EnemyRunaway : IEnemyState
{
    private Enemy enemy;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        enemy.state = Enemy.STATE.RUNAWAY;

        enemy.ani.SetBool("isRun", true);
    }
    public void Update()
    {
        //새 목표지점 = 플레이어 반대 방향
        enemy.target = enemy.transform.position + (enemy.transform.position - enemy.player.transform.position) * 2;

        //추적 범위에 없을 때
        if (enemy.distance > enemy.followBoundary)
        {
            enemy.isDamaged = false;
            enemy.ChangeState(new EnemyWalk());
        }
    }
    public void Exit()
    {
        enemy.nav.ResetPath();
        enemy.ani.SetBool("isRun", false);
    }
}
