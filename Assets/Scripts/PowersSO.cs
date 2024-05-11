using AYellowpaper.SerializedCollections;
using UnityEngine;

[CreateAssetMenu(fileName = "Powers", menuName = "Blob Odyssey/Powers")]
public class PowersSO : ScriptableObject
{
    public SerializedDictionary<EmotionType, string> Descriptions;
}