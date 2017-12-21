using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Extensions
{
    public static class AttributesHelper
    {
        public static bool HasPropertyAttribute<T>(this T instance, string propertyName, Type attribute)
        {
            return Attribute.GetCustomAttributes(typeof(T).GetProperty(propertyName), attribute, true).Any();
        }
    }
}