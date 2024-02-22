using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChangePlanetStats : MonoBehaviour
{
    public List<GameObject> planets;

    TMP_InputField mass;
    TMP_InputField posX;
    TMP_InputField posY;
    TMP_InputField posZ;
    TMP_InputField speedX;
    TMP_InputField speedY;
    TMP_InputField speedZ;
    TMP_InputField size;

    Slider posXSlider;
    Slider posYSlider;
    Slider posZSlider;
    Slider speedXSlider;
    Slider speddYSlider;
    Slider speedZSlider;
    Slider massSlider;
    Slider sizeSlider;

    // Start is called before the first frame update
    void Start()
    {
        planets = GetComponent<CreatePlanet>().planetsList;
        GetCanva();
        InitSliderValues();
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    void GetCanva()
    {
        //name = GameObject.Find("Name").GetComponent<TMP_InputField>();
        posX = GameObject.Find("Pos X").GetComponent<TMP_InputField>();
        posY = GameObject.Find("Pos Y").GetComponent<TMP_InputField>();
        posZ = GameObject.Find("Pos Z").GetComponent<TMP_InputField>();
        speedX = GameObject.Find("Speed X").GetComponent<TMP_InputField>();
        speedY = GameObject.Find("Speed Y").GetComponent<TMP_InputField>();
        speedZ = GameObject.Find("Speed Z").GetComponent<TMP_InputField>();
        mass = GameObject.Find("Mass").GetComponent<TMP_InputField>();
        size = GameObject.Find("Size").GetComponent<TMP_InputField>();

        posXSlider = GameObject.Find("Pos X").GetComponent<Slider>();
        posYSlider = GameObject.Find("Pos Y").GetComponent<Slider>();
        posZSlider = GameObject.Find("Pos Z").GetComponent<Slider>();
        speedXSlider = GameObject.Find("Speed X").GetComponent<Slider>();
        speddYSlider = GameObject.Find("Speed Y").GetComponent<Slider>();
        speedZSlider = GameObject.Find("Speed Z").GetComponent<Slider>();
        massSlider = GameObject.Find("Mass").GetComponent<Slider>();
        sizeSlider = GameObject.Find("Size").GetComponent<Slider>();
    }

    void InitSliderValues()
    {
        posXSlider.value = float.Parse(posX.text);
        posYSlider.value = float.Parse(posY.text);
        posZSlider.value = float.Parse(posZ.text);
        speedXSlider.value = float.Parse(speedX.text);
        speddYSlider.value = float.Parse(speedY.text);
        speedZSlider.value = float.Parse(speedZ.text);
        massSlider.value = float.Parse(mass.text);
        sizeSlider.value = float.Parse(size.text);

    }

    public void SliderChanged()
    {
        //posX.text = (posXSlider.value)ToString();
        //posY.text = (posYSlider.value)ToString();
        //posZ.text = (posZSlider.value)ToString();
        //speedX.text = (speedXSlider.value)ToString();
        //speedY.text = (speddYSlider.value)ToString();
        //speedZ.text = (speedZSlider.value)ToString();
        //mass.text = (massSlider.value)ToString();
        //size.text = (sizeSlider.value)ToString();

    }
}
