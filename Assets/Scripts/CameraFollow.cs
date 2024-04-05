using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Vector3 _offset;
    private Vector3 _offsetRotation;
    [SerializeField] private Transform target;
    [SerializeField] private float smoothTime;
    private Vector3 _currentVelocity = Vector3.zero;

    private void Awake()
    {
        _offset = transform.position - target.position;
        _offsetRotation = transform.rotation.eulerAngles;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + _offset;
        transform.position = Vector3.Lerp(targetPosition, targetPosition,Time.deltaTime * smoothTime);//Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
        //transform.rotation = Quaternion.Euler(new Vector3(target.position.y * 0.2f,0f,0f) + _offsetRotation);

    }

}
