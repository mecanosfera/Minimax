using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Minimax
{
	public class DivElement
	{

		public Vector2 size;
		public Vector2 pos;
		public Texture2D background;
		public Color backgroundColor = Color.Transparent;
		public Color foregroundColor = Color.White;
		protected int[] margin = new int[4] {0,0,0,0}; //left,top,right,bottom
		protected int[] padding = new int[4] {0,0,0,0}; //left,top,right,bottom
		protected string align = "left"; //left, right, center;
		protected string vAlign = "top"; //top, bottom, middle;
		protected string position = "relative"; //relative, absolute, inherit
		protected DivElement parent = null;
		protected List<DivElement> children;
		protected Game1 game;



		public DivElement (Game1 g, Vector2 s, Texture2D bg=null)
		{
			game = g;
			size = s;
			pos = new Vector2(0, 0);
			background = bg;
		}

		public DivElement (Game1 g, Vector2 s, Vector2 p, Texture2D bg=null)
		{
			game = g;
			size = s;
			pos = p;
			background = bg;
		}

		public void append(DivElement e){
			children.Add(e);
			e.parent = this;
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

		public Vector2 calcPosition(){
			Vector2 actualPos = pos;
			Vector2 actualSize = calcSize();
			Vector2 parentPos = new Vector2(0,0);
			Vector2 parentSize = new Vector2(0,0);
			if (parent != null) {
				parentPos = parent.calcPosition();
				parentSize = parent.calcSize();
			}

			//calcula actualPos.X
			if (align == "left") {
				if (parent == null || position == "absolute") {
					actualPos.X += margin [0];
				} else if (parent != null && position == "relative") {
					actualPos.X = margin [0] + parentPos.X + parent.padding[0];
				} 
			} else if (align == "center") {
				if (parent == null || position == "absolute") {
					actualPos.X = (game.GraphicsDevice.Viewport.Width - actualSize.X) * 0.5f;
				} else if (parent != null && position == "relative") {
					if (actualSize.X < parentSize.X) {
						actualPos.X = parentPos.X + (parentSize.X - actualSize.X) * 0.5f;
					} else {
						actualPos.X = margin [0] + parentPos.X + parent.padding[0];
					}
				}
			} else if (align == "right") {
				
			}

			//calcula actualPos.Y
			if (vAlign == "top") {
				if (parent == null || position == "absolute") {
					actualPos.Y += margin[1];
				} else if (parent != null && position == "relative") {
					actualPos.Y = margin[1] + parentPos.Y + parent.padding[1];
				} 
			} else if (vAlign == "middle") {
				if (parent == null || position == "absolute") {
					actualPos.Y = (game.GraphicsDevice.Viewport.Height - actualSize.Y) * 0.5f;
				} else if (parent != null && position == "relative") {
					if (actualSize.Y < parentSize.Y) {
						actualPos.Y = parentPos.Y + (parentSize.Y - actualSize.Y) * 0.5f;
					} else {
						actualPos.Y = margin[1] + parentPos.Y + parent.padding[1];
					}
				}
			} else if (vAlign == "bottom") {

			}
			return actualPos;
		}

		public Vector2 calcSize(){
			//adiciona os paddings left/right e top/bottom às dimensões
			return size+new Vector2(padding[0]+padding[2],padding[1]+padding[3]);
		}

		public void DrawSprite(){
			Console.WriteLine ("n_draw");
			if (background != null) {
				Console.WriteLine ("draw");
				game.spriteBatch.Draw(
					background,
					calcPosition(),
					null,
					null,
					Vector2.Zero,
					0.0f,
					new Vector2(calcSize().X / background.Width, calcSize().Y / background.Height),
					Color.White,
					SpriteEffects.None,
					0.0f
				);
			}
		}

		public void DrawText(){}

		public void Draw(){

			DrawSprite();

			DrawText();

			foreach(DivElement ch in children){

				ch.Draw();
			}

		}
	}
}

