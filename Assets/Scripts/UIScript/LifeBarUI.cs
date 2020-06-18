using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBarUI : MonoBehaviour
{
    [SerializeField]
    List<Animator> hearts = new List<Animator>();
    // Start is called before the first frame update
    void Start()
    {
        GameStateHandler.OnDamage += VizualizeDamage;
    }

    // Update is called once per frame
    void Update()
    {
        
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
