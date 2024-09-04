namespace FSH.Framework.Core.Identity.Tokens.Models;
public record LoginResponse(Guid Id, string? UserName, string? FirstName, string? LastName, string? Email, bool IsActive, bool EmailConfirmed, string? PhoneNumber, Uri? ImageUrl, string Token, string RefreshToken, DateTime RefreshTokenExpiryTime);
