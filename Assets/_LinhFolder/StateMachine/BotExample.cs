using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class BotExample : MonoBehaviour
    {
        private IState<BotExample> currentState;

        private void Start()
        {
            ChangeState(new IdleStateExample());
        }

        // Update is called once per frame
        void Update()
        {
            if (currentState != null)
            {
                currentState.OnExecute(this);
            }
        }

        public void ChangeState(IState<BotExample> state)
        {
            if (currentState != null)
            {
                currentState.OnExit(this);
            }

            currentState = state;

            if (currentState != null)
            {
                currentState.OnEnter(this);
            }
        }

    }