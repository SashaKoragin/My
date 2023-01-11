using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using EfDatabase.Inventory.FileServerFindPathShares;
using EfDatabase.ReportXml.ModelFileServer;
using LibaryXMLAuto.ReadOrWrite;
using Microsoft.WindowsAPICodePack.Shell;

namespace EfDatabase.Inventory.BaseLogic.FileServerAddFile
{
    /// <summary>
    /// Расширение для IEnumerable
    /// </summary>
    public static class EnumerableExtensions
    {

        /// <summary>
        /// Дробление коллекции на составляющие
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="source">Ресурс коллекция</param>
        /// <param name="length">Количество дробления</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Section<T>(this IEnumerable<T> source, int length)
        {
            //return source.Select((item, index) => new { index, item }).GroupBy(x => x.index % length)
            //    .Select(x => x.Select(y => y.item));
            T[] array = null;
            int count = 0;
            foreach (T item in source)
            {
                if (array == null)
                {
                    array = new T[length];
                }
                array[count] = item;
                count++;
                if (count == length)
                {
                    yield return new ReadOnlyCollection<T>(array);
                    array = null;
                    count = 0;
                }
            }
            if (array != null)
            {
                Array.Resize(ref array, count);
                yield return  new ReadOnlyCollection<T>(array);
            }
        }
        /// <summary>
        /// Поиск позиции индекса элемента
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="source">Ресурс коллекция</param>
        /// <param name="value">Элемент для поиска</param>
        /// <returns></returns>
        public static int IndexOf<T>(this IEnumerable<T> source, T value)
        {
            return source.IndexOf(value, null);
        }
        /// <summary>
        /// Поиск позиции индекса элемента
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="source">Ресурс коллекция</param>
        /// <param name="value">Элемент для поиска</param>
        /// <param name="comparer">Условие поиска</param>
        /// <returns></returns>
        public static int IndexOf<T>(this IEnumerable<T> source, T value, IEqualityComparer<T> comparer)
        {
            comparer = comparer ?? EqualityComparer<T>.Default;
            var found = source.Select((a, i) => new { a, i }).FirstOrDefault(x => comparer.Equals(x.a, value));
            return found?.i ?? -1;
        }
    }
    public class ProcessFindAllFileServers
    {
        private readonly string[] sizes = { "B", "KB", "MB", "GB", "TB" };

        /// <summary>
        ///  Папка для сохранения документов xml
        /// </summary>
        public string InXmlFile { get; set; }
        /// <summary>
        ///  Папка для подготовки документов xml для БД
        /// </summary>
        public string OutXmlFile { get; set; }
        /// <summary>
        /// Определения количества в пачке
        /// </summary>
        public static int CountFilePack { get; set; }
        /// <summary>
        /// Количество параллельных процессов обработки пачки 
        /// </summary>
        public static int CountParallelProcess { get; set; }
        /// <summary>
        ///   Папка для ошибочных документов xml для БД
        /// </summary>
        public string ErrorXmlFile { get; set; }
        public ProcessFindAllFileServers(string inXmlFile, string outXmlFile, string errorXmlFile, int filePackCount, int countParallelProcess)
        {
            CountParallelProcess = countParallelProcess;
            CountFilePack = filePackCount;
            InXmlFile = inXmlFile;
            OutXmlFile = outXmlFile;
            ErrorXmlFile = errorXmlFile;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nameFileServer">Наименование файлового сервера</param>
        public void ProcessAllFileServer(string nameFileServer)
        {
            var countFile = 0;
            var shi = ShareCollection.GetShares(nameFileServer);
            var fileServerAddDataBase = new FileServerAddFile(ErrorXmlFile);
            fileServerAddDataBase.AddFileServerToDataBase(null,true);
            if (shi != null)
            {
                foreach (Share si in shi)
                {
                    if (si.ShareType == ShareType.Disk)
                    {
                        if (si.IsFileSystem)
                        {
                            try
                            {
                                var taskScheduler = new LimitedConcurrencyLevelTaskScheduler(CountParallelProcess);
                                var taskSchedulerSave = new LimitedConcurrencyLevelTaskScheduler(CountParallelProcess);
                                var task = Task.Factory.StartNew(() =>
                                {
                                    //Есть еще вариант получать файлы дробить этот массив по 5000 и в два цикла это не вариант в папке 1500000 тыс файлов отбор всех приведет к замедлению
                                    //Вариант с Partition дробить в IEnumerable
                                    //Похоже БД слабое место в скорости вариант такой сохранять в файл делать 1500000 файлов xml потом объединять все в один и т д стремится к одному общему файлу
                                    //Последний вариант превзошел мои ожидания 600000 за 3 часа это очень хорошо теперь пишем в БД по 2500-5000 вопрос можно и больше (будут про садки по памяти)?
                                    var enumerableFileDistinct = Directory.EnumerateFiles(si.Root.FullName, "*.*", SearchOption.AllDirectories).Distinct();
                                    var listCollectionFiles = enumerableFileDistinct.Section(CountFilePack);
                                    Parallel.ForEach(listCollectionFiles, new ParallelOptions() { TaskScheduler = taskScheduler }, () => new List<AllFileServer>(),
                                        (files, state, localList) =>
                                        {
                                            foreach (var file in files)
                                            {
                                                try
                                                {
                                                    localList.Add(SaveInfoFileToFileXml(file));
                                                    //ReSharper disable once AccessToModifiedClosure
                                                    countFile++;
                                                }
                                                catch (Exception e)
                                                {
                                                    Loggers.LogFileServer.Error(e);
                                                    Loggers.LogFileServer.Error(new Exception($"Проблема в этом файле {file}: Файл не будет обработана"));
                                                }
                                            }
                                            return localList;
                                        }, (finalResult) =>
                                        {
                                            var taskSave = Task.Factory.StartNew(() =>
                                            {
                                                if (!finalResult.Any()) return;
                                                 var xmlSave = new XmlReadOrWrite();
                                                 var model = new AllFileServerModel() { AllFileServer = finalResult.ToArray() };
                                                 xmlSave.CreateXmlFile(OutXmlFile + $"{Guid.NewGuid()}.xml", model, model.GetType()); 
                                                 finalResult.Clear();
                                            }, CancellationToken.None, TaskCreationOptions.AttachedToParent, taskSchedulerSave);
                                            taskSave.Wait();
                                            taskSave.Dispose();
                                        });
                                }, CancellationToken.None, TaskCreationOptions.AttachedToParent, taskScheduler);
                                task.Wait();
                                task.Dispose();
                                var fileXmlAll = Directory.EnumerateFiles(OutXmlFile, "*.*", SearchOption.TopDirectoryOnly);
                                var xmlDeserialize = new XmlReadOrWrite();
                                var modelDataBase = new AllFileServerModel();
                                foreach (var fileXml in fileXmlAll)
                                {
                                     var model = (AllFileServerModel)xmlDeserialize.ReadXml(fileXml, typeof(AllFileServerModel));
                                     var modelAddToDataBase = model.AllFileServer.AsEnumerable().Section(CountFilePack);
                                     foreach (var xmlModel in modelAddToDataBase)
                                     {
                                         modelDataBase.AllFileServer = xmlModel.ToArray();
                                         fileServerAddDataBase.AddFileServerToDataBase(modelDataBase);
                                         modelDataBase.AllFileServer = null;
                                     }
                                     File.Delete(fileXml);
                                }
                                Loggers.LogFileServer.Info(new Exception($"Количество файлов: {countFile}, в папке: {si.Root.FullName}, обработано за сессию: {countFile}"));
                            }
                            catch (Exception ex)
                            {
                                Loggers.LogFileServer.Error(ex);
                                Loggers.LogFileServer.Error(new Exception("Скорее всего нет доступа к папке нужно получить или удалить цикл прерван!!!"));
                            }
                        }
                    }
                    countFile = 0;
                }
                fileServerAddDataBase.AddFileServerToDataBase(null);
                fileServerAddDataBase.Dispose();
            }
        }
        /// <summary>
        /// Сохранения файлов в xml документов
        /// </summary>
        /// <param name="file">Путь к имени файлов</param>
        private AllFileServer SaveInfoFileToFileXml(string file)
        {
            string hash = "0";
            var order = 0;
            AllFileServer listFile;
            var allAuthor = new List<AuthorAll>();
            try
            {
                using (var shellFile = ShellFile.FromFilePath(file))
                {

                    var sizeFiles = shellFile.Properties.System.Size.Value;
                    shellFile.Properties.System.Author.Value?.ToList().ForEach(name =>
                    {
                        allAuthor.Add(new AuthorAll() { Name = name });
                    });

                    shellFile.Properties.System.ItemParticipants.Value?.ToList().ForEach(name =>
                    {
                        allAuthor.Add(new AuthorAll() { Name = name });
                    });

                    while (sizeFiles >= 1024 && order < sizes.Length - 1)
                    {
                        order++;
                        sizeFiles /= 1024;
                    }

                    using (var stream = File.OpenRead(file))
                    {
                        using (var md5 = MD5.Create())
                        {
                            hash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "").ToLowerInvariant();
                            md5.Clear();
                            md5.Dispose();
                        }
                        stream.Close();
                        stream.Dispose();
                    }
                    listFile = new AllFileServer()
                    {
                        HashFile = hash,
                        NameSave = shellFile.Properties.System.Document.LastAuthor.Value,
                        FileOwnerAuthor = shellFile.Properties.System.FileOwner.Value,
                        LastAuthor = shellFile.Properties.System.Document.LastAuthor.Value,
                        FullPathFile = shellFile.Properties.System.ItemPathDisplay.Value,
                        PathFile = shellFile.Properties.System.ItemFolderPathDisplay.Value,
                        NameFile = shellFile.Properties.System.FileName.Value,
                        ItemTypeText = shellFile.Properties.System.ItemTypeText.Value,
                        TypeMime = shellFile.Properties.System.ContentType.Value,
                        FileExtension = shellFile.Properties.System.FileExtension.Value,
                        SizeFile = shellFile.Properties.System.Size.Value,
                        SizeFileText = $"{sizeFiles:0.##} {sizes[order]}",
                        DateCreated = shellFile.Properties.System.DateCreated.Value,
                        DateAccessed = shellFile.Properties.System.DateAccessed.Value,
                        DateModified = shellFile.Properties.System.DateModified.Value,
                        DateSaved = shellFile.Properties.System.Document.DateSaved.Value,
                        AuthorAll = allAuthor.Count > 0 ? allAuthor.ToArray() : null
                    };
                    shellFile.Dispose();
                }
            }
            catch (Exception ex)
            {
                listFile = new AllFileServer()
                {
                    FullPathFile = file,
                    NameFile = ex.Message,
                    HashFile = hash,
                };
            }
            return listFile;
        }
    }

    // Provides a task scheduler that ensures a maximum concurrency level while
    // running on top of the thread pool.
    public class LimitedConcurrencyLevelTaskScheduler : TaskScheduler
    {
        // Indicates whether the current thread is processing work items.
        [ThreadStatic]
        private static bool _currentThreadIsProcessingItems;

        // The list of tasks to be executed
        private readonly LinkedList<Task> tasks = new LinkedList<Task>(); // protected by lock(_tasks)

        // The maximum concurrency level allowed by this scheduler.
        private readonly int maxDegreeOfParallelism;

        // Indicates whether the scheduler is currently processing work items.
        private int delegatesQueuedOrRunning;

        // Creates a new instance with the specified degree of parallelism.
        public LimitedConcurrencyLevelTaskScheduler(int maxDegreeOfParallelism)
        {
            if (maxDegreeOfParallelism < 1) throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism));
            this.maxDegreeOfParallelism = maxDegreeOfParallelism;
        }

        // Queues a task to the scheduler.
        protected sealed override void QueueTask(Task task)
        {
            // Add the task to the list of tasks to be processed.  If there aren't enough
            // delegates currently queued or running to process tasks, schedule another.
            lock (tasks)
            {
                tasks.AddLast(task);
                if (delegatesQueuedOrRunning < maxDegreeOfParallelism)
                {
                    ++delegatesQueuedOrRunning;
                    NotifyThreadPoolOfPendingWork();
                }
            }
        }

        // Inform the ThreadPool that there's work to be executed for this scheduler.
        private void NotifyThreadPoolOfPendingWork()
        {
            ThreadPool.UnsafeQueueUserWorkItem(_ =>
            {
                // Note that the current thread is now processing work items.
                // This is necessary to enable inlining of tasks into this thread.
                Task item = null;
                _currentThreadIsProcessingItems = true;
                try
                {
                    // Process all available items in the queue.
                    while (true)
                    {

                        lock (tasks)
                        {
                            // When there are no more items to be processed,
                            // note that we're done processing, and get out.
                            if (tasks.Count == 0)
                            {
                                --delegatesQueuedOrRunning;
                                break;
                            }

                            // Get the next item from the queue
                            item = tasks.First.Value;
                            tasks.RemoveFirst();
                        }

                        // Execute the task we pulled out of the queue
                        TryExecuteTask(item);

                    }
                }
                // We're done processing items on the current thread
                finally
                {
                    _currentThreadIsProcessingItems = false;
                    item?.Dispose(); //Моя идея сбрасывать каждый Task при заходе 23.09.2022
                }
            }, null);
        }

        // Attempts to execute the specified task on the current thread.
        protected sealed override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            // If this thread isn't already processing a task, we don't support inlining
            if (!_currentThreadIsProcessingItems) return false;

            // If the task was previously queued, remove it from the queue
            if (taskWasPreviouslyQueued)
                // Try to run the task.
                if (TryDequeue(task))
                    return TryExecuteTask(task);
                else
                    return false;
            else
                return TryExecuteTask(task);
        }

        // Attempt to remove a previously scheduled task from the scheduler.
        protected sealed override bool TryDequeue(Task task)
        {
            lock (tasks) return tasks.Remove(task);
        }

        // Gets the maximum concurrency level supported by this scheduler.
        public sealed override int MaximumConcurrencyLevel => maxDegreeOfParallelism;

        // Gets an enumerable of the tasks currently scheduled on this scheduler.
        protected sealed override IEnumerable<Task> GetScheduledTasks()
        {
            bool lockTaken = false;
            try
            {
                Monitor.TryEnter(tasks, ref lockTaken);
                if (lockTaken) return tasks;
                else throw new NotSupportedException();
            }
            finally
            {
                if (lockTaken) Monitor.Exit(tasks);
            }
        }
    }
}
