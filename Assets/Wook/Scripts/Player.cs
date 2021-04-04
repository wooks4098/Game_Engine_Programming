using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : Player_Base
{
    enum STATE { Idle, Move, GetItem, Attack, Gathering };//대기, 이동, 아이템획득, 공격, 채집

    private int State = 0;
    private RaycastHit hit;

    //필요한 컴포넌트
    private NavMeshAgent agent;
    private Animator animator;
    [SerializeField]
    private ItemPiekUp itempickup;
    private void Awake()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        agent.updateRotation = false;//NavMeshAgent회전 제한
        agent.speed = Speed;
    }

    private void Update()
    {
        switch(State)
        {
            case (int)STATE.Idle:
                break;
            case (int)STATE.Move:
                break;
            case (int)STATE.GetItem:
                GetItem();
                break;
            case (int)STATE.Attack:
                break;
            case (int)STATE.Gathering:
                break;

        }
        Look_SetPoint();
        EndMoveCheck();
    }

    public void GetItem_start(RaycastHit _hit)
    {
        hit = _hit;
        State = (int)STATE.GetItem;
        
    }

    void GetItem()
    {
        if (Vector3.Distance(transform.position, hit.transform.position) <= Get_Item_Range)
        {
            //아이템 획득
            Debug.Log("아이템 획득");
            itempickup.Get_Item();
            ResetMove();
            State = (int)STATE.Idle;
        }
        else
        {
            //이동
            agent.SetDestination(hit.point);
        }
    }


    //이동
    public void Move(RaycastHit _hit)
    {
        if (Vector3.Distance(transform.position, _hit.point) > 0.2f)
        {
            agent.SetDestination(_hit.point);
        }
    }


    //이동방향 바라보기
    void Look_SetPoint()
    {
        if (agent.hasPath)
        {
            //회전 미끄러짐 방지
            agent.acceleration = (agent.remainingDistance < 2f) ? 8f : 60f;
            //에이전트의 이동방향
            Vector3 direction = agent.desiredVelocity;
            //회전각도(쿼터니언)산출
            Quaternion targetangle = Quaternion.LookRotation(direction);
            //선형보간 함수를 이용해 부드러운 회전
            animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, targetangle, Time.deltaTime * 8.0f);
        }
    }
    //이동이 끝났는지 체크
    void EndMoveCheck()
    {
        if (agent.velocity.sqrMagnitude >= 0.1f * 0.1f && agent.remainingDistance <= 0.1f)//이동종료
        {
            ResetMove();
        }
    }
    //이동 초기화
    void ResetMove()
    {
        agent.ResetPath();
        agent.velocity = Vector3.zero;

    }
    void ChangeState(int _state)
    {
        State = _state;
    }

}
