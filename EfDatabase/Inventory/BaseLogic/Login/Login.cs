using System;
using System.Collections.Generic;
using System.Linq;
using EfDatabase.Inventory.Base;
using EfDatabaseXsdInventoryAutorization;
using LibaryXMLAuto.ReadOrWrite.SerializationJson;
using Rule = EfDatabase.Inventory.Base.Rule;


namespace EfDatabase.Inventory.BaseLogic.Login
{
   public class Login : IDisposable 
    {
        public InventoryContext Inventory { get; set; }

        public Login()
        {
            Inventory?.Dispose();
            Inventory = new InventoryContext();
        }
        /// <summary>
        /// Запрос на авторизацию пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        public Autorization Identification(Autorization user)
        {
            SerializeJson json = new SerializeJson();
            var  queryUser = from users in Inventory.Users where  users.TabelNumber == user.Login
                        join department in Inventory.Otdels on users.IdOtdel equals department.IdOtdel
                        select new 
                        {
                            Users = users,
                            Otdels = department,
                        };

            List<string> queryRule = (from ruleAndUsers in Inventory.RuleAndUsers
                         where ruleAndUsers.IdUser == queryUser.FirstOrDefault().Users.IdUser
                         join rules in Inventory.Rules on ruleAndUsers.IdRule equals rules.IdRule
                         select rules.NameRules).ToList();

            if (queryRule.Any())
            {
                user.IdUser = queryUser.FirstOrDefault().Users.IdUser;
                user.TabelNumber = queryUser.FirstOrDefault().Users.TabelNumber;
                user.Name = queryUser.FirstOrDefault().Users.SmallName;
                user.Rule = queryRule.ToArray();
                return user;
            }

            user.ErrorAutorization = "Роли пользователю не определены Вход не возможен!!!";
            return user;
        }

        /// <summary>
        /// Disposing
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Inventory?.Dispose();
                Inventory = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
