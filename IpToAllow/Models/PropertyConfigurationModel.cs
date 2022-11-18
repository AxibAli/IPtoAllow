namespace IpToAllow.Models
{
    public class PropertyConfigurationModel
    {
        public string XmlLabel { get; set; }
        public bool IsRequired { get; set; }
        public string Value { get; set; }
        public bool IsList { get; set; }
        public ValueCollectionType ValueType { get; set; }
        public char Delimeter { get; set; }
    }
    public enum ValueCollectionType
    {
        Normal = 1,
        List = 2,
        Dictionary = 3,
    }
}
