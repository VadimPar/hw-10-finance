using Microsoft.VisualBasic;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using static System.Net.WebRequestMethods;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.IO;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using File = System.IO.File;

internal class Program
{
    static readonly HttpClient client = new();
    private static async Task Main()
    {
        int i = 0;
        DateTime start = new DateTime(2022, 01, 01);
        DateTime end = DateTime.Now;
        int arrSize = (end - start).Days;
        Rate[] rate = new Rate[arrSize+1]; 
        string pathToData = "D:/rates.json";
        try
        {
            for (DateTime dateStart = new(2022, 01, 01); dateStart < DateTime.Now; dateStart = dateStart.AddDays(1))
            {
                var response = await client.GetAsync(
            $"http://www.nbrb.by/api/exrates/rates/usd?parammode=2&ondate={dateStart:yyyy-MM-dd}");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
                using (StreamWriter writer = new StreamWriter(pathToData, true))
                {
                    await writer.WriteAsync(responseBody);
                    rate[i] = JsonConvert.DeserializeObject<Rate>(responseBody);
                    i++;
                }
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("\nВозник Exception!");
            Console.WriteLine("Message :{0} ", e.Message);
        }

        using (StreamReader fs = new StreamReader(pathToData))
        {

            Console.WriteLine(await fs.ReadToEndAsync());
        }
       
        Console.WriteLine("Best result");
        double  maxRate = rate[0].Cur_OfficialRate, money = 10000;
        int num=0;
        for(int j=0;j<rate.Length; j++)
        {
            if (rate[j].Cur_OfficialRate > maxRate)
            {
                maxRate = rate[j].Cur_OfficialRate;
                num = j;
            }
        }
        money = rate[num].Cur_OfficialRate * money;
        Console.WriteLine($"{rate[num].Date} sold 10000$ and has {money} rubles. Currency: {rate[num].Cur_OfficialRate}");
        
        double minRate = rate[num].Cur_OfficialRate;
        for (int j = num; j < rate.Length; j++)
        {
            if (rate[j].Cur_OfficialRate < minRate)
            {
                minRate = rate[j].Cur_OfficialRate;
                num = j;
            }
        }
        money = money / rate[num].Cur_OfficialRate;
        Console.WriteLine($"{rate[num].Date} bought {money}$.Currency: {rate[num].Cur_OfficialRate}");
       
        maxRate= rate[num].Cur_OfficialRate;
        for (int j = num; j < rate.Length; j++)
        {
            if (rate[j].Cur_OfficialRate > maxRate)
            {
                maxRate = rate[j].Cur_OfficialRate;
                num = j;
            }
        }
        money = rate[num].Cur_OfficialRate * money;
        Console.WriteLine($"{rate[num].Date} sold $ and has {money} rubles. Currency: {rate[num].Cur_OfficialRate}");
       
        minRate= rate[num].Cur_OfficialRate;
        for (int j = num; j < rate.Length; j++)
        {
            if (rate[j].Cur_OfficialRate < minRate)
            {
                minRate = rate[j].Cur_OfficialRate;
                num = j;
            }
        }
        money = money / rate[num].Cur_OfficialRate;
        Console.WriteLine($"{rate[num].Date} bought {money}$.Currency: {rate[num].Cur_OfficialRate}");



        Console.WriteLine("Worth result");
        double minRate2 = rate[0].Cur_OfficialRate, money2 = 10000;
        int num2 = 0;
        for (int j = 0; j < rate.Length; j++)
        {
            if (rate[j].Cur_OfficialRate < minRate2)
            {
                minRate2 = rate[j].Cur_OfficialRate;
                num2 = j;
            }
        }
        money2 = rate[num2].Cur_OfficialRate * money2;
        Console.WriteLine($"{rate[num2].Date} sold 10000$ and has {money2} rubles. Currency: {rate[num2].Cur_OfficialRate}");

        double maxRate2 = rate[num2].Cur_OfficialRate;
        for (int j = num2; j < rate.Length; j++)
        {
            if (rate[j].Cur_OfficialRate > maxRate2)
            {
                maxRate2 = rate[j].Cur_OfficialRate;
                num2 = j;
            }
        }
        money2 = money2 / rate[num2].Cur_OfficialRate;
        Console.WriteLine($"{rate[num2].Date} bought {money2}$.Currency: {rate[num2].Cur_OfficialRate}");

        minRate2 = rate[num2].Cur_OfficialRate;
        for (int j = num2; j < rate.Length; j++)
        {
            if (rate[j].Cur_OfficialRate < minRate2)
            {
                minRate2 = rate[j].Cur_OfficialRate;
                num2 = j;
            }
        }
        money2 = rate[num2].Cur_OfficialRate * money2;
        Console.WriteLine($"{rate[num2].Date} sold $ and has {money2} rubles. Currency: {rate[num2].Cur_OfficialRate}");

        maxRate2 = rate[num2].Cur_OfficialRate;
        for (int j = num2; j < rate.Length; j++)
        {
            if (rate[j].Cur_OfficialRate > maxRate2)
            {
                maxRate2 = rate[j].Cur_OfficialRate;
                num2 = j;
            }
        }
        money2 = money2 / rate[num2].Cur_OfficialRate;
        Console.WriteLine($"{rate[num2].Date} bought {money2}$.Currency: {rate[num2].Cur_OfficialRate}");
    }
}
