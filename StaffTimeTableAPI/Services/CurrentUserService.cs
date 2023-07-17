using System.Security.Claims;
using Application.Common.Interfaces;

namespace StaffTimeTableAPI.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

    //TODO:Создать следующего клиента, обращаться по hhtp(REFIT или аналог) или GRPC
    //TODO:Закинуть в условную некст 3 API какого-то сендера в очередь просто
    //TODO:Про Kafka прочитать
    //TODO:Метрики Promrteus\Graphana
    //TODO:Трассировка Jaeger
    //TODO:Тесты
    //TODO:Скинуть Артёму план развития в Notion
}