namespace TrackHub.Web.Utilities;

internal static class RefreshTokenHelper
{
    internal static string PackRefreshToken(string userId, string sessionId, string secret)
    => $"{userId}.{sessionId}.{secret}";

    internal static(string userId, string sessionId, string secret)? UnpackRefreshToken(string token)
    {
        var parts = token.Split('.', 3);
        if (parts.Length != 3) return null;
        return (parts[0], parts[1], parts[2]);
    }
}
