using System;
using System.Collections;
using System.Collections.Generic;
using PingPong.Utils;
using UnityEngine;

namespace PingPong.Model
{
    public class MouseInputSystem : DeviceInputSystem
    {
        public MouseInputSystem(BoundsWithRotation area, Vector3 axis) : base(area, axis)
        {
        }

        public override void Update()
        {
            if (!_isDragging)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    OnDragStart(Input.mousePosition);
                }
            }
            else
            {
                OnDrag(Input.mousePosition);

                if (Input.GetMouseButtonUp(0))
                {
                    OnDragEnd();
                }
            }
        }
    }
}
