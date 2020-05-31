using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IL.DeepLearningGames
{
    public static class ScreenUtils
    {
        public static Rect RectTransformToScreenSpace(RectTransform transform)
        {
            Vector2 size = Vector2.Scale(transform.rect.size, transform.lossyScale);
            Rect rect = new Rect(transform.position.x, Screen.height - transform.position.y, size.x, size.y);
            rect.x -= (transform.pivot.x * size.x);
            rect.y -= ((1.0f - transform.pivot.y) * size.y);
            return rect;
        }

        public static Vector3[] GetWorldCorners(RectTransform rt)
        {
            Vector3[] corners = new Vector3[4];
            rt.GetWorldCorners(corners);
            return corners;
        }

        public static Vector2 GetWorldScreenScale(RectTransform rt)
        {
            var corners = GetWorldCorners(rt);
            var rect = RectTransformToScreenSpace(rt);            
            var vec = corners[0] - corners[2];            
            return new Vector2(vec.x / rect.width, vec.y / rect.height);
        }

        static public Rect GetWorldRect(RectTransform rt, Vector2 scale)
        {
            // Convert the rectangle to world corners and grab the top left
            Vector3[] corners = new Vector3[4];
            rt.GetWorldCorners(corners);
            Vector3 topLeft = corners[0];

            // Rescale the size appropriately based on the current Canvas scale
            Vector2 scaledSize = new Vector2(scale.x * rt.rect.size.x, scale.y * rt.rect.size.y);

            return new Rect(topLeft, scaledSize);
        }

    }
}
