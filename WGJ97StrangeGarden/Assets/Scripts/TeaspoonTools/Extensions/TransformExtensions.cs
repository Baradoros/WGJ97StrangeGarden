using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TeaspoonTools;

namespace TeaspoonTools
{
	public static class TransformExtensions
	{

		/// <summary>
		/// Returns how many objects are (straight) up the family tree of this object.
		/// </summary>
		public static int ParentsAboveThis(this Transform transform)
		{
			int parentsAbove =                  0;
			Transform parent =                  transform.parent;

			while (parent != null)
			{
				parentsAbove++;
				parent =                        parent.parent;
			}

			return parentsAbove;
		}
		
	}
}