using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace NcnnDotNet.OpenCV
{

    internal static class RectTypeRepository
    {

        #region Fields

        public static readonly Dictionary<Type, ElementTypes> SupportTypes = new Dictionary<Type, ElementTypes>();

        #endregion

        #region Constructors
        
        static RectTypeRepository()
        {
            var types = new[]
            {
                new { Type = typeof(int),   ElementType = ElementTypes.Int32 },
                new { Type = typeof(float), ElementType = ElementTypes.Float  },
            };

            foreach (var type in types)
                SupportTypes.Add(type.Type, type.ElementType);
        }

        #endregion
        
        public enum ElementTypes
        {

            Int32,

            Float

        }

    }

}