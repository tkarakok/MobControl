using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    public CharacterSettings characterSettings;

    int _triggerCounter;
    Transform _target;
    NavMeshAgent _agent;


    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _triggerCounter = characterSettings.totalTrigger;
        CheckTarget();
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(_target.transform.position);
    }


    #region Target Check
    public void CheckTarget()
    {
        if (characterSettings.chracterType == ChracterType.player)
        {
            _target = GameManager.Instance.targets[0];
        }
        else
        {
            _target = GameManager.Instance.cannon;
        }
    }
    #endregion

    #region CollisionDetect
    void CheckCollision()
    {
        _triggerCounter--;
        if (_triggerCounter == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.localScale -= new Vector3(.1f, .1f, .1f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            for (int i = 1; i < other.GetComponent<Obstacle>().value; i++)
            {
                CharacterPoolManager.Instance.GetPlayer(characterSettings.big, transform);
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
            }

        }
        else
        {
            if (other.gameObject.CompareTag("Player"))
            {
                CheckCollision();
            }
        }

    }
    #endregion

}
