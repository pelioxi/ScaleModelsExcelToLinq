using LinqToExcel.Attributes;

namespace ScaleModelsExcelToLinq.Models
{
    public class ScaleModels
    {
        [ExcelColumn("Collection")]
        public string Collection { get; set; }

        [ExcelColumn("Brand")]
        public string Brand { get; set; }

        [ExcelColumn("Model")]
        public string Model { get; set; }

        [ExcelColumn("CarYear")]
        public string CarYear { get; set; }

        [ExcelColumn("Maker")]
        public string Maker { get; set; }

        [ExcelColumn("Scale18")]
        public string Scale18 { get; set; }

        [ExcelColumn("Status")]
        public string Status { get; set; }

        [ExcelColumn("Scale")]
        public string Scale { get; set; }

        [ExcelColumn("PartNumber")]
        public string PartNumber { get; set; }

        [ExcelColumn("CarNumber")]
        public string CarNumber { get; set; }

        [ExcelColumn("ColourSponsor")]
        public string ColourSponsor { get; set; }

        [ExcelColumn("Driver")]
        public string Driver { get; set; }

        [ExcelColumn("Details")]
        public string Details { get; set; }

        [ExcelColumn("ModelDate")]
        public string ModelDate { get; set; }

        [ExcelColumn("Serial")]
        public string Serial { get; set; }

        [ExcelColumn("Ledition")]
        public string Ledition { get; set; }

        [ExcelColumn("Comments")]
        public string Comments { get; set; }
    }
}