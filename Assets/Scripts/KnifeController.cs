using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KnifeController : MonoBehaviour
{
    [SerializeField] private GameObject KnifePrefab;
    [SerializeField] private GameObject KnifeTogglePrefab;

    [SerializeField] private Transform KnifeContainer;
    [SerializeField] private Transform KnifeToggleContainer;
    
    [SerializeField] private Button TapButton;
 
    private GameObject ActiveKnife;
    private bool isReady = false;
    public int knifeCounter;


    public static KnifeController instance;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        TapButton.onClick.AddListener(OnTapPeressdPressed);
      
        
    }

    public void OnTapPeressdPressed() {

        if (!isReady) return; 
        ActiveKnife?.GetComponent<Knife>().Throw();
       
    }

    public void InitKnife()
    {
        ActiveKnife = Instantiate(KnifePrefab, KnifeContainer);
        ActiveKnife.name = "Knife_" + knifeCounter;
       
    }

    public void InitToggleKnifes()
    {
        GameObject toggleObj = Instantiate(KnifeTogglePrefab, KnifeToggleContainer);

    }

    public void ClearKnifesToggles() {
        foreach(Transform t in KnifeToggleContainer)
        {
            Destroy(t.gameObject);
        }
        foreach (Transform t in KnifeContainer)
        {
            Destroy(t.gameObject);
        }


    }

    public void DecreaseKnifes() {

        int countertoDelete = KnifeToggleContainer.childCount - knifeCounter;

        if(countertoDelete < KnifeToggleContainer.childCount) { 
            KnifeToggleContainer.GetChild(countertoDelete).GetComponent<Toggle>().isOn = false;
        
            
            --knifeCounter;
            if (knifeCounter != 0) {
                InitKnife();
            }
          
        }
        if (knifeCounter == 0) {
            isReady = false;
        }

    }

    public void OnNewStage() {


        ClearKnifesToggles();
        TapButton.interactable = true;
        InitKnife();

        knifeCounter = GameManager.instance.MaxHit - (StageController.instance.stageBullet) + Random.Range(-2,2);

        for (int i = 0; i < knifeCounter; i++)
        {
            InitToggleKnifes();
        }

        isReady = true;
    }

    private void InteractionDisable() {
        TapButton.interactable = false;
    }

    private void OnEnable()
    {
        EventManagerController.instance.RoundStartAction += OnNewStage;
        EventManagerController.instance.LostAction += InteractionDisable;
        EventManagerController.instance.HitWoodAction += DecreaseKnifes ;

    }

    private void OnDisable()
    {
        EventManagerController.instance.RoundStartAction -= OnNewStage;
        EventManagerController.instance.LostAction -= InteractionDisable;
        EventManagerController.instance.HitWoodAction += DecreaseKnifes;

    }
}
