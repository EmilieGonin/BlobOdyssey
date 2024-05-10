public enum EmotionType
{
    Joy, Sadness, Anger, Fear
}

public class Emotion
{
    public EmotionType Type { get; private set; }
    public int Level { get; private set; }
    public float Value { get; private set; }
    public bool IsOver => Value <= 0 || Value >= 100;

    public Emotion(EmotionType type)
    {
        Type = type;
        Level = 1;
        Value = 50;
    }

    public void Add(float value) => Value += value;
    public void Substract(float value) => Value -= value;
    public void LevelUp() => Level++;
}