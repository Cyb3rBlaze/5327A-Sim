using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;

public class Auton : MonoBehaviour
{
    public double ticker;
    public bool startFlag = false;
    public bool processData = false;
    public List<int> keys = new List<int>();

    public static bool reset = false;
    public bool frame2 = false;
    public double prev;

    public Text secondsLeft;
    public int secondsPassed;

    public double timeItTakesToGo6Tiles;
    public double timeItTakesToTurn90;

    //Sets the initial position of the robot
    public void startAuton()
    {
        GameObject robot = GameObject.Find("Robot");
        Rigidbody body = robot.GetComponent<Rigidbody>();
        body.transform.position = new Vector3(-1.022873f, 0.2060283f, -1.289383f);
        body.transform.eulerAngles = new Vector3(0f, 0f, 0f);

        ticker = Time.time;
        secondsPassed = 0;
        startFlag = true;
        reset = true;
        prev = Time.time;

        timeItTakesToGo6Tiles = 2000;
        timeItTakesToTurn90 = 2000;
    }

    //Starts the recording process
        //keys: 1 -> up, 2 -> down, 3 -> left, 4 -> right
    public void Update()
    {
        if (reset == true)
        {
            if (frame2 == true) {
                reset = false;
                frame2 = false;
            }
            else
            {
                frame2 = true;
            }
        }
        if (startFlag)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                keys.Add(1);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                keys.Add(2);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                keys.Add(3);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                keys.Add(4);
            }
            else
            {
                keys.Add(5);
            }

            //Stops the auton period
            if (Time.time - ticker > 15f)
            {
                print("Simulation Finished!!");
                startFlag = false;
                processData = true;
            }

            if (Time.time - prev > 1f)
            {
                secondsPassed += 1;
                print("1 second passed");
                secondsLeft.text = "Seconds Left: " + (15-secondsPassed);
                prev = Time.time;
            }
        }
        if (processData)
        {
            secondsLeft.text = "Seconds Left: 15";

            int currMove = 5;
            double currCount = 0;
            List<string> moves = new List<string>();

            moves.Add("/*==Generated Auton==*/");

            for (int i = 0; i < keys.Count; i++)
            {
                if (keys[i] != currMove)
                {
                    if (currMove == 1)
                    {
                        moves.Add("Left.spin(directionType::fwd, velocity, velocityUnits::rpm);");
                        moves.Add("Right.spin(directionType::fwd, -velocity, velocityUnits::rpm);");
                        moves.Add("vex::task::sleep(" + ((int)((currCount/104) * timeItTakesToGo6Tiles)) + ");");
                        moves.Add("Left.stop(vex::brakeType::brake)");
                        moves.Add("Right.stop(vex::brakeType::brake)");
                        moves.Add("/*============================*/");
                    }
                    else if (currMove == 2)
                    {
                        moves.Add("Left.spin(directionType::rev, velocity, velocityUnits::rpm);");
                        moves.Add("Right.spin(directionType::rev, -velocity, velocityUnits::rpm);");
                        moves.Add("vex::task::sleep(" + ((int)((currCount / 104) * timeItTakesToGo6Tiles)) + ");");
                        moves.Add("Left.stop(vex::brakeType::brake)");
                        moves.Add("Right.stop(vex::brakeType::brake)");
                        moves.Add("/*============================*/");
                    }
                    else if (currMove == 3)
                    {
                        moves.Add("Left.spin(directionType::rev, turn_velocity, velocityUnits::rpm);");
                        moves.Add("Right.spin(directionType::rev, turn_velocity, velocityUnits::rpm);");
                        moves.Add("vex::task::sleep(" + ((int)((currCount / 43) * timeItTakesToTurn90)) + ");");
                        moves.Add("Left.stop(vex::brakeType::brake)");
                        moves.Add("Right.stop(vex::brakeType::brake)");
                        moves.Add("/*============================*/");
                    }
                    else if (currMove == 4)
                    {
                        moves.Add("Left.spin(directionType::fwd, turn_velocity, velocityUnits::rpm);");
                        moves.Add("Right.spin(directionType::fwd, turn_velocity, velocityUnits::rpm);");
                        moves.Add("vex::task::sleep(" + ((int)((currCount / 43) * timeItTakesToTurn90)) + ");");
                        moves.Add("Left.stop(vex::brakeType::brake)");
                        moves.Add("Right.stop(vex::brakeType::brake)");
                        moves.Add("/*============================*/");
                    }
                    else
                    {
                        moves.Add("vex::task::sleep(" + ((int)((currCount/30)*1000) + ");"));
                        moves.Add("/*============================*/");
                    }

                    currMove = keys[i];
                    currCount = 0;
                }
                currCount += 1;
            }

            string path = "Assets/code.c++";
            StreamWriter writer = new StreamWriter(path, true);

            for (int i = 0; i < moves.Count; i++)
            {
                writer.WriteLine(moves[i]);
            }
            writer.Close();

            processData = false;
        }
    }
}
