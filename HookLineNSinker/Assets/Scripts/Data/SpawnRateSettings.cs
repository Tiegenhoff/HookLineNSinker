using UnityEngine;

[CreateAssetMenu(fileName = "SpawnRateSettings", menuName = "Scriptable Objects/SpawnRateSettings")]
public class SpawnRateSettings : ScriptableObject
{
    // This scriptable object holds the spawn rates for different depth tiers in the game.
    // It allows for easy adjustment of spawn rates without modifying code.

    [Header("Fish per minute at each depth tier")]
    public float shallowRate = 0.5f;   // depth < 10
    public float midRate = 0.7f;       // depth < 20
    public float deepRate = 1.0f;      // depth < 30
    public float abyssRate = 1.3f;     // depth >= 30
}

// Note: I'd like to keep these editable because depths are yet to be finalized and are for testing.