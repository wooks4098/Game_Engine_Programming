using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory_LeftClick : MonoBehaviour
{
    [SerializeField] private Text Name;
    [SerializeField] private Text Type;
    [SerializeField] private Text Weight;
    [SerializeField] private Text Count;
    [SerializeField] private Text Info;
    [SerializeField] private GameObject Inventory_Left_Click_Base;

    private Vector3 Name_Pos;
    private Item item;
    private int Item_Count;

    public void Show(Item _item,int _Count ,Vector3 _Pos)
    {
        Name_Pos = _Pos;
        item = _item;
        Item_Count = _Count;
        Inventory_Left_Click_Base.SetActive(true);

        Name.text = item.itemName;
        switch (item.itemType)
        {
            case Item.ITEMTYPE.Expendables:
                Type.text = "소모품";
                break;
            case Item.ITEMTYPE.Install:
                Type.text = "설치";
                break; ;
            case Item.ITEMTYPE.Material:
                Type.text = "재료";
                break;
        }

        Weight.text = "무게 - " + item.itemweight.ToString();
        Count.text = "소지 - " + Item_Count.ToString();
        Info.text = item.itemInfo;
        StartCoroutine("TextPosSet");
    }


    IEnumerator TextPosSet()
    {
        yield return null;
        Name_Pos += new Vector3(Inventory_Left_Click_Base.GetComponent<RectTransform>().rect.width * 0.5f, -Inventory_Left_Click_Base.GetComponent<RectTransform>().rect.height * 0.7f, 0f);
        Inventory_Left_Click_Base.transform.position = Name_Pos;
        Type.GetComponent<RectTransform>().anchoredPosition = new Vector2(Name.GetComponent<RectTransform>().anchoredPosition.x + Name.GetComponent<RectTransform>().rect.width + 10
                         , Name.GetComponent<RectTransform>().anchoredPosition.y - 5);
        yield break;
    }

    public void Hide()
    {
        Inventory_Left_Click_Base.SetActive(false);
    }
}
