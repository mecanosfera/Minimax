using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Minimax
{
	public class Game1 : Game
	{
		public GraphicsDeviceManager graphics;
		public SpriteBatch spriteBatch;
		public Texture2D cellEmpty;
		public Texture2D cellX;
		public Texture2D cellO;
		public Texture2D gameDraw;
		public SpriteFont arial12;
		public SpriteFont arial14;
		public SpriteFont defaultFont;
		public Board board = new Board(6);
		public Player player1;
		public Player player2;
		public Player actualPlayer;
		public StateMachine GameMode;


		public Game1(){
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
			player1 = new Player(this, 1, false);
			player2 = new Player(this, 2, false);
			actualPlayer = player1;
		}


		protected override void Initialize(){
			base.Initialize();
		}


		protected override void LoadContent(){
			spriteBatch = new SpriteBatch(GraphicsDevice);
			cellEmpty = Content.Load<Texture2D>("Sprites/cell");
			cellX = Content.Load<Texture2D>("Sprites/cellx");
			cellO = Content.Load<Texture2D>("Sprites/cello");
			gameDraw = Content.Load<Texture2D>("Sprites/Char33");
			arial12 = Content.Load<SpriteFont> ("Fonts/Arial12");
            arial14 = Content.Load<SpriteFont>("Fonts/Arial14Bold");
            defaultFont = arial14;

			GameMode = new StateMachine(new Dictionary<string,GameState>(){
				{"start",	new StartGameState (this,"start")},
				{"play",	new PlayGameState (this,"play")},
				{"end",		new EndGameState (this,"end")}
			}, "start");
		}


		protected override void Update(GameTime gameTime){
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			GameMode.Update();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime){
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
			spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);

			GameMode.Draw();

			spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}