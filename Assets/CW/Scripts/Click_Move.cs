using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Click_Move : MonoBehaviour
{
    public float Speed;//이동 속도


    //필요한 컴포넌트
    private Camera camera;
    private NavMeshAgent agent;
    private Animator animator;


    private void Awake()
    {
        camera = Camera.main;
        agent = gameObject.GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        agent.updateRotation = false;//NavMeshAgent회전 제한
        agent.speed = Speed;
    }

    private void Update()
    {
        ClickCheck();
        Look_SetPoint();
        EndMoveCheck();
    }

    //클릭했는지 확인하기
    void ClickCheck()
    {
        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                ClickMove(hit);

            }
        }
    }

    //클릭한 위치로 이동하기
    void ClickMove(RaycastHit hit)
    {
        if (Vector3.Distance(transform.position, hit.point) > 0.2f)
        {
            agent.SetDestination(hit.point);
        }
    }

    //이동 위치 바라보기
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
}
