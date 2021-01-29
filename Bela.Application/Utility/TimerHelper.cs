using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Bela.Application.Utility
{
    public static class TimerHelper
    {
        private static Dictionary<int, TimerBela> TimerCollection = new Dictionary<int, TimerBela>();

        public static TimerBela GetTimerForGameId(int gameId)
        {
            return TimerCollection.GetValueOrDefault(gameId);
        }

        public static void AddTimerForGameId(int gameId, TimerBela timer)
        {
            TimerCollection.Add(gameId, timer);
        }

        public static void RemoveTimerForGameId(int gameId)
        {
            var timer = GetTimerForGameId(gameId);
            if (timer != null)
            {
                timer.Stop();
                TimerCollection.Remove(gameId);
            }
        }

        public static int GetNumberOfTimers()
        {
            return TimerCollection.Count;
        }

    }
}
