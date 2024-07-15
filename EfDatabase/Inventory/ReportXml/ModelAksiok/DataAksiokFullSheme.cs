
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

    public class DataAksiokAddSchemes<T>
    {

        public bool? Success { get; set; }
        public bool? IsMessageAggregatorResponse { get; set; }

        public Errors[] Errors { get; set; }

        public Warnings[] Warnings { get; set; }

        public Notifications[] Notifications { get; set; }

        public T[] Data { get; set; }

    }

    public class Errors
    {
        public string Name { get; set; }
    }
    public class Warnings
    {
        public string Name { get; set; }
    }
    public class Notifications
    {
        public string Name { get; set; }
    }
}
