using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FieldComputation
{
    static public Vector3 GetAcceleration(GlobalData p_globalData, PlanetData p_planetToCheck, GameObject p_objectStudied, Vector3 p_position)
    {
        Vector3 acceleration = new Vector3(0f, 0f, 0f);

        Vector3 planetToCurIter = p_position - p_planetToCheck.Position;
        float distance = planetToCurIter.magnitude;

        if (distance < 10e+3f)
            distance = 10e+3f;

        acceleration += planetToCurIter.normalized * (p_planetToCheck.Mass * -p_globalData.G) / (distance * distance);

        if (acceleration.magnitude > 1e+6f)
            acceleration = acceleration.normalized * 1e+6f;

        return acceleration;
    }

    static public Vector3 GetAcceleration(GlobalData p_globalData, List<PlanetData> p_planetsToCheck, GameObject p_objectStudied, Vector3 p_position)
    {
        Vector3 acceleration = new Vector3(0f, 0f, 0f);

        foreach (PlanetData curPlanet in p_planetsToCheck)
        {
            if (curPlanet.PlanetObject == p_objectStudied)
                continue;

            Vector3 planetToCurIter = p_position - curPlanet.Position;
            float distance = planetToCurIter.magnitude;

            if (distance < 10e+3f)
                distance = 10e+3f;

            acceleration += planetToCurIter.normalized * (curPlanet.Mass * -p_globalData.G) / (distance * distance);
        }

        if (acceleration.magnitude > 1e+6f)
            acceleration = acceleration.normalized * 1e+6f;

        return acceleration;
    }

    public struct PositionDerivatives
    {
        public PositionDerivatives(Vector3 p_position, Vector3 p_speed, Vector3 p_acceleration)
        {
            position = p_position;
            speed = p_speed;
            acceleration = p_acceleration;
        }
        public Vector3 position;
        public Vector3 speed;
        public Vector3 acceleration;
    };

    static public PositionDerivatives GetNextPos(int p_DeltaTimeSubdiv, GlobalData p_globalData, PlanetData p_planetToCheck, GameObject p_objectStudied, Vector3 p_prevSpeed, Vector3 p_position)
    {
        Vector3 position = p_position;
        Vector3 speed = p_prevSpeed;
        Vector3 acceleration = new Vector3(0f, 0f, 0f);
        float deltaTimeSubdivision = Time.deltaTime * p_globalData.SimSpeed / (p_DeltaTimeSubdiv * 1f);

        for (int i = 0; i < p_DeltaTimeSubdiv; ++i)
        {
            acceleration = GetAcceleration(p_globalData, p_planetToCheck, p_objectStudied, position);
            position += (deltaTimeSubdivision * speed) + deltaTimeSubdivision * deltaTimeSubdivision / 2f * acceleration;
            Vector3 prevAcceleration = acceleration;
            acceleration = GetAcceleration(p_globalData, p_planetToCheck, p_objectStudied, position);
            speed += (acceleration + prevAcceleration) * (deltaTimeSubdivision / 2f);
        }

        return new PositionDerivatives(position, speed, acceleration);
    }

    static public PositionDerivatives GetNextPos(int p_DeltaTimeSubdiv, GlobalData p_globalData, List<PlanetData> p_planetsToCheck, GameObject p_objectStudied, Vector3 p_prevSpeed, Vector3 p_position)
    {
        Vector3 position = p_position;
        Vector3 speed = p_prevSpeed;
        Vector3 acceleration = new Vector3(0f, 0f, 0f);
        float deltaTimeSubdivision = Time.deltaTime * p_globalData.SimSpeed / (p_DeltaTimeSubdiv * 1f);

        for (int i = 0; i < p_DeltaTimeSubdiv; ++i)
        {
            acceleration = GetAcceleration(p_globalData, p_planetsToCheck, p_objectStudied, position);
            position += (deltaTimeSubdivision * speed) + deltaTimeSubdivision * deltaTimeSubdivision / 2f * acceleration;
            Vector3 prevAcceleration = acceleration;
            acceleration = GetAcceleration(p_globalData, p_planetsToCheck, p_objectStudied, position);
            speed += (acceleration + prevAcceleration) * (deltaTimeSubdivision / 2f);
        }

        return new PositionDerivatives(position, speed, acceleration);
    }

    public struct CurlFunctionArguments
    {
        public CurlFunctionArguments(Vector3 p_xDerivative, Vector3 p_yDerivative, Vector3 p_zDerivative)
        {
            xDerivative = p_xDerivative;
            yDerivative = p_yDerivative;
            zDerivative = p_zDerivative;
        }
        public Vector3 xDerivative;
        public Vector3 yDerivative;
        public Vector3 zDerivative;
    };

    static public CurlFunctionArguments GetCurlFunction(GlobalData p_globalData, PlanetData p_planetToCheck, GameObject p_objectStudied, Vector3 p_position, float p_step = 1e-9f)
    {
        Vector3 gAtPos = GetAcceleration(p_globalData, p_planetToCheck, p_objectStudied, p_position);

        Vector3 deltaGx = GetAcceleration(p_globalData, p_planetToCheck, p_objectStudied, p_position + Vector3.right * p_globalData.Scale * p_step) - gAtPos;

        Vector3 deltaGy = GetAcceleration(p_globalData, p_planetToCheck, p_objectStudied, p_position + Vector3.up * p_globalData.Scale * p_step) - gAtPos;

        Vector3 deltaGz = GetAcceleration(p_globalData, p_planetToCheck, p_objectStudied, p_position + Vector3.forward * p_globalData.Scale * p_step) - gAtPos;

        return new CurlFunctionArguments
        (
            (deltaGz - deltaGy) / (p_globalData.Scale * p_step),
            (deltaGx - deltaGz) / (p_globalData.Scale * p_step),
            (deltaGy - deltaGx) / (p_globalData.Scale * p_step)
        );
    }

    static public CurlFunctionArguments GetCurlFunction(GlobalData p_globalData, List<PlanetData> p_planetsToCheck, GameObject p_objectStudied, Vector3 p_position, float p_step = 1e-9f)
    {
        Vector3 gAtPos = GetAcceleration(p_globalData, p_planetsToCheck, p_objectStudied, p_position);

        Vector3 deltaGx = GetAcceleration(p_globalData, p_planetsToCheck, p_objectStudied, p_position + Vector3.right * p_globalData.Scale * p_step) - gAtPos;

        Vector3 deltaGy = GetAcceleration(p_globalData, p_planetsToCheck, p_objectStudied, p_position + Vector3.up * p_globalData.Scale * p_step) - gAtPos;

        Vector3 deltaGz = GetAcceleration(p_globalData, p_planetsToCheck, p_objectStudied, p_position + Vector3.forward * p_globalData.Scale * p_step) - gAtPos;

        return new CurlFunctionArguments
        (
            (deltaGz - deltaGy) / (p_globalData.Scale * p_step),
            (deltaGx - deltaGz) / (p_globalData.Scale * p_step),
            (deltaGy - deltaGx) / (p_globalData.Scale * p_step)
        );
    }

    static public Vector3 GetCurlAt(Vector3 p_position, CurlFunctionArguments p_curlFunction)
    {
        return new Vector3
        (
            p_curlFunction.xDerivative.x * p_position.x + p_curlFunction.xDerivative.y * p_position.y + p_curlFunction.xDerivative.z * p_position.z,
            p_curlFunction.yDerivative.x * p_position.x + p_curlFunction.yDerivative.y * p_position.y + p_curlFunction.yDerivative.z * p_position.z,
            p_curlFunction.zDerivative.x * p_position.x + p_curlFunction.zDerivative.y * p_position.y + p_curlFunction.zDerivative.z * p_position.z
        );
    }
}
 