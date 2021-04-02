﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MouseCheck : Player_Base
{

    private RaycastHit hitInfo;     //충동체 정보저장

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
        ClickCheck();
        Check_Item();
    }

    void ClickCheck()
    {
        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.transform.tag == "Item")
                {
                    player.GetItem_start(hit);
                }
                else if (hit.transform.tag == "채집")
                {

                }
                else if (hit.transform.tag == "몬스터")
                {

                }
                else
                {
                    //이동
                    player.Move(hit);
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
            }
            else
                itemPickup.InfoDisappear();
        }
    }
}