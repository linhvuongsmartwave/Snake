using UnityEngine;
using DG.Tweening;

public class UiPanelDotween : MonoBehaviour
{
    public float fadeTime = 0.5f;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform rectTransform;
    public GameObject panelblack;

    private void Awake()
    {
        panelblack.SetActive(false);
    }
    public void PanelFadeIn()
    {
        //AudioManager.Instance.AudioOpen();
        canvasGroup.alpha = 0;
        rectTransform.transform.localPosition = new Vector3(0, -1000f, 0);
        rectTransform.DOAnchorPos(new Vector2(0, 0), fadeTime, false).SetEase(Ease.InOutBack);
        canvasGroup.DOFade(1, fadeTime);
        panelblack.SetActive(true);

    }

    public void PanelFadeOut()
    {
        //AudioManager.Instance.AudioButtonClick();
        canvasGroup.alpha = 1;
        rectTransform.transform.localPosition = new Vector3(0, 0, 0);
        rectTransform.DOAnchorPos(new Vector2(0, -2000f), fadeTime, false).SetEase(Ease.InOutBack);
     
        canvasGroup.DOFade(1, fadeTime);
        panelblack.SetActive(false);

    }
}
