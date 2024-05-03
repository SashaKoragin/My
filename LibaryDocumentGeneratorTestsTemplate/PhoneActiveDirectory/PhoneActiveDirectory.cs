using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.IO;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LibraryDocumentGeneratorTestsTemplate.PhoneActiveDirectory
{
    [TestClass()]
    public class PhoneActiveDirectory
    {
        /// <summary>
        /// Тестовый скрипт выгрузки телефонного справочника из ActiveDirectory
        /// </summary>
        [TestMethod()]
        public void CreateFullTelephoneAd()
        {
            var listModelAllUsersActiveDirectory = new List<ModelAllUsersActiveDirectory>();
            var directory = "LDAP://regions.tax.nalog.ru";
            DirectoryEntry directoryEntry = new DirectoryEntry(directory) { Path = "LDAP://DC=regions,DC=tax,DC=nalog,DC=ru" };
            DirectorySearcher searcher = new DirectorySearcher(directoryEntry) { Filter = "(&(primaryGroupID=513)((telephoneNumber=*)))", SizeLimit = int.MaxValue, PageSize = int.MaxValue };
            using (var src = searcher.FindAll())
            {
                var enumerator = src.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var accountName = (enumerator.Current as SearchResult).Properties["sAMAccountName"].Count != 0 ? ((SearchResult)enumerator.Current)?.Properties["sAMAccountName"][0].ToString() : null;
                    var displayName = (enumerator.Current as SearchResult).Properties["displayName"].Count != 0 ? ((SearchResult) enumerator.Current)?.Properties["displayName"][0].ToString() : null;
                    var company = (enumerator.Current as SearchResult).Properties["company"].Count != 0 ? ((SearchResult) enumerator.Current)?.Properties["company"][0].ToString() : null;
                    var department = (enumerator.Current as SearchResult).Properties["department"].Count != 0 ? ((SearchResult) enumerator.Current)?.Properties["department"][0].ToString() .Replace("№ ", "№") : null;
                    var jobTitle = (enumerator.Current as SearchResult).Properties["title"].Count != 0 ? ((SearchResult) enumerator.Current)?.Properties["title"][0].ToString()  : null;
                    var phoneNumber = (enumerator.Current as SearchResult).Properties["telephoneNumber"].Count != 0 ? ((SearchResult) enumerator.Current)?.Properties["telephoneNumber"][0].ToString() : null;
                    var mail = (enumerator.Current as SearchResult).Properties["mail"].Count != 0  ? ((SearchResult) enumerator.Current)?.Properties["mail"][0].ToString() : null;
                    if (accountName != null && !accountName.Contains("n") & !string.IsNullOrWhiteSpace(phoneNumber) & phoneNumber!="-" & phoneNumber != ".")
                    {
                        var modelUser = new ModelAllUsersActiveDirectory()
                        {
                            DisplayName = displayName,
                            Company = company,
                            Department = department,
                            JobTitle = jobTitle,
                            PhoneNumber = phoneNumber,
                            Mail = mail,
                        };
                        listModelAllUsersActiveDirectory.Add(modelUser);
                    }
                }
                CreateXmlFile("D:\\AllTelephoneActiveDirectory.xml", listModelAllUsersActiveDirectory, typeof(List<ModelAllUsersActiveDirectory>));
            }
        }

        /// <summary>
        /// Создание xml файла
        /// </summary>
        /// <param name="pathToFullNameSave">Полный путь с расширением xml DeclarationData.xml</param>
        /// <param name="classType">Класс для сериализации</param>
        /// <param name="objType">Тип объекта</param>
        private void CreateXmlFile(string pathToFullNameSave, object classType, Type objType)
        {
            XmlSerializer writer = new XmlSerializer(objType);
            FileStream file = File.Create(pathToFullNameSave);
            writer.Serialize(file, classType);
            file.Close();
            file.Dispose();
        }



    }
    /// <summary>
    /// Телефонный справочник Active Directory
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.8.4084.0")]
    [SerializableAttribute]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    public  class ModelAllUsersActiveDirectory
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [XmlAttributeAttribute()]
        public string DisplayName { get; set; }
        /// <summary>
        /// Организация
        /// </summary>
        [XmlAttributeAttribute()]
        public string Company { get; set; }
        /// <summary>
        /// Отдел
        /// </summary>
        [XmlAttributeAttribute()]
        public string Department { get; set; }
        /// <summary>
        /// Должность
        /// </summary>
        [XmlAttributeAttribute()]
        public string JobTitle { get; set; }
        /// <summary>
        /// Телефон
        /// </summary>
        [XmlAttributeAttribute()]
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Почта
        /// </summary>
        [XmlAttributeAttribute()]
        public string Mail { get; set; }

    }
}
