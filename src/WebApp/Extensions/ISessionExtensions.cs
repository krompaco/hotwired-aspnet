using System;
using Microsoft.AspNetCore.Http;

namespace WebApp.Extensions
{
    public static class ISessionExtensions
    {
        public static string GetOrCreateSessionId(this ISession session)
        {
            const string Key = "SessionId";
            var id = session.GetString(Key);

            if (!string.IsNullOrEmpty(id))
            {
                return id;
            }

            id = Guid.NewGuid().ToString("D");

            try
            {
                session.SetString(Key, id);
            }
            catch (Exception)
            {
                // Ignored
            }

            return id;
        }
    }
}
