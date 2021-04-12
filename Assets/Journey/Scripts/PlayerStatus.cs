using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerStatus : MonoBehaviour, ICreature
{
    //public Enemy enemy;

    public bool isDead; //생존 여부

    //스테이터스
    public float startingHealth = 100; //시작 체력
    public float health;//현재 체력
    public float damage; //데미지

    //public event Action OnDeath;

    /*
    float hungry;
    float thirsty;
    */

    //스테이터스 초기화
    public virtual void OnEnable()
    {
        isDead = false;
        health = startingHealth;
    }


    //데미지를 입는 기능
    public void OnDamage(float damage)//, float accuracy, float critical)
    {
        health -= damage;
        if (health <= 0 && !isDead)
            Die();
    }

    public void Die()
    {
        isDead = true;
        Debug.Log("플레이어 사망");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("적 공격");
            //enemy.OnDamage(damage); //실험용
        }
    }
}
