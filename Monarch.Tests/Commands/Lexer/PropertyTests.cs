using Monarch.Commands.Lexer;
using Monarch.Commands.Parser;
using System.Linq;
using Xunit;

namespace Monarch.Tests.Commands.Lexer
{
    public enum TestEnum
    {
        Value1,
        Value2,
        Value3
    }

    public class PropertyTests
    {
        [Fact]
        public void Test()
        {
            var TestObject = new Property
            {
                FlagName = new OptionNameToken("Test"),
                FlagValue = new OptionValueToken[] { new OptionValueToken("Value2") }.ToList<TokenBaseClass>(),
                PropertyInfo = typeof(TestClass).GetProperty("EnumValue")
            };

            var TestItem = new TestClass();
            TestObject.GetValue(TestItem);

            Assert.Equal(TestEnum.Value2, TestItem.EnumValue);
        }
    }

    public class TestClass
    {
        public TestEnum EnumValue { get; set; }
    }
}