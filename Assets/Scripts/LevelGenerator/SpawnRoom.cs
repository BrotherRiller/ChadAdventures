using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    [SerializeField] LayerMask whatIsRoom;
    [SerializeField] LevelGenerator levelGen;
    [SerializeField] GameObject levelField;

    private GameObject parentRoom;

    // Update is called once per frame
    void Update()
    {
        Collider[] roomCollider = Physics.OverlapSphere(transform.position, 4, whatIsRoom);

        if (roomCollider.Length == 0 && levelGen.generation == false)
        {
            int rand = Random.Range(1, levelGen.rooms.Length - 1);
            parentRoom = Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);
            parentRoom.transform.SetParent(levelField.transform);
            Destroy(gameObject);
        }
    }
}
