using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleLeague
{

   /// <summary>
   /// Class that describes a row of blocks on the GameBoard
   /// </summary>
   class Row
   {
      // Array of blocks that belong to this row (0 = leftmost, 5 = rightmost)
      private Block[] blocks;

      // Defines the X,Y position of this Row
      private Point position;

      // Public accessor for 'position' field 
      public Point Position { get { return position; } }

      // Constructor
      public Row()
      {
         blocks = new Block[6];
      }

      // Add a block to the "blocks" array
      public void AddBlock(Block block, int index) => blocks[index] = block;

      // Accessability for Blocks to know which position they are in
      public int IndexOfBlock(Block item) => Array.IndexOf(blocks, item);

      // Main update method
      public void Update()
      {
         position.Y += 1;
         foreach (Block b in blocks)
            b.Update();
      }

      // Main draw method
      public void Draw(SpriteBatch spriteBatch)
      {
         foreach (Block b in blocks)
            b.Draw(spriteBatch);
      }

      // Create a random row of blocks
      static public Row RandomRow()
      {
         Row rndRow = new Row();
         for (var i = 0; i <= 5; i++)
         {
            rndRow.AddBlock(Block.RndBlockExcEmpty(rndRow), i);
         }
         return rndRow;
      }
   }
}
