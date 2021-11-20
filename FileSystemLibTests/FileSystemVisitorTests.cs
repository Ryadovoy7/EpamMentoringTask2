using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileSystemVisitorLib;
using System.IO;
using System.Collections;

namespace FileSystemLibTests
{
    [TestClass]
    public class FileSystemVisitorTests
    {
        const string STR_Start = "Start";
        const string STR_Finish = "Finish";

        [TestMethod]
        public void TestMethodFilesCounted()
        {
            
            DirectoryInfo testDirRoot = Directory.CreateDirectory(Path.GetTempPath() + "TestMethodFilesCounted");
            try
            {
                var file1 = File.Create(Path.Combine(testDirRoot.FullName, Path.GetRandomFileName()));
                file1.Close();
                var file2 = File.Create(Path.Combine(testDirRoot.FullName, Path.GetRandomFileName()));
                file2.Close();
                var file3 = File.Create(Path.Combine(testDirRoot.FullName, Path.GetRandomFileName()));
                file3.Close();

                FileSystemVisitor fsv = new FileSystemVisitor(testDirRoot.FullName);

                int count = 0;
                foreach (var path in fsv)
                {
                    count++;
                }

                Assert.AreEqual(3, count);
            }
            finally
            {
                Directory.Delete(testDirRoot.FullName, true);
            }                      
        }

        [TestMethod]
        public void TestMethodPathCorrect()
        {
            DirectoryInfo testDirRoot = Directory.CreateDirectory(Path.GetTempPath() + "TestMethodPathCorrect");

            try
            {
                var fileName = Path.Combine(testDirRoot.FullName, Path.GetRandomFileName());
                var file1 = File.Create(fileName);
                file1.Close();

                FileSystemVisitor fsv = new FileSystemVisitor(testDirRoot.FullName);
                IEnumerator e_fsv = ((IEnumerable)fsv).GetEnumerator();
                e_fsv.MoveNext();

                var foundFileName = e_fsv.Current;

                Assert.AreEqual(fileName, foundFileName);
            }
            finally
            {
                Directory.Delete(testDirRoot.FullName, true);
            }                      
        }

        [TestMethod]
        public void TestMethodFilter()
        {
            DirectoryInfo testDirRoot = Directory.CreateDirectory(Path.GetTempPath() + "TestMethodFilter");

            try
            {
                var file1 = File.Create(Path.Combine(testDirRoot.FullName, "file1.tmp"));
                file1.Close();
                var file2 = File.Create(Path.Combine(testDirRoot.FullName, "file2.tmp"));
                var filename2 = file2.Name;
                file2.Close();
                var dir1 = Directory.CreateDirectory(Path.Combine(testDirRoot.FullName, "dir1"));
                var dir2 = Directory.CreateDirectory(Path.Combine(testDirRoot.FullName, "dir2"));

                FSVFilter filter = p => p == dir2.FullName || p == filename2;

                FileSystemVisitor fsv = new FileSystemVisitor(testDirRoot.FullName, filter);

                int count = 0;
                foreach (var path in fsv)
                {
                    count++;
                }

                Assert.AreEqual(2, count);
            }
            finally
            {
                Directory.Delete(testDirRoot.FullName, true);
            }
        }

        [TestMethod]
        public void TestMethodRemoveFound()
        {
            DirectoryInfo testDirRoot = Directory.CreateDirectory(Path.GetTempPath() + "TestMethodRemoveFound");

            try
            {
                var file1 = File.Create(Path.Combine(testDirRoot.FullName, "file1.tmp"));
                file1.Close();
                var filename = file1.Name;
                var dir1 = Directory.CreateDirectory(Path.Combine(testDirRoot.FullName, "dir1"));

                FileSystemVisitor fsv = new FileSystemVisitor(testDirRoot.FullName);
                FileSystemVisitorEventHandler removeFiltered = (o, e) => e.RemoveFromList = true;
                fsv.FileFinded += removeFiltered;
                fsv.DirectoryFinded += removeFiltered;

                int count = 0;
                foreach (var path in fsv)
                {
                    count++;
                }

                Assert.AreEqual(0, count);
            }
            finally
            {
                Directory.Delete(testDirRoot.FullName, true);
            }
        }

        [TestMethod]
        public void TestMethodRemoveFiltered()
        {
            DirectoryInfo testDirRoot = Directory.CreateDirectory(Path.GetTempPath() + "TestMethodRemoveFiltered");

            try
            {
                var file1 = File.Create(Path.Combine(testDirRoot.FullName, "file1.tmp"));
                file1.Close();
                var filename = file1.Name;
                var dir1 = Directory.CreateDirectory(Path.Combine(testDirRoot.FullName, "dir1"));

                FSVFilter filter = p => p == dir1.FullName || p == filename;

                FileSystemVisitor fsv = new FileSystemVisitor(testDirRoot.FullName, filter);
                FileSystemVisitorEventHandler removeFiltered = (o, e) => e.RemoveFromList = true;
                fsv.FilteredFileFinded += removeFiltered;
                fsv.FilteredDirectoryFinded += removeFiltered;

                int count = 0;
                foreach (var path in fsv)
                {
                    count++;
                }

                Assert.AreEqual(0, count);
            }
            finally
            {
                Directory.Delete(testDirRoot.FullName, true);
            }
        }

        [TestMethod]
        public void TestMethodStartFinish()
        {
            DirectoryInfo testDirRoot = Directory.CreateDirectory(Path.GetTempPath() + "TestMethodStartFinish");

            try
            {
                var file1 = File.Create(Path.Combine(testDirRoot.FullName, Path.GetRandomFileName()));
                file1.Close();

                bool startEmpty = false;
                bool finishNotEmpty = false;

                FileSystemVisitor fsv = new FileSystemVisitor(testDirRoot.FullName);
                System.EventHandler startHandler = (o, e) => { if (fsv.TreeListing.Count == 0) startEmpty = true; };
                System.EventHandler finishHandler = (o, e) => { if (fsv.TreeListing.Count > 0) finishNotEmpty = true; };

                fsv.Start += startHandler;
                fsv.Finish += finishHandler;

                int count = 0;
                foreach (var path in fsv)
                {
                    count++;
                }

                Assert.IsTrue(startEmpty && finishNotEmpty);
            }
            finally
            {
                Directory.Delete(testDirRoot.FullName, true);
            }
        }

        [TestMethod]
        public void TestMethodStop()
        {
            DirectoryInfo testDirRoot = Directory.CreateDirectory(Path.GetTempPath() + "TestMethodStartFinish");

            try
            {
                var file1 = File.Create(Path.Combine(testDirRoot.FullName, "file1.tmp"));
                file1.Close();
                var filename2 = Path.Combine(testDirRoot.FullName, "file2.tmp");
                var file2 = File.Create(filename2);
                file2.Close();

                FileSystemVisitor fsv = new FileSystemVisitor(testDirRoot.FullName);
                FileSystemVisitorEventHandler stopWhenFile2Found = (o, e) => { if (filename2 == e.Path) e.Stop = true; };

                fsv.FileFinded += stopWhenFile2Found;

                int count = 0;
                foreach (var path in fsv)
                {
                    count++;
                }

                Assert.AreEqual(1, count);
            }
            finally
            {
                Directory.Delete(testDirRoot.FullName, true);
            }
        }
    }
}