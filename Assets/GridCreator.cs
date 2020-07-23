using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridCreator : MonoBehaviour
{
    private RectTransform rectTransform;
    private GridLayoutGroup gridLayoutGroup;
    private Texture2D verticalLineTexture;
    private Texture2D horizontalLineTexture;
    private void OnEnable()
    {
        ExpenseView.onEventComplete += createGrids;
        initializeForGridding();


        GameObject go = new GameObject();
        go.transform.SetParent(transform.parent);
        go.transform.position = transform.position;
        go.transform.localPosition = Vector3.zero;

        setTextureAndColorToObject(go, verticalLineTexture, Color.black);


    }
    private void OnDestroy()
    {
        ExpenseView.onEventComplete -= createGrids;
    }
    void initializeForGridding()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
        verticalLineTexture = new Texture2D(3, Mathf.FloorToInt(rectTransform.rect.height));
        horizontalLineTexture = new Texture2D(Mathf.FloorToInt(rectTransform.rect.width), 3);
    }

    void setTextureAndColorToObject(GameObject newGameObject,Texture2D texture, Color32 color)
    {
        RawImage imageRaw = newGameObject.AddComponent<RawImage>();
        imageRaw.texture = texture;
        imageRaw.color = color;
        imageRaw.SetNativeSize();
    }
    void createHorizontalLine(Transform target)
    {
        GameObject singleLine = new GameObject();
        setTextureAndColorToObject(singleLine, horizontalLineTexture, Color.black);
        singleLine.transform.SetParent(target);
        singleLine.transform.localPosition = new Vector3(109, -gridLayoutGroup.cellSize.y / 2);

    }
    void createGrids()
    {
        for (int i = 0; i < transform.childCount; i += 2)
        {
            createHorizontalLine(transform.GetChild(i));
        }
    }
}
