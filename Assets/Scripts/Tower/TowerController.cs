using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TowerController : MonoBehaviour
{

    #region Public Fields
    public int health;
    public int smallEnemyCount, bigEnemyCount;
    public float enemyInstantiateSpeed;
    public Transform enemySpawnPoint;
    public ParticleSystem hitEffect;
    #endregion

    #region Private Fields
    TextMeshPro healthText;
    #endregion
        

    private void Start()
    {
        healthText = transform.GetChild(0).GetComponent<TextMeshPro>();
        healthText.text = health.ToString();
    }

    private void OnEnable()
    {
        StartCoroutine(StartInstantiateEnemy());
    }

    // we check tower health when agent hits a tower
    public void CheckHealth()
    {
        health--;
        transform.DOShakePosition(.25f, new Vector3(.2f, 0, .2f), 5);
        hitEffect.Play();
        healthText.text = health.ToString();
        if (health <= 0)
        {
            UIManager.Instance.StartLevelProgressBarUpdate();
            if (GameManager.Instance.CheckGameOver())
            {
                EventManager.Instance.EndGame();
                gameObject.SetActive(false);
            }
            else
            {
                EventManager.Instance.CannonMove();
                gameObject.SetActive(false);
            }
        }
    }

    // tower enemy wave func
    IEnumerator StartInstantiateEnemy()
    {
        while (gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(enemyInstantiateSpeed);
            for (int i = 0; i < smallEnemyCount; i++)
            {
                CharacterPoolManager.Instance.GetEnemy(false, enemySpawnPoint);
            }
            for (int i = 0; i < bigEnemyCount; i++)
            {
                CharacterPoolManager.Instance.GetEnemy(true, enemySpawnPoint);
            }
        }
    }


}
