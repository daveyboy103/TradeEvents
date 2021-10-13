using System;

namespace TradeStore.Models.Exceptions
{
    public class DifferenceException : Exception
    {
        public DifferenceException(string message) : base(message)
        {
        }
    }
}