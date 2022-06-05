using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(BaseBlock))]
public class BaseBlockEditor : Editor
{

    BaseBlock targetBlock { get { return target as BaseBlock; } }
    ReorderableList blockFormReorderableList;

    public override void OnInspectorGUI()
    {
        EditorGUI.BeginChangeCheck();
        Vector2 tempSize = targetBlock.blockSize;
        targetBlock.blockSize = EditorGUILayout.Vector2Field("方块尺寸:", targetBlock.blockSize);
        if (tempSize != targetBlock.blockSize) //当方块尺寸变更时,删除超出方块尺寸范围的数据
        {

            List<Vector2> tempList = new List<Vector2>();

            foreach (BlockTrans trans in targetBlock.blockTransModels)
            {
                tempList.AddRange(trans.transCoords);
                foreach (Vector2 vec in tempList)
                {
                    if (vec.x >= targetBlock.blockSize.x || vec.y >= targetBlock.blockSize.y)
                    {
                        trans.transCoords.Remove(vec);
                    }
                }

            }
        }

        GUILayout.Space(5);

        blockFormReorderableList.DoLayoutList();

        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(target);
        }
    }

    private void OnEnable()
    {
        Color lastColor = GUI.backgroundColor;
        blockFormReorderableList = new ReorderableList(targetBlock.blockTransModels, typeof(BlockTrans));
       
        blockFormReorderableList.drawHeaderCallback = (Rect rect) => { GUI.Label(rect, "方块形状数据(按顺时针排序):"); };
        blockFormReorderableList.elementHeightCallback = (int index) => { return 5f + targetBlock.blockSize.y * EditorGUIUtility.singleLineHeight; };
        blockFormReorderableList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            for (int y = 0; y < targetBlock.blockSize.y; y++)
            {
                for (int x = 0; x < targetBlock.blockSize.x; x++)
                {

                    Vector2 nVec = new Vector2(x, y);
                    GUI.backgroundColor = targetBlock.blockTransModels[index] != null && targetBlock.blockTransModels[index].transCoords.Contains(nVec) ? Color.green : Color.gray;
                    if (GUI.Button(new Rect(rect.x + x * EditorGUIUtility.singleLineHeight,
                        rect.y + y * EditorGUIUtility.singleLineHeight, EditorGUIUtility.singleLineHeight, EditorGUIUtility.singleLineHeight), GUIContent.none))
                    {
                        if (targetBlock.blockTransModels[index] != null && targetBlock.blockTransModels[index].transCoords.Contains(nVec))
                        {
                            Debug.Log(x + "," + y + "," + targetBlock.blockTransModels[index]);

                            targetBlock.blockTransModels[index].transCoords.Remove(nVec);
                        }
                        else
                        {
                            Debug.Log(x + "," + y + "," + targetBlock.blockTransModels[index]);
                            
                            targetBlock.blockTransModels[index].transCoords.Add(nVec);
                        }
                        EditorUtility.SetDirty(target);
                    }
                }
            }
            GUI.backgroundColor = lastColor;
        };
    }
}
