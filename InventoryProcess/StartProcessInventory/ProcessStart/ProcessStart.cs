using EfDatabase.Inventory.BaseLogic.AddObjectDb;
using EfDatabase.Inventory.BaseLogic.Select;
using SignalRLibary.SignalRinventory;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InventoryProcess.StartProcessInventory.ProcessStart
{
    public class ProcessStart
    {
        /// <summary>
        /// Индекс процесса
        /// </summary>
        private int IndexProcess { get; set; }
        /// <summary>
        /// Логин пользователя может быть NULL
        /// </summary>
        private string LoginUser { get; set; }
        /// <summary>
        /// Пароль пользователя может быть NULL
        /// </summary>
        private string PasswordUser { get; set; }
        public ProcessStart(int indexProcess, string loginUser = null, string passwordUser = null)
        {
            IndexProcess = indexProcess;
            LoginUser = loginUser;
            PasswordUser = passwordUser;
        }

        public void StartProcess()
        {
            try
            {
                Select auto = new Select();
                var process = auto.SelectProcessAndParameters(IndexProcess);
                var parameters = process.ProcessAndParameters.Select(x => x.ParameterEventProcess).ToList();
                if (process.IsComplete != null && (bool)process.IsComplete)
                {
                    var addObjectDb = new AddObjectDb();
                    addObjectDb.IsProcessComplete(IndexProcess, false);
                    var task = Task.Factory.StartNew(() =>
                    {
                        Type type = Type.GetType($"{process.FindNameSpace}, {process.NameDll}");
                        var instance = Activator.CreateInstance(type ?? throw new InvalidOperationException(), LoginUser, PasswordUser, parameters);
                        instance.GetType().GetMethod(process.NameMethodProcess)?.Invoke(instance, null);
                    }, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Current);
                    task.ConfigureAwait(true).GetAwaiter().OnCompleted(() =>
                    {
                        try
                        {
                            addObjectDb.IsProcessComplete(IndexProcess, true);
                            addObjectDb.Dispose();
                            SignalRinventory.SubscribeStatusProcess(new ModelReturn<string>($"{process.InfoEvent} завершен!", null, 3));
                        }
                        catch (Exception e)
                        {
                            Loggers.Log4NetLogger.Error(e);
                        }
                    });
                    SignalRinventory.SubscribeStatusProcess(new ModelReturn<string>($"{process.InfoEvent} запущен!", null, 1));
                }
                else
                {
                    SignalRinventory.SubscribeStatusProcess(
                        new ModelReturn<string>($"{process.InfoEvent} уже запущен ожидайте окончание процесса!", null,
                            2));
                }
                auto.Dispose();
            }
            catch (Exception e)
            {
                SignalRinventory.SubscribeStatusProcess(new ModelReturn<string>(e.Message));
                Loggers.Log4NetLogger.Error(e);
            }
        }
    }
}
