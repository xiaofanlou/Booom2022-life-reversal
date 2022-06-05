using UnityEngine;

public class ClockMinute : MonoBehaviour
{
    public int clockwise;
    public int rotateRate;

    public Transform platform;

    public Transform platform1;

    public Transform platform2;

   
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        Quaternion currentRotation = transform.rotation;

        currentRotation.eulerAngles += new Vector3(0, 0, clockwise * rotateRate * Time.deltaTime);

        transform.rotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y, currentRotation.eulerAngles.z);


        Quaternion platformRotation  = platform.rotation;
        platformRotation.eulerAngles -= new Vector3(0, 0, clockwise * rotateRate * Time.deltaTime);

        Quaternion platformTarget = Quaternion.Euler(platformRotation.eulerAngles.x, platformRotation.eulerAngles.y, platformRotation.eulerAngles.z);

        platform.rotation = platformTarget;
        platform1.rotation = platformTarget;
        platform2.rotation = platformTarget;
    }
    


    public void changeClockWise(int value) {
        clockwise = value;
    }
}
