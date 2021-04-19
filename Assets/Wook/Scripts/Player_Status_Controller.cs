using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Status_Controller : Player_Base
{
    public Text Text_Hp;
    public Text Text_Hungry;
    public Text Text_Thirsty;

    private float Health_Time;
    [SerializeField]
    private float Health_Decrease_Time; //체력 감소 시간

    private float Hungry_Time;
    [SerializeField]
    private float Hungry_Decrease_Time;   //배고픔 감소 시간
    private float Thirsty_Time;
    [SerializeField]
    private float Thirsty_Decrease_Time;   //목마름 감소 시간

    private void Awake()
    {
        Hungry_Time = 0;
        Thirsty_Time = 0;
    }

    private void Update()
    {
        Decrease_Status();
        Text_Hp.text = "체력 : " + Health.ToString();
        Text_Hungry.text = "배고픔 : " + Hungry.ToString();
        Text_Thirsty.text = "목마름 : " + Thirsty.ToString();

    }

    public void Change_Health(float _Hp)
    {
        Health += _Hp;
        if (Health > Health_Max)
            Health = Health_Max;
        else if (Health <= 0)
        {
            //죽음
        }
    }
    public void Change_Hungry(float _Hungry)
    {
        Hungry += _Hungry;
        if (Hungry > 100)
            Hungry = 100;
        if (Hungry < 0)
            Hungry = 0;

    }
    public void Change_Thirsty(float _Thirsty)
    {
        Thirsty += _Thirsty;
        if (Thirsty > 100)
            Thirsty = 100;
        if (Thirsty < 0)
            Thirsty = 0;

    }

    void Decrease_Status()
    {
        Hungry_Time += Time.deltaTime;
        Thirsty_Time += Time.deltaTime;


        //배고픔 감소
        if (Hungry_Time >= Hungry_Decrease_Time && Hungry >= 1)
        {
            Hungry -= 10;
            if (Hungry < 0)
                Hungry = 0;
            Hungry_Time = 0;
        }

        //목마름 감소
        if (Thirsty_Time >= Thirsty_Decrease_Time && Thirsty >= 1)
        {
            Thirsty -= 10;
            if (Thirsty < 0)
                Thirsty = 0;
            Thirsty_Time = 0;

        }

        //체력 감소
        if(Hungry <= 0 || Thirsty <= 0)
            Health_Time += Time.deltaTime;

        if(Health_Time >= Health_Decrease_Time)
        {
            if (Hungry <= 0 && Thirsty <= 0)
                Health -= 10;
            else
                Health -= 5;

            if (Health < 0)
            {
                Health = 0;
                //죽음
            }
        }


    }

}
