using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    [SerializeField] CharacterController controller;
    [SerializeField] Transform cam;
    [SerializeField] float speed = 6f;
    [SerializeField] float turnSmoothTime = 0.1f;
    [SerializeField] float jumpSpeed;
    [SerializeField] GameObject damageDealer;

    private float turnSmoothVelocity;
    private float gravity;
    private CharacterController character;
    private float originalStepOffset;
    private Animator anim;
    private bool isAttacking;

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
        character = GetComponent<CharacterController>();
        originalStepOffset = character.stepOffset;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        gravity += Physics.gravity.y * Time.deltaTime;

        Vector3 velocity = direction * direction.magnitude;
        velocity.y = gravity;

        character.Move(velocity * Time.deltaTime);
        if (character.isGrounded)
        {
            character.stepOffset = originalStepOffset;
            gravity = -0.5f;
            if (Input.GetButtonDown("Jump"))
            {
                gravity = jumpSpeed;
            }
        }
        else
        {
            character.stepOffset = 0;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            currentState = PlayerState.attack;
            Debug.Log("Test");
        }

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
                StartCoroutine(Attack());
                break;
            default:
                break;
        }

        if(direction.magnitude >= 0.1f && !isAttacking)
        {
            currentState = PlayerState.walk;
            return;
        }
        else if(direction.magnitude <=0f && !isAttacking){
            currentState = PlayerState.idle;
        }

        
    }

    public IEnumerator Attack()
    {
        isAttacking = true;
        anim.SetTrigger("Attack1");
        character.center = new Vector3(0, 2.5f , 6);
        yield return new WaitForSecondsRealtime(0.4f);
        character.center = new Vector3(0, 2.5f, 0);
        isAttacking = false;
        currentState = PlayerState.idle;
        
    }
}
