using Moq;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnitTest.TestBuilder.Core.Abstracts;
[assembly: InternalsVisibleTo("UnitTest.TestBuilder.Moq.Test")]

namespace UnitTest.TestBuilder.Moq
{
    internal class ObjectBuilder : IObjectBuilder
    {
        private readonly Dictionary<Type, object> _mockTypeDictionary = new Dictionary<Type, object>();

        public bool CanCreate(Type type)
        {
            return type.IsInterface || (type.IsGenericType && type.BaseType == typeof(Mock));
        }

        public object Create(Type type)
        {
            if (!CanCreate(type)) return null;

            if (_mockTypeDictionary.ContainsKey(type))
            {
                return GetMockObjectProperty(_mockTypeDictionary[type], type);
            }

            //it is Mock<T>, create it and save it
            if (type.IsGenericType && type.BaseType == typeof(Mock))
            {
                var genericType = type.GenericTypeArguments[0];

                if (!_mockTypeDictionary.ContainsKey(genericType))
                {
                    _mockTypeDictionary.Add(genericType, Activator.CreateInstance(type));
                }             

                return _mockTypeDictionary[genericType];
            }

            //it is interface, wrap it w/ Mock and return the Object property
            var mock = typeof(Mock<>);
            var mockObj = Activator.CreateInstance(mock.MakeGenericType(type));

            return GetMockObjectProperty(mockObj, type);          
        }

        /// <summary>
        /// Get Mock's Object property
        /// </summary>
        /// <param name="mockObject">an object of Mock type></param>
        /// <param name="type">the type of the return type</param>
        /// <returns>object of type or null if the property does not exists</returns>
        private object GetMockObjectProperty(object mockObject, Type type)
        {
            var objectProperty = mockObject.GetType().GetProperty("Object", type);
            return objectProperty != null ? objectProperty.GetValue(mockObject) : null;
        }
    }
}
