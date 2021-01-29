using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class RestrtUI : MonoBehaviour
{
    [SerializeField] TMP_Text knifes;
    [SerializeField] TMP_Text stage;
    [SerializeField] Button restartButton;

    // Update is called once per frame
    private void Start()
    {
        restartButton.onClick.AddListener(Restart);
    }
    public void Init()
    {
        this.knifes.text = ScoreSystem._instance.currentScore.ToString();
        this.stage.text = ScoreSystem._instance.currentStage.ToString();

    }

    void Restart() {
        ScoreSystem._instance.ResetCurrentStage();
        EventManagerController.instance.RoundStart();
        GetComponent<UIWindowAnimations>().Hide();
        Init();


    }
    private void OnEnable()
    {
        Init();
    }

}
