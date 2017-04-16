using System;
using System.Collections.Generic;

namespace Minimax
{
	public class StateMachine
	{

		protected Dictionary<string,IState> states;
		public IState activeState;

		public StateMachine (Dictionary<string,IState> s)
		{
			states = s;
		}


		public void Change(string newState){
			activeState.Exit();
			activeState = states[newState];
			activeState.Enter();
		}

		public void add(string state, IState state_){
			states[state] = state_;
		}

		public IState get(string state){
			return states[state];
		}

		public void Update(){
			activeState.Update();
		}

		public void Draw(){
			activeState.Draw();
		}


	}
}

