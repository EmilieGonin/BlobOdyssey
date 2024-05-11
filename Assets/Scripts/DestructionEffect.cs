using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WhiteScreenEffect : MonoBehaviour
{
    [SerializeField] private float _fadeDuration = 2f;

    [SerializeField] private CanvasGroup _canvasGroup;

    private void Awake()
    {
        DestroyAction.OnActivate += ActivateWhiteScreen;
    }

    private void OnDestroy()
    {
        DestroyAction.OnActivate -= ActivateWhiteScreen;
    }

    private IEnumerator WhitePanelEffect()
    {
        _canvasGroup.alpha = 1f;
        float elapsedTime = 0f;
        while (elapsedTime < _fadeDuration)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
            _canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / _fadeDuration);
        }
        _canvasGroup.alpha = 0f;
    }

    public void ActivateWhiteScreen() => StartCoroutine(WhitePanelEffect());
}
