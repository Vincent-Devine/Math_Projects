using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeCameraMode : MonoBehaviour
{
    [Tooltip("Speed of camera")]
    public float speed;

    [Tooltip("Sensibility of camera")]
    public float sensibility;

    private OrbiteMode orbiteMode;
    public GameObject PlanetManager;

    // Start is called before the first frame update
    void Start()
    {
        speed = 10f;
        sensibility = 5f;

        orbiteMode = GetComponent<OrbiteMode>();
        //gameManager = orbiteMode.gameManager;
    }

    // Update is called once per frame
    void Update()
    {
        GetPlanetInfo info = PlanetManager.GetComponent<GetPlanetInfo>();

        if (info.followPlanet)
            return;

        GetInputs();
        GetMouse();

    }

    private void GetInputs()
    {
        Vector3 corectedForward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        Vector3 corectedRight = new Vector3(transform.right.x, 0f, transform.right.z).normalized;
        if (Input.GetKey(KeyCode.W))
            transform.position += corectedForward * speed * Time.unscaledDeltaTime;
        if (Input.GetKey(KeyCode.S))
            transform.position -= corectedForward * speed * Time.unscaledDeltaTime;
        if (Input.GetKey(KeyCode.D))
            transform.position += corectedRight * speed * Time.unscaledDeltaTime;
        if (Input.GetKey(KeyCode.A))
            transform.position -= corectedRight * speed * Time.unscaledDeltaTime;
        if (Input.GetKey(KeyCode.Space))
            transform.position += Vector3.up * speed * Time.unscaledDeltaTime;
        if (Input.GetKey(KeyCode.LeftShift))
            transform.position -= Vector3.up * speed * Time.unscaledDeltaTime;
    }

    private void GetMouse()
    {
        if (!Input.GetMouseButton(1))
        {
            Cursor.visible = true;
            return;
        }

        Cursor.visible = false;
        transform.eulerAngles += sensibility * new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
    }
}
