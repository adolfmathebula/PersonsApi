namespace PersonsAPI.Authorization;

public static class AppPolicies
{
    public const string AdminOnly = "AdminOnly";

    public const string ManagerOrAdmin = "ManagerOrAdmin";
}