using System;
using System.Collections;
using System.Collections.Generic;
using PingPong.Model;
using PingPong.Utils;
using UnityEngine;

namespace PingPong.View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Bounds _slideArea; // area available for input

        private PlayerModel _model;

        public Vector3 Direction    // opposite side represent back of the player there he can lose the ball
        {
            get { return this.transform.TransformDirection(Vector3.up); }
        }

        public BoundsWithRotation SlideBounds
        {
            get
            {
                return new BoundsWithRotation
                {
                    bounds = new Bounds(this.transform.TransformPoint(_slideArea.center), _slideArea.size),
                    rotation = this.transform.rotation,
                };
            }
        }

        public void Init(PlayerModel model)
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

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.matrix = this.transform.localToWorldMatrix;

            if (!Application.isPlaying)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireCube(
                    new Vector3(_slideArea.center.x, _slideArea.center.y),
                    new Vector3(_slideArea.size.x, _slideArea.size.y));
            }

            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(Vector3.zero, Vector3.up * 0.5f);
        }
#endif
    }
}