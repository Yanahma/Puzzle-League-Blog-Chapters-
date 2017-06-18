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

      // Hold an array of six lists, which serve as containing the data of the "columns"
      private List<Block>[] columns;

      // Hold an array of dirty flags for the six columns as above
      private bool[] columnIsDirty;

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
         // Init the list of rows
         rows = new List<Row>();

         // Init the array of columns + related dirty flags
         columns = new List<Block>[6];
         columnIsDirty = new bool[6];
         for (var i = 0; i < columns.Length; i++)
         {
            columns[i] = new List<Block>();
            columnIsDirty[i] = true;
         }
      }

      // Accessability for Rows to know which position they are in
      public int IndexOfRow(Row item) => rows.IndexOf(item);

      public void Update()
      {
         YOffset++;
         if (YOffset == ScaleHelper.ScaleHeight(Block.BlockHeight) - 1)
         {
            // Get a new random row
            Row randomRow = Row.RandomRow(this);

            // Insert into position 0 on both the rows and columns (its just easier this way)
            rows.Insert(0, randomRow);
            for (var i = 0; i < columns.Length; i++)
            {
               columns[i].Insert(0, randomRow[i]);
               columnIsDirty[i] = true;
            }

            YOffset = 0;
         }

         // Check for matches in rows/columns
         DoMatchChecking();

         // Update our rows
         foreach (Row r in rows)
            r.Update();
      }

      // Main draw method
      public void Draw(SpriteBatch spriteBatch)
      {
         // Draw the background (should always fill entire screen)
         Rectangle drawTo = new Rectangle(Point.Zero, new Point(ScaleHelper.BackBufferWidth,ScaleHelper.BackBufferHeight));
         spriteBatch.Draw(ContentHelper.GetTexture("gameBoardBackground"), drawTo, Color.White);

         // Draw each of the rows that belong to this GameBoard
         foreach (Row row in rows)
            row.Draw(spriteBatch);
      }

      // Method to check for matches in the rows and columns (not to be confused w/ "CheckMatches")
      private void DoMatchChecking()
      {
         var allMatchedBlocks = new List<Block>();

         // Check for horizontal matches
         foreach (Row r in rows)
         {
            if (r.isDirty)
            {
               // Compile all matched blocks into a single, unique list
               List<Block> matchedBlocks = CheckMatches(r.Blocks);
               if (matchedBlocks.Count > 0)
               {
                  foreach (Block block in matchedBlocks)
                  {
                     if (!(allMatchedBlocks.Contains(block)))
                        allMatchedBlocks.Add(block);
                  }
               }
               r.isDirty = false;
            }
         }

         // Check for vertical matches
         for (var i = 0; i < columns.Length; i++)
         {
            if (columnIsDirty[i])
            {
               // Compile all matched blocks into a single, unique list
               List<Block> matchedBlocks = CheckMatches(columns[i]);
               if (matchedBlocks.Count > 0)
               {
                  foreach (Block block in matchedBlocks)
                  {
                     if (!(allMatchedBlocks.Contains(block)))
                        allMatchedBlocks.Add(block);
                  }
               }
               columnIsDirty[i] = false;
            }
         }

         // Finally, trigger "OnMatched" for each of the matched blocks
         foreach (Block block in allMatchedBlocks)
         {
            block.OnMatched();
         }
      }


      /// <summary>
      /// Takes a list of ordered blocks and returns a list of any matches of length 3 or greater
      /// </summary>
      /// <param name="blocks">List of blocks (in order they appear)</param>
      /// <returns>Unique list of any blocks that match</returns>
      private List<Block> CheckMatches(List<Block> blocks)
      {
         int pos = 1; // Where we are in our list
         bool prevSame = false; // Used to maximize chains of matching blocks
         var matchedBlocks = new List<Block>(); // The return list of matched blocks (if any)

         while (pos <= blocks.Count - 2)// Check from the second position in the array, up to the second to last position
         {
            // See if pos and pos+1 match
            if (blocks[pos].Type == blocks[pos + 1].Type)
            {
               // If we are following a chain, we already know this is a match:
               // match the next block (pos + 1) & go forward one 
               if (prevSame)
               {
                  pos += 1;
                  matchedBlocks.Add(blocks[pos]);
               }
               // If not following a chain, check the block behind us:
               // If it matches, tag all three and start chain mode
               else if (blocks[pos - 1].Type == blocks[pos].Type)
               {
                  for (var i = pos - 1; i <= pos + 1; i++)
                     matchedBlocks.Add(blocks[i]);
                  prevSame = true;
                  pos += 1;
               }
               // If block behind isn't a match, check the one in front (if not out of range)
               // If it matches, tag all three and start chain mode from position + 2
               else if ((pos + 2) <= blocks.Count - 1
                  && blocks[pos + 2].Type == blocks[pos].Type)
               {
                  for (var i = pos; i <= pos + 2; i++)
                     matchedBlocks.Add(blocks[i]);
                  prevSame = true;
                  pos += 2;
               }
               // Since we now know pos+1 & pos+2 aren't a match
               // skip forward three instead of two
               else
               {
                  pos += 3;
               }
            }
            else
            {
               // Two blocks checked at beginning aren't a match, turn prevSame off 
               // and skip forward two blocks
               prevSame = false;
               pos += 2;
            }
         }

         return matchedBlocks;
      }

      // CheckMatches which takes a block array (from Row.Blocks) - use same logic as above, but 
      // first we should convert it to a List
      private List<Block> CheckMatches(Block[] blocks)
      {
         var newBlocks = new List<Block>();
         foreach (Block b in blocks)
         {
            newBlocks.Add(b);
         }
         return CheckMatches(newBlocks);
      }
   }
}
