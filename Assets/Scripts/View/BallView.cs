using System;
using System.Collections;
using System.Collections.Generic;
using PingPong.Model;
using UnityEngine;

namespace PingPong.View
{
    public class BallView : MonoBehaviour
    {
        [SerializeField] private float _speed = 8;
        public float Speed { get { return _speed;} }

        [SerializeField] private Rigidbody2D _rigidbody;
        public Rigidbody2D Rigidbody { get { return _rigidbody; } }

        private BallModel _model;

        public void Init(BallModel model)
        {
            _model = model;
            _model.OnPositionChanged += OnPositionChanged;
        }

        private void OnDestroy()
        {
            if (_model != null)
            {
                _model.OnPositionChanged -= OnPositionChanged;
            }
        }

        private void OnPositionChanged(Vector3 position)
        {
            if (this.transform.position != position)
            {
                this.transform.position = position;
            }
        }
    }
}