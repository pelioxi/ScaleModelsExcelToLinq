﻿using LinqToExcel;

namespace ScaleModelsExcelToLinq.Models
{
    public class ConnexionExcel
    {
        public string _pathExcelFile;
        public ExcelQueryFactory _urlConnexion;

        public ConnexionExcel(string path)
        {
            this._pathExcelFile = path;
            this._urlConnexion = new ExcelQueryFactory(_pathExcelFile);
        }

        public string PathExcelFile
        {
            get { return _pathExcelFile; }
        }

        public ExcelQueryFactory UrlConnexion
        {
            get { return _urlConnexion; }
        }
    }
}