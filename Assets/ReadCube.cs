using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCube : MonoBehaviour
{
    public Transform tUp, tDown , tRight ,tLeft, tFront,tBack;

    //this is for the faces of the cube
    private int layerMask = 1 << 8;

    CubeState cubeState;
    CubeMap cubeMap;
    private void Start()
    {
        cubeState= FindObjectOfType<CubeState>();
        cubeMap = FindObjectOfType<CubeMap>();
    }
    private void Update()
    {
        List<GameObject> faceHit = new List<GameObject>();
        Vector3 ray = tFront.transform.position;
        RaycastHit hit;
        if (Physics.Raycast(ray, tFront.forward, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(ray, tFront.forward * hit.distance, Color.yellow);
            faceHit.Add(hit.collider.gameObject);
            print(hit.collider.gameObject.name);
        }
        else
        {
            Debug.DrawRay(ray, tFront.forward * 100f, Color.green);
        }
        cubeState.front = faceHit;
        cubeMap.Set();
    }

}
