using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SceneCanvas : MonoBehaviour
{
    private static SceneCanvas _main;
    public static SceneCanvas mainTransform
    {
        get
        {
            if (_main == null)
            {
                _main = FindObjectOfType<SceneCanvas>();
            }
            return _main;
        }
    }
    public virtual Transform getMainCanvasTransform(bool isEvent = false) //Muitten classien käyttöön getteri pääcanvaksesta. Maincanvas static, joten niitä on scenessä vain yksi.
    {
        return mainTransform.transform;
    }
}
