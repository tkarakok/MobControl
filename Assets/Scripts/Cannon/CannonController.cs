using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;

public class CannonController : Singleton<CannonController>
{
    public Transform animatedObject, targetPoint, backPoint;
    public Image cannonBoostImage;
    public List<Transform> cannonPoints;


    private float _timer;
    private float _xSpeed = 25;
    private int _currentCannonPosition = 0;

    private void Start()
    {
        ResetBoostImageAmount();
        MoveNewCannonPosition();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        float _touchXDelta = 0;
        float _newX = 0;

        if (StateManager.Instance.state == State.InGame)
        {

            if (Input.GetMouseButton(0))
            {
                _touchXDelta = Input.GetAxis("Mouse X");
                _newX = transform.position.x + _xSpeed * _touchXDelta * Time.deltaTime;
                _newX = Mathf.Clamp(_newX, -3, 3);
                transform.position = new Vector3(_newX, transform.position.y, transform.position.z);

                if (_timer >= .2f)
                {
                    _timer = 0;
                    // check boost value and if boost value equals max value we instantiate big alias player
                    if (CheckBoostForCannon())
                    {
                        CharacterPoolManager.Instance.GetPlayer(true, GameManager.Instance.forcePoint);
                        ResetBoostImageAmount();
                    }
                    else
                    {
                        CharacterPoolManager.Instance.GetPlayer(false, GameManager.Instance.forcePoint);
                        cannonBoostImage.fillAmount += .05f;
                    }
                    AnimateWave();
                }

            }

        }


    }

    void ResetBoostImageAmount()
    {
        cannonBoostImage.fillAmount = 0;
    }

    bool CheckBoostForCannon()
    {
        if (cannonBoostImage.fillAmount == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void AnimateWave()
    {
        animatedObject.DOMoveZ(targetPoint.position.z, .1f).OnComplete(() =>
        {
            animatedObject.transform.position = backPoint.position;
        });
    }

    #region CANNON POSİTİON
    public void MoveNewCannonPosition()
    {
        StateManager.Instance.state = State.CannonMove;
        if (_currentCannonPosition <= cannonPoints.Count)
        {
            transform.DOMove(cannonPoints[_currentCannonPosition].position, 2).OnComplete(
                () =>
                {
                    StateManager.Instance.state = State.InGame;
                    GameManager.Instance.ChangeTarget();
                }
            );
        }
    }

    public void ChangeCannonPosition()
    {
        _currentCannonPosition++;
        MoveNewCannonPosition();
    }
    #endregion
}
