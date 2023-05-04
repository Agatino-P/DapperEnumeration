namespace BoolEnumLib;
public class BoolEnum
{
    public static BoolEnum Yes = new(nameof(Yes));
    public static BoolEnum No = new(nameof(No));

    public static BoolEnum[] All = { Yes, No };

    private readonly string Value;

    private BoolEnum(string str) => Value = str;
    private static BoolEnum FromString(string str)
        => All.FirstOrDefault(x => x.Value == str) ?? throw new ArgumentNullException(str);

    public static bool IsValid(string str) => All.Any(b => b.Value == str);
    public static implicit operator string(BoolEnum b) => b.Value;
    public static implicit operator BoolEnum(string s) => FromString(s);
}
