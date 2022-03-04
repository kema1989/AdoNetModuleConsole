using System;
using System.Collections.Generic;
using System.Data;
using AdoNetLib;
using Microsoft.Data.SqlClient;

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

                var reader = db.SelectAllCommandReader(tableName);

                var columnList = new List<string>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var name = reader.GetName(i);
                    columnList.Add(name);
                }

                for (int i = 0; i < columnList.Count; i++)
                {
                    Console.Write($"{columnList[i]}\t");
                }

                Console.WriteLine();

                while (reader.Read())
                {
                    for (int i = 0; i < columnList.Count; i++)
                    {
                        var value = reader[columnList[i]];
                        Console.Write($"{value}\t");
                    }
                    Console.WriteLine();
                }
                // Console.WriteLine("Получаем данные таблицы " + tableName);
                //
                // data = db.SelectAll(tableName);
                //
                // Console.WriteLine("Количество строк в " + tableName + ":" + data.Rows.Count);
                // Console.WriteLine("Отключаем БД");
                // connector.DisconnectAsync();
                // Console.WriteLine("Количество строк в " + tableName + ":" + data.Rows.Count);
                // Console.ReadKey();


            }
            else
            {
                Console.WriteLine("Ошибка подключения!");
            }
        }
    }
}