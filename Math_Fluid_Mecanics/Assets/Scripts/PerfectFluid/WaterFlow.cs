using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaterFlow : MonoBehaviour
{
    public GlobalData data;

    // -----------------
    //    Water Tank
    // -----------------
    GameObject waterTank;
    Transform transformTank;
    float initScaleY;
    float tankCurrentPercentageWater;
    // water position
    float initPosY;
    float endPosY;

    // -----------------
    //    Water Flow
    // -----------------
    LineRenderer waterFlowRenderer;
    Vector2 posHole;


    void Start()
    {
        // -----------------
        //    Water Tank
        // -----------------
        waterTank = transform.Find("WaterTank").gameObject;
        transformTank = waterTank.transform;
        initScaleY = transformTank.localScale.y;
        tankCurrentPercentageWater = 1f;
        data.time = 0f;
        // water position
        initPosY = transformTank.position.y;
        endPosY = 3.15f;

        // -----------------
        //    Water Flow
        // -----------------
        waterFlowRenderer = GetComponentInChildren<LineRenderer>();
        posHole = new Vector2(-2.15f, 1.005f);

        Time.timeScale = 1f;

    }

    void Update()
    {
        if(tankCurrentPercentageWater == 0f)
            return;
        
        WaterTankDecrease();
        ComputeWaterFlow();

        // Add Time
        data.time += Time.deltaTime * 1000f;
    }

    void WaterTankDecrease()
    {
        // Compute water tank decrease
        float height = ComputeHeight();
        tankCurrentPercentageWater = height / data.heightWaterTower;

        // rebase water position
        transformTank.position = new Vector3(transformTank.position.x, Mathf.Lerp(initPosY, endPosY, 1 - tankCurrentPercentageWater), transformTank.position.z);

        // Empty tank
        if (tankCurrentPercentageWater <= 0.005f)
        {
            transformTank.localScale = new Vector3(transformTank.localScale.x, 0f, transformTank.localScale.z);
            tankCurrentPercentageWater = 0f;
            waterFlowRenderer.positionCount = 0;
            return;
        }

        // Set value
        transformTank.localScale = new Vector3(transformTank.localScale.x, initScaleY * tankCurrentPercentageWater, transformTank.localScale.z);
    }

    void ComputeWaterFlow()
    {
        float time = 0f;
        for (int i = 0; i < waterFlowRenderer.positionCount; i++)
        {
            float x = ComputeSpeed() * time + posHole.x;
            float y = -data.g * time * time / 2f + posHole.y;
            time += ComputeLandingTime() / waterFlowRenderer.positionCount;
            waterFlowRenderer.SetPosition(i, new Vector3(x, y, 0f));
        }
    }

    float ComputeLandingTime()
    {
        return Mathf.Sqrt(2f * data.g * 3.4f) / data.g; ;
    }

    float ComputeSpeed()
    {
        return Mathf.Sqrt((2f * data.pressure * 100000f) / data.rho + 2f * data.g * ComputeHeight());
    }

    float ComputeHeight()
    {
        float orificeArea = data.orificeDiameter * data.orificeDiameter / 4f * Mathf.PI;
        float result = ((orificeArea * Mathf.Sqrt(2f * data.g) * data.time) / (-2f * data.horizontalSection)) + Mathf.Sqrt(data.heightWaterTower); // sqrt of height
        if (result <= 0f)
            return 0f;
        return result * result;
    }
}
