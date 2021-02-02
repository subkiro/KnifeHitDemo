using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public int MaxHit = 8;
    public int stage = 0;

    [Header("Spawn Settings")]

    public int minKnifeSpawn = 1;
    public int minAppleSpawn = 1;
    public int maxKnifeSpawn = 3;
    public int maxAppleSpawn = 1;


    public bool vibrationOn = false;

    private void Awake()
    {
       
        instance = this;
    }


    private void Start() {
        Vibration.Init();
        vibrationOn = PlayerPrefs.GetInt("Vibration", 0) > 0;
        
        Menu.instance.ShowUI(2);
    }

    public void VibrationTrigger(bool isOn) {
        if (isOn)
        {
            PlayerPrefs.SetInt("Vibration", 1);
            vibrationOn = true;
            Vibration.VibratePeek();
            Debug.Log("test");
        }
        else {
            PlayerPrefs.SetInt("Vibration", 0);
            vibrationOn = false;
        }
    }
  
}
