namespace UserCatalogService.DataBase.Repositories.Interfaces;

/// <summary>
/// Интерфейс базового репозитория для работы с БД.
/// </summary>
/// <typeparam name="T"> Тип данных для работы. </typeparam>
public interface IBaseRepository<T>
{
    /// <summary>
    /// Получение всех данных из БД.
    /// </summary>
    /// <returns> Список данных типа Т. </returns>
    public List<T> GetAll();

    /// <summary>
    /// Добавление данных.
    /// </summary>
    /// <param name="model"> Модель типа Т. </param>
    public void Add(T model);

    /// <summary>
    /// Обновление данных.
    /// </summary>
    /// <param name="model"> Обновленная модель типа Т. </param>
    public void Update(T model);

    /// <summary>
    /// Удаление данных.
    /// </summary>
    /// <param name="model"> Удаляемая модель типа Т. </param>
    public void Delete(T model);
}
