using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Field : MonoBehaviour
{
    public GlobalData common;
    public GameObject arrowPrefab;

    private GetPlanetInfo planetInfo;
    private List<Transform> objects;
    private LineRenderer lineRenderer;

    enum FieldScope { Global, Planet, Clear };
    private FieldScope fieldScope = FieldScope.Global;

    enum FieldType { Gravitational, Curl, FieldLine };
    private FieldType fieldType = FieldType.Gravitational;

    // Start is called before the first frame update
    void Start()
    {
        common.VectorFieldOffset = Vector3.zero;
        planetInfo = GameObject.FindGameObjectWithTag("PlanetManager").GetComponent<GetPlanetInfo>();
        objects = new List<Transform>();
        for(int i = 0; i < common.IterationInEachDirection * common.IterationInEachDirection * common.IterationInEachDirection; i++)
        {
            GameObject vec = Instantiate(arrowPrefab, transform);
            objects.Add(vec.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(objects.Count == 0)
            return;

        if(fieldType == FieldType.FieldLine)
        {
            DrawFieldLine();
            return;
        }

        float lowerBound = (-common.IterationInEachDirection / 2f) + 0.5f;
        float upperBound = ( common.IterationInEachDirection / 2f);

        try
        {
            int index = 0;
            for (float i = lowerBound; i < upperBound; i += 1)
            {
                for(float j = lowerBound; j < upperBound; j += 1)
                {
                    for (float k = lowerBound; k < upperBound; k += 1)
                    {
                        objects[index].position = new Vector3(i, k, j) * common.SpacingBetweenVectors + common.VectorFieldOffset;

                        if (planetInfo.planetSelected && fieldScope == FieldScope.Planet)
                            objects[index].position += planetInfo.planetSelected.transform.position;

                        bool objectInPlanet = false;
                        foreach(PlanetData planet in planetInfo.planetDatas)
                        {
                            if (planet.transform.lossyScale.x * 0.6f > Vector3.Distance(objects[index].position, planet.transform.position))
                            {
                                objectInPlanet = true;
                                objects[index].gameObject.SetActive(false);
                                break;
                            }
                        }
                        if (objectInPlanet)
                        {
                            index++;
                            continue;
                        }
                        if (!objects[index].gameObject.activeInHierarchy)
                            objects[index].gameObject.SetActive(true);

                        Vector3 direction = SelectDirection(index);
                        float magnitude = direction.magnitude;
                        Draw(direction.normalized, magnitude, index);
                        index++;
                    }
                }
            }
        }
        catch(ArgumentOutOfRangeException)
        {
            return;
        }
    }

    void DrawFieldLine()
    {
        if (!planetInfo)
            return;
        lineRenderer.positionCount = 50;
        lineRenderer.SetPosition(0, planetInfo.planetSelected.transform.position);
        for(int i = 1; i < 50; ++i)
        {
            Vector3 prevPos = lineRenderer.GetPosition(i - 1);
            Vector3 accelerationAtPoint = FieldComputation.GetAcceleration(common, planetInfo.planetDatas, planetInfo.planetSelected.gameObject, prevPos * common.Scale);
            Vector3 nextPos = prevPos + accelerationAtPoint.normalized * 5f;
            bool inPlanet = false;
            foreach (PlanetData planet in planetInfo.planetDatas)
            {
                if (planet.gameObject == planetInfo.planetSelected.gameObject)
                    continue;
                if (planet.transform.lossyScale.x * 0.6f > Vector3.Distance(nextPos, planet.transform.position))
                {
                    inPlanet = true;
                    break;
                }
            }
            if (inPlanet)
                return;
            lineRenderer.SetPosition(i, nextPos);

        }
    }

    Vector3 SelectDirection(int index)
    {
        if (fieldType == FieldType.Gravitational)
        {
            if (fieldScope == FieldScope.Global)
                return FieldComputation.GetAcceleration(common, planetInfo.planetDatas, null, objects[index].position * common.Scale);
            else if (planetInfo.planetSelected && fieldScope == FieldScope.Planet)
                return FieldComputation.GetAcceleration(common, planetInfo.planetSelected, null, objects[index].position * common.Scale);
        }
        else if (fieldType == FieldType.Curl)
        {
            FieldComputation.CurlFunctionArguments curlFunction;
            if (fieldScope == FieldScope.Global)
            {
                curlFunction = FieldComputation.GetCurlFunction(common, planetInfo.planetDatas, null, objects[index].position * common.Scale, 5e-7f);
                return FieldComputation.GetCurlAt(objects[index].position * common.Scale, curlFunction);
            }
            else if (planetInfo.planetSelected && fieldScope == FieldScope.Planet)
            {
                curlFunction = FieldComputation.GetCurlFunction(common, planetInfo.planetSelected, null, objects[index].position * common.Scale, 5e-7f);
                return FieldComputation.GetCurlAt(objects[index].position * common.Scale, curlFunction);
            }
        }
        return Vector3.zero;
    }

    void Draw(Vector3 p_direction, float p_magnitude, int p_index)
    {
        if(p_direction == Vector3.zero)
        {
            objects[p_index].gameObject.SetActive(false);
            return;
        }
        objects[p_index].forward = p_direction;
        Vector3 newScale = Vector3.one * p_magnitude;
        if (newScale.x < .01f)
            newScale = Vector3.one * 0.01f;
        if (newScale.x > 0.1f)
            newScale = Vector3.one * 0.1f;
        objects[p_index].localScale = newScale;
    }

    public void OnFieldScopeChanged(TMP_Dropdown p_change)
    {
        fieldScope = (FieldScope)p_change.value;
        RedrawField();
    }

    public void OnFieldTypeChanged(TMP_Dropdown p_change)
    {
        fieldType = (FieldType)p_change.value;
        if(fieldType == FieldType.FieldLine)
        {
            lineRenderer = planetInfo.planetSelected.gameObject.GetComponent<LineRenderer>();
            return;
        }
        RedrawField();
    }

    void Clear()
    {
        foreach (Transform curObj in objects)
            Destroy(curObj.gameObject);

        objects.Clear();
        if(lineRenderer)
            lineRenderer.positionCount = 0;
    }

    void RedrawField()
    {
        if (fieldType == FieldType.FieldLine)
            return;
        Clear();
        for (int i = 0; i < common.IterationInEachDirection * common.IterationInEachDirection * common.IterationInEachDirection; i++)
        {
            GameObject obj = Instantiate(arrowPrefab, transform);
            objects.Add(obj.transform);
        }
    }

    public void OnFieldSizeChanged(Slider slider)
    {
        common.IterationInEachDirection = (int)slider.value;
        RedrawField();
    }

    public void OnVectorSpacingChanged(Slider slider) { common.SpacingBetweenVectors = slider.value; }
    public void OnXOffsetChanged(Slider slider) { common.VectorFieldOffset.x = slider.value; }
    public void OnYOffsetChanged(Slider slider) { common.VectorFieldOffset.y = slider.value; }
    public void OnZOffsetChanged(Slider slider) { common.VectorFieldOffset.z = slider.value; }
}
