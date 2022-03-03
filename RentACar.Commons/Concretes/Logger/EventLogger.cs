using System;
using System.Diagnostics;
using RentACar.Commons.Abstractions;

namespace RentACar.Commons.Abstractions.Concretes.Logger
{
    internal class EventLogger : LogBase
    {
        public override void Log(string message, bool isError)
        {
            lock (lockObj)
            {
                EventLog m_EventLog = new EventLog();
                m_EventLog.Source = "RentACarEventLog";
                m_EventLog.WriteEntry(message);
            }
        }
    }
}
