using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ICreature
{
	//생명체가 활성화 될 때 상태 리셋
	void OnEnable();

	//데미지 계산
	float Fight(float _damage, float _critical, float _accuracy);

	//데미지를 입는 기능
	void OnDamage(float _damage, float _critical, float _accuracy);
	
	//사망처리
	void Die();
}
