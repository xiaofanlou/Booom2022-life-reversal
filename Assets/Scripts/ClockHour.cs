using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockHour : MonoBehaviour
{

    public float rotateRate;

    public int clockWise;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion currentRotation = transform.rotation;

        currentRotation.eulerAngles += new Vector3(0, 0, clockWise * rotateRate * Time.deltaTime);

        transform.rotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, currentRotation.eulerAngles.z);


    }

    public void setClockWise(int value) {
        clockWise = 1;
    }
}
