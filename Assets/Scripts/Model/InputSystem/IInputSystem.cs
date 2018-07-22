using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PingPong.Model
{
    public interface IInputSystem
    {
        Vector3 Movement { get; }
        void Update();
        void Reset();
    }
}