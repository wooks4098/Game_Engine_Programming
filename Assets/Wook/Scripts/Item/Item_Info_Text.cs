using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Info_Text : MonoBehaviour
{
    [SerializeField] private Text Name;
    [SerializeField] private Text Type;
    [SerializeField] private Text Weight;
    [SerializeField] private Text Count;
    [SerializeField] private Text Info;
    [SerializeField] private Image Base;


    private void OnEnable()
    {
       // Base.color = new Vector4(1, 1, 1, 1);

        //Type.GetComponent<RectTransform>().anchoredPosition = new Vector2(Name.GetComponent<RectTransform>().anchoredPosition.x + Name.GetComponent<RectTransform>().rect.width + 10
        //                 , Name.GetComponent<RectTransform>().anchoredPosition.y - 5);
        StartCoroutine("TextPosSet");
    }
    private void OnDisable()
    {
        Name.text  = "";
        Type.text = "";
        Weight.text = "";
        Count.text = "";
        Info.text = "";
        Base.color = new Vector4(1, 1, 1, 0);
    }
    IEnumerator TextPosSet()
    {
        yield return null;
        yield return null;
        Base.color = new Vector4(1, 1, 1, 1);

        Type.GetComponent<RectTransform>().anchoredPosition = new Vector2(Name.GetComponent<RectTransform>().anchoredPosition.x + Name.GetComponent<RectTransform>().rect.width + 10
                         , Name.GetComponent<RectTransform>().anchoredPosition.y - 5);
        yield break;
    }
}
