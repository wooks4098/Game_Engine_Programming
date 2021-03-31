using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPiekUp : MonoBehaviour
{
    [SerializeField]
    private float PickUp_Range;     //습등 가능 거리
    private bool PickUpActivate = false;        //습득 가능할 시 true;

    private RaycastHit hitInfo;     //충동체 정보저장

    //아이템 레이어를 담을 레이어 변수
    [SerializeField]
    private LayerMask LayerMask;

    //필요한 컴포넌트
    [SerializeField]
    private Text Item_Info_Text;
    public GameObject Item_Info;

    private void Update()
    {
        Check_Item();
    }

    //아이템 획득 시도
    void Try_PickUp_Item()
    {

    }

    //아이템 확인(마우스 커서를 올려 아이템 정보 확인용)
    void Check_Item()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 20, Color.red);
        if (Physics.Raycast(ray.origin, ray.direction * 20, out hitInfo, LayerMask))
        {
            if (hitInfo.transform != null && hitInfo.transform.tag == "Item")
            {
                ItemInfoAppear();
            }
            else
                InfoDisappear();
        }
    }

    void ItemInfoAppear()
    {
        PickUpActivate = true;
        Item_Info.SetActive(true);
        Item_Info.gameObject.transform.position = new Vector3(Camera.main.WorldToScreenPoint(hitInfo.transform.position).x,
           Camera.main.WorldToScreenPoint(hitInfo.transform.position).y - 100f, Camera.main.WorldToScreenPoint(hitInfo.transform.position).z);
        Item_Info_Text.text = "줍기(E): " + hitInfo.transform.GetComponent<ItemInfo>().item.itemName;

    }

    void InfoDisappear()
    {
        PickUpActivate = false;
        Item_Info.gameObject.SetActive(false);
    }
}
