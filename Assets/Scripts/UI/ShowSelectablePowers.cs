using UnityEngine;

public class ShowSelectablePowers : MonoBehaviour
{
    [SerializeField] private GameObject _powerPrefab;

    private void Awake()
    {
        foreach (var emotion in GameManager.Instance.Blob.StrongestEmotions)
        {
            Instantiate(_powerPrefab, transform).GetComponent<PowerPopup>().Init(emotion);
        }
    }
}