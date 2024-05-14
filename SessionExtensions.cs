using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BabasWells.customSession
{
    public static class SessionExtensions
    {
        public static void SetSessionData<T>(this ISession session, string sessionKey, T value)
        {
            session.SetString(sessionKey, JsonConvert.SerializeObject(value));
        }

        public static T GetSessionData<T>(this ISession session, string sessionKey)
        {
            var data = session.GetString(sessionKey);
            if (data == null)
            {
                return default(T);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(data);
            }
        }
    }
}
