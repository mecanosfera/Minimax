using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Minimax
{
	public class DivElement
	{
		protected Game1 game;

		///pos funciona como position de top/bottom, left/right, mas depende do align/vAlign e não é levado em conta
		///quando os valores de align/vAlign são center ou middle.
		public Vector2 pos; 
		public Vector2 size;
		public Texture2D background;
		public Color backgroundColor = Color.Transparent;
		public Color foregroundColor = Color.White;
		public string align = "left"; //left, right, center;
		public string vAlign = "top"; //top, bottom, middle;
		public string position = "relative"; //relative, absolute, inherit
		protected int[] margin = new int[4] {0,0,0,0}; //left,top,right,bottom
		protected int[] padding = new int[4] {0,0,0,0}; //left,top,right,bottom
		public DivElement parent = null;
		protected List<DivElement> children = new List<DivElement>();

		public bool clicked = false; //foi clicado (e continua sendo)
		public bool active = false; //está ativo
		public bool disabled = false; //não responde a eventos mas está visível
		public bool display = true; //não está visíviel
		public bool mouseOver = false; //o mouse está sobre

		public delegate void EventHandler(DivElement origin, Event e);
		public event EventHandler Click;
		public event EventHandler MousePressed;
		public event EventHandler MouseReleased;
		public event EventHandler MouseOver;
		public event EventHandler MouseOut;
		public event EventHandler Change;


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

		public void Append(DivElement e){
			children.Add(e);
			e.parent = this;
		}

		public void Margin(params int[] m){
			if (m.Length == 1) {
				margin = new int[4]{ m[0], m[0], m[0], m[0] };
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

		public void Align(string ha, string va){
			align = ha;
			vAlign = va;
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
				//pos ainda é considerada da esquerda p/ direita, cima p/ baixo. modificar isso.
				if (parent == null || position == "absolute") {
					actualPos.X += game.GraphicsDevice.Viewport.Width - (size.X + margin[2] + padding[2]);
				} else if (parent != null && position == "relative") {
					//recalcular
					actualPos.X = (parentPos.X + parent.padding[2]+parent.size.X)-(size.X + margin[2] + padding[2]);
				}
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
				//pos ainda é considerada da esquerda p/ direita, cima p/ baixo. modificar isso.
				if (parent == null || position == "absolute") {
					actualPos.Y += game.GraphicsDevice.Viewport.Height - (size.Y + margin[3] + padding[3]);
					actualPos.Y += margin[1];
				} else if (parent != null && position == "relative") {
					//depois
					//actualPos.Y = margin[1] + parentPos.Y + parent.padding[1];
				}
			}
			return actualPos;
		}

		public Vector2 calcSize(){
			//adiciona os paddings left/right e top/bottom às dimensões
			return size+new Vector2(padding[0]+padding[2],padding[1]+padding[3]);
		}

		public bool detectInteracion(Vector2 p){
			if(!disabled) {
				Vector2 aSize = calcSize();
				Vector2 aPos = calcPosition();
				if((p.X >= aPos.X && p.X <= aPos.X + aSize.X) && (p.Y >= aPos.Y && p.Y <= aPos.Y + aSize.Y)) {
					return true;
				}
			}
			return false;
		}

		public void AddEventListener(string type, EventHandler callback){
			if (type.ToLower() == "click") {
				Click += callback;
			} else if (type.ToLower() == "mousepressed"){
				MousePressed += callback;	
			} else if (type.ToLower() == "mousereleased"){
				MouseReleased += callback;
			} else if (type.ToLower() == "mouseover") {
				MouseOver += callback;
			} else if (type.ToLower() == "mouseout") {
				MouseOut += callback;
			} 
		}

		public virtual bool OnMousePressed(Event e, bool fireClick=true){
			if (detectInteracion(e.vVector)) {
				if (!clicked && fireClick){
					return OnClick(e);
				}
				active = true;
				clicked = true;
				MousePressed (this, e);
				return true;
			}
			return false;
		}

		public virtual bool OnMouseReleased(Event e){
			if (detectInteracion(e.vVector)) {
				active = false;
				clicked = false;
				MouseReleased (this, e);
				return true;
			}
			return false;
		}

		public virtual bool OnClick(Event e){
			if (detectInteracion(e.vVector)) {
				Click(this,e);
				return true;
			}
			return false;
		}

		public virtual bool OnMouseOver(Event e){
			if (detectInteracion(e.vVector) && !mouseOver) {
				mouseOver = true;
				MouseOver(this, e);
				return true;
			}
			return false;
		}

		public virtual bool OnMouseOut(Event e){
			if (!detectInteracion(e.vVector) && mouseOver) {
				mouseOver = false;
				MouseOut(this, e);
				return true;
			}
			return false;
		}

		public virtual void DrawText(){}

		public virtual void DrawSprite(){
			if (background != null) {
				//Console.WriteLine (background.Name);
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

		public void Draw(){
			if (display) {
				DrawSprite();
				DrawText();

				foreach(DivElement ch in children) {
					ch.Draw();
				}
			}

		}
	}
}

