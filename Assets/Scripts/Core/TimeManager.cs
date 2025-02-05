using System;
using Zenject;

namespace Core
{
    public sealed class TimeManager
    {

        public delegate void TickHandler(DateTime currentDateTime);
        public event TickHandler OnTick;
        public DateTime CurrentIngameTime => _currentIngameTime;
        public int TimeStepSeconds { get; set; } = 1;
        private DateTime _currentIngameTime;

        [Inject]
        public TimeManager()
        {
            _currentIngameTime = DateTime.UtcNow;
        }

        public void Tick()
        {
            _currentIngameTime = _currentIngameTime.AddSeconds(TimeStepSeconds);
            OnTick?.Invoke(_currentIngameTime);
        }
    }
}
