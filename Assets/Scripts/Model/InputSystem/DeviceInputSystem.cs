using System.Collections;
using System.Collections.Generic;
using PingPong.Utils;
using UnityEngine;

namespace PingPong.Model
{
    public abstract class DeviceInputSystem : IInputSystem
    {
        private BoundsWithRotation _area;
        private Vector3 _axis;

        protected bool _isDragging;
        private Vector3 _lastPosition;
        private Vector3 _deltaPosition;

        public Vector3 Movement
        {
            get { return _deltaPosition; }
        }

        public DeviceInputSystem(BoundsWithRotation area, Vector3 axis)
        {
            _area = area;
            _axis = axis;
        }

        public abstract void Update();

        protected Vector3 ScreenToWorldPoint(Vector3 point)
        {
            var worldPoint = Camera.main.ScreenToWorldPoint(point);
            return new Vector3(worldPoint.x, worldPoint.y);
        }

        protected bool OnDragStart(Vector3 point)
        {
            _deltaPosition = Vector3.zero;

            var position = ScreenToWorldPoint(point);
            if (_area.IsContains(position))
            {
                _isDragging = true;
                _lastPosition = position;
                return true;
            }
            return false;
        }

        protected void OnDrag(Vector3 point)
        {
            var position = ScreenToWorldPoint(point);
            if (position != _lastPosition)
            {
                _deltaPosition = position - _lastPosition;
                _deltaPosition = _axis * Vector3.Dot(_deltaPosition, _axis); // projection on axis
                _lastPosition = position;
            }
            else
            {
                _deltaPosition = Vector3.zero;
            }
        }

        protected void OnDragEnd()
        {
            _isDragging = false;
            _deltaPosition = Vector3.zero;
        }

        public void Reset()
        {
            OnDragEnd();
        }
    }
}