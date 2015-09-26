using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelperLibrary;

namespace HelperLibraryUnitTests
{
     [TestClass]
    public class StringHelpersTests
    {
         [TestMethod]
         public void helper_library_reverse_string()
         {
             Assert.AreEqual("DCBA", StringHelpers.ReverseString("ABCD"));
         }

         [TestMethod]
         public void helper_library_explode()
         {
             string testString = "A,B,test,bc";
             var result = testString.Explode(',');

             Assert.AreEqual(4, result.Count);
             Assert.AreEqual("test", result[2]);
         }

         [TestMethod]
         public void helper_library_read_from_list()
         {
             string[] list = new string[] { "a", "b", "test", "c" };

             Assert.AreEqual("a", list.ReadFromList(0));
             Assert.AreEqual("b", list.ReadFromList(1));
             Assert.AreEqual("test", list.ReadFromList(2));
             Assert.AreEqual("c", list.ReadFromList(3));
             Assert.AreEqual(null, list.ReadFromList(4));
         }

         [TestMethod]
         public void helper_library_to_nullable_string_is_null()
         {
             object testObj = null;

             Assert.AreEqual(null, testObj.ToNullableString());
         }

         [TestMethod]
         public void helper_library_to_nullable_string_is_string()
         {
             object testObj = "test string";

             Assert.AreEqual("test string", testObj.ToNullableString());
         }

         [TestMethod]
         public void helper_library_to_nullable_string_is_double()
         {
             object testObj = 3.4;

             Assert.AreEqual("3.4", testObj.ToNullableString());
         }

         [TestMethod]
         public void helper_library_to_nullable_int_is_null()
         {
             object testObj = null;

             Assert.AreEqual(0, testObj.ToNullableInt());
         }

         [TestMethod]
         public void helper_library_to_nullable_string_is_int()
         {
             object testObj = 33;

             Assert.AreEqual(33, testObj.ToNullableInt());
         }

         [TestMethod]
         public void helper_library_to_int()
         {
             object testObj = "349";

             Assert.AreEqual(349, testObj.ToInt());
         }

         [TestMethod]
         public void helper_library_to_double()
         {
             object testObj = "349.48";

             Assert.AreEqual(349.48, testObj.DBNullToDouble());
         }
        
         [TestMethod]
         public void helper_library_is_numeric()
         {
             Assert.IsTrue("4".IsNumeric());
             Assert.IsTrue("74.35".IsNumeric());
             Assert.IsFalse("a".IsNumeric());
             Assert.IsFalse("@*".IsNumeric());
         }

         [TestMethod]
         public void helper_library_is_int()
         {
             Assert.IsTrue("4".IsInt());
             Assert.IsFalse("74.35".IsInt());
             Assert.IsFalse("a".IsInt());
             Assert.IsFalse("@*".IsInt());
         }

         [TestMethod]
         public void helper_library_two_char_week_name()
         {
             Assert.AreEqual(DayOfWeek.Monday, "MO".TwoCharWeekName());
             Assert.AreEqual(DayOfWeek.Tuesday, "TU".TwoCharWeekName());
             Assert.AreEqual(DayOfWeek.Wednesday, "WE".TwoCharWeekName());
             Assert.AreEqual(DayOfWeek.Thursday, "TH".TwoCharWeekName());
             Assert.AreEqual(DayOfWeek.Friday, "FR".TwoCharWeekName());
             Assert.AreEqual(DayOfWeek.Saturday, "SA".TwoCharWeekName());
             Assert.AreEqual(DayOfWeek.Sunday, "SU".TwoCharWeekName());
         }
    }
}
