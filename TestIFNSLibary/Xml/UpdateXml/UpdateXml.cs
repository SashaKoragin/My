using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TestIFNSLibary.Xml.UpdateXml
{
   public class UpdateXml
    {
        /// <summary>
        /// Открывает документ xml
        /// </summary>
        /// <param name="path">Путь к Xml</param>
        /// <returns>Документ</returns>
        public static XmlDocument Document(string path)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            return doc;
        }

        /// <summary>
        /// Создание строкового Атрибута XML
        /// </summary>
        /// <param name="xmlDocument">XML Document</param>
        /// <param name="nameAtribute">Имя атрибута</param>
        /// <param name="znacenie">Значение атрибута</param>
        /// <returns>XML атрибут который можно добавить</returns>
        internal static XmlAttribute AtributeAddString(XmlDocument xmlDocument, string nameAtribute, string znacenie)
        {
            XmlAttribute atributeinn = xmlDocument.CreateAttribute(nameAtribute);
            XmlText inntext = xmlDocument.CreateTextNode(znacenie);
            atributeinn.AppendChild(inntext);
            return atributeinn;
        }

        /// <summary>
        /// Создание текущей даты Атрибута XML
        /// </summary>
        /// <param name="xmlDocument">XML Document</param>
        /// <param name="nameAtribute">Имя атрибута</param>
        /// <returns>XML атрибут который можно добавить</returns>
        internal static XmlAttribute AtributeAddDateNow(XmlDocument xmlDocument, string nameAtribute)
        {
            XmlAttribute atributeinn = xmlDocument.CreateAttribute(nameAtribute);
            XmlText inntext = xmlDocument.CreateTextNode(DateTime.Now.ToString("O"));
            atributeinn.AppendChild(inntext);
            return atributeinn;
        }

        /// <summary>
        /// Команда для Журнала Ыукмшсу WCF Работа с ОРН
        /// </summary>
        /// <param name="path">Путь к файлу журналу</param>
        /// <param name="status">Статус Выполнено не выполнено</param>
        /// <param name="text">Текст </param>
        public void BakcupXmlJurnal(string path, string status, string text)
        {
            var doc = Document(path);
            XmlElement xRoot = doc.DocumentElement;
            XmlElement error = doc.CreateElement("Jurnal");
            error.Attributes.Append(AtributeAddDateNow(doc, "Date"));
            error.Attributes.Append(AtributeAddString(doc, "Status", status));
            error.Attributes.Append(AtributeAddString(doc, "Text", text));
            xRoot.AppendChild(error);
            doc.Save(path);
        }
        /// <summary>
        /// Запись статуса
        /// </summary>
        /// <param name="path">Путь к Журналу</param>
        /// <param name="boolen">Параметр Можно или нельзя выполнять копию</param>
        public void WriteStatus(string path, string boolen)
        {
            var doc = Document(path);
            doc.DocumentElement.SelectSingleNode("//@Status").InnerText = boolen;
            doc.Save(path);
        }
        /// <summary>
        /// Запись Даты на статусе
        /// </summary>
        /// <param name="path">Путь к Журналу</param>
        public void WriteStatus(string path)
        {
            var doc = Document(path);
            doc.DocumentElement.SelectSingleNode("//@Date").InnerText = DateTime.Now.ToString("O");
            doc.Save(path);
        }

    }
}
