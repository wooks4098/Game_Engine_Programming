using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager gamemanager;
    public GameObject menuset;

   void Update()
    {
        if (Input.GetButtonDown("Close"))
            menuset.SetActive(false);
    }
}
