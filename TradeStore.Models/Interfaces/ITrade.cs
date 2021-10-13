using System;
using EmployeeModels.Interfaces;

namespace TradeStore.Models.Interfaces
{
    public interface ITrade
    {
        long Id { get; init; }
        TradeType? TradeType { get; init; }
        DateTime? TradeDate { get; init; }
        Trader Trader { get; init; }
        IGenericPropertyBag Properties { get; }
        DateTime? Created { get; init; }
        Guid Version { get; init; }
        TradeStatus? TradeStatus { get; init; }
    }
}