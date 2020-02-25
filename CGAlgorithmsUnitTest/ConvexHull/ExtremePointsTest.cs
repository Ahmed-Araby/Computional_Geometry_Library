using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CGAlgorithms.Algorithms.ConvexHull;
using CGAlgorithms;
using CGUtilities;
using System.Collections.Generic;

namespace CGAlgorithmsUnitTest
{
    /// <summary>
    /// Unit Test for Convex Hull
    /// </summary>
    [TestClass]
    public class ExtremePointsTest : ConvexHullTest
    {
		[TestMethod]
		public void ExtremePointsTestCase1()
		{
			convexHullTester = new ExtremePoints();
			Case1();
		}
		[TestMethod]
		public void ExtremePointsTestCase2()
		{
			convexHullTester = new ExtremePoints();
			Case2();
		}
		[TestMethod]
		public void ExtremePointsTestCase3()
		{
			convexHullTester = new ExtremePoints();
			Case3();
		}
		[TestMethod]
		public void ExtremePointsTestCase4()
		{
			convexHullTester = new ExtremePoints();
			Case4();
		}
		[TestMethod]
		public void ExtremePointsTestCase5()
		{
			convexHullTester = new ExtremePoints();
			Case5();
		}
		[TestMethod]
		public void ExtremePointsTestCase16()
		{
			convexHullTester = new ExtremePoints();
			Case16();
		}
		[TestMethod]
		public void ExtremePointsTestCase17()
		{
			convexHullTester = new ExtremePoints();
			Case17();
		}
		[TestMethod]
		public void ExtremePointsTestCase18()
		{
			convexHullTester = new ExtremePoints();
			Case18();
		}
		[TestMethod]
		public void ExtremePointsTestCase19()
		{
			convexHullTester = new ExtremePoints();
			Case19();
		}
		[TestMethod]
		public void ExtremePointsTestCase20()
		{
			convexHullTester = new ExtremePoints();
			Case20();
		}
    }
}
