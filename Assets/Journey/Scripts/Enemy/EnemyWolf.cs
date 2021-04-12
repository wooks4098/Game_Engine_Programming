using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWolf : Enemy
{

    void State()
    {
        switch(state)
        {
            case STATE.IDLE:
                break;
            case STATE.FOLLOW:
                break;
            case STATE.ATTACK:
                break;
            case STATE.DIE:
                break;
        }
    }
}
