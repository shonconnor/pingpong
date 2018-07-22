using System.Collections;
using System.Collections.Generic;
using PingPong.Model;
using PingPong.Utils;
using PingPong.View;
using UnityEngine;

namespace PingPong
{
    public partial class World : MonoBehaviour
    {
        [SerializeField] private PlayerView[] _playerViews;
        [SerializeField] private BallView[] _ballViews;
        [SerializeField] private FieldView _fieldView;

        private IReferee _referee;
        private PlayerModel[] _playerModels;
        private BallModel[] _ballModels;

        private void Awake()
        {
            StartGame();
        }

        private void StartGame()
        {
            _referee = new Referee(this);
            CreateModels(_playerViews);
            CreateModels(_ballViews);
        }

        private void ResetGame()
        {
            for (var i = 0; i < _playerModels.Length; i++)
            {
                var model = _playerModels[i];
                model.Reset();
            }

            for (var i = 0; i < _ballModels.Length; i++)
            {
                var model = _ballModels[i];
                model.Reset();
            }
        }

        private void Update()
        {
            CheckFieldIsFitInToScreen();

            _referee.Update();

            for (var i = 0; i < _playerModels.Length; i++)
            {
                var model = _playerModels[i];
                model.Update();
            }

            for (var i = 0; i < _ballModels.Length; i++)
            {
                var model = _ballModels[i];
                model.Update();
            }
        }

        private Bounds WorldBounds
        {
            get { return _fieldView.Bounds; }
        }

        private void CheckFieldIsFitInToScreen()
        {
            float orthographicScale;

            if (Camera.main.aspect < 1)
            {
                var horizontalWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect;
                orthographicScale = WorldBounds.size.x / horizontalWidth;
            }
            else
            {
                orthographicScale = WorldBounds.size.y / (Camera.main.orthographicSize * 2);
            }

            if (!Mathf.Approximately(orthographicScale, 1))
                SetCameraView(orthographicScale);
        }

        private void SetCameraView(float orthographicScale)
        {
            Camera.main.transform.position = new Vector3(WorldBounds.center.x, WorldBounds.center.y, Camera.main.transform.position.z);
            Camera.main.orthographicSize *= orthographicScale;
        }

        private void CreateModels(PlayerView[] playerViews)
        {
            _playerModels = new PlayerModel[playerViews.Length];
            for (var i = 0; i < playerViews.Length; i++)
            {
                var view = playerViews[i];

                var slideBounds = view.SlideBounds;
                var slideAxis = Vector3.Cross(Vector3.forward, view.Direction).normalized;

                IInputSystem inputSystem;
                if (Input.touchSupported)
                    inputSystem = new TouchInputSystem(slideBounds, slideAxis);
                else
                    inputSystem = new MouseInputSystem(slideBounds, slideAxis);

                var model = new PlayerModel(view.transform.position, view.Direction, slideBounds, inputSystem);
                _playerModels[i] = model;

                view.Init(model);
            }
        }

        private void CreateModels(BallView[] ballViews)
        {
            _ballModels = new BallModel[ballViews.Length];
            for (var i = 0; i < ballViews.Length; i++)
            {
                var view = ballViews[i];

                IInputSystem inputSystem = new RigidbodyInputSystem(view.Rigidbody, view.Speed);

                var model = new BallModel(view.transform.position, inputSystem);
                _ballModels[i] = model;

                view.Init(model);
            }
        }
    }
}

