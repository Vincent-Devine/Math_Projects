using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particule : MonoBehaviour
{
    public float massDensity;
    public float pressure;
    public List<Particule> neighbors;
    public Vector3 speed;
    public Vector3 acceleration;
    public Vector3 pressureForce;
    public Vector3 viscosityForce;
    public Vector3 gravity;
    public float restitution;
}
