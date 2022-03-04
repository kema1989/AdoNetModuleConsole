using System;
using System.Data;
using AdoNetLib;

namespace AdoNetModuleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var connector = new MainConnector();

            var result = connector.ConnectAsync();

            var data = new DataTable();
            if (result.Result)
            {
                Console.WriteLine("Подключение успешно!");

                var db = new DbExecutor(connector);

                var tableName = "NetworkUser";

                Console.WriteLine("Получаем данные таблицы " + tableName);

                data = db.SelectAll(tableName);

                Console.WriteLine("Количество строк в " + tableName + ":" + data.Rows.Count);
                Console.WriteLine("Отключаем БД");
                connector.DisconnectAsync();
                Console.WriteLine("Количество строк в " + tableName + ":" + data.Rows.Count);
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Ошибка подключения!");
            }
        }
    }
}