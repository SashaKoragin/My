using LibaryDocumentGenerator.Documents.Template.ModelFullWord;
using LibaryXMLAuto.ModelXmlSql.Model.FullSetting;

namespace LibaryDocumentGenerator.Documents.Template.ModelTemplate
{
    /// <summary>
    /// Интерфейс реализует все модели которые будут заложенны
    /// </summary>
    public interface IModelTemplateFull
    {
        WordConstant ConstWord();
        ModelInBdk ModelInBdk(string connectionstring, string path);
        ModelOutBdk ModelOutBdk(string connectionstringtemplate, string connectionstringtaxes, string path, FullSetting setting);

    }
}
