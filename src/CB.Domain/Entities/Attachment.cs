using CB.Domain.Common;
using CB.Domain.Common.Interfaces;
using CB.Domain.Enums;

namespace CB.Domain.Entities;

public class Attachment : BaseAudit, IEntity {
    public Guid Id { get; set; }
    public Guid ParentId { get; set; }
    public FileType Type { get; set; }
    public string Name { get; set; } = null!;
    public string Path { get; set; } = null!;
}
