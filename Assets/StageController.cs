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
    void Start()
    {
       

    }

    // Update is called once per frame
   

    private void Init()
    {
        int stage = ScoreSystem._instance.currentStage;
        ++stage;
        stageNumText.text = "STAGE " + stage.ToString();
        SetBullets(stage);
    }

    private void SetBullets (int stage){
        
        Debug.Log(stage);
        for (int i = 0; i < bullets.Length; i++)
        {
            if (i < stage % bullets.Length)
            {
                bullets[i].color = Color.red;
            }
            else {
                bullets[i].color = Color.white;
            }
        }
        
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
