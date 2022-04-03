using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotRoation : MonoBehaviour
{
    private List<GameObject> activeSide;
    private Vector3 localForward;
    private Vector3 mouseRef;
    private bool dragging = false;
    public bool autoRotate = false;
    private float sens = 0.8f;
    private float speed = 300f;

    private Vector3 rotation;
    private Quaternion targetQuaternion;

    private CubeState cubeState;
    private ReadCube readCube;
    void Start()
    {
        readCube = FindObjectOfType<ReadCube>();
        cubeState = FindObjectOfType<CubeState>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dragging)
        {
            SpinWithSide(activeSide);
            if (Input.GetMouseButtonUp(0))
            {
                dragging = false;
                RotateToRightAngle();
            }
        }
            if (autoRotate)
            {
                AutoRotate();
            }
        
    }
    private void SpinWithSide(List<GameObject> side)
    {
        rotation = Vector3.zero;

        Vector3 mouseOffset = (Input.mousePosition - mouseRef);

        if(side == cubeState.right)
        {
            rotation.x = (mouseOffset.x + mouseOffset.y) * sens * 1;
        }
        if (side == cubeState.left)
        {
            rotation.x = (mouseOffset.x + mouseOffset.y) * sens * 1;
        }
        if (side == cubeState.up)
        {
            rotation.y = (mouseOffset.x + mouseOffset.y) * sens * -1;
        }
        if (side == cubeState.down)
        {
            rotation.y = (mouseOffset.x + mouseOffset.y) * sens * -1;
        }
        if (side == cubeState.front)
        {
            rotation.z = (mouseOffset.x + mouseOffset.y) * sens * -1;
        }
        if (side == cubeState.back)
        {
            rotation.z = (mouseOffset.x + mouseOffset.y) * sens * -1;
        }

        transform.Rotate(rotation, Space.Self);

        mouseRef = Input.mousePosition;
    }
    public void rotate(List<GameObject> side)
    {
        activeSide = side;
        mouseRef = Input.mousePosition;
        dragging = true;
        autoRotate = false;

        localForward = Vector3.zero - side[4].transform.parent.transform.localPosition;
    }

    public void RotateToRightAngle()
    {
        Vector3 vector = transform.localEulerAngles;

        vector.x = Mathf.Round(vector.x / 90) * 90;
        vector.y = Mathf.Round(vector.y / 90) * 90;
        vector.z = Mathf.Round(vector.z / 90) * 90;

        targetQuaternion.eulerAngles = vector;
        autoRotate = true;

    }
    private void AutoRotate()
    {
        print("rotating");
        var step = speed  * Time.deltaTime;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetQuaternion, step);

        if (Quaternion.Angle(transform.localRotation, targetQuaternion) <= 1)
        {
            transform.localRotation = targetQuaternion;
            readCube.readState();
            cubeState.PutDown(activeSide, transform.parent);
            autoRotate = false;
            dragging = false;
        }

    }
}
