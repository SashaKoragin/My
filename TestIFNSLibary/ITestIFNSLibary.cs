using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace TestIFNSLibary
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
    }

}
