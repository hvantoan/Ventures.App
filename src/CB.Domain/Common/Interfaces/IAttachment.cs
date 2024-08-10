using CB.Domain.Common.Models;

namespace CB.Domain.Common.Interfaces;

public interface IAttachment {
    public Guid? Id { get; set; }
    public List<AttachmentDto> Attachments { get; set; }
}
