using UnityEngine;

namespace Code.Scripts.StateMachine
{
    public class State
    {
        public virtual void Exit()
        {
            
        }

        public virtual void Enter()
        {
            
        }
        public virtual void Update()
        {
            
        }

        public virtual void FixedUpdate()
        {
            Debug.Log("I somehow made it to this impossible place");
        }
    }
}