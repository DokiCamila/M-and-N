using Oracle.DataAccess.Client;
using sistemaCorporativo.UTIL.databaseAdress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistemaCorporativo.UTIL
{
    class searchByCep
    {
        private string _CEP;

        public string cep
        {
            get { return _CEP; }
            set { _CEP = value; }
        }

        public OracleDataReader ProcurarCEP(OracleConnection Oracon)
        {

            string strQuery;
            strQuery = "SELECT * FROM ENDERECO ";
            strQuery += " WHERE CEP ='" + _CEP + "'";

            OracleCommand cmdReader = new OracleCommand(strQuery, Oracon);
            OracleDataReader reader = cmdReader.ExecuteReader();

            return reader;
        }
    }

}
