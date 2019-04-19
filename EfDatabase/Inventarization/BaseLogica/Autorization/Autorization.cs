using System;
using System.Linq;
using EfDatabase.Inventarization.Base;
using LibaryXMLAuto.ReadOrWrite.SerializationJson;


namespace EfDatabase.Inventarization.BaseLogica.Autorization
{
   public class Autorization : IDisposable 
    {
        public InventarizationContext Inventarization { get; set; }

        public Autorization()
        {
           Inventarization?.Dispose();
           Inventarization = new InventarizationContext();
        }
        /// <summary>
        /// Запрос на авторизацию пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        public string Identification(User user)
        {
            SerializeJson json = new SerializeJson();
               var  query = from Users in Inventarization.Users where Users.Passwords == user.Passwords && Users.NameUser == user.NameUser
                        join Otdels in Inventarization.Otdels on Users.IdOtdel equals Otdels.IdOtdel
                        join Rules in  Inventarization.Rules on Users.IdRule equals Rules.IdRule
                        select new
                        {
                            Users,
                            Otdels,
                            Rules
                        };
            if (query.Any())
            {
                return json.JsonLibary(query.Single());
            }
            return null;
        }

        /// <summary>
        /// Dispos
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Inventarization?.Dispose();
                Inventarization = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
