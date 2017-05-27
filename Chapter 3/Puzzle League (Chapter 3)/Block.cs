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
      //
      // Private Fields
      //

      // Defines which type of Block this is (cannot change)
      private readonly BlockType type;

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

      //
      // Public Fields
      //

      // Public accessor for 'type' field
      public BlockType Type { get { return type; } }

      //
      // Public Constants
      //

      // Describes the default height of a block
      public const int BlockHeight = 54;

      // Describes the default width of a block
      public const int BlockWidth = 54;

      //
      // Constructor
      //

      public Block(Row parent, BlockType type = BlockType.Empty)
      {
         // Assign parent
         this.parent = parent;

         // Assign block type
         this.type = type;

         // Set default position based on parent
         position.Y = parent.Position.Y;
         position.X = (parent.IndexOfBlock(this)) * BlockWidth;
      }

      // Create a random block (not including an empty)
      public static Block RndBlockExcEmpty(Row parent)
      {
         return new Block(parent, (BlockType)RandomHelper.Next(1, 6));
      }

      // Return a random block (includes empty)
      public static Block RndBlockIncEmpty(Row parent)
      {
         return new Block(parent, (BlockType)RandomHelper.Next(0, 6));
      }

      // Main update method
      public void Update()
      {
         if (parent != null)
         {
            position.Y = parent.Position.Y;
            position.X = (parent.IndexOfBlock(this)) * BlockWidth;
         }
      }

      // Main draw method
      public void Draw(SpriteBatch spriteBatch)
      {
         if (type != BlockType.Empty)
         {
            // Create a point for the "actual" position of the tile
            Point actualPosition = new Point(
               ScaleHelper.ScaleWidth((int)position.X),
               (int)position.Y);

            // Create a point for the "actual" scale of the tile
            Point actualScale = ScaleHelper.ScalePoint(new Point(BlockWidth,BlockHeight));

            // Draw the tile
            spriteBatch.Draw(TileTexture, new Rectangle(actualPosition, actualScale), Color.White);

            // Calculate the "actual" position of the symbol (add 1/4 of the block height)
            actualPosition.X += (int)Math.Ceiling((float)actualScale.X / 4);
            actualPosition.Y += (int)Math.Ceiling((float)actualScale.Y / 4);

            // Calculate the scale of the symbol (half of that of the block)
            actualScale.X /= 2;
            actualScale.Y /= 2;

            // Draw the symbol
            spriteBatch.Draw(SymbolTexture, new Rectangle(actualPosition, actualScale), Color.White);
         }
      }
   }
}
