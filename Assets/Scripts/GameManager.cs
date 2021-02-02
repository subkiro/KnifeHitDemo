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

    [Header("Settomgs")]

    public bool vibrationOn = false;
    public bool soundOn = false;
    [SerializeField] private AudioSource audioSource;
    private void Awake()
    {
       
        instance = this;
    }


    private void Start() {
        Vibration.Init();
        vibrationOn = PlayerPrefs.GetInt("Vibration", 0) > 0;
        soundOn = PlayerPrefs.GetInt("Sound", 1) > 0;
        SoundTrigger(soundOn);
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


    public void SoundTrigger(bool isOn)
    {
        if (isOn)
        {
            PlayerPrefs.SetInt("Sound", 1);
            soundOn = true;
            audioSource.enabled = soundOn;


        }
        else
        {
            PlayerPrefs.SetInt("Sound", 0);
            soundOn = false;
            audioSource.enabled = soundOn;

        }
    }

}
