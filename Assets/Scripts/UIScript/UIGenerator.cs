using UnityEngine;

public class UIGenerator : MonoBehaviour
{
    #region Fields
    public string resourceToLoad;
    #endregion
    public void instantiateUIObject()
    {
        GameObject go = Instantiate(Resources.Load<GameObject>(resourceToLoad));
        go.transform.SetParent(MainCanvas.mainCanvas.transform);
        go.transform.localPosition = Vector3.zero;
    }

}
