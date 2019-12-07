using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intake : MonoBehaviour
{
    private Rigidbody body;

    public Vector3 savedAngle;

    public int position;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();

        savedAngle = this.transform.localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localEulerAngles = new Vector3(savedAngle.x, this.transform.localEulerAngles.y, savedAngle.z);

        //Resetting physics values
        body.velocity = Vector3.zero;
        body.angularVelocity = Vector3.zero;

        if (Input.GetKey(KeyCode.I) && position == 0)
		{
            body.AddTorque(transform.up * 1000);
        }
        else if (Input.GetKey(KeyCode.I) && position == 1)
        {
            body.AddTorque(-transform.up * 1000);
        }
	}
}
