using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TeaspoonTools.Events;

namespace TeaspoonTools.GameObjects
{
	/// <summary>
	/// For 2D physical objects, this interface enforces implementation of several things
	/// physical objects can do, such as moving and being responded to for their collisions from
	/// afar.
	/// </summary>
	public interface IPhysical2D 
	{
		float moveSpeed 					{ get; }
		Collider2D collider 				{ get; }
		Rigidbody2D rigidbody 				{ get; }
		Vector2 velocity 					{ get; }
		float velX 							{ get; }
		float velY 							{ get; }
		ContactEvents2D cEvents         	{ get; }

	}
}