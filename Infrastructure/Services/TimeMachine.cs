using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{

    /// <summary>
    /// A replacement for the static DateTime methods (Now, UtcNow, Today) for getting the current
    /// date or time. Any entity utilizing this service can have its perception of time adjusted
    /// from the outside, allowing tests to simulate the passage of time.
    /// </summary>
    public class TimeMachine : IDateTimeService
    {
        private DateTime? FrozenDateTime { get; set; }
        private TimeSpan Offset { get; set; }

        public TimeMachine()
        {
            RevertAllTimeTravel();
        }

        /// <summary>
        /// Retrieve the current date and time.
        /// </summary>
        public DateTime Now()
        {
            return FrozenDateTime != null
                ? FrozenDateTime.Value
                : DateTime.Now.Add(Offset);
        }

        /// <summary>
        /// Retrieve the current date and time in UTC.
        /// </summary>
        public DateTime UtcNow()
        {
            return FrozenDateTime != null
                ? FrozenDateTime.Value.ToUniversalTime()
                : DateTime.UtcNow.Add(Offset);
        }

        /// <summary>
        /// Retrieve the current date.
        /// </summary>
        public DateTime Today()
        {
            return FrozenDateTime != null
                ? FrozenDateTime.Value.Date
                : DateTime.Today.Add(Offset).Date;
        }

        /// <summary>
        /// Retrieve the current date in UTC.
        /// </summary>
        public DateTime UtcToday()
        {
            return FrozenDateTime != null
                ? FrozenDateTime.Value.ToUniversalTime().Date
                : DateTime.UtcNow.Add(Offset).Date;
        }

        /// <summary>
        /// Move forward or backward in time by the specified amount of time.
        /// </summary>
        /// <param name="adjustment">The amount of time, forward or backward, to shift by.</param>
        public void TimeTravel(TimeSpan adjustment)
        {
            Offset += adjustment;
        }

        /// <summary>
        /// Move to the specific point in time provided.
        /// </summary>
        /// <param name="newDateTime">The point in time to move to.</param>
        public void TimeTravelTo(DateTime newDateTime)
        {
            Offset = newDateTime.Subtract(DateTime.Now);
        }

        /// <summary>
        /// Halts the progress of time.
        /// </summary>
        public void FreezeTime()
        {
            if (FrozenDateTime == null)
                FrozenDateTime = Now();
        }

        /// <summary>
        /// Resumes the progress of time.
        /// </summary>
        public void UnfreezeTime()
        {
            if (FrozenDateTime != null)
            {
                TimeTravelTo(FrozenDateTime.Value);
                FrozenDateTime = null;
            }
        }

        /// <summary>
        /// Undoes all adjustments to the flow of time.
        /// </summary>
        public void RevertAllTimeTravel()
        {
            UnfreezeTime();
            Offset = TimeSpan.Zero;
        }

        /// <summary>
        /// Are we currently time traveling or not?
        /// </summary>
        /// <returns></returns>
        public bool IsCurrentlyTimeTraveling()
        {
            return FrozenDateTime != null || !Offset.Equals(TimeSpan.Zero);
        }
    }
