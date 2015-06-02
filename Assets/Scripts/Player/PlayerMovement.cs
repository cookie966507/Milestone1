using UnityEngine;
using System.Collections;
using TeamUtility.IO;

namespace Assets.Scripts.Player
{
	public class PlayerMovement : PlayerControllerObject
	{
		private float _moveSpeed = 5f;
		private float _rotateSpeed = 100f;
		private float _jumpForce = 60f;
		private bool _grounded = true;
		private float _maxSlope = 70f;
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

		void Start()
		{
			_anim = this.GetComponent<Animator>();
			_rigidbody = this.GetComponent<Rigidbody>();
		}

		public override void Run ()
		{
			Debug.Log(_grounded);

			if(_currentBaseState.fullPathHash == _fallState)
			{
				_anim.SetBool("Jump", false);
				_anim.SetBool("Land", _grounded);
			}
		}

		public override void FixedRun ()
		{
			float _h = InputManager.GetAxis("Horizontal");
			float _v = InputManager.GetAxis("Vertical");
			_anim.SetFloat("Speed", _v);
			_rigidbody.MovePosition(transform.position + transform.forward * _v * Time.deltaTime * _moveSpeed);
			transform.Rotate(new Vector3(0, _v *_h * Time.deltaTime * _rotateSpeed, 0));
			_anim.SetFloat("Direction", _h);

			_currentBaseState = _anim.GetCurrentAnimatorStateInfo(0);	// set our currentState variable to the current state of the Base Layer (0) of animation

			if(InputManager.GetButtonDown("Jump"))
			{
				if (_grounded)
				{
					_anim.SetBool("Jump", true);
					_grounded = false;
					_rigidbody.AddRelativeForce(Vector3.up * _jumpForce, ForceMode.Impulse);
				}
			}
		}
	}
}
