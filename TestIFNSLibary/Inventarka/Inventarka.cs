using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EfDatabase.Inventarization.Base;
using EfDatabase.Inventarization.BaseLogica.Autorization;
using SqlLibaryIfns.Inventarization.ModelParametr;
using SqlLibaryIfns.Inventarization.Select;
using TestIFNSLibary.PathJurnalAndUse;

namespace TestIFNSLibary.Inventarka
{
   public class Inventarka : IInventarka
    {
        public async Task<string> Authorization(User user)
        {
            Autorization auto = new Autorization();
            return await Task.Factory.StartNew(() => auto.Identification(user));
        }
        /// <summary>
        /// Выборка всех пользователей
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> SelectAllUsers(ModelParametr model)
        {
           var param = new Parametr();

           SelectInventarization select = new SelectInventarization(param.Inventarization);
           return await Task.Factory.StartNew((() => select.SelectFull(model)));
        }
    }
}
