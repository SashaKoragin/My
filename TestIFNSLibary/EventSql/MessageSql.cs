using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestIFNSLibary.EventSql
{
   internal class MessageSql
    {
        /// <summary>
        /// Вывод сообщение пользователю на модель
        /// </summary>
        public static string Messages { get; set; }
        /// <summary>
        /// Событие сообщения SQL Server 2008
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Con_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            Messages = e.Message;
        }
    }
}
