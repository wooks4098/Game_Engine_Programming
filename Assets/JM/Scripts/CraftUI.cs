using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class CraftUI : MonoBehaviour
{
    public static bool CraftUIActivated = false;
    private int player_Craft_State;

    [SerializeField]
    private GameObject CraftUI_Base;

    public Button Crafting_Button;
    
    public GameObject Mix_Item;

    [SerializeField]
    private Crafting_Set crafting_set;
    [SerializeField]
    private Slot[] Slots; //인벤토리 슬롯  0,1,2 재료 슬롯 3빈 슬롯
    
    public GameObject Content;
    [SerializeField]
    private Inventory Inventory;
    public GameObject []Value;
    public Value_Item[] Value_info;
    [SerializeField]
    private Crafting_Item_info crafting_item_info;

    private int Craft_Item_Number; //크래프팅 아이템 번호
    //조합 채크
    bool[] check_craft = { true, true, true };

    void Awake()
    {
        //crafting_set = GetComponent<Crafting_Set>();
       


    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CraftUIActivated = !CraftUIActivated;

            if (CraftUIActivated)
                OpenCraftUI();
            else
                CloseCraftUI();


        }
    }


    //크래프팅
    public void OnClick_Crafting()
    {
        if (check_craft[0] && check_craft[1] && check_craft[2])
        {
            //if(Slots[3] == null)
            {
                Inventory.AcquireItem(crafting_set.item_Set[player_Craft_State].item[Craft_Item_Number], 1);
                for(int i = 0; i< crafting_set.item_Set[player_Craft_State].item[Craft_Item_Number].Crafting_Count.Length; i++)
                {
                    Slots[i].SetSlotCount(-crafting_set.item_Set[player_Craft_State].item[Craft_Item_Number].Crafting_Count[i]);
                    Value_info[i].Get_AfterCarfting_Count(Slots[i].Get_Count());
                }
            }
            //else
            {
                //인벤토리에 빈 공간이 없습니다.
            }
        }
        else
            Debug.Log("Nope");



    }




    #region 조합식 관련

    //어떤 조합식을 눌렀는지 체크 해서 해당 조합식을 보여줌
    public void OnClick_check()
    {
        //조합 가능 초기화
        check_craft[0] = true;
        check_craft[1] = true;
        check_craft[2] = true;
        switch (EventSystem.current.currentSelectedGameObject.name)
        {
            //테스트!!!!!!!!!!!!!!! : 두번쨰 파라미터 -바운더리값 

            case "Mix_Table0":
                create_MixTable(0, player_Craft_State);
                    break;
            case "Mix_Table1":
                create_MixTable(1, player_Craft_State);
                break;
            case "Mix_Table2":
                create_MixTable(2, player_Craft_State);
                break;
            case "Mix_Table3":
                create_MixTable(3, player_Craft_State);
                break;
            case "Mix_Table4":
                create_MixTable(4, player_Craft_State);
                break;
            case "Mix_Table5":
                create_MixTable(5, player_Craft_State);
                break;
            case "Mix_Table6":
                create_MixTable(6, player_Craft_State);
                break;
            case "Mix_Table7":
                create_MixTable(7, player_Craft_State);
                break;
                
        }
    }




    //플레이어 크래프팅 상태에 따라 조합식 변경하는 함수
    void create_MixTable(int _Craft_Item_Number, int _Player_Craft_State/*바운더리 정보*/)
    {
        Craft_Item_Number = _Craft_Item_Number;
        crafting_item_info.Get_Image(crafting_set.item_Set[_Player_Craft_State].item[_Craft_Item_Number].itemSprite);
        crafting_item_info.Get_Name(crafting_set.item_Set[_Player_Craft_State].item[_Craft_Item_Number].itemName);
        crafting_item_info.Get_info(crafting_set.item_Set[_Player_Craft_State].item[_Craft_Item_Number].itemInfo);



       
        string[] Craft_Item_Name;
        for(int  i = 0; i<3; i++)
        {
            Value[i].gameObject.SetActive(false);
            if (crafting_set.item_Set[_Player_Craft_State].item[_Craft_Item_Number].Crafting_Sprite.Length > i)
            {
                Value[i].gameObject.SetActive(true);
                //Craft_Item_Name
                Value_info[i].Get_Item_image(crafting_set.item_Set[_Player_Craft_State].item[_Craft_Item_Number].Crafting_Sprite[i]);
            }
            else
            {
                Value[i].gameObject.SetActive(false);
            }
        }


        int[] have_Count  = { 0, 0, 0 };
        int slotCount = 0;
        for(int i = 0; i<8; i++)
        {
            if (Inventory.Etc_slots[i].item == null && Slots[3] == null)
            {
                Slots[3] = Inventory.Etc_slots[i];
            }
            if(Inventory.Etc_slots[i].item != null)
            {
                for(int j = 0; j < crafting_set.item_Set[_Player_Craft_State].item[_Craft_Item_Number].Crafting_Count.Length; j++)
                {
                    if(crafting_set.item_Set[_Player_Craft_State].item[_Craft_Item_Number].Crafting_ItemName[j] != null
                        && Inventory.Etc_slots[i].item.itemName == crafting_set.item_Set[_Player_Craft_State].item[_Craft_Item_Number].Crafting_ItemName[j])
                    {
                        Slots[j] = Inventory.Etc_slots[i];
                        have_Count[j] = Slots[j].Get_Count();
                        slotCount++;
                    }
                }


              
            }

        }
        for(int i = 0; i<3; i++)
        {
            check_craft[i] = true;
            if (Value[i].activeSelf == true)
            {

                Value_info[i].Get_Count(crafting_set.item_Set[_Player_Craft_State].item[_Craft_Item_Number].Crafting_Count[i], have_Count[i]);
                if (have_Count[i] >= crafting_set.item_Set[_Player_Craft_State].item[_Craft_Item_Number].Crafting_Count[i])
                    check_craft[i] = true;
                else
                    check_craft[i] = false;


            }
        }



        ////조합 재료 필요한 만큼 활성화
        //for (int index = 0; index < crafting_set.item_Set[_Player_Craft_State].item[_Craft_Item_Number].Crafting_Sprite.Length; index++)
        // {
        //    //각 조합재료 이미지 세팅
        //    Value[index].GetComponent<Image>().sprite = crafting_set.item_Set[_Player_Craft_State].item[_Craft_Item_Number].Crafting_Sprite[index];

        //    for (int index1 = 0; index1 < 8; index1++)
        //    {
                
        //        //인벤토리 서치 - 아이템이 있는지
        //        if(Inventory.Etc_slots[index1].item ==null)
        //        {

        //        }



        //         if (Inventory.Etc_slots[index1].item!=null && Inventory.Etc_slots[index1].item.itemName 
        //            == crafting_set.item_Set[_Player_Craft_State].item[_Craft_Item_Number].Crafting_ItemName[index])
        //        {
        //            Value[index].GetComponentInChildren<Text>().text =
        //            Inventory.Etc_slots[index1].itemCount + " / " + crafting_set.item_Set[_Player_Craft_State].item[_Craft_Item_Number].Crafting_Count[index];
        //            //인벤토리내 오브젝트의 갯수가 같거나 많으면
        //            if (Inventory.Etc_slots[index1].itemCount >= crafting_set.item_Set[_Player_Craft_State].item[_Craft_Item_Number].Crafting_Count[index])
        //            {
        //                Value[index].GetComponentInChildren<Text>().color = new Color(0.3f, 0.3f, 0.3f, 1.0f);
        //                check_craft[index] = true;
        //                break;
        //            }
        //            else            //아이템의 갯수가 적다면
        //            {
                       
        //                Value[index].GetComponentInChildren<Text>().color = new Color(0.3f, 0.3f, 0.3f, 0.5f);
        //                check_craft[index] = false;
        //                break;
        //            }
        //        }
        //        else            //아이템이 없다면
        //        {
                    
        //            Value[index].GetComponentInChildren<Text>().color = new Color(0.3f, 0.3f, 0.3f, 0.5f);
        //            Value[index].GetComponentInChildren<Text>().text =
        //            "0 / " + crafting_set.item_Set[_Player_Craft_State].item[_Craft_Item_Number].Crafting_Count[index];
        //            check_craft[index] = false;
        //        }
        //        if (check_craft[index] == true)
        //        { break; }
        //    }

        //}
        ////조합 재료 필요없는부분 비활성화
        //for (int index1 = crafting_set.item_Set[_Player_Craft_State].item[_Craft_Item_Number].Crafting_Sprite.Length; index1 < 3; index1++)
        //{
            
        //    R.transform.GetChild(1).transform.GetChild(index1).gameObject.SetActive(false);
        //}


    }
    #endregion

    #region 초기설정
    void Binding_Crafting_Set()
    {
        crafting_Menu_Create(player_Craft_State);
    }

    void crafting_Menu_Create(int num)
    {
        //메뉴의 갯수에 맞게 content 높이 조절
        Content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, crafting_set.item_Set[num].item.Length * 70);

        //필요한 갯수 만큼 메뉴를 활성화
        for (int index = 0; index < crafting_set.item_Set[num].item.Length; index++)
        {
            //메뉴 스프라이트, 이름 설정
            Content.transform.GetChild(0).transform.GetChild(index).transform.GetChild(0).GetComponent<Image>().sprite = crafting_set.item_Set[num].item[index].itemSprite;
            Content.transform.GetChild(0).transform.GetChild(index).GetComponentInChildren<Text>().text = crafting_set.item_Set[num].item[index].itemName;

            //메뉴의 버튼액션 바인딩

        }
        //필요없는 메뉴 비활성화

        for (int index1 = crafting_set.item_Set[num].item.Length; index1 < 8; index1++)
        {
            Content.transform.GetChild(0).transform.GetChild(index1).gameObject.SetActive(false);
        }
    }
    #endregion

    public void OpenCraftUI()
    {
        player_Craft_State = Player_Base.Crafting_State;
        CraftUI_Base.SetActive(true);
        create_MixTable(0, player_Craft_State);
        Binding_Crafting_Set();
    }

    public void CloseCraftUI()
    {
        CraftUI_Base.SetActive(false);
    }
}
