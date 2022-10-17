using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.Business.Extension
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class DescriptionAttribute : Attribute
    {
        private readonly string description;
        public string Description { get { return description; } }
        public DescriptionAttribute(string description)
        {
            this.description = description;
        }
    }

    public static class EnumHelper
    {
        public static T GetAttributeOfType<T>(this Enum enumVal) where T : Attribute
        {
            var typeInfo = enumVal.GetType().GetTypeInfo();
            var v = typeInfo.DeclaredMembers.First(x => x.Name == enumVal.ToString());
            return v.GetCustomAttribute<T>();
        }
        public static string GetDescription(this Enum enumVal)
        {
            var attr = GetAttributeOfType<DescriptionAttribute>(enumVal);
            return attr != null ? attr.Description : string.Empty;
        }

    }
}
