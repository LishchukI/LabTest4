using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.BinaryFlag;
using IIG.CoSFE.DatabaseUtils;

namespace Lab4Flag
{
    [TestClass]
    public class BinaryFlagWithDB
    {
        private const string Server = @"DESKTOP-BM7RNHK";
        private const string Database = @"IIG.CoSWE.FlagpoleDB";
        private const bool IsTrusted = false;
        private const string Login = @"sa";
        private const string Password = @"123";
        private const int ConnectionTime = 75;
        private FlagpoleDatabaseUtils connectionWithDB = new FlagpoleDatabaseUtils(Server, Database, IsTrusted, Login, Password, ConnectionTime);


        [TestMethod]
        public void AddFlagTest()
        {
            MultipleBinaryFlag FlagTest = new MultipleBinaryFlag(6, true);
            Assert.IsTrue(connectionWithDB.AddFlag(FlagTest.ToString(), FlagTest.GetFlag()));

            FlagTest.ResetFlag(1);
            FlagTest.ResetFlag(2);
            Assert.IsTrue(connectionWithDB.AddFlag(FlagTest.ToString(), FlagTest.GetFlag()));
            

            MultipleBinaryFlag FlagTest1 = new MultipleBinaryFlag(33, false);
            Assert.IsTrue(connectionWithDB.AddFlag(FlagTest1.ToString(), FlagTest.GetFlag()));

            MultipleBinaryFlag FlagTest2 = new MultipleBinaryFlag(66, false);
            Assert.IsTrue(connectionWithDB.AddFlag(FlagTest2.ToString(), FlagTest.GetFlag()));

            //AddFlag manually
            Assert.IsTrue(connectionWithDB.AddFlag("TFT", false));
            Assert.IsTrue(connectionWithDB.AddFlag("TT", true));
            Assert.IsTrue(connectionWithDB.AddFlag("FFFTTFT", false));

            //AddFlag manually with errors
            Assert.IsFalse(connectionWithDB.AddFlag("FF", true));
            Assert.IsFalse(connectionWithDB.AddFlag("TT", false));
        }

        [TestMethod]
        public void GetFlagTest()
        {
            MultipleBinaryFlag FlagTest1 = new MultipleBinaryFlag(3, false);
            FlagTest1.SetFlag(0);
            FlagTest1.SetFlag(2);
            connectionWithDB.GetFlag(7, out string flagView1, out bool? flagValue1);
            Assert.AreEqual(flagView1, FlagTest1.ToString());
            Assert.AreEqual(flagValue1, FlagTest1.GetFlag());

            MultipleBinaryFlag FlagTest2 = new MultipleBinaryFlag(6, true);
            connectionWithDB.GetFlag(3, out string flagView2, out bool? flagValue2);
            Assert.AreEqual(flagView2, FlagTest2.ToString());
            Assert.AreEqual(flagValue2, FlagTest2.GetFlag());


            MultipleBinaryFlag FlagTest3 = new MultipleBinaryFlag(6, false);
            connectionWithDB.GetFlag(3, out string flagView3, out bool? flagValue3);
            Assert.AreNotEqual(flagView3, FlagTest3.ToString());
            Assert.AreNotEqual(flagValue3, FlagTest3.GetFlag());

            MultipleBinaryFlag FlagTest4 = new MultipleBinaryFlag(33, false);
            FlagTest4.SetFlag(32);
            connectionWithDB.GetFlag(5, out string flagView4, out bool? flagValue4);
            Assert.AreNotEqual(flagView4, FlagTest4.ToString());
            Assert.AreEqual(flagValue4, FlagTest4.GetFlag());
        }
    }
}
