using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ChallengeWheelzy.QuestionNumber5;
using System.IO;

namespace ConsoleApp.Tests
{
    public class FileProcessorTests
    {
        [Fact]
        public void ShouldRenameAsyncMethods()
        {
            string input = "public async Task DoWork() { }";
            string path = "temp.cs";
            File.WriteAllText(path, input);

            FileProcessor.ProcessCsFiles(".");
            string output = File.ReadAllText(path);

            Assert.Contains("DoWorkAsync", output);
        }

        [Fact]
        public void ShouldFixVmAndDto()
        {
            string input = "CustomerVm customer; OrderDtos orders;";
            string path = "temp2.cs";
            File.WriteAllText(path, input);

            FileProcessor.ProcessCsFiles(".");
            string output = File.ReadAllText(path);

            Assert.Contains("CustomerVM", output);
            Assert.Contains("OrderDTOs", output);
        }

        [Fact]
        public void ShouldAddBlankLineBetweenMethods()
        {
            string input = "public void A() {}\npublic void B() {}";
            string path = "temp3.cs";
            File.WriteAllText(path, input);

            FileProcessor.ProcessCsFiles(".");
            string output = File.ReadAllText(path);

            Assert.Matches(@"A\(\) \{\}\s+public void B\(\)", output);
        }
    }
}
