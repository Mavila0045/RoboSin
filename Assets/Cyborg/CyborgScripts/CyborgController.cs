using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CyborgController : MonoBehaviour
{
    // declare reference variables
    CyborgMovement playerInput;
    CharacterController characterController;
    public Transform cam;
    
    // variables to store player input values
    Vector3 direction;
    Vector3 moveDir;
    bool isMovementPressed;

    float targetAngle;
    float angle;
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    

    // called earlier than Start within UNity's event life cycle
    void Awake()
    {
        Cursor.visible = true;
        //initializes the reference variable
        playerInput = new CyborgMovement();
        characterController = GetComponent<CharacterController>();

        playerInput.Player.Move.started += onMovementInput;
        playerInput.Player.Move.canceled += onMovementInput;
        playerInput.Player.Move.performed += onMovementInput;
    }

    void onMovementInput(InputAction.CallbackContext context)
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0f, vertical).normalized;

        isMovementPressed = direction.x != 0 || direction.y != 0;
        print(isMovementPressed);
    }

    void playerRotation()
    {
        targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg +  cam.eulerAngles.y;
        angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
        moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
    }

    void Update()
    {   
        if(direction.magnitude >= 0.1f)
        {
            playerRotation();
            characterController.Move(moveDir.normalized * Time.deltaTime);
        }
        
    }

    void OnEnable()
    {
        // enable the character controls action map
        playerInput.Player.Enable();
    }

    void OnDisable()
    {
        // disbale the character controls action map
        playerInput.Player.Disable();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Traps"))
        {
            playerInput.Player.Disable();
            characterController.enabled = false;
            other.gameObject.SetActive(false);
            cam.position = new Vector3(cam.position.x, cam.position.y, cam.position.z);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            playerInput.Player.Disable();
            characterController.enabled = false;
            cam.position = new Vector3(cam.position.x, cam.position.y, cam.position.z);
        }
    }
}