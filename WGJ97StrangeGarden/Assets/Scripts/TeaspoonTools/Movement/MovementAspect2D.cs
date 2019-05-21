using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TeaspoonTools;


namespace TeaspoonTools.Movement
{
	[CreateAssetMenu(menuName = "CustomMovement/MovementAspect2D", fileName = "NewMovementAspect2D")]
	public class MovementAspect2D : ScriptableObject
	{
		#region Serializable Fields
		[SerializeField] 
		[Tooltip("What gravity scale to apply when this aspect is being applied.")]
		float _gravityScale = 1;

		[SerializeField]
		[Tooltip("What should the gameobject be colliding with, for this aspect to apply?")]
		LayerMask _collisionReq;

		[SerializeField]
		float _moveSpeed;

		[SerializeField]
		[Tooltip("What direction to move in?")]
		Vector2 _direction;

		[SerializeField]
		[Tooltip("What input needs to be applied to apply this aspect?")]
		InputRequirement inputReq;

		[SerializeField]
		[Tooltip("Should this be applied as a force?")]
		bool _isForce;

		#endregion

		#region Properties
		public float gravityScale 				{ get { return _gravityScale; } }
		public LayerMask collisionReq 			{ get { return _collisionReq; } }
		public float moveSpeed 					{ get { return _moveSpeed; } }
		public Vector2 direction 				{ get { return _direction; } }
		public bool isForce 					{ get { return _isForce; } }
 
		#endregion
	}
}