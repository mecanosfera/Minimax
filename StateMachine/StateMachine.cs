using System;
using System.Collections.Generic;

namespace Minimax
{
	public class StateMachine
	{

		protected Dictionary<string,IState> states;
		public IState activeState;

		public StateMachine (Dictionary<string,IState> s=null, string initialState=null)
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
			activeState.Exit();
			activeState = states[newState];
			activeState.Enter();
		}

		public void add(string state, IState state_){
			states[state] = state_;
			if(activeState == null) {
				activeState = state_;
				activeState.Enter();
			}
		}

		public IState get(string state){
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

