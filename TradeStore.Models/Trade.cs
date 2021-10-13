using System;
using EmployeeModels.Interfaces;
using TradeStore.Models.Exceptions;
using TradeStore.Models.Interfaces;
using TradeStore.Models.Storage;

namespace TradeStore.Models
{
    public partial class Trade : ITrade, IDifference<Trade>
    {
        public long Id { get; init; }
        public TradeType? TradeType { get; init; }
        public DateTime? TradeDate { get; init; }
        public Trader Trader { get; init; }
        public IGenericPropertyBag Properties { get; } = new GenericPropertyBag();
        public DateTime? Created { get; init; }
        public Guid Version { get; init; }
        public TradeStatus? TradeStatus { get; init; }
    }
}