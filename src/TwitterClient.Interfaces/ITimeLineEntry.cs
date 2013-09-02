using System;

namespace TwitterClient.Interfaces
{
    public interface ITimeLineEntry
    {
        string Status { get; }
        DateTime Date { get; }
    }
}