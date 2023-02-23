using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    [SerializeField] CharacterController controller;
    [SerializeField] Transform cam;
    [SerializeField] float speed = 6f;
    [SerializeField] float turnSmoothTime = 0.1f;

    private float turnSmoothVelocity;
    private Animator anim;

    public enum PlayerState
    {
        idle,
        walk,
        attack
    }

    private PlayerState currentState;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponent<Animator>();
        currentState = PlayerState.idle;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        switch (currentState)
        {
            case PlayerState.idle:
                anim.SetTrigger("Idle");
                break;
            case PlayerState.walk:
                anim.SetTrigger("walk");

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                transform.rotation = Quaternion.Euler(0, angle, 0);
                controller.Move(moveDir.normalized * speed * Time.deltaTime);

                break;
            case PlayerState.attack:
                anim.SetTrigger("Attack1");
                break;
            default:
                break;
        }

        if(direction.magnitude >= 0.1f)
        {
            currentState = PlayerState.walk;
            return;
        }
            
        
        currentState = PlayerState.idle;
    }

    private void OnMouseDown()
    {
        Debug.Log("Test");
        currentState = PlayerState.attack;
    }
}
