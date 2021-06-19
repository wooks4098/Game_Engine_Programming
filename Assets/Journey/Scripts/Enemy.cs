using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, ICreature
{
    public enum STATE { WANDER, FOLLOW, RUNAWAY, ATTACK }
    public enum MONSTER { WOLF, DEER, RABIT }
    
    [Header("Kind")]
    public STATE state;
    public MONSTER monster;

    [HideInInspector] public Player player;
    [HideInInspector] public Vector3 target;
    [HideInInspector] public NavMeshAgent nav;
    [HideInInspector] public Animator ani;
    
    [Header("Status")]
    public bool isDead; //생존 여부
    [HideInInspector] public float startingHealth = 100; //시작 체력
    public float health;//현재 체력
    [HideInInspector] public float damage; //데미지
    [HideInInspector] public float defense; //방어력
    [HideInInspector] public float evasion; //회피율
    [HideInInspector] public float accuracy; //명중률
    [HideInInspector] public float critical; //크리티컬 확률


    [Header("Attack")]
    public float attackBoundary = 2.5f; //공격 범위
    public float followBoundary = 5; //추적 범위
    public float distance; //플레이어와 거리
    [HideInInspector] public float delay = 3f; //공격 딜레이
    [HideInInspector] public float lastAttack; //마지막 공격 시점
    [HideInInspector] public bool isDamaged = false; //플레이어에게 공격을 받았는지
    

    [Header("State")]
    private IEnemyState currentState;

    [Header("Wander")]
    public float wanderRadius = 5f; //제한하는 원의 지름
    public float wanderDistance = 8f; //원이 투사되는 거리
    public float wanderJitter = 1f; //무작위 변위의 최대 크기
    public Vector3 wanderTarget;

    [Header("Raycast")]
    [HideInInspector] public bool hasObstacle; //장애물이 있다면 true, 없다면 false
    [HideInInspector] public Vector3 dir;
    [HideInInspector] public RaycastHit hit;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();

        target = wanderTarget;
        ChangeState(new EnemyWalk());
    }

    public virtual void OnEnable()
    {
        //스테이터스 초기화
        isDead = false;
        health = startingHealth;
        damage = 50;
        defense = 50;
        evasion = 50;
        accuracy = 50;
        critical = 50;
    }

    void Update()
    {
        //상태 전이
        currentState.Update();

        nav.SetDestination(target);

        //플레이어와 거리 계산
        distance = Vector3.Distance(player.transform.position, transform.position);

        //플레이어가 추적 범위에 있을때만 레이를 쏘도록
        if (distance < followBoundary)
        {
            dir = (player.transform.position - transform.position).normalized; //플레이어로 향하는 정규화 벡터

            if (Physics.Raycast(transform.position, dir, out hit, followBoundary))
            {
                //레이의 충돌체가 플레이어라면
                if (hit.transform.tag == "Player")
                    hasObstacle = false; //장애물이 없다고 판단
                else
                    hasObstacle = true; //장애물이 있다고 판단
            }
            //레이 시각화
            Debug.DrawRay(transform.position, dir * followBoundary, Color.red);
        }

    }

    //상태 전이
    public void ChangeState(IEnemyState nextState)
    {
        if(currentState != null)
            currentState.Exit();

        currentState = nextState;
        currentState.Enter(this);
    }

    //추적할 대상이 존재하는지
    private bool hasTarget
    {
        get
        {
            if (player != null)//&& !player.isDead)
               return true;

            return false;
        }
    }
    
    //받을 데미지 계산 (플레이어가 적을 공격할 때)
    public float Fight(float _damage, float _critical, float _accuracy)
    {
        int rand = Random.Range(0, 101);
        
        if (rand < _accuracy - evasion)
        {
            rand = Random.Range(0, 101);
            if (rand < _critical)
            {
                _damage = _damage * 1.2f - defense;
            }
            else
            {
                _damage = _damage - defense;
            }
        }
        else
        {
            _damage = 0;
        }

        return _damage;
    }

    //공격 받을 때
    public void OnDamage(float _damage, float _critical, float _accuracy)
    {
        health -= Fight(_damage, _critical, _accuracy);
        isDamaged = true;

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

    void OnDrawGizmos()
    {
        //공격 범위
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackBoundary);

        //추적 범위
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, followBoundary);

        //배회 지점
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position + transform.TransformDirection(Vector3.forward * wanderDistance), wanderRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + transform.TransformDirection(Vector3.forward * wanderDistance) + wanderTarget, wanderRadius / 5);
    }
}