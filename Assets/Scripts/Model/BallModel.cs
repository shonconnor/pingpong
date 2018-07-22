using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PingPong.Model
{
    public class BallModel
    {
        private IInputSystem _inputSystem;

        private Vector3 _startPosition;

        private Vector3 _position;
        public Vector3 Position
        {
            get { return _position; }
            private set
            {
                if (_position != value)
                {
                    _position = value;
                    if (OnPositionChanged != null)
                        OnPositionChanged(_position);
                }
            }
        }
        public event Action<Vector3> OnPositionChanged; 

        public BallModel(Vector3 position, IInputSystem inputSystem)
        {
            _startPosition = position;
            _inputSystem = inputSystem;

            Reset();
        }

        public void Update()
        {
            _inputSystem.Update();

            Position = _inputSystem.Movement;
        }

        public void Reset()
        {
            Position = _startPosition;
            _inputSystem.Reset();
        }
    }
}