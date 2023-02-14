using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    [SerializeField] LayerMask whatIsRoom;
    [SerializeField] LevelGenerator levelGen;

    private void Start()
    {
        levelGen = GetComponent<LevelGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] roomCollider = Physics.OverlapSphere(transform.position, 10, whatIsRoom);
        foreach (var roomDetection in roomCollider)
        {
            if(roomDetection == null && levelGen.generation)
            {
                int rand = Random.Range(0, levelGen.rooms.Length);
                Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
