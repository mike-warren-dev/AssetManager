using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace AssetManager.Enums;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        if (enumValue == null)
        {
            return String.Empty;
        }
        else
        {
            var name = enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()?
            .GetName();

            return name == null ? enumValue.ToString() : name;
        }
    }
}
