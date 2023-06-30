using Application.Common.Interfaces;
using MediatR;

namespace Application.Identity.Queries.GetToken;

public record AuthenticateResponseQuery : IRequest<AuthenticateResponseDto>
{
    public string? UserName { get; init; }
    public string? Email { get; init; }
    public string Password { get; init; }
}

public class GetTokenQueryHandler : IRequestHandler<AuthenticateResponseQuery, AuthenticateResponseDto>
{
    private readonly IIdentityService _identityService;

    public GetTokenQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    
    public async Task<AuthenticateResponseDto> Handle(AuthenticateResponseQuery request, CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(request.Email))
        {
            return await _identityService.AuthenticateByEmail(request.Email, request.Password);
        }

        return await _identityService.AuthenticateByUserName(request.UserName!, request.Password);
    }
}