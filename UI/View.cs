using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Minimax
{
	public class View : Element
	{
		public GameState state;
		public new Color backgroundColor = Color.CornflowerBlue;
		public List<DivElement> allChildren = new List<DivElement>();
		protected event EventHandler Resize = delegate(Event e) { };


		public View(Game1 g,GameState s)
		{
			game = g;
			state = s;
			size = new Vector2(game.GraphicsDevice.Viewport.Width,game.GraphicsDevice.Viewport.Height);
			pos = new Vector2(0, 0);
		}

		public override void Append(DivElement e){
			base.Append(e);
			allChildren = GetChildren();
			//Console.WriteLine(state.name+" - ch: "+children.Count+" allch:"+allChildren.Count+" t:"+e.GetType());
		}

		public override bool Remove(DivElement e){
			if(base.Remove(e)){
				allChildren = GetChildren();
				return true;
			}
			return false;
		}


		public override void DrawBackgroundColor()
		{
			game.graphics.GraphicsDevice.Clear(backgroundColor);
		}

		public override void AddEventListener(string type, EventHandler callback){
			if(type=="resize"){
				Resize += callback;
			}
		}

		public virtual bool OnResize(Event e){
			return true;
		}




	}
}

