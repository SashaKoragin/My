using System;
using Ifns51.Risk;
using LibaryXMLAuto.Reports.FullTemplateSheme;

namespace EfDatabase.AddTable.AddTemplate
{
    /// <summary>
    /// Класс добавления Шаблона в БД по средством EntytiFramework
    /// </summary>
   public class AddTemplate : IDisposable
    {
        /// <summary>
        /// БД
        /// </summary>
    public RisksContext Risk { get; set; }
        /// <summary>
        /// Возвращаемое сообщение
        /// </summary>
    public string Message { get; set; }
        public AddTemplate()
        {
            Risk?.Dispose();
            Risk = new RisksContext();
        }
        /// <summary>
        /// Сохранение документа
        /// </summary>
        /// <param name="angular">Модель документа</param>
        /// <returns></returns>
        public string SaveTemplate(AngularTemplate angular)
        {
            if (angular.NameDocument != null) { SaveNameDocumentAndTemplare(angular); }
            if (angular.Headers != null) { SaveHeaders(angular.Headers); }
            if (angular.Body != null) { SaveBody(angular.Body); }
            if (angular.Stone != null) { SaveStone(angular.Stone); }
            return Message;
        }
        /// <summary>
        /// Сохранение документа
        /// </summary>
        /// <param name="angular">Документ</param>
        private void SaveNameDocumentAndTemplare(AngularTemplate angular)
        {
            Ifns51.Risk.NameDocument docum = new Ifns51.Risk.NameDocument() {
                NameDocument_ = angular.NameDocument.NameDocument1,
                ManualDoc = angular.NameDocument.ManualDoc,
                Template =  new Ifns51.Risk.Template()
                {
                    IdBody = angular.Template.IdBody,
                    IdHeaders = angular.Template.IdHeaders,
                    IdStone = angular.Template.IdStone
                } };
            Risk.NameDocuments.Add(docum);
            Risk.SaveChanges();
            Message = "Документ добавлен!!!";
        }

        /// <summary>
        /// Сохранение документа
        /// </summary>
        /// <param name="headers">Заголовки</param>
        private void SaveHeaders(Headers headers)
        {
            Header head = new Header()
            {
                TextHeade1 = headers.TextHeade1,
                TextHeade2 = headers.TextHeade2,
                TextHeade3 = headers.TextHeade3,
                TextHeade4 = headers.TextHeade4,
                TextHeade5 = headers.TextHeade5,
                TextHeade6 = headers.TextHeade6,
                TextHeade7 = headers.TextHeade7,
                TextHeade8 = headers.TextHeade8,
                TextHeade9 = headers.TextHeade9,
                TextHeade10 = headers.TextHeade10
            };
            Risk.Headers.Add(head);
            Risk.SaveChanges();
            Message = "Заголовок добавлен!!!";
        }

        /// <summary>
        /// Сохранение документа
        /// </summary>
        /// <param name="body">Тело документа</param>
        private void SaveBody(LibaryXMLAuto.Reports.FullTemplateSheme.Body body)
        {
            Ifns51.Risk.Body bod = new Ifns51.Risk.Body()
            {
                BodyGl1 = body.BodyGl1,
                BodyGl2 = body.BodyGl2,
                BodyGl3 = body.BodyGl3,
                BodyGl4 = body.BodyGl4,
                BodyGl5 = body.BodyGl5
            };
            Risk.Bodies.Add(bod);
            Risk.SaveChanges();
            Message = "Основная часть добавлена!!!";
        }
        /// <summary>
        /// Сохранение документа
        /// </summary>
        /// <param name="stone">Основание документа</param>
        private void SaveStone(LibaryXMLAuto.Reports.FullTemplateSheme.Stone stone)
        {
            Ifns51.Risk.Stone st = new Ifns51.Risk.Stone()
            {
                Stone1 = stone.Stone1,
                Stone2 = stone.Stone2,
                Stone3 = stone.Stone3,
                Stone4 = stone.Stone4,
                Stone5 = stone.Stone5,
                Stone6 = stone.Stone6,
                Stone7 = stone.Stone7
            };
            Risk.Stones.Add(st);
            Risk.SaveChanges();
            Message = "Окончание добавлено!!!";
        }
        /// <summary>
        /// Dispos
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Risk?.Dispose();
                Risk = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
