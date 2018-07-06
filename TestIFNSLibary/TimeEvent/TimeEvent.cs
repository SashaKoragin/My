using System;
using System.Collections.Generic;
using System.Linq;
using SqlLibaryIfns.SqlZapros.Report;
using SqlLibaryIfns.ExcelReport.Report;
using System.Threading.Tasks;
using System.Timers;
using SqlLibaryIfns.SqlSelect.SqlReshenia;
using TestIFNSLibary.PathJurnalAndUse;
using TestIFNSLibary.WebSevice.Bakcup;

namespace TestIFNSLibary.TimeEvent
{
    /// <summary>
    /// Класс для автоматических заданий
    /// </summary>
   public class TimeEvent
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public TimeEvent()
        {
            var timerstart = new Timer
            {
                Interval = 60000,
                Enabled = true,
                AutoReset = true
            };
            timerstart.Elapsed += Bakcup;
            timerstart.Start();
           var selectsqlreport = new Timer()
            {
                Interval = 60000,
                Enabled = true,
                AutoReset = true
            };
            selectsqlreport.Elapsed += ReportSql;
            selectsqlreport.Start();
        }
        /// <summary>
        /// Автоматическая задача резервного копирования 2NDFL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Bakcup(object sender, EventArgs e)
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    Parametr parametr = new Parametr();
                    DateTime date = DateTime.Now;
                    if (date.Hour == parametr.Hours && date.Minute == parametr.Minutes)
                    {
                        BakcupingDb bakcuping = new BakcupingDb();
                        bakcuping.Backup(parametr.WorkDB, parametr.TestDB);
                    }
                }
                catch (Exception ex)
                {
                    Loggers.Log4NetLogger.Error(ex);
                }
            });
        }
        /// <summary>
        /// Автоматическая задача сбора данных по Требованиям по которым не выставленны решения
        /// Задача Sql и Excel 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void ReportSql(object sender, EventArgs e)
        {
            await Task.Factory.StartNew(() =>
            {
                try
                {
                    Parametr parametr = new Parametr();
                    DateTime date = DateTime.Now;
                    if (date.Hour == parametr.Hours && date.Minute == parametr.Minutes)
                    {
                        var sql = new ReportSqlQbe();
                        var xlsx = new ReportExcel();
                        xlsx.ReportSave(Parametr.Report,"Требования","Требования",sql.ReportQbe(Parametr.ConnectionString, ProcedureReshenie.SqlFullTreb));
                    }
                }
                catch (Exception ex)
                {
                    Loggers.Log4NetLogger.Error(ex);
                }
            });
        }
    }
}
