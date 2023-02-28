using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    //Haben nicht wirklich Stats, aber man kann sie töten indem man sie schlägt oder diese umrennt, ich hab ein wenig getrickst, weil Character Controller etwas komisch ist mit Trigger

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var charContrl = other.GetComponent<CharacterController>();
            charContrl.enabled = false;
            Destroy(this.gameObject);
            charContrl.enabled = true;
        }
    }
}
