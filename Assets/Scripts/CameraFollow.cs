using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Vector3 _offset;
    private Vector3 _offsetRotation;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    private float finalAngle = 0f;
    //private Vector3 _currentVelocity = Vector3.zero;
    private Vector3 _offsetAdd;

    private void Awake()
    {
        _offset = transform.position - target.position;
        _offsetRotation = transform.rotation.eulerAngles;

    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + _offset + _offsetAdd;
        transform.position = Vector3.Lerp(targetPosition, targetPosition,Time.deltaTime * smoothTime);//Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(_offsetRotation);
        RotateCamera();
    }

    private void RotateCamera()
    {
        float angle = target.position.y * 0.1f;
        angle = Mathf.Clamp(angle, -3, 3);
        finalAngle = Mathf.Lerp(finalAngle, angle, Time.deltaTime);
        _offsetAdd.y = angle - 3f;
        _offsetRotation.x = angle * 3f;

    }

}
