using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPiekUp : MonoBehaviour
{
    [SerializeField]
    private float PickUp_Range;     //습등 가능 거리
    private bool PickUpActivate = false;        //습득 가능할 시 true;
    RaycastHit hitInfo;


    //필요한 컴포넌트
    [SerializeField]
    private Text Item_Info_Text;
    [SerializeField]
    private GameObject Item_Full_Text;
    public GameObject Item_Info;
    [SerializeField]
    private Inventory inventory;
    private void Update()
    {
        if (Inventory.inventoryActivated)
            InfoDisappear();//인벤토리 켜졌을 때 아이템 정보 보여주던거 숨기기
    }

    //아이템 획득 시도
    public void Get_Item(RaycastHit _hitInfo)
    {
        if (_hitInfo.transform != null)
        {
            if(inventory.AcquireItem(_hitInfo.transform.GetComponent<ItemInfo>().item))
            {
                _hitInfo.transform.gameObject.SetActive(false);
                InfoDisappear();
            }
            else
            {
                Debug.Log("인벤토리가 꽉찼습니다.");
                StartCoroutine("Item_Full_Text_Show");
            }

        }
    }
    IEnumerator Item_Full_Text_Show()
    {
        Item_Full_Text.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Item_Full_Text.SetActive(false);
        yield break;
    }


    //아이템 확인(마우스 커서를 올려 아이템 정보 확인용)
    public void ItemInfoAppear(RaycastHit _hitInfo)
    {
        hitInfo = _hitInfo;
        PickUpActivate = true;
        Item_Info.SetActive(true);
        Item_Info.gameObject.transform.position = new Vector3(Camera.main.WorldToScreenPoint(hitInfo.transform.position).x,
        Camera.main.WorldToScreenPoint(hitInfo.transform.position).y - 100f, Camera.main.WorldToScreenPoint(hitInfo.transform.position).z);
        Item_Info_Text.text = "줍기(E): " + hitInfo.transform.GetComponent<ItemInfo>().item.itemName;

    }

    public void InfoDisappear()
    {
        PickUpActivate = false;
        Item_Info.gameObject.SetActive(false);
    }
}
