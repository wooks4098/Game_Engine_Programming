using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButtons : MonoBehaviour
{
    public Enemy wolf;
    public Enemy deer;
    public Enemy rabbit;

    public void AttackWolf()
    {
        wolf.OnDamage(20, 10, 10);
    }

    public void AttackDeer()
    {
        deer.OnDamage(20, 10, 10);
    }

    public void AttackRabbit()
    {
        rabbit.OnDamage(20, 10, 10);
    }
}
