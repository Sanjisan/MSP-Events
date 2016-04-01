using UnityEngine;
using System.Collections;

using Helper;

/// <summary>
/// Attached to the player and includes all dependencies that are required in order for the
/// character controller system to function.
/// 
/// INCLUDES
/// 
/// PlayerCamera.cs - Camera behavior
/// PlayerMotor.cs- Player movement
/// </summary>

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerCamera))]
[RequireComponent(typeof(PlayerMotor))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(NetworkView))]
public class PlayerCharacter : MonoBehaviour {

    #region Private Fields & Properties
    private CharacterController _controller;

    private Animator _animator;
    private RuntimeAnimatorController _animatorController;

    #endregion

    #region Getters & Setters
    public Animator Animator
    {
        get { return this._animator; }
    }

    public CharacterController Controller
    {
        get { return this._controller; }
    }
    #endregion

    private void Awake()
    {
        _animator = this.GetComponent<Animator>();
        _controller = this.GetComponent<CharacterController>();
    }

    // Use this for initialization
    void Start ()
    {
        // Set at runtime, and typcast as runtime controller.
        _animatorController = Resources.Load(Resource.AnimatorController) as RuntimeAnimatorController;
        // Actually set it to the animatorController.
        _animator.runtimeAnimatorController = _animatorController;

        // Set default values for characters for universal across
        _controller.center = new Vector3(0f, 1f, 0f); // Middle of player
        _controller.height = 1.0f;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
