using System.ComponentModel.DataAnnotations;

namespace UserCatalogService.Models;

public class UserModel
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Patronymic { get; set; } = null!;
    public string Login { get; set; } = null!;
    public bool IsActive { get; set; }
}
