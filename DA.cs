using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Desafio_Fagron {
    public static class DA {

        #region Create connection

        private static MySqlConnection connection;

        private static MySqlConnection createConnection() {
            connection = new MySqlConnection(@"server=localhost; database=fagronClients; user id=root; password='root';");
            connection.Open();
            return connection;
        }

        public static void closeConnection() {
            if (connection != null) {
                connection.Close();
                connection = null;
            }
        }

        #endregion

        #region Parameters going to the database

        private static MySqlParameterCollection parameterObj = new MySqlCommand().Parameters;

        public static void clearParameters() {
            parameterObj.Clear();
        }

        public static void addParameter(string parameterName, object objValue) {
            parameterObj.Add(new MySqlParameter(parameterName, objValue));
        }

        #endregion

        #region CRUD - Create, read, update and delete

        //Insert, Update e Delete
        public static object ExecutarManipulacao(CommandType objTipo, string strSql) {
            try {
                //SP = Stored Procedure (Procedimento Armazenado no MySQL)
                //strSql => é o comando SQL ou o nome da SP

                if (connection == null) //check if connection is empty
                    connection = createConnection(); //open connection if empty

                MySqlConnection connectionObj = connection;
                MySqlCommand commandObj = connectionObj.CreateCommand();

                //Inform if an SP or an SQL text will be executed
                commandObj.CommandType = objTipo;
                commandObj.CommandText = strSql;
                commandObj.CommandTimeout = 2000; //Seconds

                //Add parameters that will be stored at the database
                foreach (MySqlParameter objParameter in parameterObj)
                    commandObj.Parameters.Add(new MySqlParameter(objParameter.ParameterName, objParameter.Value));

                return commandObj.ExecuteScalar();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        //Read data from database
        public static DataTable executeRead(CommandType objType, string strSql) {
            try {
                if (connection == null) //check if connection is empty
                    connection = createConnection(); //open connection if empty

                MySqlConnection connectionObj = connection;
                MySqlCommand commandObj = connectionObj.CreateCommand();

                //Inform if an SP or an SQL text will be executed
                commandObj.CommandType = objType;
                commandObj.CommandText = strSql;
                commandObj.CommandTimeout = 2000; //Seconds

                foreach (MySqlParameter objParameter in parameterObj)
                    commandObj.Parameters.Add(new MySqlParameter(objParameter.ParameterName, objParameter.Value));

                MySqlDataAdapter adapterObj = new MySqlDataAdapter(commandObj);
                DataTable dataTableObj = new DataTable();

                adapterObj.Fill(dataTableObj);

                return dataTableObj;
            }
            catch (Exception errorObj) {
                throw new Exception(errorObj.Message);
            }
        }
        #endregion
    }
}