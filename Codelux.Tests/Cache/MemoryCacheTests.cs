using System;
using Codelux.Cache;
using NUnit.Framework;

namespace Codelux.Tests.Cache
{
    [TestFixture]
    public class MemoryCacheTests
    {
        private MemoryCache _memoryCache;

        [SetUp]
        public void Setup()
        {
            _memoryCache = new(TimeSpan.FromSeconds(5));
        }

        [Test]
        public void GivenMemoryCacheInitializedWhenIAddAnItemThenItemIsAdded()
        {
            TestModel model = new() {Id = Guid.NewGuid()};

            Assert.DoesNotThrow(delegate()
            {
                _memoryCache.Set("TestModel", model, ExpirationOptions.CreateWithNoExpiration());
            });

            Assert.AreEqual(1, _memoryCache.NumberOfItems);
        }

        [Test]
        public void GivenMemoryCacheInitializedWhenIAddAnItemWithTheSameKeyThenItemIsReplaced()
        {
            TestModel model = new() { Id = Guid.NewGuid() };
            TestModel modelReplacement = new() { Id = Guid.NewGuid() };

            Assert.DoesNotThrow(delegate ()
            {
                _memoryCache.Set("TestModel", model, ExpirationOptions.CreateWithNoExpiration());
            });

            Assert.AreEqual(1, _memoryCache.NumberOfItems);

            Assert.DoesNotThrow(delegate ()
            {
                _memoryCache.Set("TestModel", modelReplacement, ExpirationOptions.CreateWithNoExpiration());
            });

            Assert.AreEqual(1, _memoryCache.NumberOfItems);

            bool retrieved = _memoryCache.TryGet<TestModel>("TestModel", out TestModel retrievedModel);

            Assert.IsTrue(retrieved);
            Assert.IsTrue(modelReplacement == retrievedModel);
        }

        [Test]
        public void GivenMemoryCacheInitializedWhenIGetAnItemThenItemIsReturned()
        {
            TestModel model = new() { Id = Guid.NewGuid() };

            Assert.DoesNotThrow(delegate ()
            {
                _memoryCache.Set("TestModel", model, ExpirationOptions.CreateWithNoExpiration());
            });

            Assert.AreEqual(1, _memoryCache.NumberOfItems);

            bool retrieved = _memoryCache.TryGet<TestModel>("TestModel", out TestModel retrievedModel);

            Assert.IsTrue(retrieved);
            Assert.IsTrue(model == retrievedModel);
        }

        [Test]
        public void GivenMemoryCacheInitializedWhenIGetAnItemAndItDoesNotExistThenItemIsNotReturned()
        {
            TestModel model = new() { Id = Guid.NewGuid() };

            Assert.DoesNotThrow(delegate ()
            {
                _memoryCache.Set("TestModel", model, ExpirationOptions.CreateWithNoExpiration());
            });

            Assert.AreEqual(1, _memoryCache.NumberOfItems);

            bool retrieved = _memoryCache.TryGet<TestModel>("NotTheTestModel", out TestModel retrievedModel);

            Assert.IsFalse(retrieved);
            Assert.IsTrue(retrievedModel == null);
        }

        [Test]
        public void GivenMemoryCacheInitializedWhenIGetAnItemAndItIsExpiredThenItemIsNotReturned()
        {
            TestModel model = new() { Id = Guid.NewGuid() };

            Assert.DoesNotThrow(delegate ()
            {
                _memoryCache.Set("TestModel", model, ExpirationOptions.CreateWithExpirationAt(DateTime.Now.Subtract(TimeSpan.FromDays(1))));
            });

            Assert.AreEqual(1, _memoryCache.NumberOfItems);

            bool retrieved = _memoryCache.TryGet<TestModel>("TestModel", out TestModel retrievedModel);

            Assert.IsFalse(retrieved);
            Assert.IsTrue(retrievedModel == null);
        }

        [Test]
        public void GivenMemoryCacheInitializedWhenIRemoveAnItemThenItIsRemoved()
        {
            TestModel model = new() { Id = Guid.NewGuid() };

            Assert.DoesNotThrow(delegate ()
            {
                _memoryCache.Set("TestModel", model, ExpirationOptions.CreateWithNoExpiration());
            });

            Assert.AreEqual(1, _memoryCache.NumberOfItems);

            bool result = _memoryCache.Remove("TestModel");
            
            Assert.IsTrue(result);
            Assert.AreEqual(0, _memoryCache.NumberOfItems);
        }

        [Test]
        public void GivenMemoryCacheInitializedWhenIRemoveAnItemThatDoesNotExistThenCacheIsIntact()
        {
            TestModel model = new() { Id = Guid.NewGuid() };

            Assert.DoesNotThrow(delegate ()
            {
                _memoryCache.Set("TestModel", model, ExpirationOptions.CreateWithExpirationAt(DateTime.Now.Subtract(TimeSpan.FromDays(1))));
            });

            Assert.AreEqual(1, _memoryCache.NumberOfItems);

            bool result = _memoryCache.Remove("NotATestModel");

            Assert.IsFalse(result);
            Assert.AreEqual(1, _memoryCache.NumberOfItems);
        }

        [Test]
        public void GivenMemoryCacheInitializedWhenIFlushItThenNoItemsAreKept()
        {
            TestModel model1 = new() { Id = Guid.NewGuid() };
            TestModel model2 = new() { Id = Guid.NewGuid() };
            TestModel model3 = new() { Id = Guid.NewGuid() };
            TestModel model4 = new() { Id = Guid.NewGuid() };

            Assert.DoesNotThrow(delegate ()
            {
                _memoryCache.Set("TestModel1", model1, ExpirationOptions.CreateWithNoExpiration());
                _memoryCache.Set("TestModel2", model2, ExpirationOptions.CreateWithNoExpiration());
                _memoryCache.Set("TestModel3", model3, ExpirationOptions.CreateWithNoExpiration());
                _memoryCache.Set("TestModel4", model4, ExpirationOptions.CreateWithNoExpiration());
            });

            Assert.AreEqual(4, _memoryCache.NumberOfItems);
            Assert.DoesNotThrow(delegate() { _memoryCache.Flush(); }); 

            Assert.AreEqual(0, _memoryCache.NumberOfItems);
        }

        [Test]
        public void GivenMemoryCacheInitializedWhenIInsertAnItemWithThatDoesNotExistThenItIsAdded()
        {
            TestModel model = new() { Id = Guid.NewGuid() };
            
            bool inserted = _memoryCache.InsertIfNotExists("TestModel", model, ExpirationOptions.CreateWithNoExpiration());

            Assert.IsTrue(inserted);
            Assert.AreEqual(1, _memoryCache.NumberOfItems);
        }

        [Test]
        public void GivenMemoryCacheInitializedWhenInsertAnItemWithThatExistsThenItIsNotAdded()
        {
            TestModel model = new() { Id = Guid.NewGuid() };
            TestModel model2 = new() { Id = Guid.NewGuid() };

            bool inserted = _memoryCache.InsertIfNotExists("TestModel", model, ExpirationOptions.CreateWithNoExpiration());

            Assert.IsTrue(inserted);
            Assert.AreEqual(1, _memoryCache.NumberOfItems);

            bool secondInserted = _memoryCache.InsertIfNotExists("TestModel", model2, ExpirationOptions.CreateWithNoExpiration());
            
            Assert.IsFalse(secondInserted);
            Assert.AreEqual(1, _memoryCache.NumberOfItems);
        }


        // This test does not work on BitBucket pipelines
        //[Test]
        //public void GivenMemoryCacheInitializedWhenInsertAnItemWithExpiryDaysThenItIsDeletedAfterTimespan()
        //{
        //    TestModel model = new TestModel() { Id = Guid.NewGuid() };

        //    bool inserted = _memoryCache.InsertIfNotExists("TestModel", model,
        //        ExpirationOptions.CreateWithExpirationIn(TimeSpan.FromSeconds(5)));

        //    Assert.IsTrue(inserted);
        //    Assert.AreEqual(1, _memoryCache.NumberOfItems);

        //    //await Task.Delay(TimeSpan.FromSeconds(12));

        //    Thread.Sleep(1000 * 12);

        //    bool result = _memoryCache.TryGet("TestModel", out TestModel outModel);

        //    Assert.IsFalse(result);
        //    Assert.IsNull(outModel);

        //    Assert.AreEqual(0, _memoryCache.NumberOfItems);
        //}
    }
    
    class TestModel
    {
        public Guid Id { get; set; }
    }
}
