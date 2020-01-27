using System;
using System.Linq;
using EfDatabase.Inventory.Base;
using LibaryXMLAuto.ReadOrWrite.SerializationJson;


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
        public string Identification(User user)
        {
            SerializeJson json = new SerializeJson();
               var  query = from users in Inventory.Users where users.Passwords == user.Passwords && users.NameUser == user.NameUser
                        join department in Inventory.Otdels on users.IdOtdel equals department.IdOtdel
                        join rules in Inventory.Rules on users.IdRule equals rules.IdRule
                        select new 
                        {
                            Users = users,
                            Otdels = department,
                            rules
                        };
            if (query.Any())
            {
                return json.JsonLibaryIgnoreDate(query.Single());
            }
            return null;
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
