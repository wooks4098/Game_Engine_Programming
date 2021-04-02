using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Base : MonoBehaviour
{
    protected static float Speed = 5f;//이동 속도
    protected static float Get_Item_Range = 2f;//아이템 획득 가능 거리
    protected static float Attack_Range = 2f;//공격 가능 거리
    protected static float Gatheringm_Range = 2f;//채집 가능 거리

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Get_Item_Range);
    }
}
