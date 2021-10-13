using System;
using System.Collections.Generic;

namespace TradeStore.Models.Interfaces
{
    public interface IBook
    {
        public long Id { get; init; }
        public string Name { get; init; }
        IDictionary<long, IEnumerable<Trade>> Trades { get; }
        DateTime? Created { get; init; }
        Guid Version { get; set; }
    }
}