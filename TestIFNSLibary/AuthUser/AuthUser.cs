using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibaryXMLAuto.ModelXmlSql.Model.FullSetting;
using System.DirectoryServices.AccountManagement;

namespace TestIFNSLibary.AuthUser
{
   public class AuthUser
    {

        public ModelUser AuthUserService(FullSetting setting)
        {
            if (setting.ModelUser.Login != null)
            {
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain, null, setting.ModelUser.Login,setting.ModelUser.Password))
                {
                    if (context.ValidateCredentials(setting.ModelUser.Login, setting.ModelUser.Password))
                    {
                        using (var users = new UserPrincipal(context))
                        {
                            users.SamAccountName = setting.ModelUser.Login;
                            using (var searcher = new PrincipalSearcher(users))
                            {
                                var user = searcher.FindOne() as UserPrincipal;
                                if (user != null)
                                {
                                    setting.ModelUser.UserName = user.Name;
                                    setting.ModelUser.UserNameGuide = user.Name + setting.ModelUser.Guid;
                                    return setting.ModelUser;
                                }
                            }
                        }
                        setting.ModelUser.Error = "Пользователь не найден!!!";
                        return setting.ModelUser;
                    }
                    setting.ModelUser.Error = "Не правильный логин/пароль!!!";
                    return setting.ModelUser;
                }
            }
            setting.ModelUser.Error = "Пользователь не введен!!!";
            return setting.ModelUser; 
        }
    }
}
