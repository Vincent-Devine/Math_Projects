using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputeNextPos : MonoBehaviour
{
    public PlanetData data;
    GetPlanetInfo g_info;

    // Start is called before the first frame update
    void Start()
    {
        data = GetComponent<PlanetData>();
        g_info = GameObject.FindGameObjectWithTag("PlanetManager").GetComponent<GetPlanetInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        FieldComputation.PositionDerivatives derivatives = FieldComputation.GetNextPos(data.common.DeltaTimeSubdiv, data.common, g_info.planetDatas, gameObject, data.Speed, data.Position);
        data.Position = derivatives.position;
        data.Speed = derivatives.speed;
        data.Acceleration = derivatives.acceleration;
        transform.position = data.Position / data.common.Scale;
    }
}
