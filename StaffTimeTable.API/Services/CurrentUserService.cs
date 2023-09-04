using System.Security.Claims;
using StaffTimetable.Application.Common.Interfaces;

namespace StaffTimeTable.API.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

    //TODO:Посмотреть как нужно убрать либо пользователей, либо TODO и как это связать
    //TODO:Создать следующего клиента, обращаться по hhtp(REFIT или аналог) или GRPC
    //TODO:Закинуть в условную некст 3 API какого-то сендера в очередь просто
    //TODO:Собирать health check в Grafana
    //TODO:GateWay добавить
    //TODO:Notify для решения Kafka
    //TODO:DB на solution
    //TODO:Тесты
    //TODO:Валидировать на статусы пользователя синхронно при создании задачи
}