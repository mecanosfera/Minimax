using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Minimax
{
	public class UIElement
	{

		public Vector2 size;
		public Vector2 position;
		public Texture2D background;
		protected int[] margin = new int[4] {0,0,0,0};
		protected int[] padding = new int[4] {0,0,0,0};
		//protected string align = "left";
		//protected string vAlign = "top";
		protected UIElement parent;
		protected List<UIElement> children;
		protected Game1 game;


		public UIElement (Game1 g, Vector2 s, Vector2 p, Texture2D bg=null)
		{
			game = g;
			size = s;
			position = p;
			background = bg;
		}

		public void append(UIElement e){
			children.Add(e);
			e.setParent (this);
		}

		public void setParent(UIElement e){
			parent = e;
		}

		public void Margin(params int[] m){
			if (m.Length == 1) {
				margin = new int[4]{ m [0], m [0], m [0], m [0] };
			} else {
				for (int i = 0; i < m.Length; i++) {
					margin [i] = m [i];
				}
			}
		}

		public void Padding(params int[] p){
			if (p.Length == 1) {
				padding = new int[4]{ p[0], p[0], p[0], p[0] };
			} else {
				for (int i = 0; i < p.Length; i++) {
					padding[i] = p[i];
				}
			}
		}

		public Vector2 getPosition(){
			return position;
		}

		public void Draw(){
			if (background != null) {
				game.spriteBatch.Draw(
					background,
					position,
					null,
					null,
					Vector2.Zero,
					0.0f,
					new Vector2(size.X / background.Width, size.Y / background.Height),
					Color.White,
					SpriteEffects.None,
					0.0f
				);
			}

			foreach(UIElement ch in children){
				ch.Draw();
			}

		}
	}
}

