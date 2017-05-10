using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace Minimax
{
	public abstract class Element
	{

		public Game1 game;
		public Vector2 pos; 
		public Vector2 size;
		public Color backgroundColor = Color.Transparent;
		public Color foregroundColor = Color.White;
		public Texture2D backgroundImage;
		public string backgroundType = "no-repeat"; //repeat, no-repeat, cover	
		public string display="block"; //block, inline, none
		public string align = "left"; //left, right, center;
		public string vAlign = "flow"; //top, bottom, middle;
		public string position = "relative"; //relative, absolute, inherit
		public Element parentNode = null;
		public Element firstNode = null;
		public Element lastNode = null;
		public Element previousNode = null;
		public Element nextNode = null;
		public List<Element> children = new List<Element>();
		public string id="";
		public int[] margin = new int[4] {0,0,0,0}; //left,top,right,bottom
		public int[] padding = new int[4] {0,0,0,0}; //left,top,right,bottom
		public delegate void EventHandler(Event e);

		public bool clicked = false; //foi clicado (e continua sendo)
		public bool active = false; //está ativo
		public bool disabled = false; //não responde a eventos mas está visível
		public bool mouseOver = false; //o mouse está sobre

		protected event EventHandler Click = delegate(Event e) { };
		protected event EventHandler MousePressed = delegate (Event e) { };
		protected event EventHandler MouseReleased = delegate (Event e) { };
		protected event EventHandler MouseOver = delegate (Event e) { };
		protected event EventHandler MouseOut = delegate (Event e) { };

		public Element()
		{
			
		}



		public virtual Vector2 calcPosition(){
			return pos;
		}

		public virtual Vector2 calcSize(){
			return size;
		}
			
		public virtual void Append(Element e){
			children.Add(e);
			e.parentNode = this;
			if(firstNode == null) {
				firstNode = e;
				lastNode = e;
			} else {
				e.previousNode = lastNode;
				lastNode.nextNode = e;
				lastNode = e;
			}

		}

		public virtual Element GetElementById(string id){
			foreach(Element e in children) {
				if(e.id == id) {
					return e;
				}
			}
			return null;
		}

		public virtual bool Remove(DivElement e){
			if(children.Remove(e)) {					
				if(e.previousNode != null) {
					e.previousNode.nextNode = e.nextNode;
				}
				if(e.nextNode != null) {
					e.nextNode.previousNode = e.previousNode;
				}
				if(this.firstNode == e) {
					if(e.nextNode != null) {
						this.firstNode = e.nextNode;
					}
				}
				if(this.lastNode == e) {
					this.lastNode = e.previousNode;
				}
				e.parentNode = null;
				return true;
			}
			return false;
		}

		public virtual void DrawBackgroundColor(){
			if(backgroundColor != Color.Transparent) {
				
			}
		}

		public virtual void DrawBackgroundImage(){
			if (backgroundImage != null) {
				Vector2 bgCover = new Vector2(0, 0);
				if(backgroundType == "cover") {
					bgCover = new Vector2(calcSize().X / backgroundImage.Width, calcSize().Y / backgroundImage.Height); 
				}
				game.spriteBatch.Draw(
					backgroundImage,
					calcPosition(),
					null,
					null,
					Vector2.Zero,
					0.0f,
					bgCover,
					foregroundColor,
					SpriteEffects.None,
					0.0f
				);
			}
		}

		public virtual void Draw(){
			DrawBackgroundColor();
			DrawBackgroundImage();
			foreach(Element ch in children) {
				ch.Draw();
			}
		}

		public List<Element> GetChildren(){
			List<Element> c = new List<Element>();
			foreach(Element ch in children) {
				c.Add(ch);
				c = c.Concat(ch.GetChildren()).ToList();
			}
			return c;
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

		public virtual void AddEventListener(string type, EventHandler callback){
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
	}		
}

