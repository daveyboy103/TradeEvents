using System;
using System.Collections.Generic;
using TradeStore.Models.Exceptions;
using TradeStore.Models.Interfaces;

// ReSharper disable once CheckNamespace
namespace TradeStore.Models
{
    public partial class Book : IBook, IDifference<Models.Book>
    {
        public Book GetDiff(Models.Book diffObject)
        {
            
            if (this.Id != diffObject.Id)
                throw new DifferenceException(
                    $"Book Ids do not match of object being diffed, Subject: [{this.Id}], Object: [{diffObject.Id}]");

            if (Version == diffObject.Version)
                return this;

            string name = null;
            DateTime? created = null;

            if (Name != diffObject.Name)
                name = diffObject.Name;
            
            if (Created != diffObject.Created)
                created = diffObject.Created;

            Book ret = new Book
            {
                Id = Id,
                Created = created,
                Name = name
            };

            foreach (long key in diffObject.Trades.Keys)
            {
                ret.Trades.Add(key, new List<Trade>(diffObject.Trades[key]));
            }

            return ret;
        }
    }
}