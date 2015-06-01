using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Camera
{
	public class ThirdPersonCameraFollow : MonoBehaviour
	{
		public float _smooth = 3f;
		private Transform _pos;

		void Start()
		{
			_pos = GameObject.Find ("CameraPosition").transform;
		}
		
		void FixedUpdate ()
		{
			transform.position = Vector3.Lerp(transform.position, _pos.position, Time.deltaTime * _smooth);	
			transform.forward = Vector3.Lerp(transform.forward, _pos.forward, Time.deltaTime * _smooth);
		}
	}
}
