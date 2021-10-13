using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using NUnit.Framework;
using TradeStore.Models;
using TradeStore.Models.Exceptions;

namespace TradeStore.Tests
{
    [TestFixture]
    public class BookProcessing
    {
        [Test]
        public void ShouldProduceCorrectDiffObject()
        {
            Book subjectBook = new Book { Id = 1234, Name = "ABC", Created = DateTime.UtcNow, Version = Guid.NewGuid()};
            subjectBook.Trades.Add(1234, new List<Trade>
            {
                new Trade(){Id = 345678}
            });
            
            Book objectBook = new Book { Id = 1234, Name = "AFD", Created = DateTime.UtcNow, Version = Guid.NewGuid()};
            
            objectBook.Trades.Add(1234, new List<Trade>
            {
                new(){Id = 345678},
            }); 
            
            objectBook.Trades.Add(1264, new List<Trade>
            {
                new(){Id = 45678},
                new(){Id = 56789},
            });

            var result = subjectBook.GetDiff(objectBook);
            
            Assert.AreNotEqual(result.Version, subjectBook.Version);
            Assert.AreNotEqual(result.Version, objectBook.Version);
            Assert.AreEqual(1234, result.Id);
            Assert.IsNotNull(result.Name);
            Assert.IsNotNull(result.Created);
            Assert.AreEqual(2, result.Trades.Count);
        }

        [Test]
        public void ShouldThrowExceptionForIdMismatchWhenDiffing()
        {
            Book subjectBook = new Book { Id = 1234, Name = "ABC" };
            Book objectBook = new Book { Id = 1244, Name = "ABC" };

            try
            {
                var result = subjectBook.GetDiff(objectBook);
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
                Book subjectBook = new Book { Id = 1234, Name = "ABC", Version = guid};
                Book objectBook = new Book { Id = 1244, Name = "ABC", Version = guid};
                
                var result = subjectBook.GetDiff(objectBook);
                
                Assert.Fail();
            }
            catch (DifferenceException)
            {
                Assert.Pass();
            }
        }

        [Test]
        public void ShouldReturnSameObjectIfVersionsAreSame()
        {
            Guid guid = Guid.NewGuid();
            Book subjectBook = new Book { Id = 1234, Name = "ABC", Version = guid};
            Book objectBook = new Book { Id = 1234, Name = "ASH", Version = guid};
                
            var result = subjectBook.GetDiff(objectBook);
            
            Assert.AreSame(result, subjectBook);
        }
    }
}