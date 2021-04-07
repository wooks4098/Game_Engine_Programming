using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Creature : MonoBehaviour
{ 
	public bool isDead; //생존 여부

	//스테이터스
	public float startingHealth = 100; //시작 체력
	public float health;//현재 체력
	public float damage; //데미지
	/*
	public float defense; //방어력
	public int evasion; //회피율
	public int accuracy; //명중률
	public float critical; //크리티컬 확률
	*/

	//생명체가 활성화 될 때 상태 리셋
	protected virtual void OnEnable()
    {
		isDead = false;
		health = startingHealth;
    }

	//데미지를 입는 기능
	public virtual void OnDamage(float damage) { }

	public virtual void Die() { }
}
