using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Player
{
	public class PlayerAnimator : PlayerControllerObject
	{

		private Animator anim;
		private AnimatorStateInfo currentBaseState;

		
		static int idleState = Animator.StringToHash("Base Layer.Idle");	
		static int moveState = Animator.StringToHash("Base Layer.Move");
		//static int jumpState = Animator.StringToHash("Base Layer.Jump");
		//static int jumpDownState = Animator.StringToHash("Base Layer.JumpDown");
		//static int fallState = Animator.StringToHash("Base Layer.Fall");

		public override void Run ()
		{
			throw new System.NotImplementedException ();
		}
		public override void FixedRun ()
		{

		}
	}
}
