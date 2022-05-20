using DeloUltimate.Domain.Entities.Attributes;
using DeloUltimate.Domain.Entities.Base;
using System;

namespace DeloUltimate.Domain.Entities
{
    [RussianName("Активность пользователей")]
    public class AccountActivity : IEntity
    {
        [RussianName("Имя пользователя")]
        public string AccountName { get; set; }

        [RussianName("Дата последней авторизации")]
        public DateTime LastAutorizationDate { get; set; }

        [RussianName("Прошло дней")]
        public int DaysGone { get; set; }
    }
}
