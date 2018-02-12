using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;


namespace TestIFNSTools.Detalizacia.WpfUserControl.Face.FL.File.SqlParamFile
{
    public class ZaprosFileFl : SqlZaprosFile.SQLFILEFL
    {
        /// <summary>
        /// Выборка файлов 2012
        /// </summary>
        private readonly List<string> _fileDb2012 = new List<string>
        {
            FilePRFL2012andFileMI2012
        };
        /// <summary>
        /// Выборка фаилов 2013
        /// </summary>
        private readonly List<string> _fileDb2013 = new List<string>
        {
            FilePRFL2013andFileMI2013
        };
        /// <summary>
        /// Выборка фаилов 2014
        /// </summary>
        private readonly List<string> _fileDb2014 = new List<string>
        {
            FilePRFL2014andFileMI2014
        };
        /// <summary>
        /// Выборка фаилов 2015
        /// </summary>
        private readonly List<string> _fileDb2015 = new List<string>
        {
            FilePRFL2015andFileMI2015
        };
        /// <summary>
        /// Выборка фаилов 2016
        /// </summary>
        private readonly List<string> _fileDb2016 = new List<string>
        {
            FilePRFL2016andFileMI2016
        };
        /// <summary>
        /// Выборка фаилов 2017
        /// </summary>
        private readonly List<string> _fileDb2017 = new List<string>
        {
            FilePRFL2017andFileMI2017
        };
        /// <summary>
        /// Выборка фаилов 2018
        /// </summary>
        private readonly List<string> _fileDb2018 = new List<string>
        {
            FilePRFL2018andFileMI2018
        };
        /// <summary>
        ///  Логика выбора года для выборки
        /// </summary>
        /// <param name="god">Сам год подачи файла выбирается пользователем</param>
        /// <param name="sqlparam">Sql параметры запроса которые генерируются заранее</param>
        /// <param name="detal">Прокидка формы для манипуляции с ней</param>
        /// <returns>Возвращается таблица с данными для генерации путей к файлам</returns>
        public DataSet Zaprosfilefl(string god, List<string> sqlparam, Detalizacia detal)
        {
            List<string> sqlselect = new List<string>();
            var goding = Convert.ToInt32(god);
            switch (goding)
            {
                case 2012:
                    DataSet table2012 = Sqlfl( GenerateSqlZapros(sqlparam, _fileDb2012, sqlselect), detal);
                    return table2012;
                case 2013:
                    DataSet table2013 = Sqlfl(GenerateSqlZapros(sqlparam, _fileDb2013, sqlselect), detal);
                    return table2013;
                case 2014:

                    DataSet table2014 = Sqlfl(GenerateSqlZapros(sqlparam, _fileDb2014, sqlselect), detal);
                    return table2014;
                case 2015:
                    DataSet table2015 = Sqlfl(GenerateSqlZapros(sqlparam, _fileDb2015, sqlselect), detal);
                    return table2015;
                case 2016:
                    DataSet table2016 = Sqlfl(GenerateSqlZapros(sqlparam, _fileDb2016, sqlselect), detal);
                    return table2016;
                case 2017:
                    DataSet table2017 = Sqlfl(GenerateSqlZapros(sqlparam, _fileDb2017, sqlselect), detal);
                    return table2017;
                case 2018:
                    DataSet table2018 = Sqlfl(GenerateSqlZapros(sqlparam, _fileDb2018, sqlselect), detal);
                    return table2018;
                default:
                    return null;
            }
        }
        /// <summary>
        /// Функция генерации запросов по каждой выборке проставить параметры
        /// </summary>
        /// <param name="sqlparam">Сами параметры заданные пользователем</param>
        /// <param name="sql">Выборки без параметров заданная заранее</param>
        /// <param name="sqlSelect">Переменная в которую добавляются выборки полностью с параметрами</param>
        /// <returns>Возвращаем список запросов полностью сгенерированый</returns>
        private List<string> GenerateSqlZapros(List<string> sqlparam, List<string> sql,List<string> sqlSelect)
        {
            var i = 1;
            foreach (var sqlselect in sql)
            {
                string sqlone = null;
                foreach (string param in sqlparam)
                {
                    if (i == 1)
                    {
                       sqlone = String.Format(sqlselect, sqlparam.Count > 1 ? param + " and {0}" : param);
                    }
                    else
                    {
                        if (sqlone != null)
                            sqlone = String.Format(sqlone, sqlparam.Count == i ? param : param + " and {0}");
                    }
                    i++;
                }
                i = 1;
                sqlSelect.Add(sqlone);
            }
            return sqlSelect;
        }
        /// <summary>
        /// Собственно клас для запроса по файлам
        /// </summary>
        /// <param name="sqlselectall">Подаются все сгенерированые выборки</param>
        /// <param name="detal">Наша форма для манипуляции</param>
        /// <returns>Возвращаем данные для генерации путей к файлам</returns>
     private DataSet Sqlfl(List<string> sqlselectall, Detalizacia detal)
            {
                var service = new ServiceTestIfns.ReaderCommandDbfClient("BasicHttpBinding_IReaderCommandDbf");
                var table = new DataSet();
                var i = 0;
                foreach (string sqlselect in sqlselectall)
                {
                    table.Tables.Add();
                    table = service.SqlFl(sqlselect, Arhivator.Pathing.ConnectString.Connection, table, i);
                    detal?.BeginInvoke(new MethodInvoker(delegate { detal.StatusBarFl.Value += 10000; }));
                i++;
                }
                detal?.BeginInvoke(new MethodInvoker(delegate { detal.StatusBarFl.Value = 0; }));
                return table;
            }
        }
}