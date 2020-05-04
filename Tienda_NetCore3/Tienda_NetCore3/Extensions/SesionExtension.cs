using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Tienda_NetCore3.Extensions
{
    public static class SessionExtensions
    {
        public static void SetObject(this ISession sesion, string key, object value)
        {
            sesion.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession sesion, string key)
        {
            var value = sesion.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }

    }
}
