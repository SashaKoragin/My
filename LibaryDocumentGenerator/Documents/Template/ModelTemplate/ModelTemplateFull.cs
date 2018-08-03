using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibaryDocumentGenerator.Documents.Template.ModelFullWord;

namespace LibaryDocumentGenerator.Documents.Template.ModelTemplate
{
    /// <summary>
    /// Класс реализации т.к в C# нет множественного наследия мы наследуем интерфейс с реализациями
    /// </summary>
   public class ModelTemplateFull : IModelTemplateFull
    {
        /// <summary>
        /// Реализация Модели на принятия БДК
        /// </summary>
        /// <param name="connectionstring"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public ModelInBdk ModelInBdk(string connectionstring, string path)
        {
            return new ModelInBdk(connectionstring, path);
        }
        /// <summary>
        /// Реализация Модели на отправку БДК
        /// </summary>
        /// <param name="connectionstringtemplate">Строка соединения с шаблонами</param>
        /// <param name="connectionstringtaxes">Строка соединения с данными</param>
        /// <param name="path">Путь сохранения</param>
        /// <returns></returns>
        public ModelOutBdk ModelOutBdk(string connectionstringtemplate, string connectionstringtaxes, string path)
        {
            return new ModelOutBdk(connectionstringtemplate, connectionstringtaxes, path);
        }
        /// <summary>
        /// Реализация модели констант
        /// </summary>
        /// <returns></returns>
        public WordConstant ConstWord()
        {
            return new WordConstant();
        }
    }
}
