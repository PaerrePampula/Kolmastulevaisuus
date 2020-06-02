
using UnityEngine;

[CreateAssetMenu(fileName = "Typetest", menuName = "Typetest")]
public class TypeTestingScriptable<T> : ScriptableObject
{
    public T something;
}
