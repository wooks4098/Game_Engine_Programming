using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[System.Serializable]
public class Item_set
{
    public Item[] item;
}

public class Crafting_Set : MonoBehaviour
{
    public Item[] Self;
    public Item[] Hut;
    public Item[] CampFire;

    public Item_set[] item_Set;
    
}
