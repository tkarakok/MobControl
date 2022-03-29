using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerController : MonoBehaviour
{
    public int health;
    public int smallEnemyCount, bigEnemyCount;
    public float enemyInstantiateSpeed;
    public Transform enemySpawnPoint;

    TextMeshPro healthText;


    private void Start() {
        healthText = transform.GetChild(0).GetComponent<TextMeshPro>();
        healthText.text = health.ToString();
    }

    private void OnEnable() {
        StartCoroutine(StartInstantiateEnemy());
    }

    public void CheckHealth(){
        health--;
        healthText.text = health.ToString();
        if (health == 0)
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator StartInstantiateEnemy(){
        while (gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(.25f);
            for (int i = 0; i < smallEnemyCount; i++)
            {
                CharacterPoolManager.Instance.GetEnemy(false,enemySpawnPoint);
            }
            for (int i = 0; i < bigEnemyCount; i++)
            {
                CharacterPoolManager.Instance.GetEnemy(true,enemySpawnPoint);
            }
            yield return new WaitForSeconds(enemyInstantiateSpeed);
        }
    }


}
