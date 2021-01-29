using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EffectVfx : MonoBehaviour
{
    [SerializeField] private SpriteRenderer flashBackground;
    [SerializeField] private SpriteRenderer crashEffect;
    [SerializeField] private SpriteRenderer hitEffect;
    public void FlashBackground() {
        CrashEffect();
        flashBackground.gameObject.SetActive(true);
        flashBackground.DOFade(0, 0.3f).From(.8f).SetEase(Ease.Flash).OnComplete(()=>flashBackground.gameObject.SetActive(false));
    }
    public void CrashEffect()
    {
        crashEffect.gameObject.SetActive(true);
        crashEffect.transform.DOPunchScale(Vector3.one*0.3f, 0.1f,2);
        crashEffect.DOFade(0, 0.2f).From(.5f).SetEase(Ease.Flash).OnComplete(() => crashEffect.gameObject.SetActive(false));
        
    }
    public void HitEffect()
    {
        CrashEffect();
        hitEffect.gameObject.SetActive(true);
        hitEffect.transform.DOPunchScale(Vector3.one*0.1f, 0.2f, 1);
        hitEffect.DOFade(0, 0.2f).From(.5f).SetEase(Ease.Flash).OnComplete(() => hitEffect.gameObject.SetActive(false));

    }


   

    private void Start()
    {
        EventManagerController.instance.LostAction += FlashBackground;
        EventManagerController.instance.BonusHitAction += HitEffect;
    }

    private void OnDisable()
    {
        EventManagerController.instance.LostAction -= FlashBackground;
        EventManagerController.instance.BonusHitAction -= HitEffect;
    }
}
