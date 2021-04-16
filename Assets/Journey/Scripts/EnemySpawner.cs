using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public int enemyNum;
    private Transform WorldCenter;

    void Awake()
    {
        WorldCenter = GameObject.FindGameObjectWithTag("WorldCenter").GetComponent<Transform>();
    }

    private void Start()
    {
        for (int i = 0; i < enemyNum; i++)
            Spawn();
    }

    //스폰하기
    private void Spawn()
    {
        //랜덤 지점 받아오기
        Vector3 spawnPoint = GetRandomPoint(WorldCenter.position, 15f);

        //프리팹 인스턴스화
        GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPoint, Quaternion.identity);
        
        //EnemySpawner 오브젝트의 자식으로 설정
        enemy.transform.parent = GameObject.FindGameObjectWithTag("EnemySpawner").transform;
    }

    //랜덤 지점 찾기
    public Vector3 GetRandomPoint(Vector3 center, float radius)
    {
        //구 안의 랜덤한 하나의 점
        Vector3 randomPos = Random.insideUnitSphere * radius + center;

        //네브메쉬 샘플링 결과 정보를 저장
        NavMeshHit hit;

        //randomPos와 가장 가까운 네브 메쉬 위의 한 점을 찾음
        NavMesh.SamplePosition(randomPos, out hit, radius, NavMesh.AllAreas);

        //찾은 점 반환
        return hit.position;
    }
}
