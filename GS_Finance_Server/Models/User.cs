using System;

namespace GS_Finance_Server.Models
{
    public class User:ICloneable
    {
        public string UserName { get; internal set; }
        public int Id { get; internal set; }
        public string PasswordHash { get; internal set; }
        public bool Enabled { get; internal set; }

        public object Clone() => new User
        {
            Enabled = this.Enabled,
            PasswordHash = (string) this.PasswordHash.Clone(),
            Id = this.Id,
            UserName = this.UserName
        };

    }
}