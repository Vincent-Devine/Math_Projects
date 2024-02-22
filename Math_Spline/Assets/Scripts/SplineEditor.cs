using System.Collections.Generic;
using UnityEngine;

public enum SPLINE_TYPE
{
    HERMITE,
    BEZIER,
    B_SPLINE,
    CATMULL_ROM
}

public class SplineEditor : MonoBehaviour
{
    [Header("Spline")]
    [SerializeField] private SPLINE_TYPE splineType;
    [SerializeField] private float step = 0.05f;
    [SerializeField] private List<GameObject> points = new List<GameObject>();
    [SerializeField] private List<Vector3> derived = new List<Vector3>();

    private void OnDrawGizmos()
    {
        DisplayCurve(ComputeCurve());

        if (splineType == SPLINE_TYPE.HERMITE)
            DisplayDerived();
    }

    private List<Vector3> ComputeCurve()
    {
        switch (splineType)
        {
            case SPLINE_TYPE.HERMITE:
                return ComputeHermite();

            case SPLINE_TYPE.BEZIER:
                return ComputeBezier();

            case SPLINE_TYPE.B_SPLINE:
                return ComputeBSpline();

            case SPLINE_TYPE.CATMULL_ROM:
                return ComputeCatmullRom();

            default:
                break;
        }
        return new List<Vector3>();
    }

    private void DisplayCurve(List<Vector3> curve)
    {
        for(int i = 0; i < curve.Count - 1; i++)
            Debug.DrawLine(curve[i], curve[i + 1], Color.blue);
    }

    private void DisplayDerived()
    {
        for (int i = 0; i < derived.Count; i++)
            Debug.DrawLine(points[i].transform.position, derived[i], Color.red);
    }

    private List<Vector3> ComputeHermite()
    {
        float time = 0f;
        Matrix4x4 Mh = new Matrix4x4(new Vector4( 2, -2,  1,  1),
                                     new Vector4(-3,  3, -2, -1),
                                     new Vector4( 0,  0,  1,  0),
                                     new Vector4( 1,  0,  0,  0));
        Matrix4x4 Gh = new Matrix4x4(points[0].transform.position,
                                     points[1].transform.position,
                                     derived[0],
                                     derived[1]);

        List<Vector3> curve = new List<Vector3>();
        while(time < 1f)
        {
            Vector4 T = new Vector4(Mathf.Pow(time, 3), Mathf.Pow(time, 2), time, 1);
            curve.Add(Gh * Mh * T);
            time += step;
        }

        // Last
        curve.Add(Gh * Mh * Vector4.one);
        return curve;
    }

    private List<Vector3> ComputeBezier()
    {
        float time = 0f;
        List<Vector3> curve = new List<Vector3>();

        Vector3 point = new Vector3();
        while (time < 1f)
        {
            for (int i = 0; i < points.Count; i++)
            {
                point += (Factorial(points.Count - 1) / (Factorial(i) * Factorial(points.Count - 1 - i)) // Binomial coefficient
                    * Mathf.Pow(time, i) * Mathf.Pow(1 - time, points.Count - 1 - i)
                    * points[i].transform.position);
            }

            curve.Add(point);
            point = Vector3.zero;
            time += step;
        }
                    

        // Last
        for (int i = 0; i < points.Count; i++)
        {
            point += (Factorial(points.Count - 1) / (Factorial(i) * Factorial(points.Count - 1 - i)) // Binomial coefficient
                * Mathf.Pow(1, i) * Mathf.Pow(1 - 1, points.Count - 1 - i)
                * points[i].transform.position);
        }

        curve.Add(point);
        return curve;
    }

    private List<Vector3> ComputeBSpline()
    {
        float time = 0f;
        List<Vector3> curve = new List<Vector3>();

        Matrix4x4 Mbs = new Matrix4x4(new Vector4(-1,  3, -3, 1),
                                      new Vector4( 3, -6,  3, 0),
                                      new Vector4(-3,  0,  3, 0),
                                      new Vector4( 1,  4,  1, 0));
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                Mbs[i, j] *= 1f/6f;

        Matrix4x4 Gbs = new Matrix4x4(points[0].transform.position,
                                      points[1].transform.position,
                                      points[2].transform.position,
                                      points[3].transform.position);
        while (time < 1f)
        {
            Vector4 T = new Vector4(Mathf.Pow(time, 3), Mathf.Pow(time, 2), time, 1);
            curve.Add(Gbs * Mbs * T);
            time += step;
        }

        // Last
        curve.Add(Gbs * Mbs * Vector4.one);
        return curve;
    }

    private List<Vector3> ComputeCatmullRom()
    {
        float time = 0f;
        List<Vector3> curve = new List<Vector3>();

        Matrix4x4 Mscr = new Matrix4x4(new Vector4(-1,  3, -3,  1),
                                       new Vector4( 2, -5,  4, -1),
                                       new Vector4(-1,  0,  1,  0),
                                       new Vector4( 0,  2,  0,  0));
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 4; j++)
                Mscr[i, j] *= 1f/2f;

        Matrix4x4 Gscr = new Matrix4x4(points[0].transform.position,
                                       points[1].transform.position,
                                       points[2].transform.position,
                                       points[3].transform.position);
        while (time < 1f)
        {
            Vector4 T = new Vector4(Mathf.Pow(time, 3), Mathf.Pow(time, 2), time, 1);
            curve.Add(Gscr * Mscr * T);
            time += step;
        }

        // Last
        curve.Add(Gscr * Mscr * Vector4.one);
        return curve;
    }

    private int Factorial(int x)
    {
        if(x <= 1)
            return 1;

        return x + Factorial(x - 1);
    }
}
