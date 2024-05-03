using System;
using System.Threading.Tasks;
using System.Timers;
using EfDatabase.Inventory.BaseLogic.Select;

namespace InventoryProcess.StartProcessInventory.ProcessStart
{
    public class TimeEventProcess
    {
        /// <summary>
        /// Класс для запуска процессов по времени старта
        /// </summary>
        public TimeEventProcess()
        {
            var startProcessTimer = new Timer() { Interval = 60000, Enabled = true, AutoReset = true };
            startProcessTimer.Elapsed += StartAllProcess;
            startProcessTimer.Start();
        }

        /// <summary>
        /// Автоматическая задача сбора Ip Адресов пляшем от домена поиск и ping
        ///  Задача поиска компьютеров и добавление их в БД
        /// </summary>
        public async void StartAllProcess(object sender, EventArgs e)
        {
            await Task.Factory.StartNew(async () =>
            {
                try
                {
                    var selectEvent = new Select();
                    var listProcess = selectEvent.SelectAllProcessTimeStart();
                    DateTime date = DateTime.Now;
                    foreach (var process in listProcess)
                    {
                        if (process.HoursX != null || process.MinutesX != null)
                        {
                            if  (process.SelectDayOfTheWeek != null && 
                                (date.Hour == process.HoursX && date.Minute == process.MinutesX && (int)date.DayOfWeek == process.SelectDayOfTheWeek?.EnumDay)|
                                (date.Hour == process.HoursX && date.Minute == process.MinutesX && process.SelectDayOfTheWeek.EnumDay == -1))
                            {
                                await Task.Run(() =>
                                {
                                    var processStart = new ProcessStart(process.IdProcess);
                                    processStart.StartProcess();
                                });
                            }
                        }
                        else
                        {
                            Loggers.Log4NetLogger.Error(new Exception($"Ошибка в процессе {process.InfoEvent} не указано минуты или часы запуска!!!"));
                        }
                    }
                    selectEvent.Dispose();
                }
                catch (Exception ex)
                {
                    Loggers.Log4NetLogger.Error(ex);
                }
            });
        }

    }
}
