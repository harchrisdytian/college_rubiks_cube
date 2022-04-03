using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XYZMoves : MonoBehaviour
{
    private Vector2 firstPressedPos, secondPressedPos, currentSwipeDir;
    private Vector3 mouseDelta, prevMousePos;
    public GameObject target;

    float speed = 200f;


    private void Update()
    {
        Swipe();
        Drag();
    }
    private void Drag()
    {
        if (Input.GetMouseButton(1))
        {
            var mouseDelta = Input.mousePosition - prevMousePos;
            mouseDelta *= 0.1f;
            transform.rotation = Quaternion.Euler(mouseDelta.y, -mouseDelta.x, 0f) * transform.rotation;
        }
        else
        {
            if (transform.rotation != target.transform.rotation)
            {
                float step = speed * Time.deltaTime;
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target.transform.rotation, step);
            }
        }
        prevMousePos = Input.mousePosition;
    }

    void Swipe()
    {
        if (Input.GetMouseButtonDown(1))
        { 
            firstPressedPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        if (Input.GetMouseButtonUp(1))
        {
            secondPressedPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currentSwipeDir = secondPressedPos - firstPressedPos;
            currentSwipeDir.Normalize();
            if (LeftSwipe())
            {
 
                target.transform.Rotate(0, 90, 0, Space.World);
            }
            else if (RightSwipe())
            {
                target.transform.Rotate(0, -90, 0, Space.World);
            }
            else if(UpLeftSwipe())
            {
                target.transform.Rotate(90,0,0 , Space.World);
            }
            else if (UpRightSwipe())
            {
                target.transform.Rotate(0,0, -90, Space.World);
            }
            else if (DownLeftSwipe())
            {
                target.transform.Rotate(0, 0, 90, Space.World);
            }
            else if (DownRightSwipe())
            {
                target.transform.Rotate(-90, 0, 0, Space.World);
            }


        }
    

    }
    bool LeftSwipe()
    {
        return currentSwipeDir.x < 0 && currentSwipeDir.y > -0.5f && currentSwipeDir.y < 0.5f;
    }
    bool RightSwipe()
    {
        return currentSwipeDir.x > 0 && currentSwipeDir.y > -0.5f && currentSwipeDir.y < 0.5f;
    }
    bool UpLeftSwipe()
    {
        return currentSwipeDir.y > 0f && currentSwipeDir.x < 0f;
    }
    bool UpRightSwipe()
    {
        return currentSwipeDir.y > 0f && currentSwipeDir.x > 0f;
    }
    bool DownLeftSwipe()
    {
        return currentSwipeDir.y < 0f && currentSwipeDir.x < 0f;
    }
    bool DownRightSwipe()
    {
        return currentSwipeDir.y < 0f && currentSwipeDir.x > 0f;
    }
}
