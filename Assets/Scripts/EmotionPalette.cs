using UnityEngine;

public static class EmotionPalette
{
    public static Color GetColor(EmotionType emotion)
    {
        return emotion switch
        {
            EmotionType.Joy => new Color32(255, 231, 65, 255),
            EmotionType.Sadness => new Color32(93, 178, 255, 255),
            EmotionType.Anger => new Color32(249, 86, 78, 255),
            EmotionType.Fear => new Color32(150, 118, 255, 255),
            _ => Color.clear,

            //new Color32(94, 177, 191, 255)
        };
    }
}