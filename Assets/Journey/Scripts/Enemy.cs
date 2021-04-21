using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, ICreature
{
    [HideInInspector] public Player player;
    [HideInInspector] public Transform EnemyLookPoint; //적이 바라볼 플레이어의 지점 (회전 오류 때문에)
    [HideInInspector] public Transform playerPos; //실제로 추적할 대상
    [HideInInspector] public Transform DestinationPos; //목표지점
    [HideInInspector] public NavMeshAgent nav;
    [HideInInspector] public Animator ani;

    private EnemySpawner enemySpawner;
    
    // Status //
    public bool isDead; //생존 여부
    public float startingHealth = 100; //시작 체력
    public float health;//현재 체력
    public float damage; //데미지
    public float defense; //방어력
    public float evasion; //회피율
    public float accuracy; //명중률
    public float critical; //크리티컬 확률

    // Attack //
    public float delay = 3f; //공격 딜레이
    public float lastAttack; //마지막 공격 시점
    public bool isDamaged = false; //플레이어에게 공격을 받았는지
    private float attackBoundary; //공격 범위

    // State //
    private IEnemyState currentState;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        EnemyLookPoint = GameObject.FindGameObjectWithTag("EnemyLookPoint").GetComponent<Transform>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        DestinationPos = GameObject.FindGameObjectWithTag("DestinationPos").GetComponent<Transform>();
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponent<Animator>();

        nav.SetDestination(DestinationPos.position);
        attackBoundary = nav.stoppingDistance;

        ChangeState(new EnemyWalk());
        Debug.Log("성공");
    }

    // 스테이터스 초기화 //
    public virtual void OnEnable()
    {
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
    
    //공격 범위에 들어왔는지
    public bool isAttackArea()
    {
        if (attackBoundary <= Vector3.Distance(transform.position, playerPos.position))
            return true;
        else
            return false;
    }

    //추적 범위에 들어왔는지
    public bool isFollowArea()
    {
        if (attackBoundary * 3 <= Vector3.Distance(transform.position, playerPos.position))
            return true;
        else
            return false;
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
                Debug.Log("적이 플레이어를 공격 - 1");
            }
            else
            {
                _damage = _damage - defense;
                Debug.Log("적이 플레이어를 공격 - 2");
            }
        }
        else
        {
            _damage = 0;
            Debug.Log("적이 플레이어를 공격 - 3");
        }

        return _damage;
    }

    //공격 받을 때
    public void OnDamage(float _damage, float _critical, float _accuracy)
    {
        health -= Fight(_damage, _critical, _accuracy);
        isDamaged = true;

        Debug.Log("enemy 피격");

        if (health <= 0)
            Die();
    }
    
    //사망 처리 //state로 수정
    public void Die()
    {
        ani.SetTrigger("Die");
        nav.isStopped = true;
        isDead = true;
        Destroy(gameObject, 5f);
    }
}