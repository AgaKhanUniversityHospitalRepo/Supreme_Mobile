using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Supreme_Mobile.Models;

namespace Supreme_Mobile.Managers
{
    public class TokenValidation
    {
        public bool Validated { get { return Errors.Count == 0; } }
        public readonly List<TokenValidationStatus> Errors = new List<TokenValidationStatus>();
    }

    public enum TokenValidationStatus
    {
        Expired,
        WrongUser,
        WrongPurpose,
        WrongGuid
    }

    public static class TokenManager
    {
        private const string SecurityStampp = "7df71a83-6c45-40d3-82c4-5be78d999efb";

        public static string GeneratePorterToken(string reason, MyPortersModel porter)
        {
            byte[] _time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] _key = Guid.Parse(SecurityStampp).ToByteArray();
            byte[] _Id = Encoding.UTF8.GetBytes(porter.Porter_ID.ToString());
            byte[] _reason = Encoding.UTF8.GetBytes(reason);
            byte[] data = new byte[_time.Length + _key.Length + _reason.Length + _Id.Length];

            System.Buffer.BlockCopy(_time, 0, data, 0, _time.Length);
            System.Buffer.BlockCopy(_key, 0, data, _time.Length, _key.Length);
            System.Buffer.BlockCopy(_reason, 0, data, _time.Length + _key.Length, _reason.Length);
            System.Buffer.BlockCopy(_Id, 0, data, _time.Length + _key.Length + _reason.Length, _Id.Length);

            return Convert.ToBase64String(data.ToArray());
        }

        public static string GetValidOperatorID(string reason, string token)
        {
            var result = new TokenValidation();
            byte[] data = Convert.FromBase64String(token);
            byte[] _Id = data.Skip(26).ToArray();
            
            return Encoding.UTF8.GetString(_Id);
        }

    }
}