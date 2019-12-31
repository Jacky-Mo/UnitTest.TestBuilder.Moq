using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using UnitTest.TestBuilder.Moq.Test.Implementation;

namespace UnitTest.TestBuilder.Moq.Test
{
    [TestClass]
    public class ObjectBuilderTests
    {
        #region CanCreate

        [TestMethod]
        public void CanCreate_ReferenceType_ReturnFalse()
        {
            var builder = new ObjectBuilder();

            var result = builder.CanCreate(typeof(TestObject));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CanCreate_ValueType_ReturnFalse()
        {
            var builder = new ObjectBuilder();

            var result = builder.CanCreate(typeof(int));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CanCreate_StringType_ReturnFalse()
        {
            var builder = new ObjectBuilder();

            var result = builder.CanCreate(typeof(string));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CanCreate_Interface_ReturnTrue()
        {
            var builder = new ObjectBuilder();

            var result = builder.CanCreate(typeof(ITestService));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanCreate_RandomGenericType_ReturnFalse()
        {
            var builder = new ObjectBuilder();

            var result = builder.CanCreate(typeof(List<ITestService>));

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CanCreate_GenericTypeOfMock_ReturnTrue()
        {
            var builder = new ObjectBuilder();

            var result = builder.CanCreate(typeof(Mock<ITestService>));

            Assert.IsTrue(result);
        }

        #endregion


        #region Create
        [TestMethod]
        public void Create_ReferenceType_ReturnNull()
        {
            var builder = new ObjectBuilder();

            var result = builder.Create(typeof(TestObject));

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Create_ValueType_ReturnNull()
        {
            var builder = new ObjectBuilder();

            var result = builder.Create(typeof(int));

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Create_StringType_ReturnNull()
        {
            var builder = new ObjectBuilder();

            var result = builder.Create(typeof(string));

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Create_Interface_ReturnObject()
        {
            var builder = new ObjectBuilder();

            var result = builder.Create(typeof(ITestService));

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ITestService));
        }

        [TestMethod]
        public void Create_RandomGenericType_ReturnNull()
        {
            var builder = new ObjectBuilder();

            var result = builder.Create(typeof(List<ITestService>));

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Create_GenericTypeOfMock_ReturnObject()
        {
            var builder = new ObjectBuilder();

            var result = builder.Create(typeof(Mock<ITestService>));

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Mock<ITestService>));
        }
        #endregion
    }
}
