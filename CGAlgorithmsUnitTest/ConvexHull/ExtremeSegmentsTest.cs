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
    public class ExtremeSegmentsTest : ConvexHullTest
    {
		[TestMethod]
		public void ExtremeSegmentsTestCase1()
		{
			convexHullTester = new ExtremeSegments();
			Case1();
		}
		[TestMethod]
		public void ExtremeSegmentsTestCase2()
		{
			convexHullTester = new ExtremeSegments();
			Case2();
		}
		[TestMethod]
		public void ExtremeSegmentsTestCase3()
		{
			convexHullTester = new ExtremeSegments();
			Case3();
		}
		[TestMethod]
		public void ExtremeSegmentsTestCase4()
		{
			convexHullTester = new ExtremeSegments();
			Case4();
		}
		[TestMethod]
		public void ExtremeSegmentsTestCase5()
		{
			convexHullTester = new ExtremeSegments();
			Case5();
		}
		[TestMethod]
		public void ExtremeSegmentsTestCase6()
		{
			convexHullTester = new ExtremeSegments();
			Case6();
		}
		[TestMethod]
		public void ExtremeSegmentsTestCase7()
		{
			convexHullTester = new ExtremeSegments();
			Case7();
		}
		[TestMethod]
		public void ExtremeSegmentsTestCase8()
		{
			convexHullTester = new ExtremeSegments();
			Case8();
		}
		[TestMethod]
		public void ExtremeSegmentsTestCase9()
		{
			convexHullTester = new ExtremeSegments();
			Case9();
		}
		[TestMethod]
		public void ExtremeSegmentsTestCase10()
		{
			convexHullTester = new ExtremeSegments();
			Case10();
		}
		[TestMethod]
		public void ExtremeSegmentsTestCase16()
		{
			convexHullTester = new ExtremeSegments();
			Case16();
		}
		[TestMethod]
		public void ExtremeSegmentsTestCase17()
		{
			convexHullTester = new ExtremeSegments();
			Case17();
		}
		[TestMethod]
		public void ExtremeSegmentsTestCase18()
		{
			convexHullTester = new ExtremeSegments();
			Case18();
		}
		[TestMethod]
		public void ExtremeSegmentsTestCase19()
		{
			convexHullTester = new ExtremeSegments();
			Case19();
		}
		[TestMethod]
		public void ExtremeSegmentsTestCase20()
		{
			convexHullTester = new ExtremeSegments();
			Case20();
		}
    }
}
