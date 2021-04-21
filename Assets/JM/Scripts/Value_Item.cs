using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Value_Item : MonoBehaviour
{
    [SerializeField]
    private Text Mix_Item_Info;
    [SerializeField]
    private Image image;
    private int Required_Count; //조합에 필요한 개수
    private int Have_Count; //가지고 있는 개수

    public void Get_Item_image(Sprite _item_image)
    {
        image.sprite = _item_image;
    }

    public void Get_Count(int _Required_Count, int _Have_Count)
    {
        Required_Count = _Required_Count;
        Have_Count = _Have_Count;

        Mix_Item_Info_Set();
    }

    //크래프팅 후 업데이트용
    public void Get_AfterCarfting_Count(int _Have_Count)
    {
        Have_Count = _Have_Count;

        Mix_Item_Info_Set();
    }



    //text보여주는 함수
    void Mix_Item_Info_Set()
    {
        Mix_Item_Info.text = Have_Count.ToString() + "/" + Required_Count.ToString();   

        if (Have_Count >= Required_Count)
        {
            Mix_Item_Info.color =  new Color(0.3f, 0.3f, 0.3f, 1.0f);
        }
        else
        {
            Mix_Item_Info.color = new Color(0.3f, 0.3f, 0.3f, 0.5f);

        }
    }

}
