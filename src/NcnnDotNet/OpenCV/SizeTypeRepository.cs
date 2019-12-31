using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.OpenCV
{

    internal static class SizeTypeRepository
    {

        #region Fields

        public static readonly Dictionary<Type, ElementTypes> SupportTypes = new Dictionary<Type, ElementTypes>();

        #endregion

        #region Constructors
        
        static SizeTypeRepository()
        {
            var types = new[]
            {
                new { Type = typeof(int),    ElementType = ElementTypes.Int32 },
                new { Type = typeof(long),   ElementType = ElementTypes.Int64 },
                new { Type = typeof(float),  ElementType = ElementTypes.Float  },
                new { Type = typeof(double), ElementType = ElementTypes.Double  },
            };

            foreach (var type in types)
                SupportTypes.Add(type.Type, type.ElementType);
        }

        #endregion
        
        public enum ElementTypes
        {

            Int32,

            Int64,

            Float,

            Double

        }

    }

}