using System;
using System.Data;
using AdoNetLib;

namespace AdoNetModuleConsole
{
    public class Manager
    {
        private MainConnector connector;
        private DbExecutor dbExecuter;
        private Table userTable;

        public Manager()
        {
            connector = new MainConnector();

            userTable = new Table()
            {
                Name = "NetworkUser",
                ImportantField = "Login"
            };
            
            userTable.Fields.Add("Id");
            userTable.Fields.Add("Login");
            userTable.Fields.Add("Name");
        }

        public void Connect()
        {
            var result = connector.ConnectAsync();

            if (result.Result)
            {
                Console.WriteLine("Подключение успешно!");

                dbExecuter = new DbExecutor(connector);
            }
            else
            {
                Console.WriteLine("Ошибка подключения!");
            }
        }

        public void Disconnect()
        {
            Console.WriteLine("Отключаем БД!");
            connector.DisconnectAsync();
        }

        public void ShowData()
        {
            var tableName = "NetworkUser";

            Console.WriteLine("Получаем данные таблицы " + tableName);

            var data = dbExecuter.SelectAll(tableName);

            Console.WriteLine("Количество строк в " + tableName + ": " + data.Rows.Count);

            Console.WriteLine();

            foreach (DataColumn column in data.Columns)
            {
                Console.Write($"{column.ColumnName}\t");
            }
            
            Console.WriteLine();

            foreach (DataRow row in data.Rows)
            {
                var cells = row.ItemArray;

                foreach (var cell in cells)
                {
                    Console.Write($"{cell}\t");
                }
                
                Console.WriteLine();
            }
        }

        public int DeleteUserByLogin(string value)
        {
            return dbExecuter.DeleteByColumn(userTable.Name, userTable.ImportantField, value);
        }
    }
}