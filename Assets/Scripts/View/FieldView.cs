using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PingPong.View
{
    public class FieldView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Bounds _bounds;
        public Bounds Bounds
        {
            get { return _bounds; }
        }

        private void Awake()
        {
            _bounds = new Bounds(this.transform.position, _spriteRenderer.size);
        }
    }
}