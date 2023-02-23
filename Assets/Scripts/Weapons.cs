using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] GameObject sword;
    [SerializeField] GameObject weaponSlot;

    Vector3 axeRotOffset = new Vector3(0, 90f, 20);
    Vector3 axePosOffset = new Vector3(0, 0, 0);

    void Start()
    {
        Debug.Log("Test");
        GameObject weapon;

        weapon = Instantiate(sword, weaponSlot.transform.position + axePosOffset, Quaternion.Euler(axeRotOffset));
        weapon.transform.SetParent(weaponSlot.transform);
    }
}
