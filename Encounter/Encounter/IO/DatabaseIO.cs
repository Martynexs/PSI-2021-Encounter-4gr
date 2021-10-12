using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.Windows;

namespace Encounter.IO
{
    class DatabaseIO
    {
        private string _connectionPath = @"Database.db";
        private SQLiteConnection _connection;

        public DatabaseIO()
        {
            _connection = new SQLiteConnection("Data source=" + _connectionPath + ";version=3;");
        }

        private void OpenConnection()
        {
            try
            {
                _connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CloseConnection()
        {
            try
            {
                _connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public DataSet GetDataSet(string query, string table)
        {
            try
            {
                OpenConnection();
                var dataSet = new DataSet();
                var adapter = new SQLiteDataAdapter(query, _connection);
                adapter.Fill(dataSet, table);
                return dataSet;
            }
            catch
            {
                throw new Exception();
            }
            finally
            {
                CloseConnection();
            }
        }

        public void ExecuteNonQuery(string query)
        {
            try
            {
                OpenConnection();
                var command = new SQLiteCommand(query, _connection);
                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }



    }
}
