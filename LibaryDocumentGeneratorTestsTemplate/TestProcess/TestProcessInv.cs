using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.WindowsAPICodePack.Shell;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using EfDatabase.Inventory.BaseLogic.Select;
using EfDatabase.Inventory.FileServerFindPathShares;
using EfDatabase.ReportXml.ModelFileServer;
using EfDatabaseParametrsModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using EfDatabase.Inventory.BaseLogic.FileServerAddFile;
using EfDatabase.Inventory.ComparableSystem.StartComparable;
using LibaryXMLAuto.ReadOrWrite;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

namespace LibaryDocumentGeneratorTestsTemplate.TestProcess
{
    [TestClass()]
    public class TestProcessInv
    {
        private readonly string[] sizes = {"B", "KB", "MB", "GB", "TB"};

        /// <summary>
        /// Бд для обновления или добавления в БД
        /// </summary>
        //     private readonly FileServerAddFile FileServerAddDataBase = new FileServerAddFile();


        [TestMethod()]
        public void TestP()
        {
            var start = new StartComparable("Data Source=77068-app016;Initial Catalog=imns51;Integrated Security=True", "LDAP://OU=IFNS7751,OU=UFNS77,DC=regions,DC=tax,DC=nalog,DC=ru", "http://77068-app065:8585/ServiceOutlook/AllUsersLotusNotes", "LDAP://regions.tax.nalog.ru");
            start.StartFullModelComparable();
        }

        [TestMethod()]
        public void ProcessCreateXml()
        {
            var fileServerAddDataBase = new FileServerAddFile(null);
            fileServerAddDataBase.AddFileServerToDataBase(null);
            fileServerAddDataBase.Dispose();
        }

        [TestMethod()]
        public void CreateXml()
        {
            //var xmlConvert = new XmlReadOrWrite();
            //var i = 0;
            //var allFilesModel = new List<AllFileServer>();
            //var filesXml = Directory.EnumerateFiles("\\\\77068-app041\\it\\InXmlFile", "*.*");
            //var countFileXml = filesXml.Count();
            //foreach (var file in filesXml)
            //{
            //    allFilesModel.AddRange(((AllFileServerModel)xmlConvert.ReadXml(file, typeof(AllFileServerModel))).AllFileServer);
            //  //  File.Delete(file);
            //    i++;
            //    if (!(i == countFileXml & allFilesModel.Count < 20000) | (allFilesModel.Count >= 20000)) continue;
            //  //  FileServerAddDataBase.AddFileServerToDataBase(new AllFileServerModel() { AllFileServer = allFilesModel.ToArray() });
            //    xmlConvert.CreateXmlFile("D:\\Testing\\Xml\\OutXmlFile\\" + $"{Guid.NewGuid()}.xml", new AllFileServerModel() { AllFileServer = allFilesModel.ToArray() }, typeof(AllFileServerModel));
            //    allFilesModel.Clear();
            //}
            //  Loggers.LogFileServer.Info(new Exception($"Количество файлов: {countFile}, в папке: {si.Root.FullName}, обработано за сессию: {countFile}"));
            var taskScheduler = new LimitedConcurrencyLevelTaskScheduler(10);
            //var task = Task.Factory.StartNew(() =>
            //{
            //Есть еще вариант получать файлы дробить этот массив по 5000 и в два цикла это не вариант в папке 1500000 тыс файлов отбор всех приведет к замедлению
            //Вариант с Partition дробить в IEnumerable
            //Похоже БД слабое место в скорости вариант такой сохранять в файл делать 1500000 файлов xml потом объединять все в один и т д стремится к одному общему файлу
            //Последний вариант превзошел мои ожидания 600000 за 3 часа это очень хорошо теперь пишем в БД по 2500-5000 вопрос можно и больше (будут про садки по памяти)?
            //   var enumerableFileDistinctd = Directory.GetFiles("\\\\77068-app041\\Network-Disk", "*.*", SearchOption.AllDirectories).Length;
                var enumerableFileDistinct = Directory.EnumerateFiles("\\\\77068-app041\\Network-Disk", "*.*", SearchOption.AllDirectories).Distinct();
                var listCollectionFiles = Section(enumerableFileDistinct, 5000);
            //    Parallel.ForEach(listCollectionFiles, new ParallelOptions() { TaskScheduler = taskScheduler }, () => new List<AllFileServer>(),
            //        (files, state, localList) =>
            //        {
            //            foreach (var file in files)
            //            {
            //                try
            //                {
            //                    localList.Add(SaveInfoFileToFileXml(file));
            //                                        //ReSharper disable once AccessToModifiedClosure
            //                                     //   countFile++;
            //                }
            //                catch (Exception e)
            //                {
            //                  //  Loggers.LogFileServer.Error(e);
            //                  //  Loggers.LogFileServer.Error(new Exception($"Проблема в этом файле {file}: Файл не будет обработана"));
            //                }
            //            }
            //            return localList;
            //        }, (finalResult) =>
            //        {
            //            if (!finalResult.Any()) return;
            //            var xmlSave = new XmlReadOrWrite();
            //            var model = new AllFileServerModel() { AllFileServer = finalResult.ToArray() };
            //            xmlSave.CreateXmlFile("D:\\Testing\\Xml\\OutXmlFile\\" + $"{Guid.NewGuid()}.xml", model, model.GetType());
            //            finalResult.Clear();
            //        });
            //}, CancellationToken.None, TaskCreationOptions.AttachedToParent, taskScheduler);
            //task.Wait();
            //task.Dispose();





        }


        /// <summary>
        /// Дробление коллекции на составляющие
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="source">Ресурс коллекция</param>
        /// <param name="length">Количество дробления</param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Section<T>(IEnumerable<T> source, int length)
        {
           // var t1 = source.Select((item, index) => new {index}).Last();
            var t = source.Count();
            var indexLast = source.IndexOf(source.Last());
            var count = Math.Ceiling((decimal)((float)(indexLast) / (float)(length)));
            return source.Select((item, index) => new {index, item}).GroupBy(x => x.index % count)
                   .Select(x => x.Select(y => y.item));
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

        private static ConcurrentDictionary<int, Func<PropertyKey, ShellPropertyDescription, object,IShellProperty>> _storeCache = new ConcurrentDictionary<int, Func<PropertyKey, ShellPropertyDescription, object, IShellProperty>>();

        //private static IShellProperty GenericCreateShellProperty<T>(PropertyKey propKey, T thirdArg)
        //{
        //    Func<PropertyKey, ShellPropertyDescription, object, IShellProperty> ctor;
        //    ctor = _storeCache.GetOrAdd((hash, (key, args)) 
        //    {
        //        Type[] argTypes = {typeof(PropertyKey), typeof(ShellPropertyDescription),args.thi}
        //    })
        //}
    }

    }

public static class AsyncEx
    {
        public static async Task ParallelForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> asyncAction, int maxDegreeOfParallelism = 10)
        {
            var semaphoreSlim = new SemaphoreSlim(maxDegreeOfParallelism);
            var tcs = new TaskCompletionSource<object>();
            var exceptions = new ConcurrentBag<Exception>();
            bool addingCompleted = false;

            foreach (T item in source)
            {
                await semaphoreSlim.WaitAsync();
                await asyncAction(item).ContinueWith(t =>
                {
                    semaphoreSlim.Release();
                    if (t.Exception != null)
                    {
                        exceptions.Add(t.Exception);
                    }
                    if (Volatile.Read(ref addingCompleted) && semaphoreSlim.CurrentCount == maxDegreeOfParallelism)
                    {
                        tcs.TrySetResult(null);
                    }
                });
            }
            Volatile.Write(ref addingCompleted, true);
            await tcs.Task;
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
    }

public static class EnumerableExtensions
{

    public static int IndexOf<T>(this IEnumerable<T> obj, T value)
    {
        return obj.IndexOf(value, null);
    }

    public static int IndexOf<T>(this IEnumerable<T> obj, T value, IEqualityComparer<T> comparer)
    {
        comparer = comparer ?? EqualityComparer<T>.Default;
        var found = obj.Select((a, i) => new { a, i }).FirstOrDefault(x => comparer.Equals(x.a, value));
        return found == null ? -1 : found.i;
    }
}
