using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void PlayerGain(GameObject gameObject);
    public static event PlayerGain playerGainLog;

    public static void PlayerGained(GameObject gameObject)
    {
        playerGainLog?.Invoke(gameObject);
    }
}
