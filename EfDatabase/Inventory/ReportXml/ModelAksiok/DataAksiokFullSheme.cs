
namespace EfDatabase.Inventory.ReportXml.ModelAksiok
{
    /// <summary>
    /// Общая схема данных АКСИОК ЭПО
    /// </summary>
    /// <typeparam name="T">Массив классов который прицепляется</typeparam>
    public class DataAksiokFullSchemes<T>
    {
        public int? TotalCount { get; set; }
        public string Message { get; set; }

        public bool? Success { get; set; }

        public T Data { get; set; }
    }
}
