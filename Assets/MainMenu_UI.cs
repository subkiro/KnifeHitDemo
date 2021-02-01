using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MainMenu_UI : MonoBehaviour
{
    [SerializeField] TMP_Text knifes;
    [SerializeField] TMP_Text stage;
    [SerializeField] TMP_Text BONUS;
    [SerializeField] Button NextButton;

    // Update is called once per frame
    private void Start()
    {
        NextButton.onClick.AddListener(Continue);
    }
    public void Init()
    {

        this.knifes.text = ScoreSystem._instance.GetTotalScore().ToString();
        this.stage.text = ScoreSystem._instance.GetTotalStage().ToString();
        this.BONUS.text = ScoreSystem._instance.GetTotalBonus().ToString();
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
