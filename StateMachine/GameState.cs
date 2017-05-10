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
		public View view;

		public GameState (Game1 g, string n)
		{
			game = g;
			name = n;
			view = new View(g,this);
		}
			
		public virtual void Enter(string lastState=null){}
			
		public virtual void HandleInput(){
			Vector2 newMousePos = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y);

			foreach(Element e in view.allChildren) {
				if(newMousePos != mousePos) {
					e.OnMouseOver(new Event("mouseover",e,(int)newMousePos.X,(int)newMousePos.Y));
					e.OnMouseOut(new Event("mouseout",e,(int)newMousePos.X,(int)newMousePos.Y));
				}
				if(Mouse.GetState().LeftButton == ButtonState.Pressed) {
					e.OnMousePressed(new Event("mousepressed",e,(int)newMousePos.X,(int)newMousePos.Y));
				}
				if(Mouse.GetState().LeftButton == ButtonState.Released) {
					e.OnMouseReleased(new Event("mousereleased",e,(int)newMousePos.X,(int)newMousePos.Y));
				}
			}
			mousePos = newMousePos;
		}

		public virtual void Update(){
			HandleInput();
		}

		public virtual void Draw(){
			view.Draw();
		}

		public virtual void Exit(string newState=null){}
	}
}

