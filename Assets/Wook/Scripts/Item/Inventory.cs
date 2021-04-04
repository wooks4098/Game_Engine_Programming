using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static bool inventoryActivated = false;

    //필요한 컴포넌트
    [SerializeField]
    private GameObject Inventory_base;
    [SerializeField]
    private GameObject Tool_SlotsParent;
    [SerializeField]
    private GameObject Etc_SlotsParent;

    //슬롯들
    private Slot[] Tool_slots;
    private Slot[] Etc_slots;

    private void Awake()
    {
        
    }

    void Start()
    {
        Tool_slots = Tool_SlotsParent.GetComponentsInChildren<Slot>();
        Etc_slots = Etc_SlotsParent.GetComponentsInChildren<Slot>();
    }

    private void Update()
    {
        TryOpenInventory();
    }

    void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryActivated = !inventoryActivated;

            if (inventoryActivated)
                OpenInventory();
            else
                CloseInventory();


        }
    }

    //아이템 추가
    public void AcquireItem(Item _item, int _count = 1)
    {
        if (Item.ITEMTYPE.Tool == _item.itemType)//장비인지 체크
            Tool_Acquire(_item, _count);
        else
            Etc_Acquire(_item, _count);

    
    }

    void Tool_Acquire(Item _item, int _count)
    {
        for (int i = 0; i < Tool_slots.Length; i++)
        {
            if (Tool_slots[i].item == null)
            {
                Tool_slots[i].AddItem(_item, _count);
                return;
            }
        }
    }
    void Etc_Acquire(Item _item, int _count)
    {
        //같은 아이템이 있다면 증가
        for (int i = 0; i < Etc_slots.Length; i++)
        {
            if (Etc_slots[i].item != null)
            {
                if (Etc_slots[i].item.itemName == _item.itemName)
                {
                    Etc_slots[i].SetSlotCount(_count);
                    return;
                }
            }

        }

        //해당 아이템이 없다면 새로 등록
        for (int i = 0; i < Etc_slots.Length; i++)
        {
            if (Etc_slots[i].item == null)
            {
                Etc_slots[i].AddItem(_item, _count);
                return;
            }
        }
    }

    void OpenInventory()
    {
        Inventory_base.SetActive(true);
    }

    void CloseInventory()
    {
        Inventory_base.SetActive(false);

    }

}
