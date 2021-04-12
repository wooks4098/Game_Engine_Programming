using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour, ICreature
{
    public bool isDead; //생존 여부

    //스테이터스
    public float startingHealth = 100; //시작 체력
    public float health;//현재 체력
    public float damage; //데미지
    public float defense; //방어력
    public float evasion; //회피율
    public float accuracy; //명중률
    public float critical; //크리티컬 확률


    //스테이터스 초기화
    public virtual void OnEnable()
    {
        isDead = false;
        health = startingHealth;
        damage = 0;
        defense = 0;
        evasion = 0;
        accuracy = 0;
        critical = 0;
    }

    //데미지를 입는 기능
    public void OnDamage(float damage)//, float accuracy, float critical)
    {
        health -= damage;
        if (health <= 0 && !isDead)
            Die();
    }

    //사망 처리
    public void Die()
    {
        isDead = true;
        Debug.Log("플레이어 사망");
    }

    public Enemy enemy;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            enemy.OnDamage(enemy.Fight(evasion, defense));
            Debug.Log("플레이어가 적 공격");
        }
    }

    //받을 데미지 계산 (적이 플레이어를 공격할 때)
    public float Fight(float _evasion, float _defense)
    {
        int rand = Random.Range(0, 101);

        float _damage = 0f;

        if (0 < accuracy - _evasion)
        {
            rand = Random.Range(0, 101);
            if (0 < critical)
            {
                _damage = damage * 1.2f - _defense;
                Debug.Log("적이 플레이어를 공격 - 1");
            }
            else
            {
                _damage = damage - _defense;
                Debug.Log("적이 플레이어를 공격 - 2");
            }
        }
        else
        {
            _damage = 0;
            Debug.Log("적이 플레이어를 공격 - 3");
        }

        return _damage;
    }

}
