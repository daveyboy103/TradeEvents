using System;
using TradeStore.Models.Exceptions;
using TradeStore.Models.Interfaces;

// ReSharper disable once CheckNamespace
namespace TradeStore.Models
{
    public partial class Trade : ITrade, IDifference<Trade>
    {
        public Trade GetDiff(Trade diffObject)
        {
            if (this.Id != diffObject.Id)
                throw new DifferenceException(
                    $"Trade Ids do not match of object being diffed, Subject: [{this.Id}], Object: [{diffObject.Id}]");

            if (Version == diffObject.Version)
                return this;

            long id;
            TradeType? tradeType = null;
            DateTime? tradeDate =  null;
            Trader trader = null;
            DateTime? created = null;
            TradeStatus? tradeStatus = null;

            if (TradeType != diffObject.TradeType)
                tradeType = diffObject.TradeType;

            if (TradeDate != diffObject.TradeDate)
                tradeDate = diffObject.TradeDate; 
            
            if (Trader != diffObject.Trader)
                trader = diffObject.Trader;

            if (Created != diffObject.Created)
                created = diffObject.Created;

            if (TradeStatus != diffObject.TradeStatus)
                tradeStatus = diffObject.TradeStatus;

            var ret = new Trade
            {
                Id = Id,
                Created = created,
                TradeDate = tradeDate,
                Trader = trader,
                TradeStatus = tradeStatus,
                TradeType = tradeType,
                Version = Guid.NewGuid()
            };

            foreach (var key in Properties.Keys)
            {
                if (diffObject.Properties.ContainsKey(key))
                {
                    if (Properties[key] != diffObject.Properties[key])
                    {
                        ret.Properties.Add(key, diffObject.Properties[key]);
                    }
                }
            }

            foreach (var key in diffObject.Properties.Keys)
            {
                if (!Properties.ContainsKey(key))
                {
                    ret.Properties.Add(key, diffObject.Properties[key]);
                }
            }

            return ret;
        }
    }
}