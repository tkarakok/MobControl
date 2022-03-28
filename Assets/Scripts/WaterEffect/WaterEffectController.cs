using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WaterEffectController : MonoBehaviour
{
    public Vector2 scrollRate;

    Vector2 _offSet;
    Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        StartCoroutine(ScrollWaterEffect());
    }

    IEnumerator ScrollWaterEffect()
    {
        while (true)
        {
            _offSet += scrollRate;
            _renderer.material.DOOffset(_offSet, 2).OnComplete(() =>
            {
                _offSet -= scrollRate * 2;
                _renderer.material.DOOffset(_offSet, 2);
            });
            yield return new WaitForSeconds(4);
        }
    }



}
