using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ET;


namespace CapaDb
{
    public class Connection
    {
        private SqlConnection connect = new SqlConnection();

        private void Open()
        {
            string cs = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString;
            connect.ConnectionString = cs;
            connect.Open();
        }
        private void Close()
        {
            connect.Close();

        }

        public DataTable Read(string procedure, List<SqlParameter> parameters = null)
        {
            Open();

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = NewSqlCommandText(procedure, parameters);
            adapter.Fill(table);

            Close();
            return table;
        }
        public DataTable ReadSp(string procedure, List<SqlParameter> parameters = null)
        {
            Open();

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();

            adapter.SelectCommand = NewSqlCommand(procedure, parameters);
            adapter.Fill(table);

            Close();
            return table;
        }
        

        public SqlParameter NewSqlParameterString(string nameParam, string value)
        {
            SqlParameter param = new SqlParameter();

            param.ParameterName = nameParam;
            param.Value = value;
            param.DbType = DbType.String;

            return param;


        }

        public SqlParameter NewSqlParameterInt(string nameParam, int value)
        {
            SqlParameter param = new SqlParameter();

            param.ParameterName = nameParam;
            param.Value = value;
            param.DbType = DbType.Int32;

            return param;


        }

        public SqlParameter NewSqlParameterDate(string nameParam, DateTime value)
        {
            SqlParameter param = new SqlParameter();

            param.ParameterName = nameParam;
            param.Value = value;
            param.DbType = DbType.DateTime;

            return param;


        }

        private SqlCommand NewSqlCommand(string procedure, List<SqlParameter> Lparam = null)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connect;
            command.CommandText = procedure;
            command.CommandType = CommandType.StoredProcedure;

            if (Lparam != null && Lparam.Count > 0)
            {
                command.Parameters.AddRange(Lparam.ToArray());
            }
            return command;
        }
        private SqlCommand NewSqlCommandText(string procedure, List<SqlParameter> Lparam = null)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connect;
            command.CommandText = procedure;
            command.CommandType = CommandType.Text;

            if (Lparam != null && Lparam.Count > 0)
            {
                command.Parameters.AddRange(Lparam.ToArray());
            }
            return command;
        }


        public SqlDataReader ExecuteRead(string procedure, List<SqlParameter> parameters)
        {

            Close();
            SqlCommand command = NewSqlCommand(procedure, parameters);
            Open();
            SqlDataReader reader = command.ExecuteReader();
            return reader;


        }
        public SqlDataReader ExecuteReadT(string procedure, List<SqlParameter> parameters)
        {
            Close();
            SqlCommand command = NewSqlCommandText(procedure, parameters);
            Open();
            SqlDataReader reader = command.ExecuteReader();

            return reader;


        }

    }
}
