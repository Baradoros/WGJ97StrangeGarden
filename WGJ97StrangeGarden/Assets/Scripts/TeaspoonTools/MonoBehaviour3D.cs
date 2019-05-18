using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TeaspoonTools.Events;

public class MonoBehaviour3D : MonoBehaviour 
{
	
	new public Collider collider 				{ get; protected set; }
	new public Rigidbody rigidbody 				{ get; protected set; }
	public ContactEvents3D cEvents 		{ get; protected set; }

	// Use this for initialization
	protected virtual void Awake () 
	{
		collider = 								GetComponent<Collider>();
		rigidbody = 							GetComponent<Rigidbody>();
		cEvents = 						new ContactEvents3D();
	}
	

	// Contact event handlers
	protected virtual void OnCollisionEnter(Collision other)
	{
		cEvents.CollisionEnter.Invoke(other);
	}

	protected virtual void OnCollisionStay(Collision other)
	{
		cEvents.CollisionStay.Invoke(other);
	}

	protected virtual void OnCollisionExit(Collision other)
	{
		cEvents.CollisionExit.Invoke(other);
	}

	protected virtual void OnTriggerEnter(Collider other)
	{
		cEvents.TriggerEnter.Invoke(other);
	}

	protected virtual void OnTriggerStay(Collider other)
	{
		cEvents.TriggerStay.Invoke(other);
	}

	protected virtual void OnTriggerExit(Collider other)
	{
		cEvents.TriggerExit.Invoke(other);
	}
}
