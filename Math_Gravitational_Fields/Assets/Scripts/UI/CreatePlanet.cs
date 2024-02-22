using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreatePlanet : MonoBehaviour
{
    TMP_InputField mass;
    TMP_InputField posX;
    TMP_InputField posY;
    TMP_InputField posZ;
    TMP_InputField speedX;
    TMP_InputField speedY;
    TMP_InputField speedZ;
    TMP_InputField size;

    public GameObject planetPrefab;
    public List<GameObject> planetsList;
    private GetPlanetInfo planetInfo;

    public GlobalData common;
    public List<Material> materialList;
    int randMeshIndex;

    // Start is called before the first frame update
    void Start()
    {
        GetInputField();

        planetInfo = GetComponent<GetPlanetInfo>();

        foreach(PlanetDataInfo info in common.KnownObjects)
            InstantiatePlanet(info);
    }

    void InstantiatePlanet(PlanetDataInfo info)
    {
        GameObject planetCreated = Instantiate(planetPrefab);
        planetCreated.name = info.Name;
        planetsList.Add(planetCreated);

        PlanetData data = planetCreated.GetComponent<PlanetData>();
        data.PlanetObject = planetCreated;
        data.Mass = info.Mass;
        data.InitSpeed = info.InitSpeed;
        data.Speed = info.InitSpeed;
        data.InitPosition = info.InitPosition;
        data.Name = info.Name;
        data.Position = info.InitPosition;
        planetInfo.planetDatas.Add(data);
        planetCreated.GetComponent<MeshRenderer>().material = materialList[randMeshIndex];
        planetCreated.transform.position = data.Position / common.Scale;
        float size = Mathf.Clamp(info.Mass / 1e+25f * 0.1f, .02f, 20f);
        planetCreated.transform.localScale = Vector3.one * size;

        if (randMeshIndex < 9)
            randMeshIndex++;
        else
            randMeshIndex = 0;

    }

    public void OnButtonPress()
    {
        InstantiatePlanet(new PlanetDataInfo(float.Parse(mass.text), new Vector3(float.Parse(speedX.text), float.Parse(speedY.text), float.Parse(speedZ.text)), new Vector3(float.Parse(posX.text), float.Parse(posY.text), float.Parse(posZ.text)), "New"));
    }  

    public void GetInputField()
    {
        posX = GameObject.Find("Pos X").GetComponent<TMP_InputField>();
        posY = GameObject.Find("Pos Y").GetComponent<TMP_InputField>();
        posZ = GameObject.Find("Pos Z").GetComponent<TMP_InputField>();

        speedX = GameObject.Find("Speed X").GetComponent<TMP_InputField>();
        speedY = GameObject.Find("Speed Y").GetComponent<TMP_InputField>();
        speedZ = GameObject.Find("Speed Z").GetComponent<TMP_InputField>();

        mass = GameObject.Find("Mass").GetComponent<TMP_InputField>();
        size = GameObject.Find("Size").GetComponent<TMP_InputField>();

        posX.text = "0";
        posY.text = "0";
        posZ.text = "0";

        speedX.text = "0";
        speedY.text = "0";
        speedZ.text = "0";

        mass.text = "0";
        size.text = "0";
    }

}
