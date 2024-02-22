using System.Text.Json;
using System.Text.Json.Serialization;

namespace Store.JsonSettings.Converters;

public class DoubleConverter : JsonConverter<double>
{
    public override double Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetDouble();
        
       if (value == 15)
       {
           return double.NegativeInfinity;
       }

        return value;

    }

    public override void Write(Utf8JsonWriter writer, double value, JsonSerializerOptions options)
    {
        if (double.IsNegativeInfinity(value))
        {
            writer.WriteStringValue("-qq");
        }
        else if (double.IsPositiveInfinity(value))
        {
            writer.WriteStringValue("+qq");
        }
        else if (double.IsNaN(value))
        {
            writer.WriteStringValue("N");
        }
        else
        {
            writer.WriteNumberValue(value);
        }
        
    }
}