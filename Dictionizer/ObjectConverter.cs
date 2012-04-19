using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dictionizer
{
    public static class ObjectConverter
    {
        /// <summary>
        /// Convert the public properties of a class into a dictionary
        /// </summary>
        /// <param name="item">The object to be turned into a dictionary</param>
        /// <returns>IDictionary<string,object></returns>
        public static IDictionary<string,object> ToDictionary(this object item)
        {
            return ToDictionary(item, false);
        }
        
        /// <summary>
        /// Convert the public properties of a class into a dictionary
        /// </summary>
        /// <param name="item">The object to be turned into a dictionary</param>
        /// <param name="deepCopy">Whether reference type properties should also be converted to dictionaries</param>
        /// <returns>IDictionary<string,object></returns>
        public static IDictionary<string,object> ToDictionary(this object item, bool deepCopy)
        {
            var result = new Dictionary<string, object>();
            foreach (var prop in item.GetType().GetProperties())
            {
                var name = prop.Name;
                var valueType = prop.PropertyType == typeof(String) || prop.PropertyType.IsValueType;
                if (!valueType && deepCopy)
                {       
                    result[name] = prop.GetValue(item, null).ToDictionary(true);                        
                }else
                {
                    result[name] = prop.GetValue(item, null);
                }
            }
            return result;
        }
    }
}
