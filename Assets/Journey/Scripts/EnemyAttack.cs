using UnityEngine;

public class EnemyAttack : IEnemyState
{
    //이 상태는 늑대와 닭만 사용

    private Enemy parentEnemy;
    private Transform EnemyLookPoint; //적이 바라볼 플레이어의 지점 (회전 오류 때문에)

    void Awake()
    {
        EnemyLookPoint = GameObject.FindGameObjectWithTag("EnemyLookPoint").GetComponent<Transform>();
    }

    public void Enter(Enemy parentEnemy)
    {
        this.parentEnemy = parentEnemy;
        parentEnemy.nav.ResetPath();


        //확인용
        Debug.Log(parentEnemy.name + "의 Attack Enter");
    }
    public void Update()
    {
        parentEnemy.nav.SetDestination(parentEnemy.playerPos.position);
        parentEnemy.transform.LookAt(EnemyLookPoint);

        //공격 딜레이가 지났다면
        if (Time.time >= parentEnemy.lastAttack + parentEnemy.delay)
        {
            parentEnemy.player.OnDamage(parentEnemy.damage,parentEnemy.critical,parentEnemy.accuracy);
            Debug.Log("적이 플레이어를 공격");
            
            parentEnemy.lastAttack = Time.time;
            parentEnemy.ani.SetTrigger("Attack");
        }

        //공격 범위에 없고, 추적 범위에 있을 때
        if (!parentEnemy.inAttackArea && parentEnemy.CheckFollow())
            parentEnemy.ChangeState(new EnemyFollow());
        else if(!parentEnemy.inAttackArea && !parentEnemy.CheckFollow())
            parentEnemy.ChangeState(new EnemyWalk());


        //확인용
        Debug.Log(parentEnemy.name + "의 Attack Update");
    }
    public void Exit()
    {
        //확인용
        Debug.Log(parentEnemy.name + "의 Attack Exit");
    }
}
