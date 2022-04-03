using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CubeMap : MonoBehaviour
{
    public Transform Up, Down, Left, Right, Front, Back;
    CubeState cubeState;
    public void Set()
    {
        cubeState = FindObjectOfType<CubeState>();
        UpdateMap(cubeState.front, Front);
    }
    void UpdateMap(List<GameObject> face, Transform side)
    {
        int i = 0;
        foreach(Transform map in side)
        {
            if(face[i].name[0] == 'F')
            {
                map.GetComponent<Image>().color = Color.green;
            }
            if (face[i].name[0] == 'B')
            {
                map.GetComponent<Image>().color = Color.blue;
            }
            if (face[i].name[0] == 'R')
            {
                map.GetComponent<Image>().color = Color.red;
            }
            if (face[i].name[0] == 'L')
            {
                map.GetComponent<Image>().color = new Color(1, 0.5f,0,1);
            }
            if (face[i].name[0] == 'U')
            {
                map.GetComponent<Image>().color = Color.white;
            }
            if (face[i].name[0] == 'D')
            {
                map.GetComponent<Image>().color = Color.yellow;
            }
        }
    }
}
