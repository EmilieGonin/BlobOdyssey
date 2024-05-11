using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UpdateEmotionUI : MonoBehaviour
{
    [SerializeField] private List<ProgressBar> _emotionBars;

    private void Awake()
    {
        EmotionType[] emotions = Enum.GetValues(typeof(EmotionType)) as EmotionType[];

        for (int i = 0; i < _emotionBars.Count; i++)
        {
            _emotionBars[i].gameObject.GetComponent<Image>().color = EmotionPalette.GetColor(emotions[i]);
        }
    }

    private void Update()
    {
        for (int i = 0; i < _emotionBars.Count; i++)
        {
            _emotionBars[i].UpdateValue(GameManager.Instance.Blob.Emotions.ElementAt(i).Value.Value / 100);
        }
    }
}