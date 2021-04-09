using UnityEngine;
using UnityEngine.AI;

public class Enemy : Creature
{
    //public enum STATE { IDLE, FOLLOW, ATTACK, RUNAWAY, DIE }; //몬스터 상태
    //public STATE enemyState;

    //public PlayerStatus player;
    public Transform EnemyLookPoint; //적이 바라볼 플레이어의 지점
    public Transform target; //추적할 대상
    public Transform RunawayPoint;
    private NavMeshAgent nav; //네비 에이전트
    private Animator ani;

    public float delay = 3f; //공격 딜레이
    private float lastAttack; //마지막 공격 시점
    public bool isDamaged = false; //플레이어에게 공격을 받았는지

    //추적할 대상이 존재하는지 알려주는 프로퍼티
    /*
    private bool hasTarget
    {
        get
        {
            if (playerState != null && !playerState.isDead)
                return true;

            return false;
        }
    }*/

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();
    }

    //public void SetStatus()

    void Update()
    {
        RunawayPoint.position = (transform.position - target.position) * 2;
        UpdateState();
    }

    //Idle State
    public void IdleEnter()
    {
        nav.isStopped = true;
        ani.SetBool("isFollow", false);
    }
    public void IdleUpdate()
    {
        nav.SetDestination(target.position);
    }
    public void IdleExit()
    {

    }

    //Follow State
    public void FollowEnter()
    {
        nav.isStopped = false;
        ani.SetBool("isFollow", true);
    }
    public void FollowUpdate()
    {
        nav.SetDestination(target.position);
    }
    public void FollowExit()
    {

    }

    //Attack State
    public void AttackEnter()
    {
        ani.SetBool("isFollow", false);
    }
    public void AttackUpdate()
    {
        nav.SetDestination(target.position);
        transform.LookAt(EnemyLookPoint);
        ani.SetTrigger("Attack");
    }
    public void AttackExit()
    {

    }

    //Runaway State
    public void RunawayEnter()
    {
        ani.SetBool("isFollow", true);
    }
    public void RunawayUpdate()
    {
        nav.ResetPath();
        nav.SetDestination(RunawayPoint.position);
    }
    public void RunawayExit()
    {

    }

    //Die State
    public void DieEnter()
    {

    }
    public void DieUpdate()
    {

    }
    public void DieExit()
    {

    }


    //////////////////////////////////////////////
    //상태 업데이트
    public void UpdateState()
    {
        switch (enemyState)
        {
            case STATE.IDLE:
                break;

            case STATE.FOLLOW:
                break;

            case STATE.ATTACK:

                if (playerState.isDead)
                    enemyState = STATE.IDLE;

                if (Time.time >= lastAttack + delay) //공격 딜레이가 지났다면
                {
                    playerState.OnDamage(damage);
                    lastAttack = Time.time;
                }
                StateChange();

                break;

            case STATE.RUNAWAY: //고치기
                break;
            case STATE.DIE: //고치기
                if (!isDead)
                    Die();
                break;
        }
    }

    //상태 바꾸기
    void StateChange()
    {
        switch(enemyType)
        {
            case TYPE.WOLF:
                switch (enemyState)
                {
                    case STATE.IDLE:
                        if (isAttackArea())
                            enemyState = STATE.ATTACK;
                        else if (isFollowArea())
                            enemyState = STATE.FOLLOW;
                        break;
                    case STATE.FOLLOW:
                        if (isAttackArea())
                            enemyState = STATE.ATTACK;
                        if (!isFollowArea())
                            enemyState = STATE.IDLE;
                        break;
                        break;
                    case STATE.ATTACK:
                        if (!isFollowArea())
                            enemyState = STATE.IDLE;
                        else if (!isAttackArea())
                            enemyState = STATE.FOLLOW;
                        break;
                    case STATE.DIE:
                        break;
                }
                break;
            case TYPE.CHICKEN:
                switch(enemyState)
                {
                    case STATE.IDLE:
                        if (isDamaged && isAttackArea()) //공격 받았고 공격 범위에 있을 경우
                            enemyState = STATE.ATTACK;
                        else if (isDamaged && isFollowArea()) //공격 받았고 추적 범위에 있을 경우
                            enemyState = STATE.FOLLOW;
                        isDamaged = false;//고치기
                        break;
                    case STATE.FOLLOW:
                        if (isAttackArea())
                            enemyState = STATE.ATTACK;
                        else if(!isFollowArea())
                            enemyState = STATE.IDLE;
                        break;
                    case STATE.ATTACK:
                        if (!isAttackArea() && !isFollowArea())
                            enemyState = STATE.IDLE;
                        else if (!isAttackArea() && isFollowArea()) 
                            enemyState = STATE.FOLLOW;
                        break;
                    case STATE.DIE:
                        break;
                }
                break;
            case TYPE.SHEEP:
                switch (enemyState)
                {
                    case STATE.IDLE:
                        if (isDamaged && isFollowArea()) //공격 받았고 공격 범위에 있을 경우
                            enemyState = STATE.RUNAWAY;
                        isDamaged = false;
                        break;
                    case STATE.RUNAWAY:
                        if (!isFollowArea()) 
                            enemyState = STATE.IDLE;
                        break;
                    case STATE.DIE:
                        break;
                }
                break;
        }
    }


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
            enemyState = STATE.DIE;
    }

    public override void Die()
    {
        isDead = true;
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