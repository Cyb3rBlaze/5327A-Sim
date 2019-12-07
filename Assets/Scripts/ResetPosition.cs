using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    public Vector3 savedPos;
    public Vector3 savedAngles;

    // Start is called before the first frame update
    void Start()
    {
        savedPos = this.transform.position;
        savedAngles = this.transform.eulerAngles;
    }

    // Updates every frame
    void Update()
    {
        if (Auton.reset == true)
        {
            this.transform.position = savedPos;
            this.transform.eulerAngles = savedAngles;
        }
    }

    // flag to reset
    public void resetPos()
    {
        Auton.reset = true;
    }
}
