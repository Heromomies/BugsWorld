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
        _characterController.Move(_velocity * Time.deltaTime);
    }
    public void Move (Vector3 velocity)
    {
        _velocity = velocity;
    }
}
