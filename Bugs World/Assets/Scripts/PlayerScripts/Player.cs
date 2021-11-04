using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputsActions _playerInputsActions;
    private PlayerController _playerController;
    [Header("Movement parameters")]
    public Vector3 currentMovement, currentRunMovement;
    public Vector2 inputsVector;
    public Vector3 appliedMovement;
    public bool isRunPressed, isMovementPressed;
    [SerializeField] private float walkMultiplier = 2f;
    [SerializeField] private float runMultiplier = 3f;

    public float rotationPower = 1f;
    //public Transform camera;
   // public Quaternion camRot;
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        //Player Inputs callback 
        _playerInputsActions = new PlayerInputsActions();
        _playerInputsActions.Player.Movement.started += OnMovementInput;
        _playerInputsActions.Player.Movement.canceled += OnMovementInput; 
        _playerInputsActions.Player.Movement.performed += OnMovementInput;
        _playerInputsActions.Player.Run.started += OnRun;
        _playerInputsActions.Player.Run.canceled += OnRun;
        
        //camera = Camera.main.transform;
    }

    private void OnRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        //On récupère grace au context la valeur des inputs
        inputsVector = context.ReadValue<Vector2>();
        
        currentMovement.x = inputsVector.x * walkMultiplier;
        currentMovement.z = inputsVector.y * walkMultiplier;
        
        currentRunMovement.x = inputsVector.x * runMultiplier;
        currentRunMovement.z = inputsVector.y * runMultiplier;
        
        isMovementPressed = inputsVector.x != 0 || inputsVector.y != 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
        
      
    }


 
    
    private void OnEnable()
    {
        _playerInputsActions.Player.Enable();
    }

    private void OnDisable()
    {
        _playerInputsActions.Player.Disable();
    }
}
