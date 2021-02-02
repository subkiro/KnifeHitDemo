using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Menu : MonoBehaviour
{
    public static Menu instance;



    [SerializeField] UIWindowAnimations[] UI_Windows; 
    // Start is called before the first frame update
    private UIWindowAnimations ActiveUI;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
    }
    public void ShowUI(int indexToShow) {
        if (ActiveUI != null)
        {
            ActiveUI.Hide(() => ShowUIAfterCheck(indexToShow));
        }
        else {
            ShowUIAfterCheck(indexToShow);
        }
    }
    private void ShowUIAfterCheck(int _index) {

        this.ActiveUI = UI_Windows[_index];
        this.ActiveUI.gameObject.SetActive(true);
        this.ActiveUI.Show();
     
    }



}
