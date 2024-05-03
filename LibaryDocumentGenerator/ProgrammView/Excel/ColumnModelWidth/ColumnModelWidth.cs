using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;

namespace LibaryDocumentGenerator.ProgrammView.Excel.ColumnModelWidth
{
   public class ColumnModelWidth
   {
       /// <summary>
       /// Формула расчета Ширины ячейки 
       /// </summary>
       /// <param name="pixel">Пиксель</param>
       /// <returns></returns>
       public static DoubleValue FormulaWidth(int pixel)
       {
           return 0.14294118 * pixel;
       }

        /// <summary>
        /// Колонки отчета по табельным номерам 42 колонки размеры
        /// </summary>
        public List<DoubleValue> ListColumnWidthReportCard = new List<DoubleValue>()
        {
            FormulaWidth(22),
            FormulaWidth(94),
            FormulaWidth(55),
            FormulaWidth(18),
            FormulaWidth(110),
            FormulaWidth(9),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(32),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(22),
            FormulaWidth(33),
            FormulaWidth(34),
            FormulaWidth(21),
            FormulaWidth(21),
        };
        /// <summary>
        /// Модель для отчета какие поля сравниваем
        /// </summary>
        public List<ModelReportExcel> ListColumnWidthReportComparableUser = new List<ModelReportExcel>()
        {
            new ModelReportExcel() {IsComparable = false, NameModel = "NameSystem", Width = 30.00D},
            new ModelReportExcel() {IsComparable = false, NameModel = "IdUser", Width = 16.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "FullName", Width = 37.00D},
            new ModelReportExcel() {IsComparable = false, NameModel = "PersonnelNumber", Width = 17.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "Department", Width = 48.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "Room", Width = 9.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "NamePosition", Width = 64.00D},
            new ModelReportExcel() {IsComparable = false, NameModel = "Ranks", Width = 77.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "InternalPhone", Width = 19.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "TelephonUndeground", Width = 18.00D},
            new ModelReportExcel() {IsComparable = false, NameModel = "Mail", Width = 68.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "Name", Width = 12.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "Surname", Width = 15.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "Patronymic", Width = 16.00D},
            new ModelReportExcel() {IsComparable = false, NameModel = "CountGrooup", Width = 20.00D},
        };
        /// <summary>
        /// Модель для отчета сравнения техники
        /// </summary>
        public List<ModelReportExcel> ListColumnWidthReportComparableTech = new List<ModelReportExcel>()
        {
            new ModelReportExcel() {IsComparable = false, NameModel = "NameSystem", Width = 30.00D},
            new ModelReportExcel() {IsComparable = false, NameModel = "RowNumber", Width = 16.00D},
            new ModelReportExcel() {IsComparable = false, NameModel = "EquipmentType", Width =59.00D },
            new ModelReportExcel() {IsComparable = false, NameModel = "ProducerName", Width = 28.00D},
            new ModelReportExcel() {IsComparable = false, NameModel = "ModelName", Width = 37.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "ComputerName", Width = 26.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "IpAdress", Width = 17.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "SerialNumber", Width = 27.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "InventoryNumber", Width = 20.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "ServiceNumber", Width = 37.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "AddressOrganization", Width = 83.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "Bulding", Width = 7.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "Room", Width = 22.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "Division", Width = 45.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "FullName", Width = 46.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "Info", Width = 48.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "OperationSystem", Width = 32.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "CpuType", Width = 51.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "CpuMHz", Width = 24.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "CoreCount", Width = 16.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "CpuCount", Width = 24.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "HddType", Width = 64.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "HddCount", Width = 17.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "HddSize", Width = 22.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "NetworkAdapter", Width = 90.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "NetworkAdapterSpeed", Width = 23.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "Mac", Width = 37.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "RamType", Width = 20.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "MemorySize", Width = 15.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "DiskSize", Width = 33.00D},
            new ModelReportExcel() {IsComparable = true, NameModel = "CdOrDvd", Width = 33.00D},
            new ModelReportExcel() {IsComparable = false, NameModel = "CountGroup", Width = 20.00D}
        };
   }

   public class ModelReportExcel
   {
       public string NameModel { get; set; }
       public DoubleValue Width { get; set; }
       public bool IsComparable { get; set; }
    }
}
