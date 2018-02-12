using System;
using System.Data;

namespace TestIFNSTools.Detalizacia.WpfUserControl.Face.UL.SqlParamSelect
{
    public class Zapros : SqlZapros.SQLQBE
    {
        static readonly object[] SqlQueriesUl2012 =
        {
            SQLNameOrg,
            SQLNameFile,
            SQLSved2012
        };

        static readonly object[] SqlQueriesUl2013 =
        {
            SQLNameOrg,
            SQLNameFile,
            SQLSved2013
        };

        static readonly object[] SqlQueriesUl2014 =
        {
            SQLNameOrg,
            SQLNameFile,
            SQLSved2014
        };

        static readonly object[] SqlQueriesUl2015 =
        {
            SQLNameOrg,
            SQLNameFile,
            SQLSved2015
        };

        static readonly object[] SqlQueriesUl2016 =
        {
            SQLNameOrg,
            SQLNameFile,
            SQLSved2016
        };

        static readonly object[] SqlQueriesUl2017 =
        {
            SQLNameOrg,
            SQLNameFile,
            SQLSved2017
        };

        static readonly object[] SqlQueriesUl2018 =
        {
            SQLNameOrg,
            SQLNameFile,
            SQLSved2018
        };

        public DataSet ZaprosSql(string inn, string god, Detalizacia detal)
      {
          var q = new Qbe();
          var goding = Convert.ToInt32(god);
          switch (goding)
          {
              case 2012:
                  DataSet table2012 = q.Sqlul(inn, goding, SqlQueriesUl2012,detal);
                  return table2012;
              case 2013:
                  DataSet table2013 = q.Sqlul(inn, goding, SqlQueriesUl2013,detal);
                  return table2013;
              case 2014:
                  DataSet table2014 = q.Sqlul(inn, goding, SqlQueriesUl2014,detal);
                  return table2014;
              case 2015:
                  DataSet table2015 = q.Sqlul(inn, goding, SqlQueriesUl2015,detal);
                  return table2015;
              case 2016:
                  DataSet table2016 = q.Sqlul(inn, goding, SqlQueriesUl2016,detal);
                  return table2016;
              case 2017:
                  DataSet table2017 = q.Sqlul(inn, goding, SqlQueriesUl2017,detal);
                  return table2017;
                case 2018:
                  DataSet table2018 = q.Sqlul(inn, goding, SqlQueriesUl2018,detal);
                  return table2018;
          }
          var tablezero = new DataSet();
          return tablezero;
      }
    }
}
