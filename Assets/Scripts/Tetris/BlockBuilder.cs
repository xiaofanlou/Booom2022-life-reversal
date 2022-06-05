using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBuilder : MonoBehaviour
{
    public List<BaseBlock> blockList = new List<BaseBlock>();
    public GameObject squarePrefab;
    public Vector2 squareSize;
    [HideInInspector]
    public BlockModel nowBlock;
    public List<Sprite> blockSpriteList = new List<Sprite>();
    public Sprite punishedSprite;

    public void BuildRandomBlock()
    {
        if (nowBlock != null)
        {
            DestroyBlock(nowBlock);
        }

        BaseBlock inBuildingBlock = blockList[Random.Range(0, blockList.Count)];

        nowBlock = new BlockModel();
        nowBlock.initBlock(inBuildingBlock);
        Sprite sprite = blockSpriteList[Random.Range(0, blockSpriteList.Count)];

        foreach (Vector2 vector in inBuildingBlock.blockTransModels[0].transCoords)
        {
            GameObject newSquare = Instantiate(squarePrefab);
            newSquare.transform.SetParent(transform);
            newSquare.transform.localPosition = new Vector3(squareSize.x * vector.x, -squareSize.x * vector.y);
            //newSquare.GetComponent<RectTransform>().sizeDelta = squareSize;
            newSquare.transform.localScale = new Vector3(1f, 1f, 1f);
            newSquare.transform.GetComponent<SpriteRenderer>().sprite = sprite;
            nowBlock.squares.Add(newSquare);
        }
    }


    public void DestroyBlock(BlockModel block)
    {
        foreach (GameObject square in block.squares)
        {
            Destroy(square);
        }
    }
}
