using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PuzzleLeague.Utilities;

namespace PuzzleLeague
{

   /// <summary>
   /// Public enum to define the 5 different types of block (+ Empty)
   /// </summary>
   public enum BlockType
   {
      Empty = 0,
      Red,
      Pink,
      Yellow,
      Green,
      Blue
   }

   /// <summary>
   /// Class that describes information associated with a single block on the GameBoard
   /// </summary>
   class Block
   {
      // Defines which type of Block this is (cannot change)
      private readonly BlockType type;

      // Public accessor for 'type' field
      public BlockType Type { get { return type; } }

      // Defines the X,Y position of this Block
      private Point position;

      // Defines who this Block belongs to (used for positioning)
      private Row parent;

      // Get-only access to the tile texture (based on type)
      private Texture2D TileTexture
      {
         get
         {
            switch (type)
            {
               case BlockType.Blue:
                  return ContentHelper.GetTexture("tileBlue_27");
               case BlockType.Green:
                  return ContentHelper.GetTexture("tileGreen_27");
               case BlockType.Red:
                  return ContentHelper.GetTexture("tileRed_27");
               case BlockType.Yellow:
                  return ContentHelper.GetTexture("tileYellow_27");
               case BlockType.Pink:
                  return ContentHelper.GetTexture("tilePink_27");
            }
            return null;
         }
      }

      // Get-only access to the symbol texture (based on type)
      private Texture2D SymbolTexture
      {
         get
         {
            switch (type)
            {
               case BlockType.Blue:
                  return ContentHelper.GetTexture("tileBlue_31");
               case BlockType.Green:
                  return ContentHelper.GetTexture("tileGreen_35");
               case BlockType.Red:
                  return ContentHelper.GetTexture("tileRed_36");
               case BlockType.Yellow:
                  return ContentHelper.GetTexture("tileYellow_33");
               case BlockType.Pink:
                  return ContentHelper.GetTexture("tilePink_30");
            }
            return null;
         }
      }

      // Constructor
      public Block(Row parent, BlockType type = BlockType.Empty)
      {
         // Assign parent
         this.parent = parent;

         // Assign block type
         this.type = type;

         // Set default position based on parent
         position.Y = parent.Position.Y;
         position.X = (parent.IndexOfBlock(this)) * 54;
      }

      // Create a random block (not including an empty)
      static Random rng = new Random();
      public static Block RndBlockExcEmpty(Row parent)
      {
         BlockType rndType = (BlockType)rng.Next(1,6);
         return new Block(parent, rndType);
      }

      // Main update method
      public void Update()
      {
         if (parent != null)
         {
            position.Y = parent.Position.Y;
            position.X = (parent.IndexOfBlock(this)) * 54;
         }
      }

      // Main draw method
      public void Draw(SpriteBatch spriteBatch)
      {
         if (type != BlockType.Empty)
         {
            spriteBatch.Draw(TileTexture, new Vector2(position.X, position.Y), Color.White);
            spriteBatch.Draw(SymbolTexture, new Vector2(position.X + 14, position.Y + 14), Color.White);
         }
      }
   }
}
