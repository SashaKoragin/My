
using System.Collections.Generic;

namespace LibraryAutoSupportSto.PassportSto.PassportStoPostGet
{
    /// <summary>
    /// Параметры для запросов на СТО
    /// </summary>
   public class ParametersStoPostGet
    {
        /// <summary>
        /// Параметры авторизации на СТО
        /// </summary>
        public Dictionary<string, string> ListParametersAuthorization = new Dictionary<string, string>()
        {
            { "__EVENTTARGET","" },
            { "__EVENTARGUMENT", "" },
            { "Replace", "Replace" },
            { "ctl00$content$LoginUser$UserName", "{0}" },
            { "ctl00$content$LoginUser$Password","{1}" },
            { "ctl00$content$LoginUser$LoginButton","Войти" }
        };
        /// <summary>
        /// Параметры загрузки файла с сайта
        /// </summary>
        public Dictionary<string, string> ListParametersReportExcel = new Dictionary<string, string>()
        {
            {"__EVENTTARGET","" },
            { "__EVENTARGUMENT", "" },
            { "Replace", "Replace" },
            {"__SCROLLPOSITIONX", "0" },
            {"__SCROLLPOSITIONY","0" },
            {"ctl00$content$ddl_Trgts","0" },
            {"ctl00$content$EquipmentCategoryList","1" },
            {"ctl00$content$EquipmentCategoryCascadingDropDown_ClientState","1:::Оборудование и ПО:::" },
            {"ctl00$content$ColumnList$0","on" },
            {"ctl00$content$ColumnList$1","on" },
            {"ctl00$content$ColumnList$2","on" },
            {"ctl00$content$ColumnList$3","on" },
            {"ctl00$content$ColumnList$4","on" },
            {"ctl00$content$ColumnList$5","on" },
            {"ctl00$content$ColumnList$6","on" },
            {"ctl00$content$ColumnList$7","on" },
            {"ctl00$content$ColumnList$8","on" },
            {"ctl00$content$ColumnList$9","on" },
            {"ctl00$content$ColumnList$10","on" },
            {"ctl00$content$ColumnList$11","on" },
            {"ctl00$content$ColumnList$12","on" },
            {"ctl00$content$ColumnList$13","on" },
            {"ctl00$content$ColumnList$14","on" },
            {"ctl00$content$ColumnList$15","on" },
            {"ctl00$content$ColumnList$16","on" },
            {"ctl00$content$ColumnList$17","on" },
            {"ctl00$content$ColumnList$18","on" },
            {"ctl00$content$ColumnList$19","on" },
            {"ctl00$content$ColumnList$20","on" },
            {"ctl00$content$ColumnList$21","on" },
            {"ctl00$content$ColumnList$22","on" },
            {"ctl00$content$ColumnList$23","on" },
            {"ctl00$content$ColumnList$24","on" },
            {"ctl00$content$ColumnList$25","on" },
            {"ctl00$content$ColumnList$26","on" },
            {"ctl00$content$ColumnList$27","on" },
            {"ctl00$content$ColumnList$28","on" },
            {"ctl00$content$ColumnList$29","on" },
            {"ctl00$content$ColumnList$30","on" },
            {"ctl00$content$ColumnList$31","on" },
            {"ctl00$content$ColumnList$32","on" },
            {"ctl00$content$ColumnList$33","on" },
            {"ctl00$content$ColumnList$34","on" },
            {"ctl00$content$ColumnList$35","on" },
            {"ctl00$content$ColumnList$36","on" },
            {"ctl00$content$ColumnList$37","on" },
            {"ctl00$content$ColumnList$38","on" },
            {"ctl00$content$ColumnList$39","on" },
            {"ctl00$content$ColumnList$40","on" },
            {"ctl00$content$ColumnList$41","on" },
            {"ctl00$content$ColumnList$42","on" },
            {"ctl00$content$ColumnList$43","on" },
            {"ctl00$content$ColumnList$44","on" },
            {"ctl00$content$ColumnList$45","on" },
            {"ctl00$content$ColumnList$46","on" },
            {"ctl00$content$ColumnList$47","on" },
            {"ctl00$content$ColumnList$48","on" },
            {"ctl00$content$ColumnList$49","on" },
            {"ctl00$content$ColumnList$50","on" },
            {"ctl00$content$ColumnList$51","on" },
            {"ctl00$content$ColumnList$52","on" },
            {"ctl00$content$ColumnList$53","on" },
            {"ctl00$content$ColumnList$54","on" },
            {"ctl00$content$ColumnList$55","on" },
            {"ctl00$content$ColumnList$56","on" },
            {"ctl00$content$ColumnList$57","on" },
            {"ctl00$content$ColumnList$58","on" },
            {"ctl00$content$ColumnList$59","on" },
            {"ctl00$content$ColumnList$60","on" },
            {"ctl00$content$ColumnList$61","on" },
            {"ctl00$content$ColumnList$62","on" },
            {"ctl00$content$ColumnList$63","on" },
            {"ctl00$content$ColumnList$64","on" },
            {"ctl00$content$ColumnList$65","on" },
            {"ctl00$content$ColumnList$66","on" },
            {"ctl00$content$ColumnList$67","on" },
            {"ctl00$content$ColumnList$68","on" },
            {"ctl00$content$ColumnList$69","on" },
            {"ctl00$content$ColumnList$70","on" },
            {"ctl00$content$ColumnList$71","on" },
            {"ctl00$content$ColumnList$72","on" },
            {"ctl00$content$ColumnList$73","on" },
            {"ctl00$content$ColumnList$74","on" },
            {"ctl00$content$ColumnList$75","on" },
            {"ctl00$content$ColumnList$76","on" },
            {"ctl00$content$ColumnList$77","on" },
            {"ctl00$content$ColumnList$78","on" },
            {"ctl00$content$ColumnList$79","on" },
            {"ctl00$content$ColumnList$80","on" },
            {"ctl00$content$ColumnList$81","on" },
            {"ctl00$content$ColumnList$82","on" },
            {"ctl00$content$ColumnList$83","on" },
            {"ctl00$content$ColumnList$84","on" },
            {"ctl00$content$ColumnList$85","on" },
            {"ctl00$content$ColumnList$86","on" },
            {"ctl00$content$EquipmentSubCategoryList","0" },
            {"ctl00$content$EquipmentSubCategoryCascadingDropDown_ClientState","0::::::" },
            {"ctl00$content$EquipmentTypeList","0" },
            {"ctl00$content$EquipmentTypeCascadingDropDown_ClientState","0::::::" },
            {"ctl00$content$EquipmentMakerList","0" },
            {"ctl00$content$EquipmentMakerCascadingDropDown_ClientState","0::::::" },
            {"ctl00$content$EquipmentModelList","0" },
            {"ctl00$content$EquipmentModelCascadingDropDown_ClientState","0::::::" },
            {"ctl00$content$tb_serialn","" },
            {"ctl00$content$tb_serialn_TextBoxWatermarkExtender_ClientState","" },
            {"ctl00$content$tb_servicen","" },
            {"ctl00$content$tb_servicen_TextBoxWatermarkExtender_ClientState","" },
            {"ctl00$content$tb_inventoryn","" },
            {"ctl00$content$tb_inventoryn_TextBoxWatermarkExtender_ClientState","" },
            {"ctl00$content$tb_WindowsName","" },
            {"ctl00$content$tb_WindowsName_TextBoxWatermarkExtender_ClientState","" },
            {"ctl00$content$tb_id","" },
            {"ctl00$content$tb_id_TextBoxWatermarkExtender_ClientState","" },
            {"ctl00$content$tb_ProdDate_from","" },
            {"ctl00$content$tb_ProdDate_to","" },
            {"ctl00$content$tb_GarantDate_from","" },
            {"ctl00$content$tb_GarantDate_to","" },
            {"ctl00$content$ddl_STOBaseStatus","0" },
            {"ctl00$content$ddl_HasSTO","-1" },
            {"ctl00$content$ddl_SMS_Status","-1" },
            {"ctl00$content$ddl_HasChecked","-1" },
            {"ctl00$content$NewParamList","" },
            {"ctl00$content$NewParamValue","" },
            {"ctl00$content$btn_Excel","Выгрузить в формате Excel" },
            {"ctl00$content$PageSizeList","20" },
            {"hiddenInputToUpdateATBuffer_CommonToolkitScripts","1" }
        };
        /// <summary>
        /// Параметры выхода с сайта СТО
        /// </summary>
        public Dictionary<string, string> ListParametersExitAuthorization = new Dictionary<string, string>()
        {
            {"__EVENTTARGET", "ctl00$HeadLoginView$HeadLoginStatus$ctl00"},
            {"__EVENTARGUMENT", ""},
            { "Replace", "Replace" },
        };

    }
}
