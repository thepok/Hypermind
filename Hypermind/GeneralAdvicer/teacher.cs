using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.Json.Nodes;
using OpenAI.GPT3.ObjectModels.ResponseModels;

namespace HypermindLib
{
    public class Teacher
    {
        public List<Message> messages = new List<Message>();
        
        public string BotName = "Teacher";

        public string UserName = "Student";

        public ModelResult GetRespons()
        {
            var promp = GetPromp();
            ModelWithPromp model = new ModelWithPromp(new OAI(maxNewTokens: 500), new PrompTemplate(promp));

            var answerJSON = model.Process().Result[0].Value;
            ModelResult result = JsonConvert.DeserializeObject<ModelResult>(answerJSON);
            AddMessage(BotName, result.AnswerToRender);
            return result;
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

        public class ModelResult
        {
            public string AnswerToRender { get; set; }
            public string AnswerToRead { get; set; }
            public string HtmlToDraw { get; set; }
            public string[] FollowUpQuestions { get; set; }

            public string LanguageOfAnswer { get; set; }
        }

        public string GetPromp()
        {
            var start = @"A universal genius teacher, who can teach everything, is asked a question by a student. 
Be carful to not invent URLs where you are not sure they realy exist. Rather create a google search link. And allways open links in a new tab.
Remember to include examples in your answer.  Rather try to answer your self, than to send the student to another Website.
The teacher responds optimally by presenting the answer in multiple ways. The answer will be read aloud and rendered as HTML. Thus, the teacher must create an answer suitable for reading aloud as well as an answer that can be displayed.
Use the HTML Tag <Math> for formulas. 
This answer should be packaged into a JSON string with the following form:

{
  ""answerToRender"": [answer containing HTML for nice formating],
  ""answerToRead"": [Speach synthesizer will read this],
  ""LanguageOfAnswer"": [Language the Speech synthesizer should use{de, en,  fr, it, es, pt, ru, ja, ko, zh, ...}]
  ""FollowUpQuestions"": [List of possible follow up questions a student could have]
}

Previous conversation:
None

Start by greeting the student. Suggest 5 Followup Questions covering typical school and univerity topics in the FollowUpQuestions field.
Answer
JSON:" + Environment.NewLine;
            var log = GetLog();
            var YouPart = BotName + ":";

            var final = start + log + YouPart;
            return final;
        }

    }
}
