using System;

namespace Minimax
{
	public interface IGameState
	{
		
		void Enter();

		void HandleInput();

		void Update();

		void Exit();
	}
}

