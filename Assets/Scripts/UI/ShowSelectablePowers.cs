using UnityEngine;

public class ShowSelectablePowers : MonoBehaviour
{
    [SerializeField] private GameObject _powerPrefab;

    private void Awake()
    {
        PowerPopup.OnEmotionSelect += PowerPopup_OnEmotionSelect;

        foreach (var emotion in GameManager.Instance.Blob.StrongestEmotions)
        {
            Instantiate(_powerPrefab).GetComponent<PowerPopup>().Init(emotion);
        }
    }

    private void OnDestroy()
    {
        PowerPopup.OnEmotionSelect -= PowerPopup_OnEmotionSelect;
    }

    private void PowerPopup_OnEmotionSelect(EmotionType emotion)
    {
        gameObject.SetActive(false);
    }
}