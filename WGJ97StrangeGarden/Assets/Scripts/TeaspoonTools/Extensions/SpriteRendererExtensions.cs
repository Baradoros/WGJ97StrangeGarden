﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace TeaspoonTools.Utils
{

	public static class SpriteRendererExtensions
	{

		public static float Height(this SpriteRenderer sprRenderer)
		{
			return sprRenderer.bounds.size.y;
		}

		public static float Width(this SpriteRenderer sprRenderer)
		{
			return sprRenderer.bounds.size.x;
		}

		public static float UpperEdge(this SpriteRenderer sprRenderer)
		{
			return sprRenderer.transform.position.y + (sprRenderer.Height() / 2);
		}

		public static float LowerEdge(this SpriteRenderer sprRenderer)
		{
			return sprRenderer.transform.position.y - (sprRenderer.Height() / 2);
		}
	}

}