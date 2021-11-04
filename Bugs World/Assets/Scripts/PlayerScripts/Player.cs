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
    private Vector3 _currentMovement, _currentRunMovement;
    private Vector2 _inputsVector;
    public Vector3 appliedMovement;
    public bool isRunPressed, isMovementPressed;
    [SerializeField] private float walkMultiplier = 2f;
    [SerializeField] private float runMultiplier = 3f;
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
    }

    private void OnRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        //On récupère grace au context la valeur des inputs
        _inputsVector = context.ReadValue<Vector2>();
        
        _currentMovement.x = _inputsVector.x * walkMultiplier;
        _currentMovement.z = _inputsVector.y * walkMultiplier;
        
        _currentRunMovement.x = _inputsVector.x * runMultiplier;
        _currentRunMovement.z = _inputsVector.y * runMultiplier;
        
        isMovementPressed = _inputsVector.x != 0 || _inputsVector.y != 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRunPressed)
        {
            appliedMovement.x = _currentRunMovement.x;
            appliedMovement.z = _currentRunMovement.z;
        }
        else
        {
            appliedMovement.x = _currentMovement.x;
            appliedMovement.z = _currentMovement.z;
        }
        
        _playerController.Move(appliedMovement);
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
