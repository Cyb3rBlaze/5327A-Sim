using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opponent : MonoBehaviour
{
    public GameObject robot;

    public bool flag;

    private Rigidbody body;

    //Displacement variables used to set velocity of the userRobot
    private float displacementX;
    private float displacementY;

    private float deltaTurn;

    // Start is called before the first frame update
    void Start()
    {
        flag = false;

        body = GetComponent<Rigidbody>();

        //The initial values used to move the robot
        displacementX = 0;
        displacementY = -0.02f;

        //Constant used to perform calculations for robot movement velocities
        deltaTurn = 0.000366666667f;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, 0.06f, this.transform.position.z);
        this.transform.eulerAngles = new Vector3(0f, this.transform.eulerAngles.y, 0f);

        //Resetting physics values
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;

        if (Auton.reset == true && flag == false)
        {
            flag = true;
        }
        else
        {
            Vector3 robotPos = robot.transform.position;
            Vector3 myPos = this.transform.position;
            double angle = Mathf.Rad2Deg * (Mathf.Atan(((myPos.z-robotPos.z)/(myPos.x-robotPos.x))));
            print(angle);
            if ((this.transform.eulerAngles.y - 180) + angle < 0)
            {
                this.transform.eulerAngles = this.transform.eulerAngles + new Vector3(0f, 1f, 0f);
            }
            else if((this.transform.eulerAngles.y - 180) + angle > 0)
            {
                this.transform.eulerAngles = this.transform.eulerAngles + new Vector3(0f, -1f, 0f);
            }

            updateDisplacements();
            body.velocity = new Vector3(displacementX * 20, 0f, displacementY * 20);
        }
    }

    //Calculates displacements to determine velocities necessary to move the robot
    private void updateDisplacements()
    {
        /*Uses the current robot's angle to calculate the necessary displacements   
            to move in the given direction*/
        float angle = this.transform.eulerAngles.y;
        if (angle >= 0 && angle < 90)
        {
            displacementX = (90 - angle) * deltaTurn;
            displacementY = (0 - angle) * deltaTurn;
        }
        else if (angle >= 90 && angle < 180)
        {
            displacementX = (90 - angle) * deltaTurn;
            displacementY = -(180 - angle) * deltaTurn;
        }
        else if (angle >= 180 && angle < 270)
        {
            displacementX = -(270 - angle) * deltaTurn;
            displacementY = -(180 - angle) * deltaTurn;
        }
        else if (angle >= 270 && angle < 360)
        {
            displacementX = -(270 - angle) * deltaTurn;
            displacementY = (360 - angle) * deltaTurn;
        }
    }
}
