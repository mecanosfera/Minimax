using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Minimax
{
	public class DivElement : Element
	{
		

		public bool clicked = false; //foi clicado (e continua sendo)
		public bool active = false; //está ativo
		public bool disabled = false; //não responde a eventos mas está visível
		public bool mouseOver = false; //o mouse está sobre

        protected event EventHandler Click = delegate(Event e) { };
        protected event EventHandler MousePressed = delegate (Event e) { };
        protected event EventHandler MouseReleased = delegate (Event e) { };
        protected event EventHandler MouseOver = delegate (Event e) { };
        protected event EventHandler MouseOut = delegate (Event e) { };
        
	
		public DivElement (Game1 g, Vector2 s, Texture2D bg=null)
		{
			game = g;
			size = s;
			pos = new Vector2(0, 0);
			backgroundImage = bg;
		}

		public DivElement (Game1 g, Vector2 s, Vector2 p, Texture2D bg=null)
		{
			game = g;
			size = s;
			pos = p;
			backgroundImage = bg;
		}



		public virtual void Margin(params int[] m){
			if (m.Length == 1) {
				margin = new int[4]{ m[0], m[0], m[0], m[0] };
			} else {
				for (int i = 0; i < m.Length; i++) {
					margin [i] = m [i];
				}
			}
		}

		public virtual void Padding(params int[] p){
			if (p.Length == 1) {
				padding = new int[4]{ p[0], p[0], p[0], p[0] };
			} else {
				for (int i = 0; i < p.Length; i++) {
					padding[i] = p[i];
				}
			}
		}

		public virtual void Align(string ha, string va){
			align = ha;
			vAlign = va;
		}

		public override Vector2 calcPosition(){
			Vector2 actualPos = pos;
			Vector2 actualSize = calcSize();
			Vector2 parentPos = new Vector2(0,0);
			Vector2 parentSize = new Vector2(0,0);
			if (parentNode != null) {
				parentPos = parentNode.calcPosition();
				parentSize = parentNode.calcSize();
			}

			//calcula actualPos.X
			/*
				- align center funciona apenas se o display for block.
				- align left ou right só leva em conta vizinhos de mesmo alinhamento.
			*/
			if (align == "left" || (align=="center" && display=="inline")) {
				if(position == "relative") {
					if(display == "block") {
						actualPos.X += margin[0] + parentPos.X + parentNode.padding[0];
					} else if(display == "inline") {
						if(previousNode != null && previousNode.display!="block" && previousNode.align=="right") {
							actualPos.X += margin[0] + previousNode.calcSize().X + previousNode.margin[2] + previousNode.calcPosition().X;
						} else if (previousNode == null || (previousNode != null && previousNode.display =="block") || (previousNode!=null&& previousNode.align=="right")) {
							actualPos.X += margin[0] + parentPos.X + parentNode.padding[0];
						}
					}
				} else if(position == "absolute") {
					actualPos.X += margin[0] + parentPos.X + parentNode.padding[0];
				}
			} else if (align == "center" && display=="block") {
				if (parentNode == null || position == "absolute") {
					actualPos.X = (game.GraphicsDevice.Viewport.Width - actualSize.X) * 0.5f;
				} else if (parentNode != null && position == "relative") {
					if (actualSize.X < parentSize.X) {
						actualPos.X = parentPos.X + (parentSize.X - actualSize.X) * 0.5f;
					} else {
						actualPos.X = margin [0] + parentPos.X + parentNode.padding[0];
					}
				}
			} else if (align == "right") {
				//pos ainda é considerada da esquerda p/ direita, cima p/ baixo. modificar isso.
				if (parentNode == null || position == "absolute") {
					actualPos.X += game.GraphicsDevice.Viewport.Width - (size.X + margin[2] + padding[2]);
				} else if (parentNode != null && position == "relative") {
					//recalcular
					actualPos.X += (parentPos.X + parentNode.padding[2]+parentNode.size.X)-(size.X + margin[2] + padding[2]);
				}
			}

			//calcula actualPos.Y


			if(vAlign == "top") {
				actualPos.Y += margin[1] + parentPos.Y + parentNode.padding[1];
			} else if (vAlign == "flow") {				
				if(position == "relative") {
					if(display == "block") {						
						if(previousNode != null && previousNode.display == "block" && previousNode.vAlign == "flow") {
							actualPos.Y += previousNode.calcPosition().Y + previousNode.calcSize().Y + previousNode.margin[3] + margin[1];
						} else if(previousNode != null && previousNode.display == "inline" && previousNode.vAlign == "flow") {
							actualPos.Y += previousNode.calcPosition().Y;
						} else if(previousNode == null || previousNode.vAlign != "flow") {
							actualPos.Y += parentNode.calcPosition().Y + margin[1];
						} 
					}
				} else {					
					actualPos.Y += margin[1] + parentPos.Y + parentNode.padding[1];
				}
				/*if (parentNode == null || position == "absolute") {
					actualPos.Y += margin[1];
				} else if (parentNode != null && position == "relative") {
					
				} */
			} else if (vAlign == "middle") {
				if (parentNode == null || position == "absolute") {
					actualPos.Y = (game.GraphicsDevice.Viewport.Height - actualSize.Y) * 0.5f;
				} else if (parentNode != null && position == "relative") {
					if (actualSize.Y < parentSize.Y) {
						actualPos.Y = parentPos.Y + (parentSize.Y - actualSize.Y) * 0.5f;
					} else {
						actualPos.Y = margin[1] + parentPos.Y + parentNode.padding[1];
					}
				}
			} else if (vAlign == "bottom") {
				//pos ainda é considerada da esquerda p/ direita, cima p/ baixo. modificar isso.
				if (parentNode == null || position == "absolute") {
					actualPos.Y += game.GraphicsDevice.Viewport.Height - (size.Y + margin[3] + padding[3]);
					actualPos.Y += margin[1];
				} else if (parentNode != null && position == "relative") {
					actualPos.Y += (parentPos.Y + parentNode.padding[3]+parentNode.size.Y) - (size.Y + margin[3] + padding[3]);
					actualPos.Y -= margin[3];
					//depois
					//actualPos.Y = margin[1] + parentPos.Y + parent.padding[1];
				}
			}
			return actualPos;
		}

		public override Vector2 calcSize(){
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

		public override void AddEventListener(string type, EventHandler callback){
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
            if (detectInteracion(e.coords)) {                
                if (!clicked && fireClick){
                    OnClick(e);
                }
				active = true;
				clicked = true;
				MousePressed(e);
				return true;
			}
			return false;
		}

		public virtual bool OnMouseReleased(Event e){
			if (detectInteracion(e.coords) && MouseReleased!=null) {
				active = false;
				clicked = false;
				MouseReleased(e);
				return true;
			}
			return false;
		}

		public virtual bool OnClick(Event e){
			if (detectInteracion(e.coords)) {
				//Console.WriteLine("click");
				Click(e);
				return true;
			}
			return false;
		}

		public virtual bool OnMouseOver(Event e){
			if (detectInteracion(e.coords) && !mouseOver) {
				mouseOver = true;                
				MouseOver(e);
				return true;
			}
			return false;
		}

		public virtual bool OnMouseOut(Event e){
			if (!detectInteracion(e.coords) && mouseOver) {
				mouseOver = false;
				MouseOut(e);
				return true;
			}
			return false;
		}
			

		public override void DrawBackgroundImage(){
			if (backgroundImage != null) {
				//Console.WriteLine (background.Name);
				game.spriteBatch.Draw(
					backgroundImage,
					calcPosition(),
					null,
					null,
					Vector2.Zero,
					0.0f,
					new Vector2(calcSize().X / backgroundImage.Width, calcSize().Y / backgroundImage.Height),
					foregroundColor,
					SpriteEffects.None,
					0.0f
				);
			}
		}

		public override void Draw(){
			if (display!="none") {
				DrawBackgroundColor();
				DrawBackgroundImage();
				foreach(DivElement ch in children) {
					ch.Draw();
				}
			}

		}
	}
}

