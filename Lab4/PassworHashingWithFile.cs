using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.PasswordHashingUtils;
using IIG.FileWorker;
using System;

namespace Lab4Password
{
    [TestClass]
    public class PasswordHashingWithFile
    {
        string path = "F:/KPI FIOT 4 course/КПІ/Lab4/Lab4/PasswordsTest/";

        string first_string = PasswordHasher.GetHash("pass1", "salt1", 3);
        string second_string = PasswordHasher.GetHash("pass2", "salt2", 4);
        string third_string = PasswordHasher.GetHash("pass3", "salt3", 1);

        [TestMethod]
        public void WriteTest()
        {
            Assert.IsTrue(BaseFileWorker.Write(first_string + "\r\n" + second_string + "\r\n" + third_string, 
                path + "PasswrodHashTest1.txt"));

            Assert.IsTrue(BaseFileWorker.Write(PasswordHasher.GetHash("укр_тест/[]*&^#@!$%,.}<>{:_-+=§₽¶妈妈") + "\r\n" +
                PasswordHasher.GetHash("укр_тест/[]*&^#@!$%,.}<>{:_-+=§₽¶妈妈", " ") + "\r\n" +
                PasswordHasher.GetHash("", " ") + "\r\n" +
                PasswordHasher.GetHash(" ", " "),
                path + "PasswrodHashTest3.txt"));

            Assert.IsFalse(BaseFileWorker.Write(first_string, ""));
            Assert.IsTrue(BaseFileWorker.Write(first_string, path + "PasswrodHashTest4"));
            Assert.ThrowsException<OverflowException>(() => (BaseFileWorker.Write(PasswordHasher.GetHash("test", "укр_текст", 3), path + "PasswrodHashTest1.txt")));
        }

        [TestMethod]
        public void ReadTest()
        {
            //ReadLines
            string[] lines1 = BaseFileWorker.ReadLines(path + "PasswrodHashTest1.txt");
            Assert.AreEqual(lines1[0], first_string);
            Assert.AreEqual(lines1[1], second_string);
            Assert.AreEqual(lines1[2], third_string);

            Assert.AreNotEqual(lines1[0], PasswordHasher.GetHash("pass", "salt", 3));


            string[] lines2 = BaseFileWorker.ReadLines(path + "PasswrodHashTest2.txt");
            Assert.AreEqual(lines2[0], first_string);
            Assert.AreEqual(lines2[1], second_string);
            Assert.AreEqual(lines2[2], third_string);

            Assert.AreNotEqual(lines2[1], PasswordHasher.GetHash("pass2", "salt", 4));


            //ReadAll
            string[] lines3 = BaseFileWorker.ReadAll(path + "PasswrodHashTest1.txt").Split("\r\n");
            Assert.AreEqual(lines3[0], first_string);
            Assert.AreEqual(lines3[1], second_string);
            Assert.AreEqual(lines3[2], third_string);

            Assert.AreNotEqual(lines3[2], PasswordHasher.GetHash("pass3", "salt3", 3));

            string[] lines4 = BaseFileWorker.ReadAll(path + "PasswrodHashTest2.txt").Split("\r\n");
            Assert.AreEqual(lines4[0], first_string);
            Assert.AreEqual(lines4[1], second_string);
            Assert.AreEqual(lines4[2], third_string);

            Assert.AreNotEqual(lines4[1], PasswordHasher.GetHash("2", "salt2", 4));
        }
    }
}