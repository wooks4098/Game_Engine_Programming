using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 사용 효과 클래스 (예) 고기먹기)
/// </summary>
[System.Serializable]
public class ItemEffect
{
    public string itemName;     //아이템 이름
    public string[] part;       //부위
    public int[] num;           //수치
}


public class Item_Using : MonoBehaviour
{
    private const string Health = "Health", Hungry = "Hungry", Thirsty = "Thirsty";


    [SerializeField]
    private ItemEffect[] itemEffects;

    [SerializeField]
    private Player_Status_Controller Status_Controller;
    private Slot slot;


    public void Click_Item(Slot _slot)
    {
        slot = _slot;
    }

    public void Use_item()
    {
        if(slot.item.itemType == Item.ITEMTYPE.Expendables)
        {
            for(int x = 0; x< itemEffects.Length; x++)
            {
                if(itemEffects[x].itemName == slot.item.name)
                {
                    for (int y = 0; y < itemEffects[x].part.Length; y++)
                    {
                        switch(itemEffects[x].part[y])
                        {
                            case Health:
                                Status_Controller.Change_Health(itemEffects[x].num[y]);
                                break;
                            case Hungry:
                                Status_Controller.Change_Hungry(itemEffects[x].num[y]);

                                break;
                            case Thirsty:
                                Status_Controller.Change_Thirsty(itemEffects[x].num[y]);

                                break;
                            default:
                                Debug.Log("적절한 상태를 찾지 못했습니다.");
                                break;
                        }
                    }
                    slot.SetSlotCount(-1);
                    slot.inventory_RightClick_Off();
                    return;
                }
            }
            Debug.Log("일치하는 itemName이 없습니다");
        }
    }

}
