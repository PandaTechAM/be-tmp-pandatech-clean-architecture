﻿namespace Pandatech.CleanArchitecture.Application.Contracts.Auth.Authenticate;

public class IdentityCookies
{
  public string AccessTokenSignature { get; set; } = null!;
  public string RefreshTokenSignature { get; set; } = null!;
  public DateTime AccessTokenExpiresAt { get; set; }
  public DateTime RefreshTokenExpiresAt { get; set; }
}
