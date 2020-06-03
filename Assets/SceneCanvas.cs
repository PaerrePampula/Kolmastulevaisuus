using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SceneCanvas : MonoBehaviour
{
    private static SceneCanvas _main;
    private static SceneCanvas mainTransform
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
    public static Transform getMainCanvasTransform() //Muitten classien käyttöön getteri pääcanvaksesta. Maincanvas static, joten niitä on scenessä vain yksi.
    {
        return mainTransform.transform;
    }
}
