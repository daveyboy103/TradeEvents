using System;
using NUnit.Framework;
using TradeStore.Models;
using TradeStore.Models.Exceptions;
using TradeStore.Models.Interfaces;

namespace TradeStore.Tests
{
    public class TradeProcessing
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldProduceCorrectDiffObject()
        {
            Trade tradeSubject = new Trade
            {
                Id = 1234,
                Created = DateTime.Now, 
                Trader = new Trader(1234, "Bill Jones"),
                TradeDate = new DateTime(2021, 3,5),
                TradeStatus = TradeStatus.Live,
                Version = Guid.NewGuid()
            };
            
            tradeSubject.Properties.Add("Sample", "Sample Property");
            tradeSubject.Properties.Add("SampleChanged", "Sample Property");
            
            Trade tradeObject = new Trade {
                Id = 1234,
                Created = DateTime.Now, 
                Trader = new Trader(1234, "Bill Jones"),
                TradeDate = new DateTime(2021, 3,5),
                TradeStatus = TradeStatus.Cancelled,
                Version = Guid.NewGuid()
            };
            
            tradeObject.Properties.Add("Sample", "Sample Property");
            tradeObject.Properties.Add("SampleChanged", "Sample Property Changed");
            tradeObject.Properties.Add("ImNew", "New Property");

            var result = tradeSubject.GetDiff(tradeObject);
            
            Assert.IsTrue(result.Version != tradeSubject.Version);
            Assert.IsTrue(result.Version != tradeObject.Version);
            Assert.AreEqual(1234, result.Id);
            Assert.IsNotNull(result.Created);
            Assert.IsNull(result.Trader);
            Assert.IsNull(result.TradeDate);
            Assert.IsNotNull(result.TradeStatus);
            Assert.AreEqual(2, result.Properties.Count);
            
        }

        [Test]
        public void ShouldThrowExceptionForTradeIdMismatchWhenDiffing()
        {
            Trade tradeObject = new Trade { Id = 1234 };
            Trade tradeSubject = new Trade { Id = 1567 };

            try
            {
                var result = tradeSubject.GetDiff(tradeObject);
            }
            catch (DifferenceException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void ShouldThrowExceptionIfVersionsSameButIdDifferent()
        {
            try
            {
                Guid guid = Guid.NewGuid();
                Trade tradeObject = new Trade { Id = 1234 , Version = guid};
                Trade tradeSubject = new Trade { Id = 1254 , Version = guid};

                var result = tradeObject.GetDiff(tradeSubject);
                
                Assert.Fail();
            }
            catch (DifferenceException )
            {
                Assert.Pass();
            }
        }

        [Test]
        public void ShouldReturnSameObjectIfVersionsAreSame()
        {
            Guid guid = Guid.NewGuid();
            Trade tradeObject = new Trade { Id = 1234 , Version = guid};
            Trade tradeSubject = new Trade { Id = 1234 , Version = guid};

            var result = tradeObject.GetDiff(tradeSubject);
            
            Assert.AreSame(tradeObject, result);
        }
    }
}