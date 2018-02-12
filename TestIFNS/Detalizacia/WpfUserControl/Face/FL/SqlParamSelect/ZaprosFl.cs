using System;
using System.Collections.Generic;
using System.Data;

namespace TestIFNSTools.Detalizacia.WpfUserControl.Face.FL.SqlParamSelect
{
   public class ZaprosFl : SqlZapros.SQLQBEFL
    {
        /// <summary>
        /// Генерация параметров для выборки подается с формы
        /// </summary>
        /// <param name="inn">ИНН</param>
        /// <param name="serianumdok">Серия номер паспорта</param>
        /// <param name="familia">Фамилия</param>
        /// <param name="name">Имя</param>
        /// <param name="midlename">Отчество</param>
        /// <returns>Возвращаем параметры для выборки</returns>
        public List<string> GenerateParam(string inn,string serianumdok,string familia, string name, string midlename)
        {
            List<string> sqlparam = new List<string>
            {
                !String.IsNullOrWhiteSpace(inn) ? $"t1.ИННФЛ = '{inn}'" : null,
                !String.IsNullOrWhiteSpace(serianumdok) ? $"t1.СЕРНОМДОК = '{serianumdok}'" : null,
                !String.IsNullOrWhiteSpace(familia) ? $"t1.ФАМИЛИЯ = '{familia}'" : null,
                !String.IsNullOrWhiteSpace(name) ? $"t1.ИМЯ = '{name}'" : null,
                !String.IsNullOrWhiteSpace(midlename) ? $"t1.ОТЧЕСТВО = '{midlename}'" : null,
            };
            //if (!String.IsNullOrWhiteSpace(inn))
            //{sqlparam.Add($"t1.ИННФЛ = {inn}");}
            sqlparam.RemoveAll(item => item == null);
            return sqlparam;
        }

        /// <summary>
        /// Запрос на выбор Физического лица по годам поступления
        /// </summary>
        /// <param name="sqlparam">Параметры используемые в выборке</param>
        /// <param name="god">Год сведений</param>
        /// <param name="detal">Форма для манипуляции с ней</param>
        /// <returns>Возвращаем полученый ответ от Server</returns>
        public DataSet ZaprosSql(List<string> sqlparam, string god, Detalizacia detal)
        {
            string sqlselect;
            var q = new Qbe();
            var goding = Convert.ToInt32(god);
            switch (goding)
            {
                case 2012:
                    sqlselect = GenerateSqlZapros(sqlparam,SQLSvedFL2012);
                    DataSet table2012 = q.Sqlfl(sqlselect, detal);
                    return table2012;
                case 2013:
                    sqlselect = GenerateSqlZapros(sqlparam,SQLSvedFL2013);
                    DataSet table2013 = q.Sqlfl(sqlselect, detal);
                    return table2013;
                case 2014:
                    sqlselect = GenerateSqlZapros(sqlparam,SQLSvedFL2014);
                    DataSet table2014 = q.Sqlfl(sqlselect, detal);
                    return table2014;
                case 2015:
                    sqlselect = GenerateSqlZapros(sqlparam,SQLSvedFL2015);
                    DataSet table2015 = q.Sqlfl(sqlselect, detal);
                    return table2015;
                case 2016:
                    sqlselect = GenerateSqlZapros(sqlparam,SQLSvedFL2016);
                    DataSet table2016 = q.Sqlfl(sqlselect, detal);
                    return table2016;
                case 2017:
                    sqlselect = GenerateSqlZapros(sqlparam,SQLSvedFL2017);
                    DataSet table2017 = q.Sqlfl(sqlselect, detal);
                    return table2017;
                case 2018:
                    sqlselect = GenerateSqlZapros(sqlparam,SQLSvedFL2018);
                    DataSet table2018 = q.Sqlfl(sqlselect, detal);
                    return table2018;
            }
            var tablezero = new DataSet();
            return tablezero;
        }

        /// <summary>
        /// Генерация SQL выборки накладка на параметры
        /// </summary>
        /// <param name="sqlparam">Параметры</param>
        /// <param name="sqlstring">Sql без параметров</param>
        /// <returns>Возвращаем полноценный SQL запрос</returns>
        private string GenerateSqlZapros(List<string> sqlparam,string sqlstring)
        {
            var i = 1;
            foreach (string param in sqlparam)
            {
                    if (i == 1)
                    {
                        sqlstring = String.Format(sqlstring, sqlparam.Count > 1 ? param + " and {0}" : param);
                    }
                    else
                    {
                        sqlstring = String.Format(sqlstring, sqlparam.Count == i ? param : param + " and {0}");
                    }
                i++;
            }
            return sqlstring;
        }
    }
}