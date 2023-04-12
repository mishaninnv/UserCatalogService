using UserCatalogService.Enums;

namespace UserCatalogService.Models;

public class MessageModel
{
    public string? Message { get; set; }
    public StatusEnum Status { get; set; }
}
