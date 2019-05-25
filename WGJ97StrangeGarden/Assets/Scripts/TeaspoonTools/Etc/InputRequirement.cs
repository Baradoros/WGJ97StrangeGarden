using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TeaspoonTools
{
	/// <summary>
	/// Specifies details of required input. Currently only supports axis inputs.
	/// </summary>
	public abstract class InputRequirement : ScriptableObject
	{
		#region Serializable Fields
		[SerializeField]
		InputType _inputType = 					InputType.axis;

		[SerializeField]
		[Tooltip("How many times does the input have to be sent? (Best used with the Down input types.)")]
		int _inputsSent = 						1;

		[SerializeField]
		[Tooltip("For repeated inputs, what's the time limit between them? (For no time limit, set this to a negative number.)")]
		float _inputTimeLimit = 				-1;

		bool _____; // Separator line
		[Header("Applied based on InputType")]
		
		[SerializeField]
		string _axis;

		[SerializeField]
		[Tooltip("How should the axis input be?")]
		Sign _axisSign;

		KeyCode _key;
		
		#endregion

		#region Properties
		public InputType inputType 				{ get { return _inputType; } }
		public int inputsSent 					{ get { return _inputsSent; } }
		public float inputTimeLimit 			{ get { return _inputTimeLimit; } }
		public string axis 						{ get { return _axis; } }
		
		public Sign axisSign 					{ get { return _axisSign; } }
		public KeyCode key 						{ get { return _key; } }
		#endregion

		#region Methods
		/// <summary>
		/// Returns true if the non-time-related requirements for this requirement are met.
		/// </summary>
		/// <returns></returns>
		public bool IsSatisfiedAtBase()
		{
			// TODO: The rest of this function
			switch (inputType)
			{
				case InputType.axis:
					if (string.IsNullOrEmpty(axis)) 
						return true;

					break;

				default:
					throw new System.Exception("Input type " + inputType + " not accounted for.");
			}
			if (string.IsNullOrEmpty(axis))
				return true;

			throw new System.NotImplementedException();
		}

		#region Helpers

		#region For Axis Input
		bool AxisInputApplied()
		{
			// Make sure the axis input is being applied on the right sign...
			if (!AxisOnRightSign())
				return false;

			// ... that it's been sent enough times...

			// ... and that for repeated inputs, time wasn't expired.

			throw new System.NotImplementedException();
		}

		bool AxisOnRightSign()
		{
			float axisValue = 		Input.GetAxis(axis);

			switch (axisSign)
			{
				case Sign.none:
					if (axisValue == 0)
						return true;
					break;

				case Sign.positive:
					if (axisValue > 0)
						return true;
					break;
				
				case Sign.negative:
					if (axisValue < 0)
						return true;
					break;
				
				default:
					throw new System.NotImplementedException("Sign value " + axisSign + " not accounted for.");
			}

			return false;
		}

		#endregion

		#endregion

		#endregion
	}
}