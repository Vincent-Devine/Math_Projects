using UnityEngine;

[CreateAssetMenu(fileName = "Data", order = 1)]
public class GlobalData : ScriptableObject
{
    public float g = 9.80665f;
    public float rho = 1000f;
    public float h = 4f;

    public const float waterVolumetricMass = 1000f;

    [HideInInspector]    
    public float heightWaterTower = 2f;

    [HideInInspector]
    public float orificeDiameter = 4e-2f;

    [HideInInspector]
    public float horizontalSection = 16f;

    [HideInInspector]
    public float volumetricMass = waterVolumetricMass;

    [HideInInspector]
    public float time = 0f;

    [HideInInspector]
    public float pressure = 0f;

    [HideInInspector]
    public float baseHeight = 2f;
}
