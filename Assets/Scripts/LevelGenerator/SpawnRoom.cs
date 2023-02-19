using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRoom : MonoBehaviour
{
    [SerializeField] LayerMask whatIsRoom;
    [SerializeField] LayerMask checkGoldenRoom;
    [SerializeField] LevelGenerator levelGen;
    [SerializeField] GameObject goldenRoom;



    // Update is called once per frame
    void Update()
    {
        Collider[] goldRoomCollider = Physics.OverlapSphere(transform.position, 200, checkGoldenRoom);
        Collider[] roomCollider = Physics.OverlapSphere(transform.position, 1, whatIsRoom);



        if (roomCollider.Length == 0 && levelGen.generation == false)
        {
            int goldRoomChance = Random.Range(0, 2);
            Debug.Log(goldRoomChance);
            if(goldRoomCollider.Length == 0 && goldRoomChance == 0)
            {
                Debug.Log("sollte nur 1 Mal stehen");
                Instantiate(goldenRoom, transform.position, Quaternion.identity);
                Destroy(gameObject);
                return;
            }
            int rand = Random.Range(1, levelGen.rooms.Length - 1);
            Instantiate(levelGen.rooms[rand], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
