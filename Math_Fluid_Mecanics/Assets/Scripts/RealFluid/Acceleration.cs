using System.Collections.Generic;
using UnityEngine;

public class Acceleration : MonoBehaviour
{
    public RealGlobalData data;
    public List<Particule> particules;
    public GameObject particulePrefabs;
    public GameObject particleParent;
    public float speed;
    public float time;
    public float interval;
    public int inc;
    public float ticTac;
    public float angle;
    public List<MyCollider> colliders = new List<MyCollider>();

    void Start()
    {
        angle = 0f;
        inc = 0;
        time = 0f;
        ticTac = 1f;
        Time.timeScale = .05f;
    }

    void SpawnParticles()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject newParticule = Instantiate(particulePrefabs, particleParent.transform);
            newParticule.name = "Particule_" + inc;
            Particule newPart = newParticule.GetComponent<Particule>();
            newPart.restitution = data.restitution;
            newPart.neighbors = new List<Particule>();
            particules.Add(newPart);
            float curAngle = angle;
            if (i % 4 == 1)
                curAngle = Mathf.PI - angle;
            if (i % 4 == 2)
                curAngle = Mathf.PI / 2f - angle;
            if (i % 4 == 3)
                curAngle = Mathf.PI / 2f + angle;
            newPart.speed = new Vector3(Mathf.Cos(-curAngle), Mathf.Sin(-curAngle), 0f) * speed;
            inc++;
        }
        if (angle > Mathf.PI / 4f || angle < 0f)
            ticTac *= -1f;
        angle += ticTac * Mathf.Deg2Rad * 2.5f;
        time = 0f;
    }

    void Update()
    {
        if (1f / Time.unscaledDeltaTime < 2f)
            return;
        if (particleParent.transform.childCount < data.particleAmount)
        {
            time += Time.unscaledDeltaTime;
            if (time > interval)
                SpawnParticles();
        }
        foreach (Particule particule in particules)
        {
            FindNeighbors(particule);
            MassDensity(particule);
            Pressure(particule);
        }
        foreach (Particule particule in particules)
        {
            InternalForces(particule);
            ExternalForces(particule);

            particule.acceleration = (particule.pressureForce + particule.viscosityForce) / particule.massDensity + particule.gravity;

            particule.speed += particule.acceleration * Time.deltaTime;

            particule.transform.position += particule.speed * Time.deltaTime;
        }
        foreach(Particule particule in particules)
        {
            foreach(MyCollider collider in colliders)
            {
                CollisionResult collisionResult = collider.ImplicitFunction(particule);
                if (collisionResult.output <= 0f)
                    MyCollider.CollisionResponse(collisionResult, particule);
            }
        }
    }

    void FindNeighbors(Particule particule)
    {
        particule.neighbors.Clear();
        foreach (Particule part in particules)
        {
            if ((part.transform.position - particule.transform.position).magnitude <= data.supportRadius)
                particule.neighbors.Add(part);
        }
    }

    void MassDensity(Particule particule)
    {
        particule.massDensity = 0f;
        foreach(Particule neighbor in particule.neighbors)
        {
            Vector3 particleDirection = particule.transform.position - neighbor.transform.position;
            particule.massDensity += Kernels.Default(particleDirection, data.supportRadius);
        }
        particule.massDensity *= data.particleMass;
    }

    void Pressure(Particule particule)
    {
        particule.pressure = data.gasStiffness * (particule.massDensity - data.restDensity);
        particule.pressure = Mathf.Abs(particule.pressure);
    }
    
    void InternalForces(Particule particule)
    {
        // Pressure
        particule.pressureForce = Vector3.zero;
        foreach (Particule neighbor in particule.neighbors)
        {
            if (neighbor == particule)
                continue;
            Vector3 particleDirection = particule.transform.position - neighbor.transform.position;
            particule.pressureForce += (particule.pressure + neighbor.pressure) / (2f * neighbor.massDensity) * Kernels.Pressure(particleDirection, data.supportRadius);
        }
        particule.pressureForce *= -data.particleMass;

        // Viscosity
        particule.viscosityForce = Vector3.zero;
        foreach (Particule neighbor in particule.neighbors)
        {
            if (neighbor == particule)
                continue;
            Vector3 particleDirection = particule.transform.position - neighbor.transform.position;
            particule.viscosityForce += (neighbor.speed - particule.speed) / neighbor.massDensity * Kernels.Viscosity(particleDirection, data.supportRadius);
        }
        particule.viscosityForce *= data.viscosity * data.particleMass;
    }

    void ExternalForces(Particule particule)
    {
        // Gravity
        particule.gravity = data.gravity;
    }
}
