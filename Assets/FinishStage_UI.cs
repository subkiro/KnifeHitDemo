using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class FinishStage_UI : MonoBehaviour
{
    [SerializeField] TMP_Text knifes;
    [SerializeField] TMP_Text stage;
    [SerializeField] Button NextButton;

    // Update is called once per frame
    private void Start()
    {
        NextButton.onClick.AddListener(Continue);
    }
    public void Init()
    {

        this.knifes.text = ScoreSystem._instance.currentScore.ToString();
        this.stage.text = ScoreSystem._instance.currentStage.ToString();

    }

    void Continue()
    {
        
        GetComponent<UIWindowAnimations>().Hide(() => SendEvents());



    }

    public void SendEvents()
    {
        EventManagerController.instance.RoundStart(this);
        GetComponent<UIWindowAnimations>().OnHideTrigger();
    }


}
