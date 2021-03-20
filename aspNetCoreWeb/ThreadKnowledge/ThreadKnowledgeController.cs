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
        /// async、await配合使用
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
    }
}
