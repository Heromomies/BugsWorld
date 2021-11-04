using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;
    private Vector3 _velocity;
    private Player _player;
    
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerRotation();
        if (_player.isRunPressed)
        {
            _player.appliedMovement.x = _player.currentRunMovement.x;
            _player.appliedMovement.z = _player.currentRunMovement.z;
        }
        else
        {
            _player.appliedMovement.x = _player.currentMovement.x;
            _player.appliedMovement.z = _player.currentMovement.z;
        }


        _characterController.Move(_player.appliedMovement * Time.deltaTime);
        
    }

    private void PlayerRotation()
    {
        /*Vector3 camForward = _player.camera.forward;
        camForward.y = 0f;
        _player.camRot = Quaternion.LookRotation(camForward);*/
       
        Quaternion currentRotation = transform.rotation;
        if (_player.isMovementPressed)
        {
            float targetAngle = Mathf.Atan2(-_player.inputsVector.y, _player.inputsVector.x) * Mathf.Rad2Deg;
          
            //Rotation créer avec le movement du joueur
            Quaternion rot = Quaternion.Euler(0f, targetAngle, 0f);
            //Rotation final slerp
            transform.rotation = Quaternion.Slerp(currentRotation, rot, _player.rotationPower * Time.deltaTime);
        }

    }
   
}
