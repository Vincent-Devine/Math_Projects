using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySphereCollider : MyCollider
{
    float radius;

    public void Start()
    {
        radius = transform.localScale.x / 2f;
        AddSelfToGameManager();
    }

    public override CollisionResult ImplicitFunction(Particule particule)
    {
        CollisionResult result;

        Vector3 direction = particule.transform.position - transform.position;
        float particuleScale = particule.transform.localScale.x / 2f;
        if (isContainer)
            particuleScale *= -1f;

        result.output = direction.sqrMagnitude - Mathf.Pow(radius + particuleScale, 2f);
        if(isContainer)
            result.output *= -1f;

        result.contactPoint = transform.position + radius * direction.normalized;

        result.penetrationDepth = Mathf.Abs(direction.magnitude - radius - particuleScale);

        result.surfaceNormal = Mathf.Sign(result.output) * direction.normalized;
        if (!isContainer)
            result.surfaceNormal *= -1f;

        return result;
    }
}
