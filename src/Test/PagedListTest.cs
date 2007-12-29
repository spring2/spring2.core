#if (NET_1_1)
#else
using System;
using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;
using Spring2.Core.Types;
using Spring2.Core.Util;

namespace Spring2.Core.Test {

    /// <summary>
    /// Summary description for PagedListTest.
    /// </summary>
    [TestFixture()]
    public class PagedListTest {

	[Test()]
	public void ShouldBeAbleToCreatePageList() {
	    PagedList<ArrayList> pagedList = new PagedList<ArrayList>(5);
	    Assert.AreEqual(5, pagedList.PageSize);
	}

	[Test()]
	public void ShouldBeAbleToCreatePageListWithList() {
	    ArrayList list = new ArrayList();
	    list.Add(101);
	    list.Add(202);

	    PagedList<ArrayList> pageList = new PagedList<ArrayList>(5, list);
	    Assert.AreEqual(5, pageList.PageSize);
	    Assert.AreEqual(list.Count, pageList.GetListByPageNumber(1).Count);
	}

	[Test()]
	public void ShouldHaveAccessToCompleteListCount() {
	    ArrayList list = new ArrayList();
	    for(Int32 i = 100; i < 145; i++) {
		list.Add(i);
	    }

	    PagedList<ArrayList> pageList = new PagedList<ArrayList>(5, list);
	    Assert.AreEqual(45, pageList.TotalRecords);
	}

	[Test()]
	public void ShouldKnowNumberOfPagesThereAre() {
	    ArrayList list = new ArrayList();
	    for(Int32 i = 100; i < 145; i++) {
		list.Add(i);
	    }

	    PagedList<ArrayList> pageList = new PagedList<ArrayList>(5, list);
	    Assert.AreEqual(5, pageList.PageSize);
	    Assert.AreEqual(9, pageList.TotalPages);
	}

	[Test()]
	public void ShouldBeAbleToAddRangeToPageList() {
	    ArrayList list = new ArrayList();
	    for(Int32 i = 100; i < 145; i++) {
		list.Add(i);
	    }

	    PagedList<ArrayList> pageList = new PagedList<ArrayList>(5, list);
	    Assert.AreEqual(5, pageList.PageSize);
	    Assert.AreEqual(9, pageList.TotalPages);

	    ArrayList list2 = new ArrayList();
	    for(Int32 j = 200; j < 210; j++) {
		list2.Add(j);
	    }

	    pageList.AddRange(list2);
	    Assert.AreEqual(11, pageList.TotalPages);
	}

	[Test()]
	public void ShouldBeAbleToAddRangeToPageListWhenConstuctedWithoutList() {

	    PagedList<ArrayList> pageList = new PagedList<ArrayList>(5);
	    Assert.AreEqual(5, pageList.PageSize);

	    ArrayList list2 = new ArrayList();
	    for(Int32 j = 200; j < 210; j++) {
		list2.Add(j);
	    }

	    pageList.AddRange(list2);
	    Assert.AreEqual(2, pageList.TotalPages);
	}

	[Test()]
	public void ShouldGetRightSubListByPage() {
	    ArrayList list = new ArrayList();
	    for(Int32 i = 100; i < 145; i++) {
		list.Add(i);
	    }

	    PagedList<ArrayList> pageList = new PagedList<ArrayList>(5, list);
	    Assert.AreEqual(5, pageList.PageSize);
	    Assert.AreEqual(9, pageList.TotalPages);
	    ArrayList tempList = pageList.GetListByPageNumber(1);
	    Assert.AreEqual(100, tempList[0]);
	    Assert.AreEqual(104, tempList[4]);
	    tempList = pageList.GetListByPageNumber(3);
	    Assert.AreEqual(110, tempList[0]);
	    Assert.AreEqual(114, tempList[4]);
	    tempList = pageList.GetListByPageNumber(9);
	    Assert.AreEqual(140, tempList[0]);
	    Assert.AreEqual(144, tempList[4]);
	}

	[Test()]
	public void ShouldKnowIfHasPrevious() {
	    ArrayList list = new ArrayList();
	    for(Int32 i = 100; i < 145; i++) {
		list.Add(i);
	    }

	    PagedList<ArrayList> pageList = new PagedList<ArrayList>(5, list);
	    Assert.AreEqual(false, pageList.HasPrevious(1));
	    Assert.AreEqual(true, pageList.HasPrevious(5));
	    Assert.AreEqual(true, pageList.HasPrevious(9));
	}

	[Test()]
	public void ShouldKnowIfHasNext() {
	    ArrayList list = new ArrayList();
	    for(Int32 i = 100; i < 145; i++) {
		list.Add(i);
	    }

	    PagedList<ArrayList> pageList = new PagedList<ArrayList>(5, list);
	    Assert.AreEqual(false, pageList.HasNext(9));
	    Assert.AreEqual(true, pageList.HasNext(5));
	    Assert.AreEqual(true, pageList.HasNext(1));
	}

	[Test()]
	public void ShouldHandleItWhenLastPageHasFewerRecordsThenPageSize() {
	    ArrayList list = new ArrayList();
	    for(Int32 i = 100; i < 123; i++) {
		list.Add(i);
	    }

	    PagedList<ArrayList> pageList = new PagedList<ArrayList>(5, list);
	    Assert.AreEqual(3, pageList.GetListByPageNumber(5).Count);
	}

	[Test()]
	public void ShouldBeAbleToGetPageNumberFromIndex() {
	    ArrayList list = new ArrayList();
	    for(Int32 i = 100; i < 145; i++) {
		list.Add(i);
	    }

	    PagedList<ArrayList> pageList = new PagedList<ArrayList>(5, list);
	    Assert.AreEqual(1, pageList.GetPageNumber(0));
	    Assert.AreEqual(1, pageList.GetPageNumber(2));
	    Assert.AreEqual(1, pageList.GetPageNumber(4));
	    Assert.AreEqual(3, pageList.GetPageNumber(10));
	    Assert.AreEqual(3, pageList.GetPageNumber(12));
	    Assert.AreEqual(3, pageList.GetPageNumber(14));
	    Assert.AreEqual(9, pageList.GetPageNumber(40));
	    Assert.AreEqual(9, pageList.GetPageNumber(43));
	    Assert.AreEqual(9, pageList.GetPageNumber(44));
	}

	[Test()]
	public void ShouldGetIndexOutOfRangeExceptionIfTryingToGetPageNumberWithBadIndex() {
	    ArrayList list = new ArrayList();
	    for(Int32 i = 100; i < 145; i++) {
		list.Add(i);
	    }

	    PagedList<ArrayList> pageList = new PagedList<ArrayList>(5, list);
	    try {
		pageList.GetPageNumber(-1);
		Assert.Fail("Should have gotten a IndexOutOfRangeException");
	    } catch(IndexOutOfRangeException) {
		//Expected
	    }
	    try {
		pageList.GetPageNumber(45);
		Assert.Fail("Should have gotten a IndexOutOfRangeException");
	    } catch(IndexOutOfRangeException) {
		//Expected
	    }
	}

	[Test()]
	public void ShouldGetIndexOutOfRangeExceptionWhenAskingForNegitivePage() {
	    ArrayList list = new ArrayList();
	    for(Int32 i = 100; i < 145; i++) {
		list.Add(i);
	    }

	    PagedList<ArrayList> pageList = new PagedList<ArrayList>(5, list);

	    try {
		pageList.GetListByPageNumber(0);
		Assert.Fail("Should have gotten a IndexOutOfRangeException");
	    } catch(IndexOutOfRangeException) {
		//Expected
	    }
	    try {
		pageList.HasNext(0);
		Assert.Fail("Should have gotten a IndexOutOfRangeException");
	    } catch(IndexOutOfRangeException) {
		//Expected
	    }
	    try {
		pageList.HasPrevious(0);
		Assert.Fail("Should have gotten a IndexOutOfRangeException");
	    } catch(IndexOutOfRangeException) {
		//Expected
	    }
	    try {
		pageList.GetStartNumber(0);
		Assert.Fail("Should have gotten a IndexOutOfRangeException");
	    } catch(IndexOutOfRangeException) {
		//Expected
	    }
	    try {
		pageList.GetEndNumber(0);
		Assert.Fail("Should have gotten a IndexOutOfRangeException");
	    } catch(IndexOutOfRangeException) {
		//Expected
	    }
	}

	[Test()]
	public void ShouldGetIndexOutOfRangeExceptionWhenAskingForPageLargerThenTotalPages() {
	    ArrayList list = new ArrayList();
	    for(Int32 i = 100; i < 145; i++) {
		list.Add(i);
	    }

	    PagedList<ArrayList> pageList = new PagedList<ArrayList>(5, list);

	    try {
		pageList.GetListByPageNumber(10);
		Assert.Fail("Should have gotten a IndexOutOfRangeException");
	    } catch(IndexOutOfRangeException) {
		//Expected
	    }
	    try {
		pageList.HasNext(10);
		Assert.Fail("Should have gotten a IndexOutOfRangeException");
	    } catch(IndexOutOfRangeException) {
		//Expected
	    }
	    try {
		pageList.HasPrevious(10);
		Assert.Fail("Should have gotten a IndexOutOfRangeException");
	    } catch(IndexOutOfRangeException) {
		//Expected
	    }
	    try {
		pageList.GetStartNumber(10);
		Assert.Fail("Should have gotten a IndexOutOfRangeException");
	    } catch(IndexOutOfRangeException) {
		//Expected
	    }
	    try {
		pageList.GetEndNumber(10);
		Assert.Fail("Should have gotten a IndexOutOfRangeException");
	    } catch(IndexOutOfRangeException) {
		//Expected
	    }
	}

	[Test()]
	public void ShouldBeAbleToGetStartItemNumberForPage() {
	    ArrayList list = new ArrayList();
	    for(Int32 i = 100; i < 145; i++) {
		list.Add(i);
	    }

	    PagedList<ArrayList> pageList = new PagedList<ArrayList>(5, list);
	    Assert.AreEqual(1, pageList.GetStartNumber(1));
	    Assert.AreEqual(21, pageList.GetStartNumber(5));
	    Assert.AreEqual(41, pageList.GetStartNumber(9));
	}

	[Test()]
	public void ShouldBeAbleToGetEndItemNumberForPage() {
	    ArrayList list = new ArrayList();
	    for(Int32 i = 100; i < 147; i++) {
		list.Add(i);
	    }

	    PagedList<ArrayList> pageList = new PagedList<ArrayList>(5, list);
	    Assert.AreEqual(5, pageList.GetEndNumber(1));
	    Assert.AreEqual(25, pageList.GetEndNumber(5));
	    Assert.AreEqual(45, pageList.GetEndNumber(9));
	    Assert.AreEqual(47, pageList.GetEndNumber(10));
	}

	[Test()]
	public void ShouldBeAbleToUseContainsMethodOnGeneratedList() {
	    //WebFacade facade = new WebFacade();
	    //IDirectSalesAgentUser dsaUser = null;
	    //ICustomer customer = null;
	    //ICustomer customer2 = null;
	    //try {
	    //    dsaUser = TestUtility.CreateNewDSAUser(true);
	    //    customer = TestUtility.CreateNewCustomer(dsaUser.DirectSalesAgent.DirectSalesAgentId);
	    //    customer2 = TestUtility.CreateNewCustomer(dsaUser.DirectSalesAgent.DirectSalesAgentId);

	    //    facade.Login(dsaUser.Username, TestUtility.PASSWORD, "1.1.1.1");

	    //    CustomerList list = facade.CustomerLookupAll(StringType.DEFAULT, StringType.DEFAULT, StringType.DEFAULT, USStateCodeEnum.DEFAULT, CustomerStatusEnum.DEFAULT, BooleanType.DEFAULT, BooleanType.DEFAULT);

	    //    PagedList2<CustomerList, ICustomer> pagedList = new PagedList2<CustomerList, ICustomer>(5, list);
	    //    CustomerList list2 = pagedList.GetListByPageNumber(1);
	    //    Assert.AreEqual(3, list2.Count);
	    //    Assert.IsTrue(list2.Contains(list[0].CustomerId), "should have found customer");
	    //} finally {
	    //    TestUtility.DeleteCustomer(customer);
	    //    TestUtility.DeleteCustomer(customer2);
	    //    TestUtility.DeleteDemonstrator(facade.GetDemonstrator(dsaUser.DirectSalesAgent.DirectSalesAgentId));
	    //}
	}
    }
}
#endif