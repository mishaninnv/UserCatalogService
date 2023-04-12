using UserCatalogService.Models;

namespace UserCatalogService.Mock;

/// <summary>
/// Класс заглушка иммитирующий работу с ActiveDirectory.
/// </summary>
public class FakeActiveDirectoryService
{
    /// <summary>
    /// Метод иммитирующий обращение к ActiveDirectory для получения данных по имени доменного пользователя.
    /// </summary>
    /// <param name="loginName"> Имя доменного пользователя вида: «домен\логин». </param>
    /// <returns> При наличии модель пользователя, иначе null. </returns>
    public static UserModel? FindByIdentity(string loginName)
    {
        using var context = new DataBase.AppContext();
        return context.Users.Where(x => x.Login.Equals(loginName)).FirstOrDefault();
    }
}
