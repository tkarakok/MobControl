using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CannonController : MonoBehaviour
{
    public Transform animatedObject, targetPoint;
    public Image cannonBoostImage;

    private Vector3 _firstPosition;

    private void Start()
    {
        _firstPosition = animatedObject.transform.position;
        ResetBoostImageAmount();
    }

    private void Update()
    {
        if (StateManager.Instance.state == State.InGame)
        {
            if (Input.GetMouseButtonDown(0))
            {
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
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                GameManager.Instance.ChangeTarget();
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
            animatedObject.transform.position = _firstPosition;
        });
    }
}
