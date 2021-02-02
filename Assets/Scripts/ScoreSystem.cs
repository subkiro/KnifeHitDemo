using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{

    public int totalScore;
    public int bonusScore=0;
    public int currentScore;
    public int totalStage;
    public int currentStage = 1;
    public int curretnBonus = 0;

    [SerializeField] TMP_Text CurrentScoreText;
    [SerializeField] TMP_Text TotalScoreText;
    [SerializeField] TMP_Text BonusScoreRext;

    public  static ScoreSystem _instance;
    void Awake() => _instance =this;

    private void Start()
    {
        //reset totalscore
        //PlayerPrefs.SetInt("TotalScore", 0);
        currentScore = 0;
        currentStage = 1;
        curretnBonus = 0;

        totalScore = PlayerPrefs.GetInt("TotalScore");
        totalStage = PlayerPrefs.GetInt("TotalStage");
        bonusScore = PlayerPrefs.GetInt("Bonus");

        _instance.TotalScoreText.SetText(totalScore.ToString());
        _instance.BonusScoreRext.SetText(bonusScore.ToString());
        
        EventManagerController.instance.HitWoodAction += AddPoints;
        EventManagerController.instance.BonusHitAction += AddBonusPoints;
        EventManagerController.instance.RoundFinishedAction += AddStage;

    }

    private void OnDisable()
    {
        EventManagerController.instance.HitWoodAction -= AddPoints;
        EventManagerController.instance.BonusHitAction -= AddBonusPoints;
        EventManagerController.instance.RoundFinishedAction -= AddStage;
    }
    public void AddPoints() {

        ++_instance.currentScore;
       _instance.CurrentScoreText.SetText(_instance.currentScore.ToString());

        if (_instance.currentScore > _instance.totalScore) {
            _instance.totalScore = _instance.currentScore;
            _instance.TotalScoreText.SetText(_instance.totalScore.ToString());
            PlayerPrefs.SetInt("TotalScore", _instance.totalScore);
        }

     
    }
    public void AddBonusPoints(Object sender)
    {
        ++curretnBonus;
        ++_instance.bonusScore;
        _instance.BonusScoreRext.SetText(_instance.bonusScore.ToString());
        PlayerPrefs.SetInt("Bonus", _instance.bonusScore);
    }

    public void ResetCurrentStage() {
        _instance.currentStage=1;

        _instance.currentScore = 0;
        _instance.CurrentScoreText.SetText(_instance.currentScore.ToString());

        _instance.curretnBonus = 0;

    }

    
    public void AddStage()
    {
        ++_instance.currentStage;
      //  _instance.CurrentScoreText.SetText(_instance.currentScore.ToString());

        if (_instance.currentStage > _instance.totalStage)
        {
            _instance.totalStage = _instance.currentStage;
         //   _instance.TotalScoreText.SetText(_instance.totalScore.ToString());
            PlayerPrefs.SetInt("TotalStage", _instance.totalStage);
        }
    }

    public int GetTotalScore() { 
        return _instance.totalScore;
    }
    public int GetTotalBonus()
    {
        return _instance.bonusScore;
    }
    public int GetTotalStage()
    {
        return _instance.totalStage;
    }

}
