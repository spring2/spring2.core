using System;
using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;
using Spring2.Core.Types;
using Spring2.Core.Util;

namespace Spring2.Core.Test 
{
	/// <summary>
	/// Summary description for PagedListTest.
	/// </summary>
	[TestFixture()]
	public class RangeTest 
	{
		[Test()]
		public void ShouldBeAbleToCreateRange() 
		{
			DateTime today = DateTime.Now;
			DateTime yesterday = today.AddDays(-1);

			Range<DateTime> obj = new Range<DateTime>(yesterday, today);
			Assert.AreEqual(yesterday, obj.Start);
			Assert.AreEqual(today, obj.End);
		}
		[Test()]
		public void ShouldNotBeAbleToCreateRangeWhereStartIsAfterEnd()
		{
			DateTime today = DateTime.Now;
			DateTime yesterday = today.AddDays(-1);

			try
			{
				Range<DateTime> obj = new Range<DateTime>(today, yesterday);
				Assert.Fail("Should not allow a Range where end is before start");
			}
			catch
			{
				//Expected
			}
		}
		[Test()]
		public void ShouldCorrectlyDetermineIfRangesOverlap()
		{
			int one = 1;
			int five = 5;
			int ten = 10;
			int fifteen = 15;

			Assert.IsTrue(Range.Overlap<int>(new Range<int>(one, ten), new Range<int>(five, fifteen)));
			Assert.IsTrue(Range.Overlap<int>(new Range<int>(one, five), new Range<int>(five, ten)));
			Assert.IsTrue(Range.Overlap<int>(new Range<int>(one, ten), new Range<int>(one, ten)));
			Assert.IsFalse(Range.Overlap<int>(new Range<int>(one, five), new Range<int>(ten, fifteen)));
			Assert.IsFalse(Range.Overlap<int>(new Range<int>(one, one), new Range<int>(fifteen, fifteen)));
		}
	}
}