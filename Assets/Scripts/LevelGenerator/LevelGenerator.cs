using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] Transform[] startingPosition;
    public GameObject[] rooms; // index 0 --> start B, index 1 --> LR, index 2 --> LRB, index 3 --> LRT, index 4 --> LRTB, index 5 --> end T
    [SerializeField] float moveAmount;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minZ;
    [SerializeField] LayerMask room;

    private float startTimeBetweenRoom = 0.25f; 
    private float timeBetweenRoom;
    private int direction;
    public bool generation = true;
    private int downCounter;

    private void Start()
    {
        int randStartigPos = Random.Range(0, startingPosition.Length);
        transform.position = startingPosition[randStartigPos].position;
        Instantiate(rooms[0], transform.position, Quaternion.identity);

        direction = 5;
        
    }

    private void Update()
    {
        if(timeBetweenRoom <= 0 && generation)
        {
            Move();
            timeBetweenRoom = startTimeBetweenRoom;
        }
        else
        {
            timeBetweenRoom -= Time.deltaTime;
        }
    }

    private void Move()
    {
        Debug.Log(downCounter);
        if (direction == 1 || direction == 2) // Move Right
        {

            if (transform.position.x >= maxX)
            {
                direction = 5;
                return;
            }
            downCounter = 0;

            Vector3 newPos = new Vector3(transform.position.x + moveAmount, 0, transform.position.z);
            transform.position = newPos;

            int rand = Random.Range(1, rooms.Length - 1 );
            Instantiate(rooms[rand], transform.position, Quaternion.identity);

            direction = Random.Range(1, 6);
            if(direction == 3 || direction == 4)
            {
                direction = 2;
            }
        } 
        else if(direction == 3 || direction == 4) //Move Left
        {

            if(transform.position.x <= minX)
            {
                direction = 5;
                return;
            }
            downCounter = 0;

            Vector3 newPos = new Vector3(transform.position.x - moveAmount, 0, transform.position.z);
            transform.position = newPos;

            int rand = Random.Range(1, rooms.Length - 1);
            Instantiate(rooms[rand], transform.position, Quaternion.identity);

            direction = Random.Range(3, 6); 
        } 
        else if(direction == 5) // Move Down
        {
            downCounter++;

            Collider[] roomDetection = Physics.OverlapSphere(transform.position, 1, room);
            foreach( var roomCollider in roomDetection)
            {
                if (transform.position.z <= minZ)
                {
                    if(roomCollider.GetComponent<RoomType>().type != 2 && roomCollider.GetComponent<RoomType>().type != 4)
                    {
                        if (downCounter >= 2)
                        {
                            roomCollider.GetComponent<RoomType>().RoomDestruction();
                            Instantiate(rooms[4], transform.position, Quaternion.identity);
                        }
                        else
                        {
                            roomCollider.GetComponent<RoomType>().RoomDestruction();

                            int randBottomRoom = Random.Range(2, 5);
                            if (randBottomRoom == 3)
                            {
                                randBottomRoom = 2;
                            }
                            Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                        }
                    }

                    Vector3 endPos = new Vector3(transform.position.x, 0, transform.position.z - moveAmount);
                    transform.position = endPos;

                    Instantiate(rooms[rooms.Length - 1], transform.position, Quaternion.identity);

                    generation = false;
                    return;
                }
                else if (roomCollider.GetComponent<RoomType>().type != 2 && roomCollider.GetComponent<RoomType>().type != 4)
                {
                    if(downCounter >= 2)
                    {
                        roomCollider.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[4], transform.position, Quaternion.identity);
                    }
                    else
                    {
                        roomCollider.GetComponent<RoomType>().RoomDestruction();

                        int randBottomRoom = Random.Range(2, 5);
                        if (randBottomRoom == 3)
                        {
                            randBottomRoom = 2;
                        }
                        Instantiate(rooms[randBottomRoom], transform.position, Quaternion.identity);
                    }
                }
            }

            Vector3 newPos = new Vector3(transform.position.x, 0, transform.position.z - moveAmount);
            transform.position = newPos;

            int rand = Random.Range(3, rooms.Length - 1);
            Instantiate(rooms[rand], transform.position, Quaternion.identity);

            direction = Random.Range(1, 6);
        }

    }
}
