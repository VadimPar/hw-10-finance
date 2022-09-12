using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class Rate
    {
    public int Cur_ID { get; set; } // внутренний код
    public DateTime Date { get; set; } // дата, на которую запрашивается курс
    public string Cur_Abbreviation { get; set; } // буквенный код
    public int Cur_Scale { get; set; } // количество единиц иностранной валюты
    public string Cur_Name { get; set; } // наименование валюты на русском языке во множественном, либо в единственном числе, в зависимости от количества единиц
    public double Cur_OfficialRate { get; set; } // курс


    public void Show()
    {
        Console.WriteLine($"Date: {Date}  Currency: {Cur_OfficialRate}");
    }
}

