using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New item/item")]
public class Item : ScriptableObject
{
    public bool isActive;           //사용가능한 아이템인지
    public string itemName;         //아이템 이름
    public ITEMTYPE itemType;       //아이템 종류
    public float weight;            //아이템 무게
    public float Posivive_Effect;   //사용 긍정적 효과
    public float Negative_Effect;   //사용 부정적 효과

    public Sprite itemSprite;       //아이템 스프라이트
    //public GameObject itemPrefab;   //아이템 프리펩 (화면에 보여줄 오브젝트)

    public string Explanation;      //아이템 설명

    public enum ITEMTYPE
    {
        Material,       //재료
        Expendables,    //소모품
        Install,        //설치
        Tool,           //도구
    }
}
