using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameArea : MonoBehaviour
{

    public BlockBuilder blockBuilder;

    Vector2 nowBlockPos;
    int nowBlockIndex;
    public Vector2 areaSize;
    public Vector2 initPos;

    BlockModel nowControlledBlock;

    Dictionary<Vector2, GameObject> squareMap = new Dictionary<Vector2, GameObject>();

    public Transform leftBottom;
    public Transform RightBottom;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale.Set(blockBuilder.squareSize.x * areaSize.x, blockBuilder.squareSize.y, 1);
        StartGame();
        PutInBlock(blockBuilder.nowBlock, initPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartGame()
    {
       
        blockBuilder.BuildRandomBlock();
    }


    public void PutInBlock(BlockModel model, Vector2 position)
    {
        nowControlledBlock = model;
        foreach (GameObject square in model.squares)
        {
            square.transform.SetParent(transform);
        }
        MoveBlockTo(model, position);
    }


    public void NextBlock()
    {
        nowBlockIndex = 0;
        PutInBlock(blockBuilder.nowBlock, initPos);
        blockBuilder.nowBlock = null;
        blockBuilder.BuildRandomBlock();
        //isSwapedWithHolder = false;

    }


    public void MoveBlockTo(BlockModel model, Vector2 position)
    {
        bool isOver = false;
        /*if (CheckCollision(model, position, nowBlockIndex))
        {
            GameOver();
            isOver = true;

        }*/
        for (int i = 0; i < model.block.blockTransModels[nowBlockIndex].transCoords.Count; i++)
        {
            Vector2 squareCoord = position + model.block.blockTransModels[nowBlockIndex].transCoords[i];
            if (isOver && squareMap.ContainsKey(squareCoord))
            {
                continue;
            }
            else
            {
                model.squares[i].transform.localPosition = AreaPos2Local(squareCoord);
                model.squares[i].SetActive(squareCoord.y >= 0);
            }
        }
        nowBlockPos = position;
    }


    public Vector2 AreaPos2Local(Vector2 areaPos)
    {
        return new Vector2((areaPos.x + 0.5f) * blockBuilder.squareSize.x, -(0.5f + areaPos.y) * blockBuilder.squareSize.y);
    }

}
