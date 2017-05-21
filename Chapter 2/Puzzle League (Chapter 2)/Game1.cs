using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PuzzleLeague.Utilities;

namespace PuzzleLeague
{
   /// <summary>
   /// This is the main type for your game.
   /// </summary>
   public class Game1 : Game
   {
      GraphicsDeviceManager graphics;
      SpriteBatch spriteBatch;

      public Game1()
      {
         graphics = new GraphicsDeviceManager(this);
         Content.RootDirectory = "Content";
      }

      /// <summary>
      /// Allows the game to perform any initialization it needs to before starting to run.
      /// This is where it can query for any required services and load any non-graphic
      /// related content.  Calling base.Initialize will enumerate through any components
      /// and initialize them as well.
      /// </summary>
      protected override void Initialize()
      {
         // Set the screen to 800x600
         graphics.PreferredBackBufferHeight = 800;
         graphics.PreferredBackBufferWidth = 600;
         graphics.ApplyChanges();

         base.Initialize();
      }

      /// LoadContent will be called once per game and is the place to load
      /// all of your content.
      protected override void LoadContent()
      {
         // Create a new SpriteBatch, which can be used to draw textures. 
         spriteBatch = new SpriteBatch(GraphicsDevice);

         // Load textures into ContentHelper
         ContentHelper.AddTexture("tileBlue_27", Content.Load<Texture2D>("Graphics\\tileBlue_27"));
         ContentHelper.AddTexture("tileBlue_31", Content.Load<Texture2D>("Graphics\\tileBlue_31"));
         ContentHelper.AddTexture("tileGreen_27", Content.Load<Texture2D>("Graphics\\tileGreen_27"));
         ContentHelper.AddTexture("tileGreen_35", Content.Load<Texture2D>("Graphics\\tileGreen_35"));
         ContentHelper.AddTexture("tilePink_27", Content.Load<Texture2D>("Graphics\\tilePink_27"));
         ContentHelper.AddTexture("tilePink_30", Content.Load<Texture2D>("Graphics\\tilePink_30"));
         ContentHelper.AddTexture("tileRed_27", Content.Load<Texture2D>("Graphics\\tileRed_27"));
         ContentHelper.AddTexture("tileRed_36", Content.Load<Texture2D>("Graphics\\tileRed_36"));
         ContentHelper.AddTexture("tileYellow_27", Content.Load<Texture2D>("Graphics\\tileYellow_27"));
         ContentHelper.AddTexture("tileYellow_33", Content.Load<Texture2D>("Graphics\\tileYellow_33"));
      }

      /// <summary>
      /// UnloadContent will be called once per game and is the place to unload
      /// game-specific content.
      /// </summary>
      protected override void UnloadContent()
      {
         // TODO: Unload any non ContentManager content here
      }

      int count = 0;
      List<Row> rows = new List<Row>();
      /// <summary>
      /// Allows the game to run logic such as updating the world,
      /// checking for collisions, gathering input, and playing audio.
      /// </summary>
      protected override void Update(GameTime gameTime)
      {
         count++;
         if (count == 53)
         {
            rows.Add(Row.RandomRow());
            count = 0;
         }
         foreach (Row r in rows)
            r.Update();

         base.Update(gameTime);
      }

      /// This is called when the game should draw itself.
      protected override void Draw(GameTime gameTime)
      {
         GraphicsDevice.Clear(Color.CornflowerBlue);

         spriteBatch.Begin();
         foreach (Row r in rows)
            r.Draw(spriteBatch);

         spriteBatch.End();

         base.Draw(gameTime);
      }
   }
}
