using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;

namespace WebapiTest.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        [Route("api/values/GetLogs")]
        [HttpGet]
        public IEnumerable<string> GetLogs(string id,string sign,string timestamp)
        {

            bool checkTimeStamp = CheckTimeStamp(timestamp);
            if (!checkTimeStamp)
            {
                return new string[] { "时间戳不匹配！！！" };
            }

            var signStr = HexMd5("id=" + id);
            if (sign != signStr)
            {
                return new string[] { "请求参数不匹配！！！" };
            }

            return new string[] { "测试log0001", "测试log0002","测试log0003" };
        }

        private bool CheckTimeStamp(string timestamp)
        {
            long time = long.Parse(timestamp);
            DateTime start = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime callTime = start.AddMilliseconds(time);
            TimeSpan span = DateTime.Now - callTime;
            if (span.TotalSeconds > 5)
            {
                return false;
            }
            return true;
        }
        private string HexMd5(string sourceStr)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(sourceStr));
            string encrytionStr = ByteToHexStr(bytes);
            return encrytionStr;
        }
        public static string ByteToHexStr(byte[] bytes)
        {
            StringBuilder strb = new StringBuilder();
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    strb.Append(bytes[i].ToString("x2"));
                }
            }
            return strb.ToString();
        }


        // GET api/values/5
        [Route("api/values/Login")]
        [HttpGet]
        public SessionInfo Login(string code)
        {

            SessionInfo sessionInfo = null;
            using (HttpClient client=new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                string url = string.Format(@"https://api.weixin.qq.com/sns/jscode2session?appid=wxa0ca9c4e9af2db29&secret=277f04208299cab17eae4e3d00e688df&js_code={0}&grant_type=authorization_code",code);
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var strJson = response.Content.ReadAsStringAsync().Result;
                    sessionInfo=Newtonsoft.Json.JsonConvert.DeserializeObject<SessionInfo>(strJson);
                }
 
            }
            return sessionInfo;
           // return "test001";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
    public class SessionInfo
    {
        public string openid { get; set; }
        public string session_key { get; set; }

    }
}
