using Bela.Application.Interfaces;
using Bela.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;

namespace Bela.Application.Utility
{
    public class TimerBela : Timer
    {
        public DateTime StartTime { get; private set; }

        private double RemainingTime { get; set; }

        private bool IsPaused { get; set; } = false;

        public int GameId { get; private set; }
        private double OriginalInterval { get; set; }

        public TimerBela() : base()
        {
            AutoReset = false;
        }

        public TimerBela(double interval) : base(interval)
        {
            AutoReset = false;
            OriginalInterval = interval;
        }

        public TimerBela(double interval, ElapsedEventHandler eventHandler, int gameId) : base(interval)
        {
            Elapsed += eventHandler;
            AutoReset = false;
            GameId = gameId;
            OriginalInterval = interval;
        }

        public override string ToString()
        {
            return $"GameId = {GameId}, StartTime = {StartTime.ToShortDateString()}, RemainingTime = {RemainingTime}, IsPaused = {IsPaused}";
        }

        public new void Start()
        {
            StartTime = DateTime.Now;
            base.Start();
        }

        public void Pause()
        {
            if (Enabled)
            {
                SetRemainingTime();
                IsPaused = true;
                Stop();
            }
        }

        public void Resume()
        {
            if (IsPaused)
            {
                Interval = RemainingTime * 1000;
                IsPaused = false;
                Start();
            }
        }

        public void Restart()
        {
            Stop();
            Interval = OriginalInterval;
            Start();
        }

        public double GetRemainingTime()
        {
            Pause();
            Resume();
            return RemainingTime;
        }

        private void SetRemainingTime()
        {
            RemainingTime = (Interval / 1000) - (DateTime.Now - StartTime).TotalSeconds;
        }
    }
}
