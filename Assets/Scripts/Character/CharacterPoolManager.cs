using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterPoolManager : Singleton<CharacterPoolManager>
{
    [Serializable]
    public struct Character
    {
        public GameObject prefab;
        public Transform parent;
        public int total;
        public Queue<GameObject> queue;
    };

    public Character[] characters;


    private void Awake()
    {
        // we instantiate all characters
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].queue = new Queue<GameObject>();
            for (int j = 0; j < characters[i].total; j++)
            {
                GameObject character = Instantiate(characters[i].prefab);
                character.transform.SetParent(characters[i].parent);
                character.SetActive(false);
                characters[i].queue.Enqueue(character);
            }
        }
    }

    #region GetCaharcter Func
    /// <summary>
    /// get object for battle in queue
    /// </summary>
    /// <param name="bigPlayer"> check object type </param>
    /// <returns></returns>
    public GameObject GetPlayer(bool bigPlayer, Transform position)
    {

        GameObject player;
        int index;
        if (!bigPlayer)
        {
            player = characters[0].queue.Dequeue();
            index = 0;
        }
        else
        {
            player = characters[1].queue.Dequeue();
            index = 1;
        }

        player.SetActive(true);
        player.transform.position = position.position + new Vector3(0,0,.25f);
        player.GetComponent<Rigidbody>().AddForce(Vector3.forward * GameManager.Instance.forceSpeedForPlayer);
        characters[index].queue.Enqueue(player);
        GameManager.Instance.ActiveCharacters.Add(player);
        return player;
    }


    /// <summary>
    /// get enemy object for battle
    /// </summary>
    /// <param name="bigEnemy"> check object type </param>
    /// <returns></returns>
    public GameObject GetEnemy(bool bigEnemy, Transform position)
    {
        GameObject enemy;
        int index;
        if (!bigEnemy)
        {
            enemy = characters[2].queue.Dequeue();
            index = 2;
        }
        else
        {
            enemy = characters[3].queue.Dequeue();
            index = 3;
        }
        enemy.SetActive(true);
        enemy.transform.position = position.position;
        enemy.GetComponent<Rigidbody>().AddForce(Vector3.back * GameManager.Instance.forceSpeedForPlayer);
        characters[index].queue.Enqueue(enemy);
        GameManager.Instance.ActiveCharacters.Add(enemy);
        return enemy;
    }
    #endregion



}
