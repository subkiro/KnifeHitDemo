using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StageController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Image[] bullets;
    [SerializeField] TMP_Text stageNumText;
    public int stageBullet = 0;
    public static StageController instance;

    void Awake() {
        instance = this;
    }
    // Update is called once per frame
    void Start()
    {
        Init();
    }

    private void Init()
    {
        int stage = ScoreSystem._instance.currentStage;
        
        stageNumText.text = "STAGE " + stage.ToString();
        SetBullets(stage-1);
    }

    private void SetBullets (int stage){
        
        Debug.Log("Stage is:  "+stage);
        for (int i = 0; i < bullets.Length; i++)
        {
            if (i <= (stage)% bullets.Length)
            {
                bullets[i].color = Color.red;
            }
            else {
                bullets[i].color = Color.white;
            }
        }
        stageBullet = (stage % bullets.Length);
    }

    
    private void OnEnable()
    {
        EventManagerController.instance.RoundStartAction += Init;
    }
    private void OnDisable()
    {
        EventManagerController.instance.RoundStartAction -= Init;
    }
}
