using System;
using System.IO;
using Newtonsoft.Json;

namespace Hooks
{
    class ConfigManager
    {
        private const string KeyWord = @"IDontKnowWerDie";


        public void Save(Configuration conf)
        {
            using (var writer = new StreamWriter(@"hooks.config", false))
            {
                writer.Write(Code(JsonConvert.SerializeObject(conf)));
            }
        }

        public Configuration Read()
        {
            try
            {
                using (var reader = new StreamReader(@"hooks.config"))
                {
                    var config = JsonConvert.DeserializeObject<Configuration>(Code(reader.ReadToEnd()));
                    return config ?? DefaultConfig();
                }
            }
            catch (Exception)
            {
                return DefaultConfig();
            }
        }

        private Configuration DefaultConfig()
        {
            return new Configuration()
            {
                FileSize = 300
            };
        }

        private string Code(string input)
        {
            var output = string.Empty;
            for (var i = 0; i < input.Length; i++)
            {
                output += Convert.ToChar(input[i] ^ KeyWord[i % KeyWord.Length]);
            }
            return output;
        }
    }
}
