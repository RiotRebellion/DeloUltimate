using DeloUltimate.Domain.Entities.Base;
using System;

namespace DeloUltimate.Domain.Entities
{
    public class AccountActivity : IEntity
    {
        public string AccountName { get; set; }

        public DateTime LastAutorizationDate { get; set; }

        public int DaysGone { get; set; }
    }
}
