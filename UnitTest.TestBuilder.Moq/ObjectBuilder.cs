using Moq;
using System;
using System.Runtime.CompilerServices;
using UnitTest.TestBuilder.Core.Abstracts;
[assembly: InternalsVisibleTo("UnitTest.TestBuilder.Moq.Test")]

namespace UnitTest.TestBuilder.Moq
{
    internal class ObjectBuilder : IObjectBuilder
    {
        public bool CanCreate(Type type)
        {
            return (type.IsInterface && !type.IsGenericType) || (type.IsGenericType && type.BaseType == typeof(Mock));
        }

        public object Create(Type type)
        {
            if (!CanCreate(type)) return null;

            if(!type.IsGenericType) //must be an interface type
            {
                var mock = typeof(Mock<>);

                var objectProperty = mock.GetProperty("Object", type);

                if(objectProperty != null)
                {
                    var mockObj = Activator.CreateInstance(mock.MakeGenericType(type));

                    return objectProperty.GetValue(mockObj);
                }

                return null;
            }

            //must be the Mock<> type
            return Activator.CreateInstance(type);
        }
    }
}
