using System;
using System.Data;
using System.ServiceModel;
using TestIFNSLibary.PathJurnalAndUse;
using TestIFNSLibary.Xml.XmlDS;


namespace TestIFNSLibary.Service
{
    [ServiceContract]
    public interface IReaderCommandDbf
    {
        /// <summary>
        /// Операция поиска Физического лица в Б.Д.
        /// </summary>
        /// <param name="command">Команда Select длф поиска ФЛ</param>
        /// <param name="conectionstring">Строка соединения с БД</param>
        /// <param name="datasetreport">Таблица созданая и возвращаемая в виде переменной</param>
        /// <param name="i">Счет таблицы</param>
        /// <returns>Возвращается таблица</returns>
        [OperationContract]
         DataSet SqlFl(string command, string conectionstring,DataSet datasetreport, int i);
        /// <summary>
        /// Операция поиска Юридического лица в Б.Д.
        /// </summary>
        /// <param name="inn">ИНН ЮЛ</param>
        /// <param name="god">Год сведений</param>
        /// <param name="command">SQL Command</param>
        /// <param name="conectionstring">Строка соединения</param>
        /// <param name="datasetreport">Таблица созданая и возвращаемая в виде переменной</param>
        /// <param name="i">Счет таблицы</param>
        /// <returns>Возвращается таблица с запросом</returns>
        [OperationContract]
        DataSet SqlUl(string inn, string god, string command, string conectionstring, DataSet datasetreport, int i);

        /// <summary>
        /// Активно ли выполнения Bakcup
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool IsActive();
        /// <summary>
        /// Журнал Резервного копирования
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        BakcupJurnal[] Jurnal();
        /// <summary>
        /// Дата последнего Bakcup
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DateTime DateBakcup();
        /// <summary>
        /// Функция Bakcup
        /// </summary>
        [OperationContract(IsOneWay = true)]
        [STAThread]
        void FileBakcup();
        /// <summary>
        /// Получение настроек сервиса!!!
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        Parametr Config();
        /// <summary>
        /// Сохранение настроек сервиса
        /// </summary>
        [OperationContract(IsOneWay = true)]
        [STAThread]
        void SaveSeting(string testDb, string workDb, int hours, int minutes);
    }
}