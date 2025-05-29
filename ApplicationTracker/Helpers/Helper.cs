using System.ComponentModel.DataAnnotations;
using ApplicationTracker.Models;

namespace ApplicationTracker.Helpers;

public static class Helper
{
    public static string IsValidApplicationDetails(Application application)
    {
        if (application == null)
        {
            return "No application details provided.";
        }
        
        var type = application.GetType();
        var properties = type.GetProperties();

        foreach (var property in properties)
        {
            var value = property.GetValue(application);

            if (property.PropertyType == typeof(string) && string.IsNullOrEmpty(value as string))
            {
                var customAttributes = property.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];
                return $"'{customAttributes[0].Name}' must contain a value.";
            }
        }
        
        return null;
    }
}