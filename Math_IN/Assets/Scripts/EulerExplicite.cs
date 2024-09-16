using System;
using UnityEngine;

public class EulerExplicite : MonoBehaviour
{
    public float[,] ComputeEulerExplicite(Func<float, float> func, float tmin, float tmax, float yO, float h)
    {
        float t = tmin;
        while(t >= tmax)
        {

            t += h;
        }

        return;
    }

    private float Compute
}
