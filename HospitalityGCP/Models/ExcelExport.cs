using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HospitalityGCP.Models
{
    public class ExcelExport
    {
        public static byte[] BuildExportPackage<T>(List<T> aList)
        {
            //Get an array of the column types in the data
            PropertyInfo[] propList = aList.FirstOrDefault().GetType().GetProperties();

            using (var package = new ExcelPackage())
            {
                // add a new worksheet to the empty workbook
                var ws = package.Workbook.Worksheets.Add("Hospitality"); //Worksheet name                

                //call the method that fills the worksheet with the data
                ws.Cells["A1"].LoadFromCollection(aList, true);

                foreach (var col in propList)
                {
                    //find the index of the col in the array
                    int idx = Array.FindIndex(propList, row => row == col) + 1;

                    //get the col type. different method is it's nullable
                    Type theType;
                    if (col.PropertyType.IsGenericType && col.PropertyType.GetGenericTypeDefinition() == typeof(System.Nullable<>))
                    {
                        theType = System.Nullable.GetUnderlyingType(col.PropertyType);
                    }
                    else
                    {
                        theType = col.PropertyType;
                    }

                    //and then the typecode as can't switch on just type
                    TypeCode typeCode = Type.GetTypeCode(theType);

                    //set width of all columns to 20
                    ws.Column(idx).Width = 20;

                    //switch on typecode to set column stylings on certain data types. Feel free to add to.
                    switch (typeCode)
                    {
                        case TypeCode.DateTime:
                            ws.Column(idx).Style.Numberformat.Format = "dd-mm-yyyy";
                            ws.Column(idx).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            break;
                        case TypeCode.String:
                            ws.Column(idx).Style.WrapText = true;                            
                            break;
                        case TypeCode.Decimal:
                            ws.Column(idx).Style.Numberformat.Format = "£#,##0.00";
                            ws.Column(idx).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            break;
                        default: //Number
                            ws.Column(idx).Style.Numberformat.Format = "#,##0";
                            ws.Column(idx).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            break;
                    }                    
                }

                //set header range and stylings.
                ExcelRange headerRange = ws.Cells[string.Format("a1:{0}1", IndexToAlpha(propList.Count()))];
                headerRange.Style.Font.Bold = true;
                headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                headerRange.Style.WrapText = true;
                
                return package.GetAsByteArray();
            }
        }

        public static string IndexToAlpha(int index)
        {
            // A to XFD
            if (index <= 0 || index > 16384)
                throw new ArgumentOutOfRangeException("index must be between 1 and 16384 (inclusive)");

            string result = string.Empty;
            while (index > 0)
            {
                int i = index % 26;
                index /= 26;
                result = (char)((int)'A' + (i - 1)) + result;
            }

            return result;
        }
    }
}
