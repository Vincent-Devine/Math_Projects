using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBoxCollider : MyCollider
{
	Vector3 extend;

	public void Start()
	{
		AddSelfToGameManager();
		extend = transform.localScale / 2f;
	}

	static Vector3 Abs(Vector3 v)
	{
		return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
	}

	static float Max(Vector3 v)
	{
		if(v.x >= v.y && v.x >= v.z)
			return v.x;
		if (v.y >= v.x && v.y >= v.z)
			return v.y;
		return v.z;
	}

	static Vector3 Max(Vector3 v1, Vector3 v2)
	{
		Vector3 res;
		res.x = v1.x >= v2.x ? v1.x : v2.x;
		res.y = v1.y >= v2.y ? v1.y : v2.y;
		res.z = v1.z >= v2.z ? v1.z : v2.z;
		return res;
	}

	static Vector3 Min(Vector3 v1, Vector3 v2)
	{
		Vector3 res;
		res.x = v1.x <= v2.x ? v1.x : v2.x;
		res.y = v1.y <= v2.y ? v1.y : v2.y;
		res.z = v1.z <= v2.z ? v1.z : v2.z;
		return res;
	}

	static Vector3 Sign(Vector3 v)
	{
		return new Vector3(Mathf.Sign(v.x), Mathf.Sign(v.y), Mathf.Sign(v.z));
	}

	public override CollisionResult ImplicitFunction(Particule particule)
	{
		Vector3 rotEuler = transform.rotation.eulerAngles;
		Quaternion finalRot = Quaternion.Euler(-rotEuler);
		Matrix4x4 rotation = Matrix4x4.Rotate(finalRot);
		Vector3 local = rotation * (particule.transform.position - transform.position);

		CollisionResult result;
		
		result.output = Max(Abs(local) - extend);

        Vector3 localContactPoint = Min(extend, Max(-extend, local));
		
		result.contactPoint = rotation.transpose * localContactPoint;
		result.contactPoint += transform.position;


        result.penetrationDepth = (result.contactPoint - particule.transform.position).magnitude;

		result.surfaceNormal = rotation * Sign(localContactPoint - local).normalized;

		return result;
	}
}
