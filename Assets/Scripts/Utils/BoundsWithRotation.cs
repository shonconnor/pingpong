using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PingPong.Utils
{
    public struct BoundsWithRotation
    {
        public Bounds bounds;
        public Quaternion rotation;

        public bool IsContains(Vector3 point)
        {
            point -= bounds.center;
            point = Quaternion.Inverse(rotation) * point;
            point += bounds.center;
            return bounds.Contains(point);
        }
    }
}
