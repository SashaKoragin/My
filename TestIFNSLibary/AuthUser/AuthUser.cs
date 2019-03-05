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
            if (setting.ModelUser.Login!=null)
            {
                PrincipalContext context = new PrincipalContext(ContextType.Domain);
                if (context.ValidateCredentials(setting.ModelUser.Login, setting.ModelUser.Password))
                {
                    UserPrincipal user = UserPrincipal.FindByIdentity(context, setting.ModelUser.Login);
                    if (user != null)
                    {
                        setting.ModelUser.UserName = user.Name;
                        setting.ModelUser.UserNameGuide = user.Name + setting.ModelUser.Guid;
                        return setting.ModelUser;
                    }
                    setting.ModelUser.Error = "Пользователь не найден!!!";
                    return setting.ModelUser;
                }
                setting.ModelUser.Error = "Не правильный логин/пароль!!!";
                return setting.ModelUser;
            }
            setting.ModelUser.Error = "Пользователь не введен!!!";
            return setting.ModelUser;
        }
    }
}
