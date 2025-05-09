using UnityEngine;
public static class Endpoints
{
    public const string BaseUrl = "https://the-secret-shelf-api.onrender.com";
    public static string playerUrl => $"{BaseUrl}/players";
    public static string PlayerByName(string playerName) => $"{BaseUrl}/players/{playerName}";
    public static string bookUrl => $"{BaseUrl}/books";
}