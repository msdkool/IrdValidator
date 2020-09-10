using System;
using Xunit;

namespace IrdValidator.test
{
    public class IrdNumberExtensionTest
    {
        [Theory]
        [InlineData("2389438348")]

        public void Test_Invalid_IrdNumber_Returns_False(string irdNumber)
        {
            Assert.False(irdNumber.IsIrdNumberValid());
        }

        [Theory]
        [InlineData("")]

        public void Test_Empty_IrdNumber_Returns_False(string irdNumber)
        {
            Assert.False(irdNumber.IsIrdNumberValid());
        }

        [Theory]
        [InlineData("111111111")]

        public void Test_Valid_IrdNumber_Returns_True(string irdNumber)
        {
            Assert.True(irdNumber.IsIrdNumberValid());
        }

        [Theory]
        [InlineData("111-111-111")]

        public void Test_Valid_IrdNumber_Hyphen_Returns_True(string irdNumber)
        {
            Assert.True(irdNumber.IsIrdNumberValid());
        }
    }
}
