using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CannonController : MonoBehaviour
{
    public Transform animatedObject, targetPoint;

    private Vector3 _firstPosition;

    private void Start()
    {
        _firstPosition = animatedObject.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CharacterPoolManager.Instance.GetPlayer(false, GameManager.Instance.forcePoint);
            AnimateWave();
        }else if (Input.GetKeyDown(KeyCode.S))
        {
            CharacterPoolManager.Instance.GetPlayer(true, GameManager.Instance.forcePoint);
            AnimateWave();
        }
    }

    void AnimateWave()
    {
        animatedObject.DOMoveZ(targetPoint.position.z, .1f).OnComplete(() =>
        {
            animatedObject.transform.position = _firstPosition;
        });
    }
}
