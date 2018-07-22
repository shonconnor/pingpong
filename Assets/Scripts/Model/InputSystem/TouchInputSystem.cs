using System.Collections;
using System.Collections.Generic;
using PingPong.Utils;
using UnityEngine;

namespace PingPong.Model
{
    public class TouchInputSystem : DeviceInputSystem
    {
        private int _touchId;

        public TouchInputSystem(BoundsWithRotation area, Vector3 axis) : base(area, axis)
        {
        }

        public override void Update()
        {
            for (var i = 0; i < Input.touchCount; i++)
            {
                var touch = Input.GetTouch(i);

                if (!_isDragging)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        if (OnDragStart(touch.position))
                        {
                            _touchId = touch.fingerId;
                            break;
                        }
                    }
                }
                else
                {
                    if (_touchId == touch.fingerId)
                    {
                        OnDrag(touch.position);

                        if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
                        {
                            OnDragEnd();
                        }
                    }
                }
            }
        }
    }
}
