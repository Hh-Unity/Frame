using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingEditor2D
{
    public static class RectHelper
    {
        public static Rect RectForAnchorCenter(Vector2 centerPos, Vector2 size)
        {
            float width = size.x;
            float height = size.y;
            float x = size.x - width * 0.5f;
            float y = size.y - width * 0.5f;
            return new Rect(x, y, width, height);
        }

        public static Rect RectForAnchorCenter(float x, float y, float width, float height)
        {
            float finalX = x - width * 0.5f;
            float finaly = y - width * 0.5f;
            return new Rect(finalX, finaly, width, height);
        }
    }
}

