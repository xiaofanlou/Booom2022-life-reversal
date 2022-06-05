using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "NewBlock", menuName = "Tetris/BlockBase")]
public class BaseBlock : ScriptableObject
{
    public Vector2 blockSize;
    public List<BlockTrans> blockTransModels;
    
    

    public void addTransCoords(BlockTrans trans)
    {
        blockTransModels.Add(trans);
    }
}
