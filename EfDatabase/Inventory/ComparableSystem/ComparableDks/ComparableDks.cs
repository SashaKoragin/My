using System.Data.SqlClient;
using System.Text;
using System.Xml;
using LibaryXMLAuto.Inventarization.ModelComparableUserAllSystem;
using LibaryXMLAuto.ReadOrWrite;

namespace EfDatabase.Inventory.ComparableSystem.ComparableDks
{
    /// <summary>
    /// Класс сбора данных из ДКС
    /// </summary>
    public class ComparableDks
    {

        /// <summary>
        /// Выборка для сравнения пользователей из ДКС
        /// </summary>
        private string UsersDks = @"Select 'DKS' as SystemDataBase, RTRIM(FM)as Surname, RTRIM(IM) as Name, RTRIM(OT) as Patronymic, 
                                         RTRIM(FIO) as FullName, RTRIM(TAB_NUM) as PersonnelNumber, RTRIM(DSK.NAME) as Department,
                                             RTRIM(DICTIONARY_POST.NAME) as JobTitle,
                                             RTRIM(CHIN_T.NAME) as Ranks,
											 case WHEN FullModelUserAllSystem.PHONE_TYPE = 2 THEN '8'+CONVERT(varchar,FullModelUserAllSystem.PHONE_PRINT)
											 ELSE '8'+CONVERT(varchar,FullModelUserAllSystem.PHONE_PRINT) END as PhoneNumber,
											 FullModelUserAllSystem.PHONE_TYPE as TypePhone,
											 RTRIM(MAN_MAIL) as NameMail,
                                             RTRIM(MAN_KAB) as Room,
                                             NULL as UserAccountControl,
											 NULL as GroupName
                                             FRom dbo.EMPLOYERS_TBL as Fulls
                                     JOIN (  Select I2.LINK_EMPL as LINKS, STAFF_LINK,I2.LINK From ITEM_MOVE I1
                                     Join ( Select LINK_EMPL as  LINK_EMPL, MAX(LINK) as LINK From ITEM_MOVE
                                            Where ACTIVE = 1 and DATE_END > GETDATE()
                                            GROUP BY LINK_EMPL ) as I2  on I1.LINK = I2.LINK ) as T on T.LINKS=Fulls.LINK
                                     JOIN dbo.FACES_MAIN_TBL AS FM ON FM.LINK = Fulls.FACE_LINK
                                     JOIN dbo.STAFF STAF on STAF.LINK = T.STAFF_LINK
                                     JOIN dbo.SUBDIV AS DS ON STAF.SUBDIV_LINK = DS.LINK_UP
									 LEFT JOIN (SELECT CHIN.FACE_LINK,CHIN.CHIN_LINK FROM dbo.FACES_CHIN CHIN 
									       JOIN (Select A.Face_LINK, MAX(A.LINK) as LINK From dbo.FACES_CHIN A
									       GROUP BY A.Face_LINK) CHIN_FACE on CHIN_FACE.LINK = CHIN.LINK
									 ) CHIN on CHIN.Face_LINK = Fulls.FACE_LINK
									 LEFT JOIN dbo.DICTIONARY_CHIN_TABLE CHIN_T on CHIN_T.LINK = CHIN.CHIN_LINK
                                     JOIN dbo.DICTIONARY_SUBDIV_KLASSIF AS DSK ON DS.LINK_EX = DSK.LINK
                                     JOIN DICTIONARY_POST ON STAF.POST_LINK= DICTIONARY_POST.LINK
									 LEFT JOIN DICTIONARY_PHONES_FACES as FullModelUserAllSystem on FullModelUserAllSystem.LINK_EMPL = Fulls.LINK
                                     JOIN (Select State.LINK_EMPL,STATUS.STATUS From dbo.EMPLOYERS_STATUS STATUS
                                     JOIN(Select LINK_EMPL,max(DATE) as DATE From dbo.EMPLOYERS_STATUS
                                          Where DATE < Getdate()
                                          GROUP BY LINK_EMPL) State on State.LINK_EMPL = STATUS.LINK_EMPL and State.DATE = STATUS.DATE) as STATUS on STATUS.LINK_EMPL = Fulls.LINK
                                     JOIN dbo.STATUS_TYPES AS KSV ON STATUS.STATUS = KSV.LINK
                                    Where DATE_OUT is null and STATUS.STATUS != 5
									and FullModelUserAllSystem.PHONE_TYPE in (2,3) or FullModelUserAllSystem.PHONE_TYPE is null and DATE_OUT is null and STATUS.STATUS != 5
									For Xml Auto";

        private string ConnectionStringDks { get; set; }


        public ComparableDks(string connectionString)
        {
            ConnectionStringDks = connectionString;
        }

        /// <summary>
        /// Загрузка моделей из Active Derectory
        /// </summary>
        /// <param name="modelUsers">Параметры модели</param>
        public void SelectModelDks(ref ModelComparableUser modelUsers)
        {
             XmlReadOrWrite xml = new XmlReadOrWrite();
             var userReportCard = XmlString(ConnectionStringDks, UsersDks);
             userReportCard = string.Concat("<ModelComparableUser>", userReportCard, "</ModelComparableUser>");
             modelUsers.FullModelUserAllSystem.AddRange(((ModelComparableUser)xml.ReadXmlText(userReportCard, typeof(ModelComparableUser))).FullModelUserAllSystem);
        }
        /// <summary>
        /// Возврат string из for xml command
        /// </summary>
        /// <param name="connectionString">Строка соединения с БД</param>
        /// <param name="sqlForXmlCommand">Команда возврата string</param>
        /// <returns></returns>
        private string XmlString(string connectionString, string sqlForXmlCommand)
        {
            StringBuilder sb = new StringBuilder();
            using (var con = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand(sqlForXmlCommand, con))
                {
                    cmd.Connection.Open();
                    using (XmlReader reader = cmd.ExecuteXmlReader())
                    {
                        if (reader != null)
                        {
                            string str = null;
                            reader.Read();
                            while (str != "")
                            {
                                str = reader.ReadOuterXml();
                                sb.AppendLine(str);
                            }
                            return sb.ToString();
                        }
                    }
                }
            }
            return string.Empty;
        }
    }
}
