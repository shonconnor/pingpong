using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PingPong.Model
{
    public class RigidbodyInputSystem : IInputSystem
    {
        private Rigidbody2D _rigidbody;
        private float _speed;

        public Vector3 Movement { get; private set; }

        public RigidbodyInputSystem(Rigidbody2D rigidbody, float speed)
        {
            _rigidbody = rigidbody;
            _speed = speed;
            StartRigidbody();
        }

        private void StartRigidbody()
        {
            var direction = new Vector2(Random.value, Random.value);
            var velocity = direction.normalized * _speed;
            _rigidbody.velocity = velocity;
        }

        public void Update()
        {
            Movement = _rigidbody.position;
        }

        public void Reset()
        {
            StartRigidbody();
        }
    }
}