using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler ,IPointerClickHandler,IBeginDragHandler,IDragHandler,IEndDragHandler,IDropHandler
{
    public Item item; //획득한 아이템
    public int itemCount; //획득한 아이템의 개수
    public Image itemImage; //아이템의 이미지

    private bool isDragging = false;//이미지 드래그 중인지

    private Vector3 orginPos;

    //필요한 컴포넌트
    [SerializeField]
    private Text text_Count;
    [SerializeField]
    private GameObject text_Count_obj;
    private Inventory_RightClick inventory_RightClick;
    private Inventory_LeftClick inventory_LefttClick;
    [SerializeField]
    private Item_Using item_using;
    private void Start()
    {
        orginPos = transform.position;
        item_using = FindObjectOfType<Item_Using>();
        inventory_RightClick = FindObjectOfType<Inventory_RightClick>();
        inventory_LefttClick = FindObjectOfType<Inventory_LeftClick>();
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

    public void inventory_RightClick_Off()
    {
        inventory_RightClick.Hide();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            if(item!= null)
            {
                inventory_LefttClick.Hide();
                item_using.Click_Item(this);
                inventory_RightClick.Show(item, this.transform.position);
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (item != null)
            {
                if (Inventory_RightClick.Inventory_RightClick_Activated == false)
                {
                    inventory_RightClick.Hide();
                    isDragging = true;
                    DragSlot.instance.dragSlot = this;
                    DragSlot.instance.DragSetImage(itemImage);

                    DragSlot.instance.transform.position = eventData.position;
                }      
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (item != null)
            {
                DragSlot.instance.transform.position = eventData.position;
            }
        }
        

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            DragSlot.instance.dragSlot = null;
            DragSlot.instance.SetColor(0);
        }     
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (DragSlot.instance.dragSlot != null)
            {
                ChangeSlot();
            }
        }          
    }
    void ChangeSlot()
    {
        Item _tmpItem = item;
        int _tmpItemCount = itemCount;
        AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);
        if(_tmpItem != null)
            DragSlot.instance.dragSlot.AddItem(_tmpItem, _tmpItemCount);
        else
            DragSlot.instance.dragSlot.ClearSlot();

    }

    //마우스가 슬롯에 들어가면 발동
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null)
        {
           if(Inventory_RightClick.Inventory_RightClick_Activated == false)
            {
                inventory_LefttClick.Show(item, itemCount, transform.position);
            }

        }
    }

    //마우스가 슬록에서 나가면 발동
    public void OnPointerExit(PointerEventData eventData)
    {
        inventory_LefttClick.Hide();
    }
}
