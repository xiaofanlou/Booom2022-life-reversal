using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject mappedClockMinute;
    public GameObject mappedClockHour;
    public GameObject mappedClock;
    public Transform targetPosition;
    public float clockMoveSpeed;
    private Animator animator;
    private int status;
    private bool triggered;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        status = mappedClockMinute.GetComponent<ClockMinute>().clockwise;
        triggered = false;
        animator.SetInteger("stat", status);
    }

    // Update is called once per frame
    void Update()
    {

        if (triggered && Vector2.Distance(mappedClock.transform.position, targetPosition.position) > 0.1)
        {
            mappedClock.transform.position =  Vector2.MoveTowards(mappedClock.transform.position, targetPosition.position, Time.deltaTime * clockMoveSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("playerInteraction") && !triggered)
        {
            status = 0 - status;
            animator.SetInteger("stat", status);

            mappedClockHour.GetComponent<ClockHour>().clockWise = status;

            mappedClockMinute.GetComponent<ClockMinute>().changeClockWise(status);
            ClipManager.Instance.Interact();

            triggered = true;
        }

    }
}
