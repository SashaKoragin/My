using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Runtime.InteropServices;
using LibaryXMLAuto.Inventarization.ModelComparableUserAllSystem;

namespace EfDatabase.Inventory.ComparableSystem.ComparableActiveDirectory
{
    /// <summary>
    /// Класс сбора данных из Active Directory
    /// </summary>
    public class ComparableActiveDirectory : IDisposable
    {
        /// <summary>
        /// Наименование домена
        /// </summary>
        private string Domain { get; set; }
        /// <summary>
        /// Директория домена
        /// </summary>
        private DirectoryEntry DirectoryEntry { get; set; }
        /// <summary>
        /// Строка поиска
        /// </summary>
        private DirectorySearcher Searcher { get; set; }

        /// <summary>
        /// Путь к домену
        /// </summary>
        private string PathDomain { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="domain">Домен </param>
        /// <param name="pathDomain">Путь к поиску в домене</param>
        public ComparableActiveDirectory(string domain, string pathDomain)
        {
            Domain = domain;
            PathDomain = pathDomain;
        }

        /// <summary>
        /// Загрузка моделей из Active Derectory
        /// </summary>
        /// <param name="modelUsers">Параметры модели</param>
        public void SelectModelActiveDirectory(ref ModelComparableUser modelUsers)
        {
            DirectoryEntry = new DirectoryEntry(Domain) { Path = PathDomain };
            Searcher = new DirectorySearcher(DirectoryEntry) { Filter = "(objectClass=computer)", SizeLimit = int.MaxValue, PageSize = int.MaxValue };
            if (modelUsers.FullModelUserAllSystem == null) { modelUsers.FullModelUserAllSystem = new List<FullModelUserAllSystem>(); }
            if (modelUsers.WorkStationActiveDirectory == null) { modelUsers.WorkStationActiveDirectory = new List<WorkStationActiveDirectory>(); }
            if (modelUsers.GroupActiveDirectory == null) { modelUsers.GroupActiveDirectory = new List<GroupActiveDirectory>(); }
            using (var src = Searcher.FindAll())
            {
                var enumerator = src.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    modelUsers.WorkStationActiveDirectory.Add(new WorkStationActiveDirectory()
                    {
                        NameWorkStation = ((SearchResult)enumerator.Current).Properties["name"].Count != 0 ? ((SearchResult)enumerator.Current).Properties["name"][0].ToString() : null,
                        DnsName = ((SearchResult)enumerator.Current).Properties["dnshostname"].Count != 0 ? ((SearchResult)enumerator.Current).Properties["dnshostname"][0].ToString() : null,
                        Info = ((SearchResult)enumerator.Current).Properties["description"].Count != 0 ? ((SearchResult)enumerator.Current).Properties["description"][0].ToString() : null,
                        ControlledUser = ((SearchResult)enumerator.Current).Properties["managedby"].Count != 0 ? ((SearchResult)enumerator.Current).Properties["managedby"][0].ToString() : null,
                        WindowsName = ((SearchResult)enumerator.Current).Properties["operatingsystem"].Count != 0 ? ((SearchResult)enumerator.Current).Properties["operatingsystem"][0].ToString() : null,
                        WindowsVersion = ((SearchResult)enumerator.Current).Properties["operatingsystemversion"].Count != 0 ? ((SearchResult)enumerator.Current).Properties["operatingsystemversion"][0].ToString() : null,
                        Room = ((SearchResult)enumerator.Current).Properties["location"].Count != 0 ? ((SearchResult)enumerator.Current).Properties["location"][0].ToString() : null
                    });
                }
            }
            Searcher = new DirectorySearcher(DirectoryEntry) { Filter = "(objectClass=user)", SizeLimit = int.MaxValue, PageSize = int.MaxValue };
            using (var src = Searcher.FindAll())
            {
                var enumerator = src.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    if (((SearchResult)enumerator.Current).Properties["department"].Count == 0) continue;
                    var modelUser = new FullModelUserAllSystem()
                    {
                        SystemDataBase = "Active Derectory",
                        Surname = ((SearchResult)enumerator.Current).Properties["sn"].Count != 0 ? ((SearchResult)enumerator.Current).Properties["sn"][0].ToString() : null,
                        Name = ((SearchResult)enumerator.Current).Properties["givenname"].Count != 0
                            ? ((SearchResult)enumerator.Current).Properties["givenname"][0].ToString().Split(' ')[0]
                            : null,
                        Patronymic = ((SearchResult)enumerator.Current).Properties["givenname"].Count != 0
                            ? ((SearchResult)enumerator.Current).Properties["givenname"][0].ToString()
                            : null,
                        FullName =
                            ((SearchResult)enumerator.Current).Properties["name"].Count != 0 ? ((SearchResult)enumerator.Current).Properties["name"][0].ToString() : null,
                        PersonnelNumber = ((SearchResult)enumerator.Current).Properties["samaccountname"].Count != 0
                            ? ((SearchResult)enumerator.Current).Properties["samaccountname"][0].ToString()
                            : null,
                        Department = ((SearchResult)enumerator.Current).Properties["department"].Count != 0
                            ? ((SearchResult)enumerator.Current).Properties["department"][0].ToString().Replace("№ ", "№")
                            : null,
                        JobTitle = ((SearchResult)enumerator.Current).Properties["title"].Count != 0
                            ? ((SearchResult)enumerator.Current).Properties["title"][0].ToString()
                            : null,
                        Ranks = null,
                        PhoneNumber = ((SearchResult)enumerator.Current).Properties["telephonenumber"].Count != 0
                            ? ((SearchResult)enumerator.Current).Properties["telephonenumber"][0].ToString()
                            : null,
                        TypePhone = 1,
                        NameMail = ((SearchResult)enumerator.Current).Properties["mail"].Count != 0 ? ((SearchResult)enumerator.Current).Properties["mail"][0].ToString() : null,
                        Room = ((SearchResult)enumerator.Current).Properties["physicaldeliveryofficename"].Count != 0
                            ? ((SearchResult)enumerator.Current).Properties["physicaldeliveryofficename"][0].ToString()
                            : null,
                        UserAccountControl = ((SearchResult)enumerator.Current).Properties["useraccountcontrol"].Count != 0
                            ? (int)((SearchResult)enumerator.Current).Properties["useraccountcontrol"][0]
                            : 0,
                        DistingUishedName = ((SearchResult)enumerator.Current).Properties["distinguishedname"].Count != 0
                            ? ((SearchResult)enumerator.Current).Properties["distinguishedname"][0].ToString()
                            : null
                    };
                    if (((SearchResult)enumerator.Current).Properties["memberOf"].Count != 0)
                    {
                        modelUser.MemberOfGroup = new List<MemberOfGroup>();
                        var arrayGroup = ((SearchResult)enumerator.Current).Properties["memberOf"];
                        foreach (var group in arrayGroup)
                        {
                            modelUser.MemberOfGroup.Add(new MemberOfGroup() { NameGroup = group.ToString() });
                        }
                    }
                    modelUsers.FullModelUserAllSystem.Add(modelUser);
                }
            }
            Searcher = new DirectorySearcher() { Filter = "(objectClass=group)", SizeLimit = int.MaxValue, PageSize = int.MaxValue };
            using (var src = Searcher.FindAll())
            {
                var enumerator = src.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    modelUsers.GroupActiveDirectory.Add(new GroupActiveDirectory()
                    {
                        NameGroup = ((SearchResult)enumerator.Current).Properties["name"].Count != 0 ? ((SearchResult)enumerator.Current).Properties["name"][0].ToString() : null,
                        InfoGroup = ((SearchResult)enumerator.Current).Properties["description"].Count != 0 ? ((SearchResult)enumerator.Current).Properties["description"][0].ToString().Replace("№ ", "№") : null,
                        ControlledGroup = ((SearchResult)enumerator.Current).Properties["managedby"].Count != 0 ? ((SearchResult)enumerator.Current).Properties["managedby"][0].ToString() : null
                    });
                }
            }
        }
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {

                Searcher.Dispose();
                Searcher = null;
                DirectoryEntry.Close();
                DirectoryEntry.Dispose();
                DirectoryEntry = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
