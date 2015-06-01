using UnityEngine;
using System.Collections;

/*
 * This class wil manage all the player's components,
 * such as movement, data , etc
 */
namespace Assets.Scripts.Player
{
	public class PlayerController : MonoBehaviour
	{
		//componenets to manage
		private PlayerMovement _movement;

		//public delegate events to assign this controller to all listening components
		public delegate void AssignmentEvent(PlayerController _controller);
		public static event AssignmentEvent AssignController;

		void Start()
		{
			//init all componenets
			this.InitializePlayerComponents();
		}
		void Update()
		{
			//if game is not paused
			if(!Data.GameManager.IsPaused)
			{
				//run all components
				_movement.Run();
			}
		}

		void FixedUpdate()
		{
			//if game is not paused
			if(!Data.GameManager.IsPaused)
			{
				//run all fixed components for physics
				_movement.FixedRun();
			}
		}

		//assigning references
		private void InitializePlayerComponents()
		{
			//get all components to manage
			_movement = this.GetComponent<PlayerMovement>();

			//tell all components this is their controller
			AssignController(this);
		}

		public PlayerMovement Movement
		{
			get { return _movement; }
		}
	}
}
