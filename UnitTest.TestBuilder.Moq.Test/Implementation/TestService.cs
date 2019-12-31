namespace UnitTest.TestBuilder.Moq.Test.Implementation
{
    public interface ITestService
    {
        void DoNothing();
    }

    public class TestService : ITestService
    {
        public void DoNothing()
        {

        }
    }
}
