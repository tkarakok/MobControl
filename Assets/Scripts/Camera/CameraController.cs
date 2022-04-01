using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private Vector3 _offsetZ;


    private void Start()
    {
        _offsetZ = gameObject.transform.position - _target.position;
    }

    private void LateUpdate()
    {
        if (StateManager.Instance.state == State.CannonMove)
        {
            Vector3 targetPosition = new Vector3(transform.position.x,_target.position.y,_target.position.z) + _offsetZ;
            transform.position = targetPosition;
        }

    }
}