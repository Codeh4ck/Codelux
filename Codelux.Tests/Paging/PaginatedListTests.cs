using System;
using System.Collections.Generic;
using Codelux.Paging;
using NUnit.Framework;

namespace Codelux.Tests.Paging
{
    [TestFixture]
    public class PaginatedListTests
    {
        private List<string> _supersetList;

        [SetUp]
        public void Setup()
        {
            _supersetList = new();
            PopulateSupersetList();
        }

        [Test]
        public void Given_Superset_List_When_I_Populate_PaginatedList_Then_It_Paginates_Correctly()
        {
            PagedList<string> pagedList = new(_supersetList, 1, 10);

            Assert.NotNull(pagedList);
            Assert.IsTrue(pagedList.IsFirstPage);
            Assert.IsFalse(pagedList.IsLastPage);
            Assert.IsTrue(pagedList.HasNextPage);
            Assert.IsFalse(pagedList.HasPreviousPage);
            
            Assert.AreEqual(_supersetList.Count, pagedList.ItemCount);
            Assert.AreEqual(10, pagedList.Count);
        }

        [Test]
        public void Given_PagedList_When_I_Browse_All_Pages_Then_Pages_Are_Correct()
        {
            int itemsPerPage = 10;
            PagedList<string> pagedList = new(_supersetList, 1, itemsPerPage);

            Assert.NotNull(pagedList);
            Assert.IsTrue(pagedList.IsFirstPage);
            Assert.IsFalse(pagedList.IsLastPage);
            Assert.IsTrue(pagedList.HasNextPage);
            Assert.IsFalse(pagedList.HasPreviousPage);

            Assert.AreEqual(_supersetList.Count, pagedList.ItemCount);
            Assert.AreEqual(10, pagedList.Count);

            int pageCount = pagedList.GetPageHeader().PageCount;

            for (int x = 0; x < pageCount; x++)
            {
                PagedList<string> page = new(_supersetList, x + 1, 10);

                Assert.NotNull(page);

                if (x == 0)
                    Assert.IsTrue(page.IsFirstPage);
                else
                    Assert.IsFalse(page.IsFirstPage);

                if (x == pageCount - 1)
                    Assert.True(page.IsLastPage);
                else
                    Assert.IsFalse(page.IsLastPage);

                if (x == pageCount - 1)
                    Assert.IsTrue(page.IsLastPage);
                else
                    Assert.IsFalse(page.IsLastPage);

                if (x == 0)
                    Assert.IsFalse(page.HasPreviousPage);
                else
                    Assert.IsTrue(page.HasPreviousPage);

                Assert.AreEqual(10, page.Count);
            }
        }

        private void PopulateSupersetList(bool useOddNumber = false)
        {
            int pageCount = useOddNumber ? 49 : 50;

            for (int x = 0; x < pageCount; x++)
            {
                _supersetList.Add(Guid.NewGuid().ToString("N"));
            }
        }
    }
}
