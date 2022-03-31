using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int value;
    public bool moveable;


    private float _speed = 1;
    private float _amount = 1;


    private void Start() {
        if (moveable)
        {
            StartCoroutine(HorizontalMove());
        }
    }

    IEnumerator HorizontalMove(){
        while (true)
        {
            transform.parent.position = new Vector3(Mathf.Sin(Time.time * _speed) * _amount, transform.parent.position.y, transform.parent.position.z);
            yield return new WaitForSeconds(.01f);
        }
    }

}
