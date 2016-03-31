using UnityEngine;
using System.Collections;

using Helper;

public class PlayerCamera : MonoBehaviour {
    #region Public Fields & Properties

    #endregion

    #region Private Fields & Properties
    // Position camera
    private Vector3 _cameraNormalPosition = new Vector3(5f, 3.5f, 0f); // Desired camera position behind character

    // Rotate the camera
    [SerializeField] // Allows for edit in inspector
    private float sensitivity = 5f;

    // For max turn with mouse camera view.
    [SerializeField]
    private float minimumAngle = -40f;
    [SerializeField]
    private float maximumAngle = 40f;

    private float rotationY = 0f;

    private Transform _camera;
    private Transform _player;

    private PlayerCharacter _pc;

    private CameraState _state = CameraState.Normal;

    private CameraTargetObject _cameraTargetObject;
    private CameraMountPoint _cameraMountPoint;
    #endregion

    #region Getters & Setters
    public CameraState CameraState
    {
        get { return _state; }
    }
    #endregion

    // Use this for initialization
    void Start ()
    {
        _pc = this.GetComponent<PlayerCharacter>();

        _camera = GameObject.FindGameObjectWithTag(GameTag.PlayerCamera).transform;

        // What the camera will look at
        _cameraTargetObject = new CameraTargetObject();
        _cameraTargetObject.Init(
            "Camera Target", 
            new Vector3(0f, 1f, 0f), 
            new GameObject().transform, _player.transform);

        // Create object for camera to mount to parent
        _cameraMountPoint = new CameraMountPoint();
        _cameraMountPoint.Init(
            "Camera Mount",
            _cameraNormalPosition,
            new GameObject().transform, _cameraTargetObject.XForm);

        // Set cameras parent to the camera target object so the cameras mount will be consistent
        _camera.parent = _cameraTargetObject.XForm.parent;
    }

	
	void LateUpdate ()
    {
	    // FSM used to govern camera behavior
        switch (_state)
        {
            case CameraState.Normal:

                RotateCamera();
                _camera.position = _cameraMountPoint.XForm.position;
                _camera.LookAt(_cameraTargetObject.XForm);
                break;
            case CameraState.Target:

            break;
        }
	}

   private void RotateCamera()
    {
        rotationY -= Input.GetAxis(PlayerInput.Vertical) + sensitivity;
        rotationY = Mathf.Clamp(rotationY, minimumAngle, maximumAngle);

        _cameraTargetObject.XForm.localEulerAngles = new Vector3(-rotationY, _cameraTargetObject.XForm.localEulerAngles.y, 0);
    }
}
