using System;

[Serializable]
public class FloatRange
{
    public float Min;
    public float Max;

    public FloatRange(float min, float max)
    {
        this.Min = min;
        this.Max = max;
    }

    /// <summary>
    /// Returns a random float between the min and max float. (Both min and max are inclusive.)
    /// </summary>
    /// <returns>Random number.</returns>
    public float RandomValue()
    {
        return UnityEngine.Random.Range(Min, Max);
    }
}

