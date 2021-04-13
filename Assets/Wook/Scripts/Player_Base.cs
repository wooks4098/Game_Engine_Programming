using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Base : MonoBehaviour
{
    protected static float Speed = 5f;              //이동 속도

     protected static float Health;                 //플레이어 체력
     protected static float Health_Max;                 //플레이어 체력
	 protected static float Hungry;                 //플레이어 배고픔
	 protected static float Thirsty;                //플레이어 목마름

	 protected static float Attack_Damage;          //플레이어 공격력
    protected static float Attack_Boundary = 2f;         //플레이어 공격범위

    protected static float Defense;                 //플레이어 방어력
    protected static float evasion;                 //플레이어 회피율 (int)
    protected static float Accuracy;                //플레이어 명중률 (int)
    protected static float Critical;                //플레이어 크리티컬 확률
    protected static float Index_Delay = 2f;             // 딜레이 카운팅





    protected static float Get_Item_Range = 2f;     //아이템 획득 가능 거리
    protected static float Gatheringm_Range = 2f;   //채집 가능 거리

    private void OnDrawGizmosSelected()
    {
        //아이템 획득 범위
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Get_Item_Range);

        //공격 가능 범위
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Attack_Boundary);
    }
}
