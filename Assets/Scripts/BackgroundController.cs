using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public GameObject sprite;
    public int width;
    public int height;
    public Transform leftBottomCorner;
    public Transform rightTopCorner;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 startPos = leftBottomCorner.position;
        Vector2 endPos = rightTopCorner.position;

        int i = 0;
        int j = 0;
        for (float x = startPos.x; x <= endPos.x ; x += width, i++)
        {   for (float y = startPos.y; y <= endPos.y ; y += height, j++)
            {
                GameObject pic = Instantiate(sprite, new Vector2(x, y), new Quaternion());
                pic.transform.parent = transform;
                
            }

        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
