using System;
using System.Collections.Generic;
using System.DirectoryServices;
using LibaryXMLAuto.Inventarization.ModelComparableUserAllSystem;

namespace EfDatabase.Inventory.ComparableSystem.ComparableActiveDirectory
{
    /// <summary>
    /// Класс сбора данных из Active Directory
    /// </summary>
    public class ComparableActiveDirectory
    {
        /// <summary>
        /// Наименование домена
        /// </summary>
        public string Domain { get; set; }
        /// <summary>
        /// Путь к домену
        /// </summary>
        public string PathDomain { get; set; }
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


        public void SelectModelActiveDirectory(ModelComparableUser modelUsers)
        {
            DirectoryEntry directoryEntry = new DirectoryEntry(Domain) { Path = PathDomain };
            DirectorySearcher searcher = new DirectorySearcher(directoryEntry) { Filter = "(objectClass=computer)", SizeLimit = int.MaxValue, PageSize = int.MaxValue };
            List<GroupActiveDirectory> groupActiveDirectory = new List<GroupActiveDirectory>();
            List<WorkStationActiveDirectory> workStationActiveDirectory = new List<WorkStationActiveDirectory>();
            List<FullModelUserAllSystem> userActiveDirectory = new List<FullModelUserAllSystem>();
            foreach (SearchResult resEnt in searcher.FindAll())
            {
                workStationActiveDirectory.Add(new WorkStationActiveDirectory()
                {
                    NameWorkStation = resEnt.Properties["name"].Count != 0 ? resEnt.Properties["name"][0].ToString() : null,
                    DnsName = resEnt.Properties["dnshostname"].Count != 0 ? resEnt.Properties["dnshostname"][0].ToString() : null,
                    Info = resEnt.Properties["description"].Count != 0 ? resEnt.Properties["description"][0].ToString() : null,
                    ControlledUser = resEnt.Properties["managedby"].Count != 0 ? resEnt.Properties["managedby"][0].ToString() : null,
                    WindowsName = resEnt.Properties["operatingsystem"].Count != 0 ? resEnt.Properties["operatingsystem"][0].ToString() : null,
                    WindowsVersion = resEnt.Properties["operatingsystemversion"].Count != 0 ? resEnt.Properties["operatingsystemversion"][0].ToString() : null,
                    Room = resEnt.Properties["location"].Count != 0 ? resEnt.Properties["location"][0].ToString() : null
                });
            }
            searcher = new DirectorySearcher(directoryEntry) { Filter = "(objectClass=user)", SizeLimit = int.MaxValue, PageSize = int.MaxValue };
            foreach (SearchResult resEnt in searcher.FindAll())
            {
                userActiveDirectory.Add(new FullModelUserAllSystem()
                {
                    SystemDataBase = "Active Derectory",
                    Surname = resEnt.Properties["sn"].Count != 0 ? resEnt.Properties["sn"][0].ToString() : null,
                    Name = resEnt.Properties["givenname"].Count != 0 ? resEnt.Properties["givenname"][0].ToString().Split(' ')[0] : null,
                    Patronymic = resEnt.Properties["givenname"].Count != 0 ? resEnt.Properties["givenname"][0].ToString() : null,
                    FullName = resEnt.Properties["name"].Count != 0 ? resEnt.Properties["name"][0].ToString() : null,
                    PersonnelNumber = resEnt.Properties["samaccountname"].Count != 0 ? resEnt.Properties["samaccountname"][0].ToString() : null,
                    Department = resEnt.Properties["department"].Count != 0 ? resEnt.Properties["department"][0].ToString() : null,
                    JobTitle = resEnt.Properties["title"].Count != 0 ? resEnt.Properties["title"][0].ToString() : null,
                    Ranks = null,
                    Telephon = resEnt.Properties["telephonenumber"].Count != 0 ? resEnt.Properties["telephonenumber"][0].ToString() : null,
                    NameMail = resEnt.Properties["mail"].Count != 0 ? resEnt.Properties["mail"][0].ToString() : null,
                    Room = resEnt.Properties["physicaldeliveryofficename"].Count != 0 ? resEnt.Properties["physicaldeliveryofficename"][0].ToString() : null,
                    UserAccountControl = resEnt.Properties["useraccountcontrol"].Count != 0 ? (int)resEnt.Properties["useraccountcontrol"][0] : 0,
                });

            }
            searcher = new DirectorySearcher(directoryEntry) { Filter = "(objectClass=group)", SizeLimit = int.MaxValue, PageSize = int.MaxValue };
            foreach (SearchResult resEnt in searcher.FindAll())
            {
                groupActiveDirectory.Add(new GroupActiveDirectory()
                {
                    NameGroup = resEnt.Properties["name"].Count != 0 ? resEnt.Properties["name"][0].ToString() : null,
                    InfoGroup = resEnt.Properties["description"].Count != 0 ? resEnt.Properties["description"][0].ToString() : null,
                    ControlledGroup = resEnt.Properties["managedby"].Count != 0 ? resEnt.Properties["managedby"][0].ToString() : null
                });
            }
          //  Array.Resize<FullModelUserAllSystem>(ref modelUsers.FullModelUserAllSystem, userActiveDirectory.Count);
            
            modelUsers.FullModelUserAllSystem = userActiveDirectory.ToArray();
            modelUsers.GroupActiveDirectory = groupActiveDirectory.ToArray();
            modelUsers.Items = workStationActiveDirectory.ToArray();
        }

    }
}
