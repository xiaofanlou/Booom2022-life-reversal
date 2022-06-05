using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform targetTransform;
    public float smoothing;
    public int diffy;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPositon = targetTransform.position + new Vector3(0, diffy, 0);
        if (GameManager.Instance.IsGravityInversed())
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        if (targetPositon != transform.position) {
            transform.position = Vector3.Lerp(transform.position, targetPositon, smoothing);
        }

    }
}
