using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crafting_Item_info : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private Text Name;
    [SerializeField]
    private Text info;

    public void Get_Image(Sprite _sprite)
    {
        image.sprite = _sprite;
    }

    public void Get_Name(string _Name)
    {
        Name.text = _Name;
    }
    public void Get_info(string _info)
    {
        info.text = _info;
    }
}
