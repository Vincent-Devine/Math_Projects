using UnityEngine;

public struct CollisionResult
{
    public float output;
    public Vector3 contactPoint;
    public float penetrationDepth;
    public Vector3 surfaceNormal;
}

public abstract class MyCollider : MonoBehaviour
{
    public bool isContainer = false;

    protected void AddSelfToGameManager()
    {
        while (1f / Time.deltaTime < 5f) { };
        GameObject manager = GameObject.Find("GameManager");
        if (manager)
        {
            Acceleration accScript = manager.GetComponent<Acceleration>();
            if (accScript)
                accScript.colliders.Add(this);
        }
    }

    abstract public CollisionResult ImplicitFunction(Particule particule);

    static public void CollisionResponse(CollisionResult result, Particule particule)
    {
        particule.transform.position += result.penetrationDepth * result.surfaceNormal;
        particule.speed -= (1f + particule.restitution * result.penetrationDepth / (Time.deltaTime * particule.speed.magnitude)) * Vector3.Dot(particule.speed, result.surfaceNormal) * result.surfaceNormal;
    }
};
