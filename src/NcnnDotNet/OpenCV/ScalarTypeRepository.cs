using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.OpenCV
{

    internal static class ScalarTypeRepository
    {

        #region Fields

        public static readonly Dictionary<Type, ElementTypes> SupportTypes = new Dictionary<Type, ElementTypes>();

        #endregion

        #region Constructors
        
        static ScalarTypeRepository()
        {
            var types = new[]
            {
                new { Type = typeof(double), ElementType = ElementTypes.Double },
            };

            foreach (var type in types)
                SupportTypes.Add(type.Type, type.ElementType);
        }

        #endregion
        
        public enum ElementTypes
        {

            Double

        }

    }

}