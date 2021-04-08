using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory_RightClick : MonoBehaviour
{
    public static bool Inventory_RightClick_Activated = false;
    [SerializeField]
    private Button Using;
    [SerializeField]
    private Button Crafting;

    [SerializeField]
    private GameObject Inventory_Click_Base;


    public void Show(Item _item,Vector3 _Pos)
    {
        Color color = new Vector4(1,1,1,1);
        _Pos += new Vector3(Inventory_Click_Base.GetComponent<RectTransform>().rect.width * 0.5f, -Inventory_Click_Base.GetComponent<RectTransform>().rect.height * 0.7f, 0f);


        Inventory_Click_Base.SetActive(true);
        Inventory_Click_Base.transform.position = _Pos;

        if (_item.itemType == Item.ITEMTYPE.Material)//재료
        {
            Using.interactable = false;
            color.a = 0.5f;
            Using.image.color = color;
            Crafting.interactable = true;
            color.a = 1f;
            Crafting.image.color = color;
        }
        else if(_item.itemType == Item.ITEMTYPE.Expendables)//소모품
        {
            Using.interactable = true;
            color.a = 1f;
            Using.image.color = color;
            Crafting.interactable = false;
            color.a = 0.5f;
            Crafting.image.color = color;
        }
        else if (_item.itemType == Item.ITEMTYPE.Install)//설치
        {
            Using.interactable = false;
            color.a = 0.5f;
            Using.image.color = color;
            Crafting.interactable = false;
            color.a = 0.5f;
            Crafting.image.color = color;
        }

        Inventory_RightClick_Activated = true;
    }
    void Show_obj()
    { 
}
    public void Hide()
    {
        Inventory_RightClick_Activated = false;
        Inventory_Click_Base.SetActive(false);

    }
}
