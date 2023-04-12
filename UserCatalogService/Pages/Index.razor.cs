using Microsoft.AspNetCore.Components;
using System.Text.RegularExpressions;
using UserCatalogService.DataBase.Repositories.Interfaces;
using UserCatalogService.Enums;
using UserCatalogService.Mock;
using UserCatalogService.Models;

namespace UserCatalogService.Pages;

public partial class Index
{
    [Inject] IUserRepository _repository { get; set; } = null!;

    public MessageModel Message { get; set; } = null!;
    public UserModel User { get; set; } = null!;
    public List<UserModel>? Users { get; set; }

    protected override void OnParametersSet()
    {
        Users = _repository.GetAll();
        Message = new MessageModel();
        User = new UserModel();
    }

    /// <summary>
    /// Добавление пользователя
    /// </summary>
    /// <param name="model"> Модель пользователя </param>
    public void AddUser(UserModel model)
    {
        if (CheckingEmptyFormFields(model) && 
            CheckValidLogin(model) &&
            !CheckIfUserExist(model.Login))
        {
            _repository.Add(model);
            User = new UserModel();
            SetUsers();
            SetMessage("Данные успешно добавлены.", StatusEnum.Success);
        }
    }

    /// <summary>
    /// Обновление данных пользователя.
    /// </summary>
    /// <param name="model"> Модель пользователя. </param>
    public void UpdateUser(UserModel model)
    {
        if (CheckingEmptyFormFields(model) &&
            CheckValidLogin(model) &&
            !CheckIfUserExist(model.Login))
        {
            _repository.Update(model);
            SetUsers();
            SetMessage("Данные успешно обновлены.", StatusEnum.Success);
        }
    }

    /// <summary>
    /// Удаление пользователя.
    /// </summary>
    /// <param name="model"> Модель пользователя. </param>
    public void DeleteUser(UserModel model)
    {
        _repository.Delete(model);
        SetUsers();
        SetMessage("Данные успешно удалены.", StatusEnum.Primary);
    }

    /// <summary>
    /// Проверка существования пользователя по доменному имени.
    /// </summary>
    /// <param name="login"> Доменное имя пользователя. </param>
    /// <returns> True - если пользователь найден, иначе - false. </returns>
    private bool CheckIfUserExist(string login)
    {
        var existUser = FakeActiveDirectoryService.FindByIdentity(login);

        if (existUser != null && existUser.IsActive)
        {
            SetMessage("Пользователь с таким доменным именем уже существует.", StatusEnum.Danger);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Загрузка активных пользователей из БД.
    /// </summary>
    private void SetUsers()
    {
        var allUsers = _repository.GetAll();
        Users = allUsers.Where(x => x.IsActive == true).ToList();
    }

    /// <summary>
    /// Проверка правильности заполнения поля Login.
    /// </summary>
    /// <param name="model"> Модель пользователя. </param>
    /// <returns> True - если введенное значение соответствует требованиям, иначе - false. </returns>
    private bool CheckValidLogin(UserModel model)
    {
        var validLogin = Regex.Match(model.Login, @".+\\.+");
        if (!validLogin.Success)
        {
            SetMessage("Неверно заполнено поле Домен\\Логин", StatusEnum.Danger);
        }
        return validLogin.Success;
    }

    /// <summary>
    /// Установка значений выводимого сообщения.
    /// </summary>
    /// <param name="message"> Текст сообщения. </param>
    /// <param name="status"> Статус сообщения. </param>
    private void SetMessage(string message, StatusEnum status)
    {
        Message.Message = message;
        Message.Status = status;
    }

    /// <summary>
    /// Проверка пустых полей формы.
    /// </summary>
    /// <param name="model"> Модель пользователя. </param>
    /// <returns> True - если все поля заполнены, иначе - false. </returns>
    private bool CheckingEmptyFormFields(UserModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Name) ||
            string.IsNullOrWhiteSpace(model.Surname) ||
            string.IsNullOrWhiteSpace(model.Patronymic) ||
            string.IsNullOrWhiteSpace(model.Login))
        {
            SetMessage("Заполнены не все поля.", StatusEnum.Danger);
            return false;
        }
        return true;
    }
}
