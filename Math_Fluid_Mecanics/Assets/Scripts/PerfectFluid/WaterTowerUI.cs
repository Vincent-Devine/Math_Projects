using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class WaterTowerUI : MonoBehaviour
{
    public GlobalData globalData;
    public GameObject dataSlider;


    // Height
    Slider sliderHeight;
    TextMeshProUGUI valueSliderHeight;

    // Rho
    Slider sliderRho;
    TextMeshProUGUI valueSliderRho;

    // Diameter
    Slider sliderDiameter;
    TextMeshProUGUI valueSliderDiameter;

    // Pressure
    Slider sliderPressure;
    TextMeshProUGUI valueSliderPressure;

    private void Start()
    {
        StartHeight();
        StartRho();
        StartDiameter();
        StartPressure();
    }

    public void SliderHeightChanger()
    {
        globalData.heightWaterTower = sliderHeight.value;
        valueSliderHeight.text = sliderHeight.value.ToString();
    }

    public void SliderRhoChanger()
    {
        globalData.rho= sliderRho.value;
        valueSliderRho.text = sliderRho.value.ToString();
    }

    public void SliderDiameterChanger()
    {
        globalData.orificeDiameter = sliderDiameter.value;
        valueSliderDiameter.text = sliderDiameter.value.ToString();
    }

    public void SliderPressureChanger()
    {
        globalData.pressure = sliderPressure.value;
        valueSliderPressure.text = sliderPressure.value.ToString();
    }

    public void ButtonFillWaterTank()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void StartHeight()
    {
        // Get Value
        GameObject height = dataSlider.transform.Find("Height Water Tower").gameObject;
        sliderHeight = height.GetComponentInChildren<Slider>();
        GameObject value = height.transform.Find("Slider Value Height").gameObject;
        valueSliderHeight = value.GetComponent<TextMeshProUGUI>();
        // Set Value
        sliderHeight.value = globalData.heightWaterTower;
        valueSliderHeight.text = globalData.heightWaterTower.ToString();
    }

    private void StartRho()
    {
        // Get Value
        GameObject rho = dataSlider.transform.Find("Rho").gameObject;
        sliderRho = rho.GetComponentInChildren<Slider>();
        GameObject value = rho.transform.Find("Slider Value Rho").gameObject;
        valueSliderRho = value.GetComponent<TextMeshProUGUI>();
        // Set Value
        sliderRho.value = globalData.rho;
        valueSliderRho.text = globalData.rho.ToString();
    }

    private void StartDiameter()
    {
        // Get Value
        GameObject diameter = dataSlider.transform.Find("Diameter").gameObject;
        sliderDiameter = diameter.GetComponentInChildren<Slider>();
        GameObject value = diameter.transform.Find("Slider Value Diameter").gameObject;
        valueSliderDiameter = value.GetComponent<TextMeshProUGUI>();
        // Set Value
        sliderDiameter.value = globalData.orificeDiameter;
        valueSliderDiameter.text = globalData.orificeDiameter.ToString();
    }

    private void StartPressure()
    {
        // Get Value
        GameObject pressure = dataSlider.transform.Find("Pressure").gameObject;
        sliderPressure = pressure.GetComponentInChildren<Slider>();
        GameObject value = pressure.transform.Find("Slider Value Pressure").gameObject;
        valueSliderPressure = value.GetComponent<TextMeshProUGUI>();
        // Set Value
        sliderPressure.value = globalData.pressure;
        valueSliderPressure.text = globalData.pressure.ToString();
    }
}
