using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{

    public LayerMask mouseInputMask;
    public GameObject buildingPrefab;
    

    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, mouseInputMask))
            {
                Vector3 position = hit.point-transform.position;

            }
        }
        
    }



    //private void CreatebUilding(Vector3 gridPosition)
    //{
    //    Instantiate(buildingPrefab, gridPosition, Quaternion.identity);
    //}
}
