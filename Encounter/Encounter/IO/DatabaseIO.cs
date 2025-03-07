﻿using System;
using System.Data.SQLite;
using System.Data;
using System.Windows;

namespace Encounter.IO
{
    public static class DatabaseIO
    {
        private static string _connectionPath = @"Database.db";
        private static SQLiteConnection _connection = new SQLiteConnection("Data source=" + _connectionPath + ";version=3;");

        public static void OpenConnection()
        {
            try
            {
                _connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw new Exception();
            }
        }

        public static void CloseConnection()
        {
            try
            {
                _connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw new Exception();
            }
        }

        public static DataSet GetDataSet(string query, string table)
        {
            try
            {
                var dataSet = new DataSet();
                var adapter = new SQLiteDataAdapter(query, _connection);
                adapter.Fill(dataSet, table);
                return dataSet;
            }
            catch
            {
                throw new Exception();
            }
        }

        public static SQLiteDataReader GetDataReader(string query)
        {
            try
            {
                var cmd = new SQLiteCommand(query, _connection);
                return cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw new Exception();
            }
        }

        public static void ExecuteNonQuery(string query)
        {
            try
            {
                var pragma = new SQLiteCommand("PRAGMA foreign_keys = true;", _connection);
                pragma.ExecuteNonQuery();

                var command = new SQLiteCommand(query, _connection);
                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw new Exception();
            }
        }
    }
}
