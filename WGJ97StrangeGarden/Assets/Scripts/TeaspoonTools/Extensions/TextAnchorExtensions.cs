using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeaspoonTools.UI 
{
	public static class TextAnchorExtensions
	{
		static float upper = 							1f;
		static float middle = 							0.5f;
		static float lower = 							0f;

		static float right = 							1f;
		static float center = 							0.5f;
		static float left = 							0f;

		public static Vector2 ToViewportCoords(this TextAnchor textAnchor)
		{

			switch (textAnchor)
				{
					case TextAnchor.UpperLeft:
						return new Vector2(left, upper);
					case TextAnchor.UpperCenter:
						return new Vector2(center, upper);
					case TextAnchor.UpperRight:
						return new Vector2(right, upper);

					case TextAnchor.MiddleLeft:
						return new Vector2(left, middle);
					case TextAnchor.MiddleCenter:
						return new Vector2(center, middle);
					case TextAnchor.MiddleRight:
						return new Vector2(right, middle);

					case TextAnchor.LowerLeft:
						return new Vector2(left, lower);
					case TextAnchor.LowerCenter:
						return new Vector2(center, lower);
					case TextAnchor.LowerRight:
						return new Vector2(right, lower);

					default:
						throw new System.NotImplementedException("Didn't account for " + textAnchor.ToString());

				}
		}



	}

}