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
    [SerializeField] TMP_Text bonus;


    [SerializeField] Button restartButton;
    [SerializeField] Button navButton;
    // Update is called once per frame
    private void Start()
    {
        restartButton.onClick.AddListener(Restart);
        navButton.onClick.AddListener(GoTo);
    }
    public void Init()
    {
        
        this.knifes.text = ScoreSystem._instance.currentScore.ToString();
        this.stage.text = ScoreSystem._instance.currentStage.ToString();
        this.bonus.text = ScoreSystem._instance.bonusScore.ToString();
    }

    void Restart() {
        ScoreSystem._instance.ResetCurrentStage();
        GetComponent<UIWindowAnimations>().Hide(()=> SendEvents());
        


    }

    public void SendEvents() {
        EventManagerController.instance.RoundStart(this);
        GetComponent<UIWindowAnimations>().OnHideTrigger();

    }

    public void GoTo() {
        GetComponent<UIWindowAnimations>().Hide(()=> { Menu.instance.ShowUI(2); });
    }

}
