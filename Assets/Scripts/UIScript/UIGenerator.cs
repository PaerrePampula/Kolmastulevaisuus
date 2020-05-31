using UnityEngine;

public class UIGenerator : MonoBehaviour //Luo esim näppäimen painamisesta ui elementtejä.
{
    #region Fields
    [SerializeField] 
    string resourceToLoad;
    #endregion
    public void instantiateUIObject()
    {
        GameObject go = Instantiate(Resources.Load<GameObject>(resourceToLoad));
        go.transform.SetParent(MainCanvas.mainCanvas.transform);
        go.transform.localPosition = Vector3.zero;
    }

}
