using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kernels
{
    /* 
     * r is the vector from curent particle to particles arround it
     * h is the kernel support radius
     */
    public static float Default(Vector3 r, float h)
    {
        if (r.magnitude > h)
            return 0f;

        return 315f / (64f * Mathf.PI * Mathf.Pow(h, 9f)) * Mathf.Pow(h * h - r.sqrMagnitude, 3f);
    }

    public static Vector3 Pressure(Vector3 r, float h)
    {
        return -45f / (Mathf.PI * Mathf.Pow(h, 6f)) * r.normalized * Mathf.Pow(h - r.magnitude, 2f);
    }

    public static float Viscosity(Vector3 r, float h)
    {
        if (r.magnitude > h)
            return 0;

        return 45f / (Mathf.PI * Mathf.Pow(h, 6f)) * (h - r.magnitude);
    }
}
