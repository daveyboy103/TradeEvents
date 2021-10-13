using System.Collections.Generic;

namespace TradeStore.Models.Interfaces
{
    public interface IArea
    {
        public long Id { get; init; }    
        public string Name { get; init; }
        public IEnumerable<IBook> Books { get; }
    }
}