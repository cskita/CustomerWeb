using Microsoft.AspNetCore.Http;
using System.Text.Json;
using CustomerWeb.Models.Authorization;
using CustomerWeb.Models.Enumerable;
using CustomerWeb.Models.User;

namespace CustomerWeb.Extensions
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }

        public static bool IsAuthenticated(this ISession session)
        {
            if (session.Get(SessionEnum.UserSession.ToString()) != null)
                return true;

            return false;
        }

        public static string GetUserToken(this ISession session)
        {
            if (IsAuthenticated(session))
                return session.Get<AuthorizationResponse>(SessionEnum.UserSession.ToString()).Token;

            return null;
        }

        public static User GetUserSession(this ISession session)
        {
            if (IsAuthenticated(session))
                return session.Get<AuthorizationResponse>(SessionEnum.UserSession.ToString()).User;

            return null;
        }

        public static void SetUserSession(this ISession session, AuthorizationResponse data)
        {
            session.Set(SessionEnum.UserSession.ToString(), data);
        }
    }
}
