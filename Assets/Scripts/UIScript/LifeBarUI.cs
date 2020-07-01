using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBarUI : MonoBehaviour
{
    [SerializeField]
    List<Animator> hearts = new List<Animator>();
    // Start is called before the first frame update

    private void OnEnable()
    {
        GameStateHandler.OnDamage += VizualizeDamage;
    }
    private void OnDisable()
    {
        GameStateHandler.OnDamage -= VizualizeDamage;
    }
    void VizualizeDamage(int times)
    {
        GetComponent<Animator>().SetTrigger("HeartBreak");
        if (times > hearts.Count)
        {
            times = hearts.Count;
        }
        for (int i = 0; i < times; i++)
        {
            hearts[hearts.Count - 1 - i].SetTrigger("HeartBreak");
            hearts.RemoveAt(hearts.Count - 1 - i);
        }
        
    }
}
