using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] GameObject nextLevel;
    [SerializeField] GameObject sun;
    [SerializeField] float rotation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            nextLevel.SetActive(true);
            this.gameObject.SetActive(false);
            sun.transform.Rotate(Vector3.right * rotation);
        }
    }

}
