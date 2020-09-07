using System.Text;
using System.Text.Json;

namespace DotNetCore.Extensions
{
    public static class ObjectExtensions
    {
        public static byte[] Bytes(this object obj)
        {
            return Encoding.Default.GetBytes(JsonSerializer.Serialize(obj));
        }
    }
}
