using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perspective : MonoBehaviour
{
    //3 possible states
    public static int currView = 0;

    //Function used to switch the position of the camera based on the user's preference
    public void switchView()
    {
        if (currView == 0)
        {
            currView = 1;
            GameObject.Find("Main Camera").transform.position = new Vector3(2.3f, 2f, 0f);
            GameObject.Find("Main Camera").transform.eulerAngles = new Vector3(50f, -90f, 0f);
        }
        else if (currView == 1)
        {
            currView = 2;
            GameObject.Find("Main Camera").transform.position = new Vector3(0f, 2f, -2.2f);
            GameObject.Find("Main Camera").transform.eulerAngles = new Vector3(50f, 0f, 0f);
        }
        else
        {
            currView = 0;
            GameObject.Find("Main Camera").transform.position = new Vector3(-2.3f, 2f, 0f);
            GameObject.Find("Main Camera").transform.eulerAngles = new Vector3(50f, 90f, 0f);
        }
    }
}
