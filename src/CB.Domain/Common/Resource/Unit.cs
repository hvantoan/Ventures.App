namespace CB.Domain.Common.Resource;

public class Unit {
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;

    public static Unit? FromCode(string? code) {
        return !string.IsNullOrWhiteSpace(code) ? new Unit { Code = code } : null;
    }

    internal static Unit FromAu(Au au) {
        return new Unit { Code = au.Code, Name = au.Name, ShortName = au.ShortName };
    }
}

public class AddressMap {
    public string Name { get; set; } = string.Empty;
    public Unit? Unit { get; set; }
}

internal record Au(string Code, int Level, string Name, string ShortName, string? Parent = null, string? Grandparent = null);
