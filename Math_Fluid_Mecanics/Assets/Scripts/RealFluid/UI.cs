using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    public RealGlobalData globalData;
    public GameObject dataSlider;

    // Mass
    Slider sliderMass;
    TextMeshProUGUI valueSliderMass;

    // Viscosity
    Slider sliderViscosity;
    TextMeshProUGUI valueSliderViscosity;

    void Start()
    {
        StartMass();
        StartViscosity();
    }

    public void SliderMassChange()
    {
        globalData.particleMass = sliderMass.value;
        valueSliderMass.text = sliderMass.value.ToString();
    }

    public void SliderViscosityChange()
    {
        globalData.viscosity = sliderViscosity.value;
        valueSliderViscosity.text = sliderViscosity.value.ToString();
    }

    private void StartMass()
    {
        // Get Value
        GameObject mass = dataSlider.transform.Find("Mass Fluid Particle").gameObject;
        sliderMass = mass.GetComponentInChildren<Slider>();
        GameObject value = mass.transform.Find("Slider Value Mass").gameObject;
        valueSliderMass = value.GetComponent<TextMeshProUGUI>();
        // Set Value
        sliderMass.value = globalData.particleMass;
        valueSliderMass.text = globalData.particleMass.ToString();
    }

    private void StartViscosity()
    {
        // Get Value
        GameObject viscosity = dataSlider.transform.Find("Viscosity").gameObject;
        sliderViscosity = viscosity.GetComponentInChildren<Slider>();
        GameObject value = viscosity.transform.Find("Slider Value Viscosity").gameObject;
        valueSliderViscosity = value.GetComponent<TextMeshProUGUI>();
        // Set Value
        sliderViscosity.value = globalData.viscosity;
        valueSliderViscosity.text = globalData.viscosity.ToString();
    }
}
