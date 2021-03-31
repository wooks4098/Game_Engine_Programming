using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    Vector3 pos;
    float zdis;
    private void Start()
    {
        zdis = player.transform.position.z - transform.position.z;
    }
    void Update()
    {
        pos = player.transform.position;
        transform.position = new Vector3(pos.x,transform.position.y,pos.z-zdis);
    }
}
