using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum Player_STATE { Idle, Move, GetItem, Attack, Gathering };//대기, 이동, 아이템획득, 공격, 채집
public class Player : Player_Base
{

    private int State = 0;

    private bool CanAttack = true;
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
            case (int)Player_STATE.Idle:
                break;
            case (int)Player_STATE.Move:
                break;
            case (int)Player_STATE.GetItem:
                GetItem();
                break;
            case (int)Player_STATE.Attack:
                Attack();
                break;
            case (int)Player_STATE.Gathering:
                break;

        }
        Look_SetPoint();
        EndMoveCheck();
    }

 

    #region 이동
    //이동
    public void Move(RaycastHit _hit)
    {
        if (Vector3.Distance(transform.position, _hit.point) > 0.2f)
        {
            agent.SetDestination(_hit.point);
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
    #endregion

    #region 회전
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

    //타겟을 바라보게하는 코루틴 함수
    IEnumerator LookAt_Target()
    {
        float time = 0;
        while (time <= 0.7)
        {
            time += Time.deltaTime;
            //에이전트의 이동방향
            Vector3 direction = hit.transform.position - transform.position;
            //회전각도(쿼터니언)산출
            Quaternion targetangle = Quaternion.LookRotation(direction);
            //선형보간 함수를 이용해 부드러운 회전
            animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, targetangle, Time.deltaTime * 5.0f);
            yield return null;

        }
        yield break;
    }
    #endregion

    #region 아이템 획득
    public void GetItem_start(RaycastHit _hit)
    {
        hit = _hit;
        State = (int)Player_STATE.GetItem;

    }

    void GetItem()
    {
        if (Vector3.Distance(transform.position, hit.transform.position) <= Get_Item_Range)
        {
            //아이템 획득
            itempickup.Get_Item(hit);
            ResetMove();
            State = (int)Player_STATE.Idle;
        }
        else
        {
            //이동
            agent.SetDestination(hit.point);
        }
    }
    #endregion

    void Attack()
    {
        if (!CanAttack)
            return;
        if(CanAttack_Range())
        {
            //기본공격
            if (hit.transform.gameObject.activeSelf == false)
            {
                return;
            }
            ResetMove();
            //StartCoroutine("LookAt_Target");
            Debug.Log("플레이어 공격");
            StartCoroutine("Change_CanAttack");
        }
        else
        {
            //추격
            agent.SetDestination(hit.transform.position);
        }

    }
    IEnumerator Change_CanAttack()
    {
        yield return new WaitForSeconds(Index_Delay);
        CanAttack = true;
        yield break;
    }


    //공격가능한지 체크하는 함수 (거리 측정함)
    bool CanAttack_Range()
    {
        if (!CanAttack)
           return  false;
        float dir = Vector3.Distance(hit.transform.position, transform.position);
        if (dir <= Attack_Boundary)
        {
            CanAttack = false;
            return true;
        }
        else
            return false;
    }

    public void AttackStart()
    {
        CanAttack = true;
    }
    #region 피격
    public void Hit(float _Danage, float _Critical)
    {
        Debug.Log("플레이어 피격");
    }

    #endregion


    public void GetMouseHit(RaycastHit _hit)
    {

        hit = _hit;
    }

    public void ChangeState(int _state)
    {
        State = _state;
    }

}
