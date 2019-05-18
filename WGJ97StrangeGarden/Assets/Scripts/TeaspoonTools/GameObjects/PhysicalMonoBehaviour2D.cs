using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TeaspoonTools.GameObjects;
using UnityEditor;

namespace TeaspoonTools
{
	/// <summary>
	/// For Monobehaviours that interact with the physics system. This encapsulates the basic
	/// functionality of such objects.
	/// </summary>
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(Collider2D))]
	public class PhysicalMonoBehaviour2D : MonoBehaviour2D, IPhysical2D
	{
		#region Editable in Inspector
		[SerializeField] float _moveSpeed = 3;
		[SerializeField] bool _canMove = true;
		[Tooltip("What directions this object starts moving in as soon as its instantiated.")]
		[SerializeField] Vector2 _startingMovement = Vector2.one;
		#endregion
		float _prevMoveSpeed;
		

		#region For managing this object's movement
		Vector2 _prevNonZeroVel; 
		// ^ Helps resume movement after being set to stop.

		/// <summary>
		/// Note that changing this also changes both the direction and velocity properties.
		/// </summary>
		public virtual Vector2 velocity                
		{
			get { return rigidbody.velocity; }
			set 
			{
				if (velocity.magnitude != 0)
					_prevNonZeroVel = 				velocity;

				rigidbody.velocity = 				value;
				HaveVelocityUpdateMoveSpeed();
			}
		}

		public virtual Vector2 direction
		{
			get { return velocity.normalized; }
			set 
			{
				velocity = 							value.normalized * moveSpeed;
			}
		}
		public virtual float velX
		{
			get { return velocity.x; }
			set
			{
				Vector2 newVel = 					velocity;
				newVel.x = 							value;
				velocity = 							newVel;
			}
		}

		public virtual float velY
		{
			get { return velocity.y; }
			set
			{
				Vector2 newVel = 					velocity;
				newVel.y = 							value;
				velocity = 							newVel;
			}
		}
		
		public virtual float moveSpeed
		{
			get { return _moveSpeed; } // For the sake of editor-friendliness, this doesn't return velocity.magnitude.
			set 
			{
				if (velocity.magnitude != 0)
				{
					_moveSpeed = 					value;
					HaveMoveSpeedUpdateVelocity();
					_prevMoveSpeed =	 			moveSpeed;
				}
				else 
				{
					string format = 				"Tried to set {0}'s move speed to {1} while its velocity is at Vector2.zero; new move speed not applied.";
					Debug.LogWarning(string.Format(format, this.name, value));
				}

			}
		}

		public virtual bool canMove
		{
			get { return _canMove; }
			set
			{
				bool stopMovement = 				canMove == true && value == false;
				bool reenableMovement = 			canMove == false && value == true;

				if (stopMovement)
					velocity = 						Vector2.zero;
				
				else if (reenableMovement) // Set the movement back to how it was before being made to stop.
					velocity = 						_prevNonZeroVel;

				_canMove = 							value;
			}
		}
		#endregion

		// Use these for initialization
		protected override void Awake () // Called before Start
		{
			base.Awake();

			// Start the movement based on what was set in the Inspector.
			velocity = 								_startingMovement.normalized * moveSpeed;
			_prevMoveSpeed = 						moveSpeed;

			if (moveSpeed != 0)
				_prevNonZeroVel = 					velocity;
		}
		
		
		// Update is called once per frame
		protected override void Update () 
		{
			base.Awake();
			if (EditorApplication.isPlaying)
				HaveMoveSpeedUpdateVelocity(); 
				// ^ For when you want to edit the move speed in the editor, and have it actually work on the object.
			
		}

		
		#region For keeping things in sync as necessary
		protected virtual void HaveVelocityUpdateMoveSpeed()
		{
			// Using the private version of moveSpeed to avoid an infinite loop.
			_moveSpeed = 							velocity.magnitude;
		}

		protected virtual void HaveMoveSpeedUpdateVelocity()
		{
			if (_prevMoveSpeed == moveSpeed) // For performance.
				return;

			velocity = 							velocity.normalized * moveSpeed;
		}
		#endregion
	}
}