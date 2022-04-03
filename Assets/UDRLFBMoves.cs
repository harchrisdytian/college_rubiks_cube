using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UDRLFBMoves : MonoBehaviour
{
    CubeState cubeState;
    ReadCube readCube;

    int layerMask = 1 << 8;
    private void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            readCube.readState();

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 100.0f, layerMask))
            {
                GameObject face = hit.collider.gameObject;
                List<List<GameObject>> cubeSides = new List<List<GameObject>>()
                {
                    cubeState.up,
                    cubeState.down,
                    cubeState.left,
                    cubeState.right,
                    cubeState.front,
                    cubeState.back
                };
                   
                foreach(List<GameObject> cubeSide in cubeSides)
                {
                    if(cubeSide.Contains(face))
                    {
                        cubeState.PickUp(cubeSide);
                    }
                }
            }
        }
        
        
    }

   
}
