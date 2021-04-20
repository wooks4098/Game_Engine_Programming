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

    Crafting_Set crafting_set;
  
    private Slot[] Slots;
    
    public GameObject Content;
    public GameObject R;
    [SerializeField]
    private Inventory Inventory;

    
    //조합 채크
    bool[] check_craft = { true, true, true };

    void Awake()
    {
        crafting_set = GetComponent<Crafting_Set>();
        Binding_Crafting_Set();  
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

    void OpenCraftUI()
    {
        player_Craft_State = Player_Base.Crafting_State;
        CraftUI_Base.SetActive(true);
    }

    void CloseCraftUI()
    {
        CraftUI_Base.SetActive(false);
    }

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


    public void OnClick_Crafting()
    {
        if (check_craft[0] && check_craft[1] && check_craft[2])
            Debug.Log("crafting");
        else
            Debug.Log("Nope");



    }


    void create_MixTable(int Num, int Num2/*바운더리 정보*/)
    {
        

        //조합재료 레이어 초기화 (활성화)
        R.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(true);
        R.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(true);
        R.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(true);

        /*
        크래프팅 아이템 클릭
        인벤토리 슬롯 탐색 -> 해당 쟤료가 있으면 슬롯에 저장 (배열)
        개수 Get_Count()를 사용해서 변경
         */


        R.transform.GetChild(0).GetComponent<Image>().sprite = crafting_set.item_Set[Num2].item[Num].itemSprite;
        R.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = crafting_set.item_Set[Num2].item[Num].itemName;
        R.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = crafting_set.item_Set[Num2].item[Num].itemInfo;


        //조합 재료 필요한 만큼 활성화
        for (int index = 0; index < crafting_set.item_Set[Num2].item[Num].Crafting_Sprite.Length; index++)
                {
            //각 조합재료 이미지 세팅
            R.transform.GetChild(1).transform.GetChild(index).GetComponent<Image>().sprite = crafting_set.item_Set[Num2].item[Num].Crafting_Sprite[index];
            for (int index1 = 0; index1 < 8; index1++)
            {
                //인벤토리와 조합재료 비교
                if (Inventory.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetChild(index1).GetComponent<Slot>().item != null &&
                    Inventory.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetChild(index1).GetComponent<Slot>().item.itemName ==
                    crafting_set.item_Set[Num2].item[Num].Crafting_ItemName[index])
                {
                    //인벤토리내 아이템 갯수 + "/" + 조합재료 갯수
                    R.transform.GetChild(1).transform.GetChild(index).transform.GetChild(0).GetComponent<Text>().text =
                        Inventory.transform.GetChild(0).transform.GetChild(0).transform.GetChild(1).transform.GetChild(index1).GetComponent<Slot>().item.itemName + " / " + crafting_set.item_Set[Num2].item[Num].Crafting_Count[index];

                }
                else
                {
                    R.transform.GetChild(1).transform.GetChild(index).transform.GetChild(0).GetComponent<Text>().color = new Color(125f, 15f, 125f);
                    R.transform.GetChild(1).transform.GetChild(index).transform.GetChild(0).GetComponent<Text>().text = "0 / " + crafting_set.item_Set[Num2].item[Num].Crafting_Count[index];
                    // 조합 불가능
                    check_craft[index] = false;
                }
            }
        }
        //조합 재료 필요없는부분 비활성화
        for (int index1 = crafting_set.item_Set[Num2].item[Num].Crafting_Sprite.Length; index1 < 3; index1++)
        {
            
            R.transform.GetChild(1).transform.GetChild(index1).gameObject.SetActive(false);
        }

    }


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

}
