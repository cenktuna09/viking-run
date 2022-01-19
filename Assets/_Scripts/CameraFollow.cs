using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
	public static CameraFollow instance;

	public Transform target;

	public float smoothSpeed = 0.125f;
	public Vector3 offset;

    private void Awake()
    {
		instance = this;
    }

    void Update()
	{
        if (target != null)
        {
            Vector3 desiredPosition = target.localPosition + offset;
            //Vector3 smoothedPosition = Vector3.Lerp(transform.localPosition, desiredPosition, smoothSpeed);
            transform.localPosition = new Vector3(desiredPosition.x / 1.5f, desiredPosition.y, desiredPosition.z);

        }
	
	}
}
