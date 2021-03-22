using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Threading;

namespace aspNetCoreWeb.ThreadKnowledge
{
    /// <summary>
    /// 描述：多线程、并发相关内容测试
    /// </summary>

    [ApiController]
    [Route("[controller]/[action]")]
    public class ThreadKnowledgeController : ControllerBase
    {

        /// <summary>
        /// 异步编程：async、await配合使用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetJson()
        {
            var weatherForecast = new WeatherForecast() { Date = DateTime.Now, Summary = "test", TemperatureC = 12 };
            var retJson = JsonSerializer.Serialize(weatherForecast);


            //Task<TResult> Run<TResult>(Func<TResult> function);
            //为独立开辟线程

            // 1、跳过等待
            var retJson1 = Task.Run(() =>
            {
                Thread.Sleep(10000);
                return retJson;
            });

            // 2、跳过等待（ConfigureAwait（true）时为等待线程）
            var retJson2 = Task.Run(() =>
            {
                Thread.Sleep(10000);
                return retJson;
            }).ConfigureAwait(false);

            // 3、继续等待
            var retJson3 = await Task.Run(() =>
             {
                 Thread.Sleep(10000);
                 return retJson;
             }).ConfigureAwait(false);

            // 4、继续等待
            var retJson4 = await Task.Run(() =>
            {
                Thread.Sleep(10000);
                return retJson;
            });

            var retJson21 = await retJson2;

            var newWeatherForecast = JsonSerializer.Deserialize<WeatherForecast>(retJson);
            return await retJson1;
        }

        /// <summary>
        /// 异步编程二：基于任务的异步编程
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<string> GetJsonTask()
        {

            var weatherForecast = new WeatherForecast() { Date = DateTime.Now, Summary = "test", TemperatureC = 12 };
            var retJson = JsonSerializer.Serialize(weatherForecast);
            var retJson1 = Task.Run(() =>
            {
                Thread.Sleep(10000);
                return retJson;
            });

            return retJson1;
        }


        /// <summary>
        /// 多线程：基于thread
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetJsonThread()
        {
            var weatherForecast = new WeatherForecast() { Date = DateTime.Now, Summary = "test", TemperatureC = 12 };
            var retJson = string.Empty;
            Thread thread = new Thread(new ThreadStart(() =>
            {
                retJson = InstanceMethod("test");
            }));

            thread.Start();
            thread.Join();
            return retJson;
        }

        /// <summary>
        /// 描述：线程调用方法
        /// 姓名：mipan
        /// 日期：2021年3月21日
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string InstanceMethod(string input)
        {
            Console.WriteLine(
                "ServerClass.InstanceMethod is running on another thread.");

            // Pause for a moment to provide a delay to make
            // threads more apparent.
            Thread.Sleep(3000);
            Console.WriteLine(
                "The instance method called by the worker thread has ended.");
            return input;
        }

    }
}
