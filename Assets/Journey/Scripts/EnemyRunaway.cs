using UnityEngine;

public class EnemyRunaway : IEnemyState
{
    //이 상태는 양만 사용

    private Enemy enemy;

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
        enemy.nav.ResetPath();

        enemy.ani.SetBool("isRun", true);

        //확인용
        Debug.Log(enemy.name + "의 Runaway Enter");
    }
    public void Update()
    {        //적이 도망갈 때의 목표지점
             //RunawayPoint.position = transform.position + ((transform.position - playerPos.position) * 2);



        //새 목표지점 = 플레이어 반대 방향
        //parent.nav.SetDestination(parent.RunawayPoint.position);

        //추적 범위에 없을 때
        if (!enemy.CheckFollow())
        {
            enemy.isDamaged = false;
            enemy.ChangeState(new EnemyWalk());
        }

        //확인용
        Debug.Log(enemy.name + "의 Runaway Update");
    }
    public void Exit()
    {
        enemy.nav.ResetPath();
        enemy.ani.SetBool("isRun", false);

        //확인용
        Debug.Log(enemy.name + "의 Runaway Exit");
    }
}
