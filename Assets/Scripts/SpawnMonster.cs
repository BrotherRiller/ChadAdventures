using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMonster : MonoBehaviour
{
    [SerializeField] GameObject[] monsters;
    [SerializeField] GameObject[] spawner;

    private GameObject parent;

    // Start is called before the first frame update
    void Awake()
    {
        foreach (var spawn in spawner)
        {
            int rand = Random.Range(0, monsters.Length);
            parent = Instantiate(monsters[rand], spawn.transform.position, Quaternion.identity);
            parent.transform.SetParent(gameObject.transform);
        }
    }
}
