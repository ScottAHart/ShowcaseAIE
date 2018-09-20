using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Player controller for main character
/// This was pulled and modified from pervious projects. 
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerControl : MonoBehaviour, IHitable {
    private CharacterController _controller; //Character controller for movement
    private Animator _animator; //Animator
    public float _speed = 5; //Character speed
    public float _jumpForce = 10; //Jumping (not used)
    private float _jumpingVel = 0; //
    public bool _isGrounded = false; // 
    private bool _jumping = false; //
    private float _dbugSpeed = 0; //
    private int _hitIndex = 1; //Hit index used for different animations. 
    private float _hitTimer = 0; //Hit timer for dynamic animations. 
    public float _hitCooldown = 2; //Hit cooldown for dynmaic animations
    private static GameObject _attached; //Used for attached object set but other script.

    public UnityEvent _onActionChange;
    //Public accessor for attached
    public static GameObject Attached
    {
        get
        {
            return _attached;
        }
    }
    //Single player instance should only exist
    private static PlayerControl _instance;
    public static PlayerControl Instance{
        get
        {
            return _instance;
        }
        }

	void Start () {
        //Singleton 
        if(PlayerControl.Instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
	}
	
	void Update () {
        //Make 5 check for grounded
        float disCheck = 0.1f;
        //Raycasts down from center a distance, and then from 4 other points around the base of the character controller. 
        _isGrounded = Physics.Raycast(transform.position + _controller.center -(new Vector3(0,_controller.height/2,0)), -Vector3.up, disCheck);
        _isGrounded = _isGrounded || Physics.Raycast(transform.position + _controller.center + transform.forward * _controller.radius -(new Vector3(0,_controller.height/2,0)), -Vector3.up, disCheck);
        _isGrounded = _isGrounded || Physics.Raycast(transform.position + _controller.center + -transform.forward * _controller.radius -(new Vector3(0,_controller.height/2,0)), -Vector3.up, disCheck);
        _isGrounded = _isGrounded || Physics.Raycast(transform.position + _controller.center + -transform.right * _controller.radius -(new Vector3(0,_controller.height/2,0)), -Vector3.up, disCheck);
        _isGrounded = _isGrounded || Physics.Raycast(transform.position + _controller.center + -transform.right * _controller.radius -(new Vector3(0,_controller.height/2,0)), -Vector3.up, disCheck);

        //Sprint speed
        float speed = _speed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= 2;
        }
        //Forward direction of camera so input is relative to camera. 
        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0;
        Vector3 camRight = Camera.main.transform.right;
        camForward.y = 0;
        //Input
        Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //Camera
        if (inputDirection.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(inputDirection);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, Camera.main.transform.eulerAngles.y + transform.eulerAngles.y, transform.eulerAngles.z);
        }
        
        //Shoot (was going to be for interactions.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetTrigger("Shoot");
        }
        //Manual hit
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _animator.SetInteger("HitNo", _hitIndex);
            Camera.main.GetComponent<BlurEffect>().HitBlur();
        }
        //Hit cycle checks
        if (_hitIndex > 0)
        {
            if (_hitTimer > _hitCooldown)
            {
                _hitIndex = 1;
                _hitTimer = 0;
            }
            else
            {
                _hitTimer += Time.deltaTime;
            }
        }

        Vector3 grav = Physics.gravity * Time.deltaTime;
        //Zero downwards vel
        if (_isGrounded)
        {
            grav.y = 0;
        }

        //Set values;
        _animator.SetFloat("Speed", inputDirection.magnitude);
        _controller.Move(transform.forward * inputDirection.magnitude * _speed * Time.deltaTime + grav);

        
    }
    //Called by animator to say what hit animation to use next
    //i the index, look into animator for detail on what animation is played.
    public void SetHit(int i)
    {
        _hitIndex = i;
        _hitTimer = 0;
        _animator.SetInteger("HitNo", 0);
    }
    //If the rigid body hits another object check if it has a rigid body, if so add force to it. (5 in this case)
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;
        if (hit.moveDirection.y < -0.3F)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * 5;

    }
    //If hti call hit with '1 damage'
    public void Hit()
    {
        Hit(1);
    }
    //If hit play animation and blur post process effect
    public void Hit(int amt)
    {
        _animator.SetInteger("HitNo", _hitIndex);
        Camera.main.GetComponent<BlurEffect>().HitBlur();
    }
    //Set attached object for actions. 
    public void SetAttached(GameObject obj)
    {
            _attached = obj;
            _onActionChange.Invoke();
        
    }
    //Play aniation(UI)
    public void Use()
    {
        _animator.SetTrigger("Shoot");
    }
}
