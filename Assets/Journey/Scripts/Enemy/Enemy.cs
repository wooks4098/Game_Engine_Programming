using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, ICreature
{
    //확인용
    public enum STATE { IDLE, FOLLOW, ATTACK, RUNAWAY, DIE }
    public STATE state = STATE.IDLE;
    //확인용

    public PlayerStatus player;
    public Transform EnemyLookPoint; //적이 바라볼 플레이어의 지점
    public Transform target; //추적할 대상
    public Transform RunawayPoint; //도망칠 때의 목표지점
    public NavMeshAgent nav; //네비 에이전트
    public Animator ani;
    

    //스테이터스
    public bool isDead; //생존 여부
    public float startingHealth = 100; //시작 체력
    public float health;//현재 체력
    public float damage; //데미지
    public float defense; //방어력
    public float evasion; //회피율
    public float accuracy; //명중률
    public float critical; //크리티컬 확률

    public float delay = 3f; //공격 딜레이
    public float lastAttack; //마지막 공격 시점
    public bool isDamaged = false; //플레이어에게 공격을 받았는지

    //상태
    private IEnemyState currentState;

    
    void Awake()
    {
        ChangeState(new EnemyIdle());
        nav.SetDestination(target.position);
    }
    
    void Start()
    {

    }

    //스테이터스 초기화
    public virtual void OnEnable()
    {
        isDead = false;
        health = startingHealth;
        damage = 50;
        defense = 50;
        evasion = 50;
        evasion = 50;
        accuracy = 50;
        critical = 50;
    }

    void Update()
    {
        RunawayPoint.position = transform.position + ((transform.position - target.position) * 2);
        currentState.Update();
    }

    //상태 전이
    public void ChangeState(IEnemyState nextState)
    {
        if(currentState != null)
            currentState.Exit();

        currentState = nextState;
        currentState.Enter(this);
    }

    //받을 데미지 계산 (플레이어가 적을 공격할 때)
    public float Fight(float _evasion, float _defense)
    {
        int rand = Random.Range(0, 101);

        float _damage = 0f;

        if (0 < accuracy - _evasion)
        {
            rand = Random.Range(0, 101);
            if (0 < critical)
            {
                _damage = damage * 1.2f - _defense;
                Debug.Log("플레이어가 적을 공격 - 1");
            }
            else
            {
                _damage = damage - _defense;
                Debug.Log("플레이어가 적을 공격 - 2");
            }
        }
        else
        {
            _damage = 0;
            Debug.Log("플레이어가 적을 공격 - 3");
        }

        return _damage;
    }

    //추적할 대상이 존재하는지 알려주는 프로퍼티
    private bool hasTarget
    {
        get
        {
            if (player != null && !player.isDead)
                return true;

            return false;
        }
    }

    //공격 범위에 들어왔는지
    public bool isAttackArea()
    {
        if (nav.remainingDistance <= nav.stoppingDistance)
            return true;
        else
            return false;
    }

    //추적 범위에 들어왔는지
    public bool isFollowArea()
    {
        if (nav.remainingDistance <= nav.stoppingDistance * 3) //인식 범위에 들어온 경우
            return true;
        else
            return false;
    }

    //공격 받을 때
    public void OnDamage(float damage)
    {
        isDamaged = true;
        health -= damage;

        if (health <= 0)
            Die();
    }

    //사망 처리
    public void Die()
    {
        ani.SetTrigger("Die");
        nav.isStopped = true;
        isDead = true;
        Destroy(gameObject, 5f);
    }

    //적의 공격 범위, 추적 범위 보이기
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, nav.stoppingDistance*3);//몬스터별 범위 고치기
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, nav.stoppingDistance);//몬스터별 범위 고치기
    }
    
}