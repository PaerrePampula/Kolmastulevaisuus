﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persistence : MonoBehaviour
{
    #region MonobehaviourDefaults

    private void Start() //Tämä objekti ei katoa scenen vaihdossa
    {
        DontDestroyOnLoad(gameObject);
    }

    #endregion
}
