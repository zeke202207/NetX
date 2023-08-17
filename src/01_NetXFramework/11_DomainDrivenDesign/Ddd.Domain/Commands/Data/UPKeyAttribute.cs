namespace NetX.Ddd.Domain;

[AttributeUsage(AttributeTargets.Class)]
public class UPKeyAttribute : Attribute
{
    public string[] KeyNames { get; set; }

    public UPKeyAttribute(params string[] keyName)
    {
        KeyNames = keyName;
    }
}
