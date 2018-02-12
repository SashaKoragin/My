using System;
using System.Collections;
using System.Data;
using System.IO;


namespace CovertTXTtoExcel.ConvertTXTtoEXCEL.ParseTXT
{
    [Obsolete("Более не Актуально TxtString")]
   internal class TxtString
    {
       [Obsolete("Более не Актуально", true)]
       internal DataTable Txt(string path)
       {
           var tb = new DataTable();
           using (var st = new StreamReader(path))
           {
               while (!st.EndOfStream)
               {
                   string stroka = st.ReadLine();
                   if (stroka != null)
                   {
                       var mass =new ArrayList(stroka.Split('@'));
                       if (tb.Columns.Count > 0)
                       {
                           tb.Rows.Add(mass.ToArray());
                       }
                       else
                       {
                           for (int j = 0; j <= mass.Count; j++)
                           {
                               tb.Columns.Add();
                           }
                           tb.Rows.Add(mass.ToArray());
                       }
                   }
               }
               st.Close();
               st.Dispose();
               return tb;
           }
       }
    }
}
