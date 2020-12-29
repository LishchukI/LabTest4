using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.PasswordHashingUtils;
using IIG.FileWorker;

namespace Lab4Password
{
    [TestClass]
    public class PasswordHashingWithFile
    {
        string path = "F:/KPI FIOT 4 course/КПІ/Lab4/Lab4/PasswordsTest/";


        [TestMethod]
        public void WriteTest()
        {
            Assert.IsTrue(BaseFileWorker.Write(PasswordHasher.GetHash("pass1", "salt1", 3) + "\r\n"
                + PasswordHasher.GetHash("pass2", "salt2", 4) + "\r\n"
                + PasswordHasher.GetHash("pass3", "salt3", 1), path + "PasswrodHashTest1.txt"));
        }

        [TestMethod]
        public void ReadTest()
        {
            //ReadLines
            string[] lines1 = BaseFileWorker.ReadLines(path + "PasswrodHashTest1.txt");
            Assert.AreEqual(lines1[0], PasswordHasher.GetHash("pass1", "salt1", 3));
            Assert.AreEqual(lines1[1], PasswordHasher.GetHash("pass2", "salt2", 4));
            Assert.AreEqual(lines1[2], PasswordHasher.GetHash("pass3", "salt3", 1));
            Assert.AreNotEqual(lines1[0], PasswordHasher.GetHash("pass", "salt", 3));

            string[] lines2 = BaseFileWorker.ReadLines(path + "PasswrodHashTest2.txt");
            Assert.AreEqual(lines2[0], PasswordHasher.GetHash("pass1", "salt1", 3));
            Assert.AreEqual(lines2[1], PasswordHasher.GetHash("pass2", "salt2", 4));
            Assert.AreEqual(lines2[2], PasswordHasher.GetHash("pass3", "salt3", 1));
            Assert.AreNotEqual(lines2[1], PasswordHasher.GetHash("pass2", "salt", 4));

            //ReadAll
            string[] lines3 = BaseFileWorker.ReadAll(path + "PasswrodHashTest1.txt").Split("\r\n");
            Assert.AreEqual(lines3[0], PasswordHasher.GetHash("pass1", "salt1", 3));
            Assert.AreEqual(lines3[1], PasswordHasher.GetHash("pass2", "salt2", 4));
            Assert.AreEqual(lines3[2], PasswordHasher.GetHash("pass3", "salt3", 1));
            Assert.AreNotEqual(lines3[2], PasswordHasher.GetHash("pass3", "salt3", 3));

            string[] lines4 = BaseFileWorker.ReadAll(path + "PasswrodHashTest2.txt").Split("\r\n");
            Assert.AreEqual(lines4[0], PasswordHasher.GetHash("pass1", "salt1", 3));
            Assert.AreEqual(lines4[1], PasswordHasher.GetHash("pass2", "salt2", 4));
            Assert.AreEqual(lines4[2], PasswordHasher.GetHash("pass3", "salt3", 1));
            Assert.AreNotEqual(lines4[1], PasswordHasher.GetHash("2", "salt2", 4));
        }
    }
}