using System.Text.Json;

namespace Store.JsonSettings.Policies;

public class SnakeCaseNamePolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
    {
        var array = name.ToCharArray();
        var result = new List<char>();

        for (var index = 0; index < array.Length; index++)
        {
            var ch = array[index];
            if (char.IsUpper(ch) && index != 0)
            {
                result.Add('_');
            }

            result.Add(char.ToLower(ch));
        }

        return string.Concat(result);
    }
}