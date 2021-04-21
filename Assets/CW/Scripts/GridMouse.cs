using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMouse : MonoBehaviour
{
    GridGetNear grid;
    public GameObject img;
    Vector3 realpos;
    private void Awake()
    {
        grid = FindObjectOfType<GridGetNear>();
    }
    void Update()
    {
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitInfo))
        {
            realpos = grid.GetNearGridPoint(hitInfo.point);
            img.transform.position = realpos;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(Physics.Raycast(ray, out hitInfo))
            {
                PlaceNear(hitInfo.point);
            }
        }
    }

    void PlaceNear(Vector3 point)
    {
        Vector3 nearpos = grid.GetNearGridPoint(point);
        GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = nearpos;
    }
}
