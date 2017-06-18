using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PuzzleLeague.Utilities
{
   public class Timer
   {
      //
      // Private fields
      //

      // Static "master list" of all the timers created (used to update)
      private static List<Timer> timerList = new List<Timer>();

      private enum TimerState { Started, Stopped }
      private bool isLooping;
      private float timeRemaining = 0;
      private float prevTimeRemaining;

      // Property to handle state changes
      private TimerState state;
      private TimerState State
      {
         set
         {
            // If we have been started from a stopped state, add to static timer list
            if (value == TimerState.Started && state != TimerState.Started)
            {
               timerList.Add(this);
               state = value;
            }
            // Otherwise, if we have been stopped from a started state
            else if (value == TimerState.Stopped && state != TimerState.Stopped)
            {
               // If looping, ignore the "stopped" and just reset our time remaining
               if (isLooping)
               {
                  timeRemaining = prevTimeRemaining;
               }
               // Otherwise, remove ourselves from the static timer list
               else
               {
                  timerList.Remove(this);
                  state = value;
               }
            }
         }
      }

      //
      // Public fields
      //
      public event EventHandler OnComplete;

      //
      // Constructor
      //
      public Timer(bool isLooping = false)
      {
         this.isLooping = isLooping;
         state = TimerState.Stopped;
      }

      // Start the timer (defer starting to "State" property)
      public void StartTimer() => State = TimerState.Started;

      // Stop the timer (do a direct remove in case of looping timer)
      public void StopTimer()
      {
         state = TimerState.Stopped;
         if (timerList.Contains(this))
            timerList.Remove(this);
      }

      // Set the timer & remember the value for looping timers
      public void SetTimer(float time)
      {
         timeRemaining = time;
         prevTimeRemaining = timeRemaining;
      }

      // Update method to tick down all of the active timers
      public static void Update(GameTime gameTime)
      {
         for (var i = timerList.Count - 1; i >= 0; i--)
         {
            var thisTimer = timerList[i];
            thisTimer.timeRemaining -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(thisTimer.timeRemaining <= 0)
            {
               thisTimer.OnComplete?.Invoke(thisTimer, EventArgs.Empty);
               thisTimer.State = TimerState.Stopped;
            }
         }
      }
   }
}
