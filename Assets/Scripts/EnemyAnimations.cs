using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    private Animator anim;
    private int rand;
    private void Awake()
    {
        anim = GetComponent<Animator>();

        rand = Random.Range(0, 3);

    }
    private void Update()
    {
        switch (rand)
        {
            case 0:
                anim.SetTrigger("Idle");
                break;
            case 1:
                anim.SetTrigger("Walk");
                Debug.Log("Test");
                break;
            case 2:
                anim.SetTrigger("Attack");
                break;
            default:
                anim.SetTrigger("Idle");
                break;
        }
    }
}
