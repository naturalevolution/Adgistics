using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace AdgisticsMotorsReport.Tests
{
    [TestFixture]
    public class TestFileService
    {
        [Test]
        public void CanRetrieveLines()
        { 
            FileService fileService = new FileService();

            var files = fileService.RetrieveLinesFromFile();

            Assert.AreEqual(81790, files.Length);
        }
    }
}
