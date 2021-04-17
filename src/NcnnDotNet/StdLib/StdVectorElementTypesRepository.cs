using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet
{

    internal static class StdVectorTypeRepository
    {

        #region Fields

        public static readonly Dictionary<Type, ElementTypes> SupportTypes = new Dictionary<Type, ElementTypes>();

        #endregion

        #region Constructors
        
        static StdVectorTypeRepository()
        {
            var types = new[]
            {
                new { Type = typeof(int),    ElementType = ElementTypes.Int32 },
                new { Type = typeof(uint),   ElementType = ElementTypes.UInt32 },
                new { Type = typeof(float),  ElementType = ElementTypes.Float  },
                new { Type = typeof(double), ElementType = ElementTypes.Double  },
                new { Type = typeof(Mat),    ElementType = ElementTypes.Mat  },
                new { Type = typeof(VkMat),  ElementType = ElementTypes.VkMat  },
            };

            foreach (var type in types)
                SupportTypes.Add(type.Type, type.ElementType);
        }

        #endregion
        
        public enum ElementTypes
        {

            Int32,

            UInt32,

            Float,

            Double,

            Mat,

            VkMat,

        }

    }

}