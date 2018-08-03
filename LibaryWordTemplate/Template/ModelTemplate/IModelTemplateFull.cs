using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibaryWordTemplate.Template.ModelFullWord;

namespace LibaryWordTemplate.Template.ModelTemplate
{
    /// <summary>
    /// Интерфейс реализует все модели которые будут заложенны
    /// </summary>
   public interface IModelTemplateFull
   {
       WordConstant ConstWord();
       ModelInBdk ModelInBdk(string connectionstring, string path);
       ModelOutBdk ModelOutBdk(string connectionstringtemplate, string connectionstringtaxes, string path);
       

   }
}
