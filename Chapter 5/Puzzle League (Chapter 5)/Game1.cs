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
      // The graphics device manager of this game
      private GraphicsDeviceManager graphics;
      
      // The spritebatch used for drawing of this game
      private SpriteBatch spriteBatch;

      // The gameboard (temporary, replace with scenes)
      private GameBoard gameBoard;

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
         graphics.PreferredBackBufferWidth = 1280;
         graphics.PreferredBackBufferHeight = 720;
         //graphics.PreferredBackBufferWidth = 800;
         //graphics.PreferredBackBufferHeight = 600;
         graphics.ApplyChanges();

         // Update ScaleHelper static class as well
         ScaleHelper.UpdateBufferValues(graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth);

         // Initalize the GameBoard
         gameBoard = new GameBoard();

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

         // Load Background texture
         ContentHelper.AddTexture("gameBoardBackground", Content.Load<Texture2D>("Backgrounds\\gameBoardBackground"));
      }

      /// <summary>
      /// UnloadContent will be called once per game and is the place to unload
      /// game-specific content.
      /// </summary>
      protected override void UnloadContent()
      {
         // TODO: Unload any non ContentManager content here
      }

      /// <summary>
      /// Allows the game to run logic such as updating the world,
      /// checking for collisions, gathering input, and playing audio.
      /// </summary>
      /// <param name="gameTime">Provides a snapshot of timing values.</param>
      protected override void Update(GameTime gameTime)
      {
         // Update the gameboard
         gameBoard.Update();
         base.Update(gameTime);
      }

      /// <summary>
      /// This is called when the game should draw itself.
      /// </summary>
      /// <param name="gameTime">Provides a snapshot of timing values.</param>
      protected override void Draw(GameTime gameTime)
      {
         // Clear the screen
         GraphicsDevice.Clear(Color.CornflowerBlue);
         // Begin Drawing
         spriteBatch.Begin();
         // Draw the gameboard
         gameBoard.Draw(spriteBatch);
         // End Drawing
         spriteBatch.End();
         base.Draw(gameTime);
      }
   }
}
