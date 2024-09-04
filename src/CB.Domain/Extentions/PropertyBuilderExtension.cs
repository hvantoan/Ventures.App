using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CB.Domain.Extentions;

public static class PropertyBuilderExtension {

    public static PropertyBuilder<DateTimeOffset> HasDateConversion(this PropertyBuilder<DateTimeOffset> builder) {
        return builder.HasConversion(o => o.ToUnixTimeMilliseconds(), o => DateTimeOffset.FromUnixTimeMilliseconds(o));
    }

    public static PropertyBuilder<DateTimeOffset?> HasDateConversion(this PropertyBuilder<DateTimeOffset?> builder) {
        return builder.HasConversion(o => o.HasValue ? o.Value.ToUnixTimeMilliseconds() : -1,
                                     o => o >= 0 ? DateTimeOffset.FromUnixTimeMilliseconds(o) : null);
    }
}
