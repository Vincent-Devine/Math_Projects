using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovePannel : MonoBehaviour
{
    public GameObject pannelMenu;
    public TextMeshProUGUI textMenu;
    public GetPlanetInfo planetInfo;
    bool firstFrame;
    public TextMeshProUGUI showInfoButtonText;
    public GameObject validChangesButton;
    public GameObject orbitModeButon;
    // Start is called before the first frame update
    void Start()
    {
        firstFrame = true;
        planetInfo = GameObject.FindGameObjectWithTag("PlanetManager").GetComponent<GetPlanetInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if(firstFrame)
        {
            pannelMenu.SetActive(false);
            firstFrame = false;
        }

        if (planetInfo.planetSelected && pannelMenu.activeInHierarchy)
        {
            validChangesButton.SetActive(true);
        }

        //if(!planetInfo.planetSelected && pannelMenu.activeInHierarchy)
        //{
        //    pannelMenu.SetActive(false);
        //    orbitModeButon.SetActive(false);
        //    validChangesButton.SetActive(false);
        //}
    }

    public void OnButtonPressed()
    {
        if (pannelMenu.activeInHierarchy == false)
        {
            pannelMenu.SetActive(true);
            textMenu.text = "Hide Menu";
        }
        else
        {
            pannelMenu.SetActive(false);
            textMenu.text = "Show Menu";
            validChangesButton.SetActive(false);
        }
    }
    
    public void ShowSelectedInfo()
    {
        if(!pannelMenu.activeInHierarchy)
        {
            pannelMenu.SetActive(true);
            validChangesButton.SetActive(true);
            showInfoButtonText.text = "Hide Planet Info";
        }
        else
        {
            pannelMenu.SetActive(false);
            validChangesButton.SetActive(false);
            showInfoButtonText.text = "Show Planet Info";

        }
    }
}
