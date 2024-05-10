using UnityEngine;

public static class EmotionPalette
{
    public static Color GetColor(EmotionType emotion)
    {
        return emotion switch
        {
            EmotionType.Joy => Color.yellow,
            EmotionType.Sadness => Color.cyan,
            EmotionType.Anger => Color.red,
            EmotionType.Fear => Color.magenta,
            _ => Color.clear,

            //new Color32(115, 122, 170, 255)
        };
    }
}