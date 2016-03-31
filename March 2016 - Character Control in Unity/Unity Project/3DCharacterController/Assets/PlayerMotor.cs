using UnityEngine;
using System.Collections;

using Helper;

public class PlayerMotor : MonoBehaviour {

    public float walkSpeed = 3f;
    public float runSpeed = 7f;
    public float _rotationSpeed = 140f;
    public float jumpHeight = 10f;
    public float gravity = 20f; // Needed because of no rigidbody

    private float _horizontal = 0f;
    private float _vertical = 0f;

    private float _moveSpeed;

    private float _airVelocity;

    private Transform _myTransform;

    private Vector3 _moveDirection = Vector3.zero;

    private CharacterController _controller;

    private Animator _animator;

    private SpeedState _speedState = SpeedState.Run;

    private PlayerCharacter _pc;
    private PlayerCamera _camera;
    private CameraState _cameraState;

    public float MoveSpeed
    {
        get { return _moveSpeed; }
        set { _moveSpeed = value; }
    }

	// Use this for initialization
	void Start ()
    {
        _myTransform = this.GetComponent<Transform>();
        _pc = this.GetComponent<PlayerCharacter>();
        _camera = this.GetComponent<PlayerCamera>();

        _animator = _pc.Animator;
        _controller = _pc.Controller;
	}
	
	// Update is called once per frame
	void Update ()
    {
        _cameraState = _camera.CameraState;
        CalculateSpeed();

        _animator.SetFloat(AnimatorConditions.AirVelocity, _airVelocity);

        switch (_cameraState)
        {
            case CameraState.Normal:
                // Allow player to rotate their character
                if (Input.GetAxis(PlayerInput.Horizontal) > 0.1f || Input.GetAxis(PlayerInput.Horizontal) < -0.1f)
                {
                    _myTransform.Rotate(0f, Input.GetAxis(PlayerInput.Horizontal) * _rotationSpeed * Time.deltaTime, 0f);
                }

                // Check if character is grounded
                if (_controller.isGrounded == true)
                {
                    _moveDirection = Vector3.zero;
                    _airVelocity = 0f;

                    _animator.SetBool(AnimatorConditions.IsGrounded, true);

                    // Cache user input into variables
                    _horizontal = Input.GetAxis(PlayerInput.Horizontal);
                    _vertical = Input.GetAxis(PlayerInput.Vertical);

                    // Set values in animator conditions
                    _animator.SetFloat(AnimatorConditions.Direction, _horizontal);
                    _animator.SetFloat(AnimatorConditions.Speed, _vertical);

                    if (Input.GetButtonDown(PlayerInput.Jump))
                    {
                        Jump();
                    }
                }
                else
                {
                    // Allow player to move while in air
                    _moveDirection.x = Input.GetAxis(PlayerInput.Horizontal) * _moveSpeed;
                    _moveDirection.z = Input.GetAxis(PlayerInput.Vertical) * _moveSpeed;
                    _moveDirection = _myTransform.TransformDirection(_moveDirection);

                    _animator.SetBool(AnimatorConditions.IsGrounded, false);
                }
            break;

            case CameraState.Target:
                
            break;
        }

        // Apply gravity to character
        _moveDirection.y -= gravity * Time.deltaTime;
        //Move character withthe _moveDirection Vector3 calculated above.
        _controller.Move(_moveDirection * Time.deltaTime);
	}

    private void CalculateSpeed()
    {
        switch (_speedState)
        {
            case SpeedState.Walk:
                _moveSpeed = walkSpeed;
            break;

            case SpeedState.Run:
                _moveSpeed = runSpeed;
            break;
        }
    }

    private void Jump()
    {
        _moveDirection.y = jumpHeight;
        _airVelocity -= Time.time;
    }
}
