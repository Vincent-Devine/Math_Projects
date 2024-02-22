using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlSimSpeed : MonoBehaviour
{
    public GlobalData common;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = common.SimSpeed;
    }

    public void OnValueChanged()
    {
        common.SimSpeed = slider.value;
    }
}
