using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelperLibrary;

namespace HelperLibraryUnitTests
{
    [TestClass]
    public class HelperLibTests
    {
        [TestMethod]
        public void helper_library_convert_to_db_null_date_is_empty()
        {
            Assert.AreEqual(DBNull.Value, "".ConvertToDBNullDate());
        }

        [TestMethod]
        public void helper_library_convert_to_db_null_date_is_null()
        {
            string testDate = null;
            Assert.AreEqual(DBNull.Value, testDate.ConvertToDBNullDate());
        }

        [TestMethod]
        public void helper_library_convert_to_db_null_date_is_valid()
        {
            Assert.AreEqual(DateTime.Parse("3/3/2014"), "3/3/2014".ConvertToDBNullDate());
        }

        [TestMethod]
        public void helper_library_convert_to_db_null_double_is_empty()
        {
            string testDouble = "";
            Assert.AreEqual(DBNull.Value, testDouble.ConvertToDBNullDouble());
        }

        [TestMethod]
        public void helper_library_convert_to_db_null_double_is_null()
        {
            string testDouble = null;
            Assert.AreEqual(DBNull.Value, testDouble.ConvertToDBNullDouble());
        }

        [TestMethod]
        public void helper_library_convert_to_db_null_double_is_valid()
        {
            string testDouble = "3.14159";
            Assert.AreEqual(3.14159, testDouble.ConvertToDBNullDouble());
        }

        [TestMethod]
        public void helper_library_db_null_int_is_null()
        {
            object testInt = DBNull.Value;
            Assert.AreEqual(null, testInt.DBNullToInt());
        }

        [TestMethod]
        public void helper_library_db_null_int_is_valid()
        {
            object testInt = 54;
            Assert.AreEqual(54, testInt.DBNullToInt());
        }

        [TestMethod]
        public void helper_library_db_null_double_is_null()
        {
            object testInt = DBNull.Value;
            Assert.AreEqual(null, testInt.DBNullToDouble());
        }

        [TestMethod]
        public void helper_library_db_null_double_is_valid()
        {
            object testInt = 54.7;
            Assert.AreEqual(54.7, testInt.DBNullToDouble());
        }
    }
}
