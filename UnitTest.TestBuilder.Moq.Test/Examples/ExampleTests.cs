using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitTest.TestBuilder.Core.Abstracts;

namespace UnitTest.TestBuilder.Moq.Test.Examples
{
    [TestClass]
    public class ExampleTests
    {
        private class Builder : MoqBuilder<RateCalculator>
        {
            public Mock<IRateService> RateService { get; private set; }
            public DefaultSetting DefaultSetting { get; private set; }

            public Builder() : this(null)
            {

            }

            public Builder(IContainer container) : base(container)
            {

            }
        }

        [TestMethod]
        public void GetTodayRate_WithRate_ReturnCorrectRate()
        {
            var builder = new Builder();

            //set up the getRate method to return 2 and as verifable
            builder.RateService.Setup(a => a.GetRate()).Returns(2).Verifiable(); 

            var calculator = builder.Build();

            //act
            var result = calculator.GetTodayRate();

            //Assert
            Assert.AreEqual(4.0, result);
            Assert.AreEqual(builder.DefaultSetting, calculator.DefaultSetting);

            //verify RateService.GetRate() was called
            builder.RateService.Verify();
        }

        [TestMethod]
        public void GetTomorrowRate_WithRate_ReturnCorrectRate()
        {
            var builder = new Builder();

            //set up the getRate method to return 2 and as verifable
            builder.RateService.Setup(a => a.GetRate()).Returns(2).Verifiable();

            var calculator = builder.Build();

            //Act
            var result = calculator.GetTomorrowRate();

            //Assert
            Assert.AreEqual(6.0, result);
            Assert.AreEqual(builder.DefaultSetting, calculator.DefaultSetting);

            //verify RateService.GetRate() was called
            builder.RateService.Verify();
        }
    }
}
