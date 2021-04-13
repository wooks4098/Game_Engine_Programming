using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MouseCheck : Player_Base
{

    private RaycastHit hitInfo;     //충동체 정보저장
    private RaycastHit Item_Info;     //아이템 충돌체 정보저장

    //아이템 레이어를 담을 레이어 변수
    [SerializeField]
    private LayerMask Item_LayerMask;

    private Camera camera;

    [SerializeField]
    private ItemPiekUp itemPickup;
    [SerializeField]
    private Player player;

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (Inventory.inventoryActivated)
            return; 
        ClickCheck();
        Check_Item();
    }

    void ClickCheck()
    {
        if (Input.GetMouseButton(1))
        {
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hitInfo))
            {
                Debug.Log(hitInfo.transform.tag + "클릭");
                if (hitInfo.transform.tag == "Item")
                {
                    Item_Info = hitInfo;
                    player.GetItem_start(Item_Info);
                    player.ChangeState((int)Player_STATE.GetItem);
                }
                else if (hitInfo.transform.tag == "채집")
                {
                    player.ChangeState((int)Player_STATE.Gathering);
                }
                else if (hitInfo.transform.tag == "Enemy")
                {
                    player.ChangeState((int)Player_STATE.Attack);
                    player.GetMouseHit(hitInfo);
                    player.AttackStart();
                }
                else
                {
                    //이동
                    player.Move(hitInfo);
                    player.ChangeState((int)Player_STATE.Move);
                }
            }
        }
    }

    void Check_Item()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 20, Color.red);
        if (Physics.Raycast(ray.origin, ray.direction * 20, out hitInfo, Item_LayerMask))
        {
            if (hitInfo.transform != null && hitInfo.transform.tag == "Item")
            {
                itemPickup.ItemInfoAppear(hitInfo);
                //E 획득
                //if (Input.GetKeyDown(KeyCode.E))
                //    player.GetItem_start(hitInfo);
            }
            else
                itemPickup.InfoDisappear();
        }
    }
}
