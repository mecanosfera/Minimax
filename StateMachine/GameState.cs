using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Minimax
{
	public abstract class GameState : IState
	{

		public List<DivElement> elements = new List<DivElement>();
		public string name;
		public Game1 game;
		public Vector2 mousePos = new Vector2(0,0);

		public GameState (Game1 g, string n)
		{
			game = g;
			name = n;
		}

		public virtual void AddElement(DivElement e){
			elements.Add(e);
		}

		public virtual void Append(DivElement e){
			elements.Add(e);
			foreach(DivElement ch in e.children){
				elements.Add(ch);
			}
		}

		public virtual void Enter(string lastState=null){}
			
		public virtual void HandleInput(){
			Vector2 newMousePos = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);
			Event mousePosEvent = new Event(newMousePos);

			foreach(DivElement e in elements) {
				if(newMousePos != mousePos) {
					e.OnMouseOver(mousePosEvent);
					e.OnMouseOut(mousePosEvent);
				}
				if(Mouse.GetState().LeftButton == ButtonState.Pressed) {
					e.OnMousePressed(mousePosEvent);
				}
				if(Mouse.GetState().LeftButton == ButtonState.Released) {
					e.OnMouseReleased(mousePosEvent);
				}
			}
			mousePos = newMousePos;
		}

		public virtual void Update(){
			HandleInput();
		}

		public virtual void Draw(){
			foreach (DivElement e in elements) {
				if (e.parentNode == null) {
					e.Draw();
				}
			}
		}

		public virtual void Exit(string newState=null){}
	}
}

