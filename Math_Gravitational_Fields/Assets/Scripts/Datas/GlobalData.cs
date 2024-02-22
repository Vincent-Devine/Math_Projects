using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

[System.Serializable]
public struct PlanetDataInfo
{
    public float Mass;
    public Vector3 InitSpeed;
    public Vector3 InitPosition;
    public string Name;
    public PlanetDataInfo(float mass, Vector3 initPos, Vector3 initSpeed, string name)
    {
        Mass = mass;
        InitSpeed = initSpeed;
        InitPosition = initPos;
        Name = name;
    }
}

[CreateAssetMenu(fileName = "Data", order = 1)]
public class GlobalData : ScriptableObject
{
    public float G = 6.6743015e-11f;
    public float Scale = 5e+9f;
    [Range(5e+4f, 1e+6f)]
    public float SimSpeed = 5e+5f;
    [Range(7, 13)]
    public int DeltaTimeSubdiv = 10;
    public PlanetDataInfo[] KnownObjects =
    {
        new PlanetDataInfo(1.9885e+30f, new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f), "Sun"),
        new PlanetDataInfo(3.3011e+23f, new Vector3(5.790905e+10f, 0f, 0f), new Vector3(0f, 0f, 4.736e+4f), "Mercury"),
        new PlanetDataInfo(4.8675e+24f, new Vector3(1.08208e+11f, 0f, 0f), new Vector3(0f, 0f, 3.502e+4f), "Venus"),
        new PlanetDataInfo(5.972168e+24f, new Vector3(1.49598e+11f, 0f, 0f), new Vector3(0f, 0f, 2.97827e+4f), "Earth"),
        new PlanetDataInfo(7.342e+22f, new Vector3(3.84399e+8f + 1.49598e+11f, 0f, 0f), new Vector3(0f, 0f, 2.97827e+4f + 1.022e+3f), "Moon"),
        new PlanetDataInfo(6.4171e+23f, new Vector3(2.279394e+11f, 0f, 0f), new Vector3(0f, 0f, 2.407e+4f), "Mars"),
        new PlanetDataInfo(1.8982e+27f, new Vector3(7.78479e+11f, 0f, 0f), new Vector3(0f, 0f, 1.307e+4f), "Jupiter"),
        new PlanetDataInfo(5.6834e+26f, new Vector3(1.43353e+12f, 0f, 0f), new Vector3(0f, 0f, 9.68e+3f), "Saturn"),
        new PlanetDataInfo(8.681e+25f, new Vector3(2.870972e+12f, 0f, 0f), new Vector3(0f, 0f, 6.8e+3f), "Uranus"),
        new PlanetDataInfo(1.02413e+26f, new Vector3(4.5e+12f, 0f, 0f), new Vector3(0f, 0f, 5.43e+3f), "Neptune")
    };
    [Range(4, 10)]
    public int IterationInEachDirection = 6;
    [Range(0.5f, 8f)]
    public float SpacingBetweenVectors = 4f;
    public Vector3 VectorFieldOffset = Vector3.zero;
}