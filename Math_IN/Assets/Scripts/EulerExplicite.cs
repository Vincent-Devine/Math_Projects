using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EulerExplicite : MonoBehaviour
{
    Dictionary<float, float> curve = new Dictionary<float, float>();

    private void Start()
    {
        // exo 1
        Func<float, float, float> func1 = (tn, yn) => Mathf.Exp(2 * tn) + yn;
        func1 = (tn, yn) => yn;
        ComputeEulerExplicite(func1, 0.0f, 5.0f, 1.0f, 5.0f / 1000.0f);
    }

    private void Update()
    {
        for(int i = 0; i < curve.Count - 1; i++)
        {
            Vector3 start = new Vector3(0, curve.ElementAt(i).Value, curve.ElementAt(i).Key);
            Vector3 end = new Vector3(0, curve.ElementAt(i + 1).Value, curve.ElementAt(i + 1).Key);
            Debug.DrawLine(start, end, Color.blue);
        }
    }

    public void ComputeEulerExplicite(Func<float, float, float> func, float tmin, float tmax, float yO, float h)
    {
        float t = tmin;
        float yn = yO;
        curve.Clear();

        while (t <= tmax)
        {
            float yApproximate = yn + h * func(t, yn);
            float nextY = yn + (h/2) * (func(t, yn) + func(t + h, yApproximate));
            curve.Add(t, nextY);
            t += h;
        }
    }
}
