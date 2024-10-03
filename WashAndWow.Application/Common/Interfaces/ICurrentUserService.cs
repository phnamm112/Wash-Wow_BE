namespace Wash_Wow.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
        Task<bool> IsInRoleAsync(string role);
        Task<bool> AuthorizeAsync(string policy);
    }
}
