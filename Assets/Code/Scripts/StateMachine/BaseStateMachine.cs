using System;
using UnityEngine;

namespace Code.Scripts.StateMachine
{
    public class BaseStateMachine : MonoBehaviour
    {
        protected State _currentState;
        public virtual void ChangeState(State newState)
        {
            if (newState != _currentState) _currentState?.Exit();
            else return;
            _currentState = newState;
            _currentState.Enter();
        }

        protected virtual void Update()
        {
            _currentState.Update();
        }

        protected virtual void FixedUpdate()
        {
            _currentState.FixedUpdate();
        }
    }
}