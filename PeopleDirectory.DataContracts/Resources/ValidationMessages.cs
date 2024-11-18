using System.Globalization;
using System.Resources;

namespace PeopleDirectory.DataContracts.Resources
{
    public static class ValidationMessages
    {
        private static readonly ResourceManager ResourceManager =
        new("PeopleDirectory.DataContracts.Resources.ValidationMessages", typeof(ValidationMessages).Assembly);

        public static string GetMessage(string key, CultureInfo culture = null)
        {
            culture ??= CultureInfo.CurrentUICulture;
            return ResourceManager.GetString(key, culture) ?? $"[[{key}]]";
        }
    }
}
