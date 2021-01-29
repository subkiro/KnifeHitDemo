using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public int MaxHit = 8;
    public int stage = 0;

    private void Awake()
    {
       
        instance = this;
    }



   
}
