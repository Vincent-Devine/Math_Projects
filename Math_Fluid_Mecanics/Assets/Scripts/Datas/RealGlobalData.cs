using UnityEngine;

[CreateAssetMenu(fileName = "Data", order = 1)]
public class RealGlobalData : ScriptableObject
{
    public static float g = 9.80665f;
    public Vector3 gravity = new Vector3(0f, -g, 0f);

    public float restDensity = 998.29f;

    public float particleMass = 0.02f;

    public float viscosity = 3.5f;

    public float gasStiffness = 3f;

    public float supportRadius = 0.0457f;

    public uint particleAmount = 200U;

    public float restitution = 0f;
}
