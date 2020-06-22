using UnityEngine;

public class UIGenerator : MonoBehaviour //Luo esim näppäimen painamisesta ui elementtejä.
{
    #region Fields
    [SerializeField] 
    string resourceToLoad;
    Transform instantiated;
    #endregion
    public void instantiateUIObject()
    {
        if (instantiated == null)
        {
            GameObject go = Instantiate(Resources.Load<GameObject>(resourceToLoad));
            go.transform.SetParent(SceneCanvas.mainTransform.getMainCanvasTransform());
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = new Vector3(1, 1, 1);
            instantiated = go.transform;
        }

    }
    
    public Transform getInstantiated()
    {
        return instantiated;
    }

}
