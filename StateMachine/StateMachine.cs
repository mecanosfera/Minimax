using System;
using System.Collections.Generic;

namespace Minimax
{
	public class StateMachine
	{

		protected Dictionary<string,GameState> states;
		public GameState activeState;

		public StateMachine (Dictionary<string,GameState> s=null, string initialState=null)
		{
			if(s != null) {
				states = s;
				if(initialState != null) {
					activeState = states[initialState];
					activeState.Enter();
				}
			}
		}


		public void Change(string newState){
            string lastState = activeState.name;
			activeState.Exit(newState);
			activeState = states[newState];
			activeState.Enter(lastState);
		}

		public void add(string state, GameState state_){
			states[state] = state_;
			if(activeState == null) {
				activeState = state_;
				activeState.Enter();
			}
		}

		public GameState get(string state){
			return states[state];
		}

		public void Update(){
			if(activeState != null) {
				activeState.Update();
			}
		}

		public void Draw(){
			if(activeState != null) {
				activeState.Draw();
			}
		}


	}
}

