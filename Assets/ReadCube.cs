using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadCube : MonoBehaviour
{
    public Transform tUp, tDown , tRight ,tLeft, tFront, tBack;

    private List<GameObject> frontRays = new List<GameObject>();
    private List<GameObject> backRays = new List<GameObject>();
    private List<GameObject> upRays = new List<GameObject>();
    private List<GameObject> downRays = new List<GameObject>();
    private List<GameObject> leftRays = new List<GameObject>();
    private List<GameObject> rightRays = new List<GameObject>();
    //this is for the faces of the cube
    private int layerMask = 1 << 8;

    CubeState cubeState;
    CubeMap cubeMap;
    public GameObject emptyGO;
    private void Start()
    {
        setRayTransforms();

        cubeState = FindObjectOfType<CubeState>();
        cubeMap = FindObjectOfType<CubeMap>();
    }
    private void Update()
    {

    }

    public void readState()
    {
        cubeState = FindObjectOfType<CubeState>();
        cubeMap = FindObjectOfType<CubeMap>();
        //set state
        cubeState.up = ReadFaces(upRays, tUp);
        cubeState.down = ReadFaces(downRays, tDown);
        cubeState.left = ReadFaces(leftRays, tLeft);
        cubeState.right = ReadFaces(rightRays, tRight);
        cubeState.front = ReadFaces(frontRays, tFront);
        cubeState.back = ReadFaces(backRays, tBack);

        cubeMap.Set();
    }
    void setRayTransforms()
    {
        upRays = BuildRays(tUp, new Vector3(90, 90, 0));
        downRays = BuildRays(tDown, new Vector3(270, 270, 0));
        backRays  = BuildRays(tBack, new Vector3(0, 180, 0));
        frontRays = BuildRays(tFront, new Vector3(0, 0, 0));
        rightRays  = BuildRays(tRight, new Vector3(0, 90, 0));
        leftRays  = BuildRays(tLeft, new Vector3(0, 270, 0));

    }

    List<GameObject> BuildRays(Transform rayTransform, Vector3 direction)
    {
        int rayCount = 0;
        List<GameObject> rays = new List<GameObject>();
        // 012
        // 345
        // 678
        for (int y = 1; y > -2; y--)
        {
            for (int x = -1; x < 2; x++)
            {
                Vector3 startPos = new Vector3(rayTransform.localPosition.x + x,
                    rayTransform.localPosition.y + y,
                    rayTransform.localPosition.z);
                GameObject rayStart = Instantiate(emptyGO, startPos, Quaternion.identity, rayTransform);
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                rayCount++;
            }
        }
        rayTransform.localRotation = Quaternion.Euler(direction);
        return rays;
    }

    public List<GameObject> ReadFaces(List<GameObject> rayStarts, Transform rayTransform)
    {
        List<GameObject> facesHit = new List<GameObject>();
        foreach(GameObject rayStart in rayStarts)
        {
            Vector3 ray = rayStart.transform.position;
            RaycastHit hit;


            if (Physics.Raycast(ray, rayTransform.forward, out hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(ray, rayTransform.forward * hit.distance, Color.yellow);
                facesHit.Add(hit.collider.gameObject);
                print(hit.collider.gameObject.name);
            }
            else
            {
                Debug.DrawRay(ray, rayTransform.forward * 100f, Color.green);
            }

        }

        return facesHit;
    }
}
