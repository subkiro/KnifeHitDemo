
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
[RequireComponent(typeof(CanvasGroup))]
public class UIWindowAnimations : MonoBehaviour
{
    public void Show(string id = "show")
    {
        this.transform.GetComponent<RectTransform>().DOAnchorPosX(0, 0);
        this.GetComponent<CanvasGroup>().alpha = 1f;
        foreach (Transform t in transform)
        {
            DOTweenAnimation a = t.GetComponent<DOTweenAnimation>();
            if (a == null) return;
            a.DORestartById(id);

        }
    }

    public void Hide(UnityAction callback = null)
    {
        this.transform.GetComponent<RectTransform>().DOAnchorPosX(-1f, 0);
        this.GetComponent<CanvasGroup>().DOFade(0, 0.5f).OnComplete(()=>callback());

        
    }


    public void DeactivateSelf() {
        this.gameObject.SetActive(false);
    }



}
