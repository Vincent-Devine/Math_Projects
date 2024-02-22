using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbiteMode : MonoBehaviour
{
    [Header("Orbite mode")]
    [Tooltip("Selected planet")]
    public GameObject planet = null;

    [Tooltip("Speed of zoom")]
    public float speedZoom;
    [Tooltip("Speed of colatitude and longitude deplacment")]
    public float speedColatitudeLongitude;

    public GameObject gameManager;
    private float radius;
    private float rotationX;
    private float rotationY;

    // Start is called before the first frame update
    void Start()
    {
        radius = 5f;
        speedZoom = 100f;
        speedColatitudeLongitude = 1000f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.GetComponent<GetPlanetInfo>().followPlanet || planet == null)
            return;

        Zoom();
        ColatitudeLongitude();

        transform.position = planet.transform.position - transform.forward * radius;
    }

    private void Zoom()
    {
        radius -= Input.mouseScrollDelta.y * Time.unscaledDeltaTime * speedZoom;
        if (planet.transform.localScale.x < 0.32f)
        {
            if (radius < 0.32f)
                radius = 0.32f;
            else if (radius > 0.64f)
                radius = 0.64f;
            return;
        }
        if (radius < planet.transform.localScale.x * 0.75f)
            radius = planet.transform.localScale.x * 0.75f;
        else if (radius > planet.transform.localScale.x * 2f)
            radius = planet.transform.localScale.x * 2f;
    }

    private void ColatitudeLongitude()
    {
        if (!Input.GetMouseButton(1))
            return;

        float mouseX = Input.GetAxis("Mouse X") * speedColatitudeLongitude * Time.unscaledDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * speedColatitudeLongitude * Time.unscaledDeltaTime;

        rotationY += mouseX;
        rotationX -= mouseY;

        rotationX = Mathf.Clamp(rotationX, -75f, 75f);

        transform.localEulerAngles = new Vector3(rotationX, rotationY);
    }
}
