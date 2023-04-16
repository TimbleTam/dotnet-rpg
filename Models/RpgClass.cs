

using System.Text.Json.Serialization;

namespace dotnet_rpg.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Hobbit = 0,

        Knight = 1,

        Mage = 2,

        Cleric = 3
    }
}