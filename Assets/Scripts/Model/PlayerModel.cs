using System;
using System.Collections;
using System.Collections.Generic;
using PingPong.Utils;
using UnityEngine;

namespace PingPong.Model
{
    public class PlayerModel
    {
        private IInputSystem _inputSystem;
        private BoundsWithRotation _movementArea;   // valid area for movement

        private Vector3 _startPosition;

        public Vector3 Direction { get; private set; }  // opposite side represent back of the player there he can lose the ball

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

        private float _score;
        public float Score
        {
            get { return _score; }
            set
            {
                if (_score != value)
                {
                    _score = value;
                    if (OnScoreChanged != null)
                        OnScoreChanged(_score);
                }
            }
        }
        public event Action<float> OnScoreChanged;

        public PlayerModel(Vector3 position, Vector3 direction, BoundsWithRotation movementArea, IInputSystem inputSystem)
        {
            Direction = direction;
            _startPosition = position;
            _inputSystem = inputSystem;
            _movementArea = movementArea;

            Reset();
        }

        public void Update()
        {
            _inputSystem.Update();

            var position = Position + _inputSystem.Movement;
            if (position != Position)
            {
                if (_movementArea.IsContains(position))
                {
                    Position = position;
                }
            }
        }

        public void Reset()
        {
            Position = _startPosition;
        }
    }
}