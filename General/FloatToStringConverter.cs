namespace Utilities.General
{
    public class FloatToStringConverter : ObjectToStringConverter.ToStringConverter<float>
    {
        public override string ConvertObject(object obj) => GetObject(obj).ToString("0.00");
    }
}