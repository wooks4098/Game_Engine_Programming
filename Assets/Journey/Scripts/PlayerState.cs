using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : Creature
{
    public Enemy enemy;

    /*
    float hungry;
    float thirsty;
    */

    //데미지를 입는 기능
    public override void OnDamage(float damage)//, float accuracy, float critical)
    {
        health -= damage;
        if (health <= 0 && !isDead)
            Die();
    }

    public override void Die()
    {
        isDead = true;
        transform.rotation = Quaternion.Euler(new Vector3(45, 0, 0));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("마우스 클릭");
            enemy.OnDamage(damage); //실험하느라 여기 넣어둠(수정해야함)
        }
    }
}
