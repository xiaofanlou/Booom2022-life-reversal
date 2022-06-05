using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockTrans
{
    public List<Vector2> transCoords;

    public BlockTrans() {
        transCoords = new List<Vector2>();
    }

}
