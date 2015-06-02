using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Player
{
	public class PlayerMovement : PlayerControllerObject
	{
		private float _moveSpeed = 5f;
		private float _rotateSpeed = 100f;
		private float _jumpForce = 35f;
		private bool _grounded = true;
		private float _maxSlope = 45f;
		private Animator _anim;
		private AnimatorStateInfo _currentBaseState;
		private Rigidbody _rigidbody;

		static int _idleState = Animator.StringToHash("Base Layer.Idle");	
		static int _moveState = Animator.StringToHash("Base Layer.Move");
		static int _backwardState = Animator.StringToHash("Base Layer.Backward");
		static int _jumpState = Animator.StringToHash("Base Layer.Jump");
		static int _fallState = Animator.StringToHash("Base Layer.Fall");

		void OnCollisionEnter(Collision _col)
		{
			foreach(ContactPoint _contact in _col.contacts)
			{
				if(Vector3.Angle(_contact.normal, Vector3.up) < _maxSlope)
					_grounded = true;
			}
		}

		void OnCollisionStay(Collision _col)
		{
			foreach(ContactPoint _contact in _col.contacts)
			{
				if(Vector3.Angle(_contact.normal, Vector3.up) < _maxSlope)
					_grounded = true;
			}
		}

		void Start()
		{
			_anim = this.GetComponent<Animator>();
			_rigidbody = this.GetComponent<Rigidbody>();
		}

		public override void Run ()
		{
			if(_currentBaseState.fullPathHash == _fallState)
			{
				_anim.SetBool("Jump", false);
				_anim.SetBool("Land", _grounded);
			}
		}

		public override void FixedRun ()
		{
			float _h = Input.GetAxis("Horizontal");
			float _v = Input.GetAxis("Vertical");
			_anim.SetFloat("Speed", _v);
			_rigidbody.MovePosition(transform.position + transform.forward * _v * Time.deltaTime * _moveSpeed);
			transform.Rotate(new Vector3(0, _v *_h * Time.deltaTime * _rotateSpeed, 0));
			_anim.SetFloat("Direction", _h);

			_currentBaseState = _anim.GetCurrentAnimatorStateInfo(0);	// set our currentState variable to the current state of the Base Layer (0) of animation

			// STANDARD JUMPING

			if (_currentBaseState.fullPathHash == _moveState || _currentBaseState.fullPathHash == _idleState)
			{
				if(Input.GetButtonDown("Jump"))
				{
					_anim.SetBool("Jump", true);
					_grounded = false;
					_rigidbody.AddRelativeForce(Vector3.up * _jumpForce, ForceMode.Impulse);
				}
			}
			

				
				// Raycast down from the center of the character.. 
			//	Ray ray = new Ray(transform.position + Vector3.up, -Vector3.up);
			//	RaycastHit hitInfo = new RaycastHit();
				
			//	if (Physics.Raycast(ray, out hitInfo))
			//	{
					// ..if distance to the ground is more than 1.75, use Match Target
			//		if (hitInfo.distance > 1.75f)
			//		{
						
						// MatchTarget allows us to take over animation and smoothly transition our character towards a location - the hit point from the ray.
						// Here we're telling the Root of the character to only be influenced on the Y axis (MatchTargetWeightMask) and only occur between 0.35 and 0.5
						// of the timeline of our animation clip
			//			anim.MatchTarget(hitInfo.point, Quaternion.identity, AvatarTarget.Root, new MatchTargetWeightMask(new Vector3(0, 1, 0), 0), 0.35f, 0.5f);
			//		}
			//	}
			//}
			
			
			// JUMP DOWN AND ROLL 
			
			// if we are jumping down, set our Collider's Y position to the float curve from the animation clip - 
			// this is a slight lowering so that the collider hits the floor as the character extends his legs
			//else if (currentBaseState.nameHash == jumpDownState)
			//{
			//	col.center = new Vector3(0, anim.GetFloat("ColliderY"), 0);
			//}
			
			// if we are falling, set our Grounded boolean to true when our character's root 
			// position is less that 0.6, this allows us to transition from fall into roll and run
			// we then set the Collider's Height equal to the float curve from the animation clip
			//else if (currentBaseState.nameHash == fallState)
			//{
			//	col.height = anim.GetFloat("ColliderHeight");
			//}
			
			// if we are in the roll state and not in transition, set Collider Height to the float curve from the animation clip 
			// this ensures we are in a short spherical capsule height during the roll, so we can smash through the lower
			// boxes, and then extends the collider as we come out of the roll
			// we also moderate the Y position of the collider using another of these curves on line 128
			//else if (currentBaseState.nameHash == rollState)
			//{
			//	if(!anim.IsInTransition(0))
			//	{
			//		if(useCurves)
			//			col.height = anim.GetFloat("ColliderHeight");
					
			//		col.center = new Vector3(0, anim.GetFloat("ColliderY"), 0);
					
			//	}
			//}
			// IDLE
			
			// check if we are at idle, if so, let us Wave!
			//if(_currentBaseState.fullPathHash == _idleState)
			//{
				//if(Input.GetButtonUp("Jump"))
				//{
				//	anim.SetBool("Wave", true);
				//}
			//}
		}
	}
}
