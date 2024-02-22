using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GetPlanetInfo : MonoBehaviour
{
    [HideInInspector]
    public List<PlanetData> planetDatas;
    TMP_InputField posX;
    TMP_InputField posY;
    TMP_InputField posZ;
    TMP_InputField speedX;
    TMP_InputField speedY;
    TMP_InputField speedZ;
    TMP_InputField mass;
    //TMP_InputField size;
    TMP_InputField planetName;

    public bool followPlanet;
    public PlanetData planetSelected;
    public Canvas canva;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    public EventSystem m_EventSystem;

    public GameObject gameManager;
    bool changeValided;

    public TextMeshProUGUI textFollowButton;

    public GameObject showPlanetInfoButton;
    public GameObject orbitModeButton;

    private OrbiteMode orbite;
    public bool doOnce;
    // Start is called before the first frame update
    void Start()
    {
        orbite = Camera.main.GetComponent<OrbiteMode>();
        GetCanva();
        changeValided = false;
        followPlanet = false;
        doOnce = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (planetSelected)
        {
            showPlanetInfoButton.SetActive(true);
            orbitModeButton.SetActive(true);
            if(!changeValided && doOnce)
            {
                GivePlanetSelectedInfo();
                doOnce = false;
            }
            else if(changeValided)
            {
                planetSelected.Name = planetName.text;
                planetSelected.Position.x = float.Parse(posX.text);
                planetSelected.Position.y = float.Parse(posY.text);
                planetSelected.Position.z = float.Parse(posZ.text);
                planetSelected.Speed.x = float.Parse(speedX.text);
                planetSelected.Speed.y = float.Parse(speedY.text);
                planetSelected.Speed.z = float.Parse(speedZ.text);
                planetSelected.Mass = float.Parse(mass.text);
                doOnce = true;
            }

            orbite.planet = followPlanet ? planetSelected.gameObject : null;
        }
        else
        {
            ResetCanva();
            showPlanetInfoButton.SetActive(false);
            if(!followPlanet)
            {
                orbitModeButton.SetActive(false);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(!CheckIfUIClicked() && !CheckIfPlanetClicked())
            {
                planetSelected = null;
            }
        }

        changeValided = false;
    }
    public void OnButtonPress()
    {
        if(textFollowButton)
        {
            followPlanet = !followPlanet;
            textFollowButton.text = followPlanet ? "Stop following" : "Follow planet";
        }
    }
     
    public void OnChangesValid()
    {
        changeValided = true;
    }
    public void GivePlanetSelectedInfo()
    {
        if (!CheckIfUIClicked())
        {
            planetName.text = planetSelected.Name;
            posX.text = (planetSelected.Position.x).ToString();
            posY.text = (planetSelected.Position.y).ToString();
            posZ.text = (planetSelected.Position.z).ToString();
            speedX.text = (planetSelected.Speed.x).ToString();
            speedY.text = (planetSelected.Speed.y).ToString();
            speedZ.text = (planetSelected.Speed.z).ToString();
            mass.text = (planetSelected.Mass).ToString();
        }
    }

    public bool CheckIfPlanetClicked()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, 1000f);
        foreach (PlanetData planet in planetDatas)
        {
            if (hit.transform && planet.gameObject == hit.transform.gameObject)
            {
                planetSelected = planet;
                return true;
            }
        }
        return false;

    }

    public bool CheckIfUIClicked()
    {
        if (m_EventSystem.IsPointerOverGameObject())
        {
            return true;
        }
        return false;
    }

    public void GetCanva()
    {
        planetName = GameObject.Find("Name").GetComponent<TMP_InputField>();
        posX = GameObject.Find("Pos X").GetComponent<TMP_InputField>();
        posY = GameObject.Find("Pos Y").GetComponent<TMP_InputField>();
        posZ = GameObject.Find("Pos Z").GetComponent<TMP_InputField>();
        speedX = GameObject.Find("Speed X").GetComponent<TMP_InputField>();
        speedY = GameObject.Find("Speed Y").GetComponent<TMP_InputField>();
        speedZ = GameObject.Find("Speed Z").GetComponent<TMP_InputField>();
        mass = GameObject.Find("Mass").GetComponent<TMP_InputField>();

        m_Raycaster = canva.GetComponent<GraphicRaycaster>();
    }
    public void ResetCanva()
    {
        planetName.text = "";
        posX.text = "";
        posY.text = "";
        posZ.text = "";
        speedX.text = "";
        speedY.text = "";
        speedZ.text = "";
        mass.text = "";
    }
}
