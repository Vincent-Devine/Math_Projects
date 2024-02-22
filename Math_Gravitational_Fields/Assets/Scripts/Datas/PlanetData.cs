using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlanetData : MonoBehaviour
{
    public float Mass;
    public Vector3 Speed;
    public Vector3 InitSpeed;
    public Vector3 Acceleration;
    public Vector3 Position;
    public Vector3 InitPosition;
    public float Size;
    public GameObject PlanetObject;
    public string Name;

    public GlobalData common;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
