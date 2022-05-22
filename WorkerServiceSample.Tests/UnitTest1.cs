using System;
using Xunit;

namespace WorkerServiceSample.Tests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("01:01:00")]
        [InlineData("10:00:00")]
        [InlineData("23:59:59")]

        public void Test1(string startTime)
        {
            var resp = HelpersLibrary.CalculateFirstTimeStart(startTime);

            Assert.True(resp >= 0);
        }
    }
}