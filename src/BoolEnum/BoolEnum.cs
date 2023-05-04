namespace BoolEnumLib;
public class BoolEnum
{
    public static BoolEnum Yes = new("yes");
    public static BoolEnum No = new("no");

    public static BoolEnum[] All = { Yes, No };

    public string Value { get; private set; }

    private BoolEnum(string str)
    {
        Value = str.ToLower();
    }
    private static BoolEnum FromString(string str) 
        => All.FirstOrDefault(x => x.Value == str.ToLower()) ?? throw new ArgumentNullException(str);

    public static bool IsValid(string str) => All.Any(b => b.Value == str.ToLower());
    public static implicit operator string (BoolEnum b) => b.Value;
    public static implicit operator BoolEnum (string s) => FromString(s);
}
