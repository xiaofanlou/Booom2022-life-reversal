using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AwardCount : MonoBehaviour
{
    private int count;
    public GameObject portal;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        EventManager.playerGainLog += CountAdded;

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = count.ToString();
        if (count == 3 && !portal.activeInHierarchy)
        {
            portal.SetActive(true);
        }
    }


    private void CountAdded(GameObject target)
    {   count++;
       
    }
}
