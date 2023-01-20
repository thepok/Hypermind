using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HypermindLib
{
    public class ChatGPTwithTools
    {
        public List<Message> messages = new List<Message>();
        
        public string BotName = "Advicer";

        public string UserName = "User";

        public string GetRespons()
        {
            var promp = GetPromp();
            ModelWithPromp model = new ModelWithPromp(new OAI(maxNewTokens: 500), new PrompTemplate(promp));
            var answer = model.Process().Result[0].Value;
            AddMessage(BotName, answer);
            return answer;
        }

        public void AddMessage(string senderName, string text)
        {
            messages.Add(new Message { sender = senderName, text = text, time = DateTime.Now });
        }


        private string GetLog()
        {
            var sb = new StringBuilder();

            foreach (var message in messages)
            {
                sb.AppendLine(message.ToLogline());
            }
            return sb.ToString();
        }
        public class Message
        {
            public string sender;
            public string text;
            public DateTime time;

            public string ToLogline()
            {
                return $"{sender}: {text}";
            }
        }

        public string GetPromp()
        {
            var start = @"You are a nice Advicer helping the User. Your answers are rendered as HTML. So if you want to show a Table or provide a link, simply do so in HTML. Start by greeting the User." + Environment.NewLine;
            var log = GetLog();
            var YouPart = BotName + ":";

            return start + log + YouPart;
        }

    }
}
