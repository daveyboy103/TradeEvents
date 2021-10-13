using System;
using System.Collections.Generic;
using TradeStore.Models.Interfaces;

namespace TradeStore.Models
{
    public partial class Book : IBook, IDifference<Book>
    {
        public long Id { get; init; }
        public string Name { get; init; }
        public IDictionary<long, IEnumerable<Trade>> Trades { get; } = new Dictionary<long, IEnumerable<Trade>>();
        public DateTime? Created { get; init; }
        public Guid Version { get; set; }
    }
}