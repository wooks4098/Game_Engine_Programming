using UnityEngine;
using UnityEngine.AI;

public class Enemy : Creature
{
    //public enum TYPE { WOLF, CHICKEN, SHEEP }; //몬스터 타입
    //public TYPE enemyType;

    public enum STATE { IDLE, FOLLOW, ATTACK, RUNAWAY, DIE }; //몬스터 상태
    public STATE state;

    public PlayerState playerState;
    public Transform EnemyLookPoint; //적이 바라볼 플레이어의 지점
    public Transform target; //추적할 대상
    private NavMeshAgent nav; //네비 에이전트
    private Animator ani;

    public float delay = 3f; //공격 딜레이
    private float lastAttack; //마지막 공격 시점
    public bool isDamaged = false; //플레이어에게 공격을 받았는지

    //추적할 대상이 존재하는지 알려주는 프로퍼티
    private bool hasTarget
    {
        get
        {
            if (playerState != null && !playerState.isDead)
                return true;

            return false;
        }
    }

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
    }

    //public void SetStatus()
    
    private void Start()
    {
    }

    void Update()
    {
        nav.SetDestination(target.position);
        UpdateState();
    }
    
    public void UpdateState()
    {
        switch (state)
        {
            case STATE.IDLE:
                nav.isStopped = true;
                ani.SetBool("isFollow", false);

                if (isAttackArea())
                    state = STATE.ATTACK;
                else if (isFollowArea())
                    state = STATE.FOLLOW;
                break;

            case STATE.FOLLOW:
                nav.isStopped = false;
                ani.SetBool("isFollow", true);

                if (isAttackArea())
                    state = STATE.ATTACK;
                if (!isFollowArea())
                    state = STATE.IDLE;
                break;

            case STATE.ATTACK:
                transform.LookAt(EnemyLookPoint);
                ani.SetBool("isFollow", false);
                if (Time.time >= lastAttack + delay) //공격 딜레이가 지났다면
                {
                    playerState.OnDamage(damage);
                    ani.SetTrigger("Attack");
                    lastAttack = Time.time;
                }

                if (!isFollowArea())
                    state = STATE.IDLE;
                else if (!isAttackArea())
                    state = STATE.FOLLOW;
                break;

            case STATE.RUNAWAY: //고치기
                //Vector3 dir = (nav.transform.position - transform.position);
                //nav.SetDestination(dir);
                break;
            case STATE.DIE: //고치기
                if (!isDead)
                    Die();
                break;
        }
    }

    /*
    //몬스터 종류 체크
    void CheckType()
    {
        switch(enemyType)
        {
            case TYPE.WOLF:
                Wolf_CheckState();
                break;
            case TYPE.CHICKEN:
                Chicken_CheckState();
                break;
            case TYPE.SHEEP:
                Sheep_SheckState();
                break;
        }
    }

    void Chicken_CheckState()
    {
        if (isDamaged && nav.remainingDistance <= nav.stoppingDistance)
            enemyState = STATE.ATTACK;
        else if (isDamaged && nav.remainingDistance <= nav.stoppingDistance * 3) //공격 받았고 공격 범위에 있을 경우
            enemyState = STATE.FOLLOW;
        else
        {
            isDamaged = false;
            enemyState = STATE.IDLE;
        }

    }

    void Sheep_SheckState()
    {
        if (isDamaged && nav.remainingDistance <= nav.stoppingDistance * 3) //공격 받았고 공격 범위에 있을 경우
            enemyState = STATE.RUNAWAY;
        else
        {
            isDamaged = false;
            enemyState = STATE.IDLE;
        }

    }
    */

    
    //공격 범위에 들어왔는지
    bool isAttackArea()
    {
        if (nav.remainingDistance <= nav.stoppingDistance)
            return true;
        else
            return false;
    }

    //추적 범위에 들어왔는지
    bool isFollowArea()
    {
        if (nav.remainingDistance <= nav.stoppingDistance * 3) //인식 범위에 들어온 경우
            return true;
        else
            return false;
    }

    public override void OnDamage(float damage)
    {
        health -= damage;
        isDamaged = true; //고치기

        if (health <= 0)
            state = STATE.DIE;
    }

    public override void Die()
    {
        isDead = true;
        ani.SetTrigger("Die");
        Destroy(gameObject, 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, nav.stoppingDistance*3);//몬스터별 범위 고치기
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, nav.stoppingDistance);//몬스터별 범위 고치기
    }
}