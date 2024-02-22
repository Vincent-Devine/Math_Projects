using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlanetList : MonoBehaviour
{
    public List<PlanetData> planetDatasList;
    public List<Button> planetsButtonList;
    // Start is called before the first frame update
    void Start()
    {
        planetDatasList = GameObject.FindGameObjectWithTag("PlanetManager").GetComponent<GetPlanetInfo>().planetDatas;
    }

    // Update is called once per frame
    void Update()
    {
        //foreach(PlanetData planet in planetDatasList)
        //{

        //}
    }
}
