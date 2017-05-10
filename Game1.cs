using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace Minimax
{
	public class Game1 : Game
	{
		public GraphicsDeviceManager graphics;
		public SpriteBatch spriteBatch;
		public Texture2D spriteGameTitle;
		public Texture2D cellEmpty;
		public Texture2D cellX;
		public Texture2D cellO;
		public Texture2D spriteP1;
		public Texture2D spriteP2;
		public Texture2D gameDraw;
		public Texture2D menu3;
		public Texture2D menu4;
		public Texture2D hand;
		public Texture2D[] battle_bg1;
		public Texture2D[] battle_bg2;
		public Texture2D returnersRight;
		public Texture2D returnersLeft;
		public Texture2D spriteHard;
		public Texture2D spriteMedium;
		public string charactersP2;
		public string charactersP1;
		public Dictionary<string,string[]> characters;
		public Dictionary<string,Texture2D> charFacesLeft;
		public Dictionary<string,Texture2D> charFacesRight;
		public Dictionary<string,Dictionary<string,Texture2D[]>> spritesLeft;
		public Dictionary<string,Dictionary<string,Texture2D[]>> spritesRight;
		public SpriteFont arial12;
		public SpriteFont arial14;
		public SpriteFont defaultFont;
		public SpriteFont ff6_32;
		public Board board = new Board();
		public Player player1;
		public Player player2;
		public Player actualPlayer;

		public SoundEffect menuMusic;
		public SoundEffect battleMusic;
		public SoundEffect bossMusic;
		public SoundEffect battleStart;
		public SoundEffect victoryMusic;
		public SoundEffect itemSelect;
		public SoundEffect itemHover;
		public SoundEffect kefka;
		public SoundEffectInstance menuPlay;
		public SoundEffectInstance battlePlay;
		public SoundEffectInstance bossPlay;
		public SoundEffectInstance battleStartPlay;
		public SoundEffectInstance victoryPlay;
		public SoundEffectInstance itemSelectPlay;
		public SoundEffectInstance itemHoverPlay;
		public SoundEffectInstance kefkaPlay;


		public StateMachine GameMode;
		public bool alphabeta=false;
		public int depth = 3;
		public bool superTicTacToeDiagonalFromHell = true;
		public int IADelay = 1000;

		public Game1(){
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = 720;
			graphics.PreferredBackBufferHeight = 640;
			graphics.ApplyChanges();
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
			spriteGameTitle = Content.Load<Texture2D>("Sprites/title");
			cellEmpty = Content.Load<Texture2D>("Sprites/cell");
			cellX = Content.Load<Texture2D>("Sprites/char/gestahl_front");
			cellO = Content.Load<Texture2D>("Sprites/char/mog_front");
			menu3 = Content.Load<Texture2D>("Sprites/menu3");
			menu4 = Content.Load<Texture2D>("Sprites/menu4");
			hand = Content.Load<Texture2D>("Sprites/hand");
			spriteHard = Content.Load<Texture2D>("Sprites/char/hard");
			spriteMedium = Content.Load<Texture2D>("Sprites/char/medium");

			menuMusic = Content.Load<SoundEffect>("sfx/menu");
			battleMusic = Content.Load<SoundEffect>("sfx/battle");
			bossMusic = Content.Load<SoundEffect>("sfx/boss");
			battleStart = Content.Load<SoundEffect>("sfx/battle_start");
			victoryMusic = Content.Load<SoundEffect>("sfx/victory");
			itemSelect = Content.Load<SoundEffect>("sfx/item_click");
			itemHover = Content.Load<SoundEffect>("sfx/hand");
			kefka = Content.Load<SoundEffect>("sfx/kefka");
			menuPlay = menuMusic.CreateInstance();
			menuPlay.IsLooped = true;
			battlePlay = battleMusic.CreateInstance();
			battlePlay.IsLooped = true;
			bossPlay = bossMusic.CreateInstance();
			bossPlay.IsLooped = true;
			battleStartPlay = battleStart.CreateInstance();
			victoryPlay = victoryMusic.CreateInstance();
			victoryPlay.IsLooped = true;
			itemHoverPlay = itemHover.CreateInstance();
			itemSelectPlay = itemSelect.CreateInstance();
			kefkaPlay = kefka.CreateInstance();

			characters = new Dictionary<string,string[]>(){ 
				{"returners", new string[4]{"Terra","Edgar","Celes","Locke"}},
				{"empire", new string[4]{"Kefka","Leo","Biggs","Wedge"}}
			};
				
			charFacesLeft = new Dictionary<string, Texture2D>(){ 
				{"returners",Content.Load<Texture2D>("Sprites/returners_left")},
				{"empire", Content.Load<Texture2D>("Sprites/empire_left")}
			};

			charFacesRight = new Dictionary<string, Texture2D>(){ 
				{"returners",Content.Load<Texture2D>("Sprites/returners_right")},
				{"empire", Content.Load<Texture2D>("Sprites/empire_right")}
			};

			charactersP1 = "empire";
			charactersP2 = "returners";


			spritesLeft = new Dictionary<string, Dictionary<string, Texture2D[]>>(){
				{"returners", new Dictionary<string,Texture2D[]>(){
						{"front", new Texture2D[4]{
								Content.Load<Texture2D>("Sprites/char/terra_front"),
								Content.Load<Texture2D>("Sprites/char/edgar_front"),
								Content.Load<Texture2D>("Sprites/char/celes_front"),
								Content.Load<Texture2D>("Sprites/char/locke_front")
							}
						},
						{"profile", new Texture2D[4]{
								Content.Load<Texture2D>("Sprites/char/terra_left"),
								Content.Load<Texture2D>("Sprites/char/edgar_left"),
								Content.Load<Texture2D>("Sprites/char/celes_left"),
								Content.Load<Texture2D>("Sprites/char/locke_left")
							}
						},
						{"victory", new Texture2D[4]{
								Content.Load<Texture2D>("Sprites/char/terra_victory_left"),
								Content.Load<Texture2D>("Sprites/char/edgar_victory_left"),
								Content.Load<Texture2D>("Sprites/char/celes_victory_left"),
								Content.Load<Texture2D>("Sprites/char/locke_victory_left")
							}
						},
						{"dead", new Texture2D[4]{
								Content.Load<Texture2D>("Sprites/char/terra_dead_left"),
								Content.Load<Texture2D>("Sprites/char/edgar_dead_left"),
								Content.Load<Texture2D>("Sprites/char/celes_dead_left"),
								Content.Load<Texture2D>("Sprites/char/locke_dead_left")
							}
						}
					}
				},
				{"empire", new Dictionary<string,Texture2D[]>(){
						{"front", new Texture2D[4]{
								Content.Load<Texture2D>("Sprites/char/kefka_front"),
								Content.Load<Texture2D>("Sprites/char/leo_front"),
								Content.Load<Texture2D>("Sprites/char/vicks_front"),
								Content.Load<Texture2D>("Sprites/char/vicks_front")
							}
						},
						{"profile", new Texture2D[4]{
								Content.Load<Texture2D>("Sprites/char/kefka_left"),
								Content.Load<Texture2D>("Sprites/char/leo_left"),
								Content.Load<Texture2D>("Sprites/char/vicks_left"),
								Content.Load<Texture2D>("Sprites/char/vicks_left")
							}
						},
						{"victory", new Texture2D[4]{
								Content.Load<Texture2D>("Sprites/char/kefka_victory_left"),
								Content.Load<Texture2D>("Sprites/char/leo_victory_left"),
								Content.Load<Texture2D>("Sprites/char/vicks_victory_left"),
								Content.Load<Texture2D>("Sprites/char/vicks_victory_left")
							}
						},
						{"dead", new Texture2D[4]{
								Content.Load<Texture2D>("Sprites/char/kefka_dead_left"),
								Content.Load<Texture2D>("Sprites/char/leo_dead_left"),
								Content.Load<Texture2D>("Sprites/char/vicks_dead_left"),
								Content.Load<Texture2D>("Sprites/char/vicks_dead_left")
							}
						}
					}
				}
			};
			spritesRight = new Dictionary<string, Dictionary<string, Texture2D[]>>(){
				{"returners", new Dictionary<string,Texture2D[]>(){
						{"front", new Texture2D[4]{
								Content.Load<Texture2D>("Sprites/char/terra_front"),
								Content.Load<Texture2D>("Sprites/char/edgar_front"),
								Content.Load<Texture2D>("Sprites/char/celes_front"),
								Content.Load<Texture2D>("Sprites/char/locke_front")
							}
						},
						{"profile", new Texture2D[4]{
								Content.Load<Texture2D>("Sprites/char/terra_right"),
								Content.Load<Texture2D>("Sprites/char/edgar_right"),
								Content.Load<Texture2D>("Sprites/char/celes_right"),
								Content.Load<Texture2D>("Sprites/char/locke_right")
							}
						},
						{"victory", new Texture2D[4]{
								Content.Load<Texture2D>("Sprites/char/terra_victory_right"),
								Content.Load<Texture2D>("Sprites/char/edgar_victory_right"),
								Content.Load<Texture2D>("Sprites/char/celes_victory_right"),
								Content.Load<Texture2D>("Sprites/char/locke_victory_right")
							}
						},
						{"dead", new Texture2D[4]{
								Content.Load<Texture2D>("Sprites/char/terra_dead_right"),
								Content.Load<Texture2D>("Sprites/char/edgar_dead_right"),
								Content.Load<Texture2D>("Sprites/char/celes_dead_right"),
								Content.Load<Texture2D>("Sprites/char/locke_dead_right")
							}
						}
					}
				},
				{"empire", new Dictionary<string,Texture2D[]>(){
						{"front", new Texture2D[4]{
								Content.Load<Texture2D>("Sprites/char/kefka_front"),
								Content.Load<Texture2D>("Sprites/char/leo_front"),
								Content.Load<Texture2D>("Sprites/char/vicks_front"),
								Content.Load<Texture2D>("Sprites/char/vicks_front")
							}
						},
						{"profile", new Texture2D[4]{
								Content.Load<Texture2D>("Sprites/char/kefka_right"),
								Content.Load<Texture2D>("Sprites/char/leo_right"),
								Content.Load<Texture2D>("Sprites/char/vicks_right"),
								Content.Load<Texture2D>("Sprites/char/vicks_right")
							}
						},
						{"victory", new Texture2D[4]{
								Content.Load<Texture2D>("Sprites/char/kefka_victory_right"),
								Content.Load<Texture2D>("Sprites/char/leo_victory_right"),
								Content.Load<Texture2D>("Sprites/char/vicks_victory_right"),
								Content.Load<Texture2D>("Sprites/char/vicks_victory_right")
							}
						},
						{"dead", new Texture2D[4]{
								Content.Load<Texture2D>("Sprites/char/kefka_dead_right"),
								Content.Load<Texture2D>("Sprites/char/leo_dead_right"),
								Content.Load<Texture2D>("Sprites/char/vicks_dead_right"),
								Content.Load<Texture2D>("Sprites/char/vicks_dead_right")
							}
						}
					}
				}
			};				



				
			battle_bg1 = new Texture2D[6] {
				Content.Load<Texture2D>("Sprites/bg/plains_bg1"),
				Content.Load<Texture2D>("Sprites/bg/narshe_plains_bg1"),
				Content.Load<Texture2D>("Sprites/bg/narshe_caves_bg1"),
				Content.Load<Texture2D>("Sprites/bg/narshe_streets_bg1"),
				Content.Load<Texture2D>("Sprites/bg/zozo_streets_bg1"),
				Content.Load<Texture2D>("Sprites/bg/opera_bg1")
			};
			battle_bg2 = new Texture2D[6] {
				Content.Load<Texture2D>("Sprites/bg/plains_bg2"),
				Content.Load<Texture2D>("Sprites/bg/narshe_plains_bg2"),
				Content.Load<Texture2D>("Sprites/bg/narshe_caves_bg2"),
				Content.Load<Texture2D>("Sprites/bg/narshe_streets_bg2"),
				Content.Load<Texture2D>("Sprites/bg/zozo_streets_bg2"),
				Content.Load<Texture2D>("Sprites/bg/opera_bg2")
			};
				

			spriteP1 = cellX;
			spriteP2 = cellO;
			arial12 = Content.Load<SpriteFont> ("Fonts/Arial12");
            arial14 = Content.Load<SpriteFont>("Fonts/Arial14Bold");
			defaultFont = Content.Load<SpriteFont>("Fonts/ff6_22");
			ff6_32 = Content.Load<SpriteFont>("Fonts/ff6_32");

			GameMode = new StateMachine(new Dictionary<string,GameState>(){
				{"title",	new TitleGameState (this,"title")},
				{"start",	new StartGameState (this,"start")},
				{"menu",	new MenuGameState (this,"menu")},
				{"play",	new PlayGameState (this,"play")},
				{"end",		new EndGameState (this,"end")}
			}, "title");
		}


		protected override void Update(GameTime gameTime){
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			GameMode.Update();

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime){
			graphics.GraphicsDevice.Clear(Color.CornflowerBlue);
			spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied, SamplerState.PointClamp);

			GameMode.Draw();

			spriteBatch.End();
			base.Draw(gameTime);
		}
	}
}