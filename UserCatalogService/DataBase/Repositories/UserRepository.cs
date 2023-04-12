using Microsoft.EntityFrameworkCore;
using UserCatalogService.DataBase.Repositories.Interfaces;
using UserCatalogService.Models;

namespace UserCatalogService.DataBase.Repositories;

/// <summary>
/// Класс работы со списком пользователей в БД.
/// </summary>
public class UserRepository : IUserRepository
{
    /// <summary>
    /// Получить список пользователей
    /// </summary>
    /// <returns> Список пользователей </returns>
    public List<UserModel> GetAll()
    {
        using var context = new AppContext();
        return context.Users.ToList();
    }

    /// <summary>
    /// Добавление пользователя в список
    /// </summary>
    /// <param name="model"> Данные пользователя </param>
    public void Add(UserModel model)
    {
        using var context = new AppContext();
        model.IsActive = true;
        context.Users.Add(model);
        context.SaveChanges();
    }

    /// <summary>
    /// Удаление пользователя из списка (без удаления из БД).
    /// </summary>
    /// <param name="model"> Данные пользователя </param>
    public void Delete(UserModel model)
    {
        using var context = new AppContext();
        model.IsActive = false;
        context.Entry(model).State = EntityState.Modified;
        context.SaveChanges();
    }

    /// <summary>
    /// Обновление данных о пользователе
    /// </summary>
    /// <param name="model"> Новая модель пользователя. </param>
    public void Update(UserModel model)
    {
        using var context = new AppContext();
        context.Entry(model).State = EntityState.Modified;
        context.SaveChanges();
    }
}
