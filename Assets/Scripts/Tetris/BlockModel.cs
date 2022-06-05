using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockModel : MonoBehaviour
{
    public List<GameObject> squares = new List<GameObject>();
    public BaseBlock block;

    public void initBlock(BaseBlock block)
    {
        this.block = block;
    }

}
