using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TeaspoonTools.Events;

namespace TeaspoonTools
{
	/// <summary>
	/// Contains fields and such we want all of our custom 2D Monobehaviours in this game to have.
	/// </summary>
	[RequireComponent(typeof(SpriteRenderer))]
	public class MonoBehaviour2D : MonoBehaviour 
	{
		#region SpriteRenderer
		public virtual SpriteRenderer spriteRenderer 		{ get; protected set; }
		public virtual Sprite sprite
		{
			get { return spriteRenderer.sprite; }
			set { spriteRenderer.sprite = value; }
		}
		public virtual Color color 
		{
			get { return spriteRenderer.color; }
			set { spriteRenderer.color = value; }
		}
		#endregion

		#region 2D Physics
		new public Collider2D collider 				{ get; protected set; }
		new public Rigidbody2D rigidbody 			{ get; protected set; }
		public ContactEvents2D cEvents 				{ get; protected set; }
		#endregion

		#region Dimensions
		public virtual float height 						{ get { return collider.bounds.size.y; } }
		public virtual float width 							{ get { return collider.bounds.size.x; } }
		#endregion

		#region Positioning
		public virtual Vector3 pos
		{
			get { return transform.position; }
			set { transform.position = value; }
		}

		public virtual float posX
		{
			get { return pos.x; }
			set 
			{ 
				Vector3 newPos = 		pos;
				newPos.x = 				value;
				pos = 					newPos;
			}
		}

		public virtual float posY
		{
			get { return pos.y; }
			set {
				Vector3 newPos = 		pos;
				newPos.y = 				value;
				pos = 					newPos;
			}
		}
		#endregion

		#region Etc
		List<Collider2D> _collidersTouching;
		public List<Collider2D> collidersTouching 	
		{ 
			get { return _collidersTouching; }
			protected set { _collidersTouching = value; } 
		}
		#endregion

		#region Initialization
		protected virtual void Awake () 
		{
			spriteRenderer = 					GetComponent<SpriteRenderer>();
			collider = 							GetComponent<Collider2D>();
			rigidbody = 						GetComponent<Rigidbody2D>();
			cEvents = 							new ContactEvents2D();
			collidersTouching = 				new List<Collider2D>();
		}
		#endregion

		protected virtual void Update()
		{
			
		}

		#region Contact event handlers
		protected virtual void OnCollisionEnter2D(Collision2D other)
		{
			CleanColliderTouchList();
			if (!collidersTouching.Contains(other.collider))
				collidersTouching.Add(other.collider);

			cEvents.CollisionEnter2D.Invoke(other);
		}

		protected virtual void OnCollisionStay2D(Collision2D other)
		{
			CleanColliderTouchList();
			cEvents.CollisionStay2D.Invoke(other);

			if (!collidersTouching.Contains(other.collider))
				collidersTouching.Add(other.collider);
		}

		protected virtual void OnCollisionExit2D(Collision2D other)
		{
			CleanColliderTouchList();
			collidersTouching.Remove(other.collider);
			cEvents.CollisionExit2D.Invoke(other);
		}

		protected virtual void OnTriggerEnter2D(Collider2D otherCollider)
		{
			CleanColliderTouchList();
			if (!collidersTouching.Contains(otherCollider))
				collidersTouching.Add(otherCollider);
			cEvents.TriggerEnter2D.Invoke(otherCollider);
		}

		protected virtual void OnTriggerStay2D(Collider2D otherCollider)
		{
			CleanColliderTouchList();
			cEvents.TriggerStay2D.Invoke(otherCollider);

			// Keep items from being removed from collidersTouching just because one 
			// collider in a child object left it.
			if (!collidersTouching.Contains(otherCollider))
				collidersTouching.Add(otherCollider);
		}

		protected virtual void OnTriggerExit2D(Collider2D otherCollider)
		{
			CleanColliderTouchList();
			collidersTouching.Remove(otherCollider);
			cEvents.TriggerExit2D.Invoke(otherCollider);
		}

		void CleanColliderTouchList()
		{
			collidersTouching.RemoveAll((Collider2D coll) => coll == null);
		}
		#endregion

	}

}