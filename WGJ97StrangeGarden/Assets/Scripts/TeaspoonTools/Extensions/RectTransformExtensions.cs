using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace TeaspoonTools.UI
{
    public static class RectTransformExtensions
    {
        /// <summary>
        /// Searches straight up this RectTransform's family tree, returning the nearest Canvas object found. 
        /// If none is found, null is returned.
        /// </summary>
        public static Canvas GetParentCanvas(this RectTransform rectTransform)
        {
            RectTransform parentRect =              rectTransform.parent as RectTransform;

            // If this has no parents, then clearly there are no Canvases to be found (straight)
            // upwards from the original caller's family tree.
            if (parentRect == null)
                return null;

            Canvas parentCanvas =                   parentRect.GetComponent<Canvas>();

            if (parentCanvas == null) // Check further up the family tree.
                return parentRect.GetParentCanvas();
            else
                return parentCanvas;
        }
        
        public static void SetLocalXScale(this RectTransform rectTransform, float newXScale)
        {
            
            Vector3 newScale =              new Vector3(newXScale,
                                                        rectTransform.localScale.y,
                                                        rectTransform.localScale.z);

            rectTransform.localScale =      newScale;
        }

        /// <summary>
        /// Sets the rectTransform's anchors to a given point.
        /// </summary>
        public static void AnchorToPoint(this RectTransform rectTransform, Vector2 anchorPoint)
        {
            rectTransform.SetAnchors(anchorPoint, anchorPoint);
        }

        /// <summary>
        /// Sets the rectTransform's anchors and pivot to a given point.
        /// </summary>
        public static void AnchorAndPivotToPoint(this RectTransform rectTransform, Vector2 point)
        {
            rectTransform.AnchorToPoint(point);
            rectTransform.SetPivot(point);
        }

        public static void SetAnchors(this RectTransform rectTransform,
                                      Vector2 anchorMin, Vector2 anchorMax)
        {
            // Make sure this doesn't change the size of the rect transform. 
            Vector2 prevSize =                      rectTransform.rect.size;
            rectTransform.anchorMin =               anchorMin;
            rectTransform.anchorMax =               anchorMax;
            rectTransform.sizeDelta =               prevSize;
        }


        public static float RightEdgeX(this RectTransform rectTransform, bool inWorldCoordinates = false)
        {
            //float effRectWidth =    rectTransform.rect.width * rectTransform.localScale.x;
            float xCoord =          rectTransform.rect.xMax;

            if (inWorldCoordinates)
                xCoord =            rectTransform.TransformPoint(new Vector3(xCoord, 0, 0)).x;

            return xCoord;
        }

        public static float LeftEdgeX(this RectTransform rectTransform, bool inWorldCoordinates = false)
        {
            float xCoord =          rectTransform.rect.xMin;

            if (inWorldCoordinates)
                xCoord =            rectTransform.TransformPoint(new Vector3(xCoord, 0, 0)).x;

            return xCoord;
        }

        public static float UpperEdgeY(this RectTransform rectTransform, bool inWorldCoordinates = false)
        {
            float yCoord =          rectTransform.rect.yMax;

            if (inWorldCoordinates)
                yCoord =            rectTransform.TransformPoint(new Vector3(0, yCoord, 0)).y;

            return yCoord;
        }

        public static float LowerEdgeY(this RectTransform rectTransform, bool inWorldCoordinates = false)
        {
            //float effRectHeight =   rectTransform.rect.height * rectTransform.localScale.y;
            float yCoord =          rectTransform.rect.yMin;

            if (inWorldCoordinates)
                yCoord = rectTransform.TransformPoint(new Vector3(0, yCoord, 0)).y;

            return yCoord;
        }

        /// <summary>
        /// Returns the coordinates of the Rect Transform's lower left corner.
        /// </summary>
        public static Vector2 LowerLeftCorner(this RectTransform rectTransform, bool inWorldCoordinates = false)
        {
            Vector2 lowerLeftCorner =               new Vector2(rectTransform.LeftEdgeX(), 
                                                        rectTransform.LowerEdgeY());

            if (inWorldCoordinates)
                lowerLeftCorner =                   rectTransform.TransformPoint(lowerLeftCorner);
            
            return lowerLeftCorner;
            
        }

        /// <summary>
        /// Returns the coordinates of the Rect Transform's lower right corner
        /// in its local space.
        /// </summary>
        public static Vector2 LowerRightCorner(this RectTransform rectTransform, bool inWorldCoordinates = false)
        {
            Vector2 lowerRightCorner = new Vector2( rectTransform.RightEdgeX(), rectTransform.LowerEdgeY());
            
            if (inWorldCoordinates)
                return rectTransform.TransformPoint(lowerRightCorner);
            
            return lowerRightCorner;
        }

        public static Vector2 UpperRightCorner(this RectTransform rectTransform, bool inWorldCoordinates = false)
        {
            Vector2 upperRightCorner = new Vector2( rectTransform.RightEdgeX(), 
                                                    rectTransform.UpperEdgeY());
            
            if (inWorldCoordinates)
                return rectTransform.TransformPoint(upperRightCorner);
            
            return upperRightCorner;
        }

        public static Vector2 UpperLeftCorner(this RectTransform rectTransform, bool inWorldCoordinates = false)
        {
            Vector2 upperLeftCorner = new Vector2(  rectTransform.LeftEdgeX(),
                                                    rectTransform.UpperEdgeY());
            if (inWorldCoordinates)
                return rectTransform.TransformPoint(upperLeftCorner);

            return upperLeftCorner;
        }

        public static Vector2 Center(this RectTransform rectTransform, bool inWorldCoordinates = false)
        {
            // calculate effective rect dimensions

            Vector2 center = rectTransform.rect.center;

            if (inWorldCoordinates)
                center = rectTransform.TransformPoint(center);

            return center;
        }

        /// <summary>
        /// Sets the pivot without changing the rect trans' physical position in the scene.
        /// Author: jmorhart at Unity Answers
        /// </summary>
        public static void SetPivot(this RectTransform rectTransform, Vector2 newPivot)
        {
            Vector2 size =                                      rectTransform.rect.size;
            Vector2 deltaPivot =                                rectTransform.pivot - newPivot;
            Vector3 deltaPosition =                             new Vector3(deltaPivot.x * size.x, deltaPivot.y * size.y);
            rectTransform.pivot =                               newPivot;
            rectTransform.localPosition -=                      deltaPosition;
        }

        public static void SetPivot(this RectTransform rectTransform, TextAnchor newPivot)
		{
			Vector2 vpCoords = 									newPivot.ToViewportCoords();
			rectTransform.SetPivot(vpCoords);
		}

		public static void ApplyAnchorPreset(this RectTransform rectTransform, 
												TextAnchor presetToApply,
												bool alsoSetPivot = false, 
												bool alsoSetPosition = false)
		{
			
			Vector2 anchorsToSet =              presetToApply.ToViewportCoords();
			rectTransform.SetAnchors(anchorsToSet, anchorsToSet);
			
			if (alsoSetPivot)
				rectTransform.SetPivot(anchorsToSet);
			
			if (alsoSetPosition)
				rectTransform.PositionRelativeToParent(anchorsToSet);

		}

		public static void ApplyAnchorPresetRecursively(this RectTransform rectTransform, 
														TextAnchor presetToApply, 
														bool alsoSetPivot = false, 
														bool alsoSetPosition = false)
		{
			rectTransform.ApplyAnchorPreset (presetToApply, alsoSetPivot, alsoSetPosition);

			foreach (RectTransform child in rectTransform) 
				child.ApplyAnchorPresetRecursively (presetToApply, alsoSetPivot, alsoSetPosition);

		}
        public static Transform GetTopParent(this Transform trans)
        {
            // Recursion OP. Don't nerf.
            if (trans.parent != null)
                return trans.parent.GetTopParent();
            
            return trans;
        }

        /// <summary>
        /// Returns an array including this transform's parent, its parent's parent,
        /// and so on up to the top of the family tree.
        /// </summary>
        public static Transform[] GetParentLine(this Transform trans)
        {
            Transform currentTrans = trans;

            // Go up the family tree and find how many parents there are above this transform
            int howManyParents = 0;

            while (currentTrans.parent != null)
            {
                howManyParents++;
                currentTrans = currentTrans.parent;
            }

            if (howManyParents == 0)
            {
                Debug.LogWarning("Transform.GetParentLine: " + trans.name + " has no parents.");
                return null;
            }

            // Now gather them all in an array, going up the family tree again
            Transform[] parentLine = new Transform[howManyParents];

            currentTrans = trans.parent;

            for (int i = 0; i < howManyParents; i++)
            {
                parentLine[i] = currentTrans;
                currentTrans = currentTrans.parent;
            }

            return parentLine;

        }

		public static void SetWidth(this RectTransform rectTransform, float newWidth)
		{
			rectTransform.sizeDelta = new Vector2 (newWidth, rectTransform.sizeDelta.y);
		}

		public static void SetHeight(this RectTransform rectTransform, float newHeight)
		{
			rectTransform.sizeDelta = new Vector2 (rectTransform.sizeDelta.x, newHeight);
		}

        public static float Width(this RectTransform rectTransform)
        {
            return rectTransform.rect.size.x;
        }

        public static float Height(this RectTransform rectTransform)
        {
            return rectTransform.rect.size.y;
        }

        /// <summary>
		/// Returns a position on the rect transform based on the anchor position passed. Say, if the anchor position is 
		/// (1, 1), this returns the position of the rect's upper right corner.
		/// </summary>
		public static Vector2 GetPositionOnRect(this RectTransform rectTransform, Vector2 anchorPos, bool inWorldCoordinates = false)
		{
			// Use the lower left corner as a starting point.
			Vector2 posOnRect =                            rectTransform.LowerLeftCorner(inWorldCoordinates);

			// Use some simple vector math to get the exact point we want.
			posOnRect.x +=                                 rectTransform.Width() * anchorPos.x;
			posOnRect.y +=                                 rectTransform.Height() * anchorPos.y;
			
			return posOnRect;
		}

		public static void PositionRelativeToParent(this RectTransform rectTransform, Vector2 anchorPos)
		{
			// Get the exact world position on the parent rect where the anchor pos is, then adjust to line 
			// this rectTransform up based on said anchor pos.
			RectTransform parentRect =                  rectTransform.parent.GetComponent<RectTransform>();

			if (parentRect == null) // Safety.
			{
				string format = 						"{0} needs to have a parent to be positioned relative to it.";
				string errMessage = 					string.Format(format, rectTransform.name);
				throw new System.NullReferenceException(errMessage);
			}

			Vector2 newPos =                            parentRect.GetPositionOnRect(anchorPos, inWorldCoordinates: true);

			// The horizontal and vertical shifts from that world position depend on the pivots.
			float hShift = 								rectTransform.Width() * (anchorPos.x - rectTransform.pivot.x);
			float vShift = 								rectTransform.Height() * (anchorPos.y - rectTransform.pivot.y);
			Vector2 offset = 							new Vector2(hShift, vShift);

			newPos -= 									offset;
			rectTransform.position =                    newPos;
		}

		public static void PositionRelativeToParent(this RectTransform rectTransform, TextAnchor preset)
		{
			Vector2 vpCoords =                          preset.ToViewportCoords();

			rectTransform.PositionRelativeToParent(vpCoords);
        }


    }
}
