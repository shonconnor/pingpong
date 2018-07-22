using System.Collections;
using System.Collections.Generic;
using PingPong.Model;
using UnityEngine;

namespace PingPong
{
    public partial class World
    {
        private class Referee : IReferee
        {
            private World _world;

            public Referee(World world)
            {
                _world = world;
            }

            public void Update()
            {
                CalculateScore();
                CheckForGameEnd();
            }

            private void CheckForGameEnd()
            {
                var worldBounds = _world.WorldBounds;

                var leftFieldCount = 0;
                for (var i = 0; i < _world._ballModels.Length; i++)
                {
                    var model = _world._ballModels[i];

                    if (!worldBounds.Contains(model.Position))
                        leftFieldCount++;
                }

                if (leftFieldCount >= _world._ballModels.Length)
                {
                    _world.ResetGame();
                }
            }

            private void CalculateScore()
            {
                for (var i = 0; i < _world._playerModels.Length; i++)
                {
                    var playerModel = _world._playerModels[i];

                    playerModel.Score = 0;

                    for (var j = 0; j < _world._ballModels.Length; j++)
                    {
                        var ballModel = _world._ballModels[j];

                        var ballDirection = ballModel.Position - playerModel.Position;
                        if (Vector3.Dot(playerModel.Direction, ballDirection) < 0)
                        {
                            playerModel.Score--;
                        }
                    }
                }
            }
        }
    }
}