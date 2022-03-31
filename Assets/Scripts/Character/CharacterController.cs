using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    public CharacterSettings characterSettings;

    int _triggerCounter;
    NavMeshAgent _agent;
    bool _activeDestination;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _triggerCounter = characterSettings.totalTrigger;
        ResetAgent();
    }

    // Update is called once per frame
    void Update()
    {
        if (StateManager.Instance.state == State.InGame && gameObject.activeInHierarchy)
        {
            if (_activeDestination)
            {
                _agent.SetDestination(CheckTarget().position);
            }
            else
            {
                _agent.Move(FindWay() * Time.deltaTime * 2f);
            }
        }
    }

    
    void ResetAgent(){
        transform.localScale = characterSettings.characterScale;
        _activeDestination = false;
    }

    #region Target Check
    public Transform CheckTarget()
    {
        if (characterSettings.chracterType == ChracterType.player)
        {
            return GameManager.Instance.Target;
        }
        else
        {
            return GameManager.Instance.cannon;
        }
    }
    #endregion

    #region Find Way
    public Vector3 FindWay()
    {
        if (characterSettings.chracterType == ChracterType.player)
        {
            return Vector3.forward;
        }
        else
        {
            return Vector3.back;
        }
    }
    #endregion


    #region CollisionDetect
    void CheckCollision()
    {
        _triggerCounter--;
        if (_triggerCounter == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            transform.localScale -= new Vector3(.1f, .1f, .1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (characterSettings.chracterType == ChracterType.player)
        {
            if (other.CompareTag("Obstacle"))
            {
                for (int i = 1; i < other.GetComponent<Obstacle>().value; i++)
                {
                    CharacterPoolManager.Instance.GetPlayer(characterSettings.big, transform);
                }
            }
            else if (other.CompareTag("Destination"))
            {
                _activeDestination = true;
            }
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        if (characterSettings.chracterType == ChracterType.player)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                CheckCollision();
                ResetAgent();
            }

        }
        else
        {
            if (other.gameObject.CompareTag("Player"))
            {
                ResetAgent();
                CheckCollision();
            }
            else if (other.gameObject.CompareTag("Cannon"))
            {
                gameObject.SetActive(false);
                EventManager.Instance.GameOver();
            }
        }

    }

    private void OnCollisionStay(Collision other)
    {
        if (characterSettings.chracterType == ChracterType.player)
        {
            if (other.gameObject.CompareTag("Tower"))
            {
                gameObject.SetActive(false);
                other.gameObject.GetComponent<TowerController>().CheckHealth();
                ResetAgent();
            }
        }

    }
    #endregion

}
