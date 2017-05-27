using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PuzzleLeague.Utilities;

namespace PuzzleLeague
{
   class GameBoard
   {
      //
      // Private fields
      //

      // The list of rows currently on the GameBoard
      private List<Row> rows;

      //
      // Public fields
      //

      // The current Y offset (bad naming - affects row and player position, when to add rows etc.)
      public int YOffset = 0;

      // Where to draw blocks on the x axis
      public const int GameBoardXAnchor = 306;

      //
      // Constructor
      //
      public GameBoard()
      {
         rows = new List<Row>();
      }

      // Accessability for Rows to know which position they are in
      public int IndexOfRow(Row item) => rows.IndexOf(item);

      public void Update()
      {
         YOffset++;
         if (YOffset == ScaleHelper.ScaleHeight(Block.BlockHeight) - 1)
         {
            rows.Insert(0, Row.RandomRow(this));
            YOffset = 0;
         }

         // Update our rows
         foreach (Row r in rows)
            r.Update();
      }

      // Main draw method
      public void Draw (SpriteBatch spriteBatch)
      {
         // Draw the background (should always fill entire screen)
         Rectangle drawTo = new Rectangle(Point.Zero, new Point(ScaleHelper.BackBufferWidth,ScaleHelper.BackBufferHeight));
         spriteBatch.Draw(ContentHelper.GetTexture("gameBoardBackground"), drawTo, Color.White);

         // Draw each of the rows that belong to this GameBoard
         foreach (Row row in rows)
            row.Draw(spriteBatch);
      }
   }
}
