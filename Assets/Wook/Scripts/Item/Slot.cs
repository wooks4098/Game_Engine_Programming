using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler,IBeginDragHandler,IDragHandler,IEndDragHandler,IDropHandler
{
    public Item item; //획득한 아이템
    public int itemCount; //획득한 아이템의 개수
    public Image itemImage; //아이템의 이미지

    private Vector3 orginPos;

    //필요한 컴포넌트
    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject text_Count_obj;

    private void Start()
    {
        orginPos = transform.position;
    }


    //이미지 투명도 조절
    private void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }

    //아이템 획득
    public void AddItem(Item _item, int _Count = 1)
    {
        item = _item;
        itemCount = _Count;
        itemImage.sprite = item.itemSprite;

        if (item.itemType != Item.ITEMTYPE.Tool)
        {
            text_Count_obj.SetActive(true);
            text_Count.text = itemCount.ToString();
        }
        else
        {
            text_Count.text = "0";
            text_Count_obj.SetActive(false);
        }

        SetColor(1);
    }

    //아이템 개수 조정
    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();
        if (itemCount <= 0)
            ClearSlot();
    }

    //슬롯 초기화
    private void ClearSlot()
    {
        item = null;
        itemCount = 0;
        itemImage.sprite = null;
        SetColor(0);

        text_Count.text = "0";
        text_Count_obj.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(item!= null)
            {
                //팝업 뜨게
            }
        }

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        if(item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(itemImage);

            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // throw new System.NotImplementedException();
        if (item != null)
        {
            DragSlot.instance.transform.position = eventData.position;
        }

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrop호출");

        // throw new System.NotImplementedException();
        DragSlot.instance.dragSlot = null;
        DragSlot.instance.SetColor(0);
        //transform.position = orginPos;
    }

    public void OnDrop(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        Debug.Log("OnDrop호출");
        ChangeSlot();
        //if (DragSlot.instance.dragSlot != null)
        //{
        //    ChangeSlot();
        //}
    }
    void ChangeSlot()
    {
        Item _tmpItem = item;
        int _tmpItemCount = itemCount;
        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);
        if(_tmpItem != null)
        {
            DragSlot.instance.dragSlot.AddItem(_tmpItem, _tmpItemCount);
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }
    }


}
