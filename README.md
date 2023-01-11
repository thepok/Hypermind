# Hypermind
Welcome to Hypermind the .NET way of building minds.

Connect foundation models to enable emergent capabilities - with ease

## Community 

[![](https://dcbadge.vercel.app/api/server/6teaRCtD?style=flat)](https://discord.gg/6teaRCtD)
##  Installation: 
Add reference to HypermindLib and OpenAILib
 
Example Code:

```csharp
using HypermindLib;
using OpenAILib;


var answerer = new AnswerQuestionWithText(new OpenAI());
var result = answerer.Process(("text", "the apple was red and the car yellow"), ("question", "what color was the apple?"));
Console.WriteLine(result.Result); //->Red


var summerizer = new SimpleSummerizer(new OpenAI());
var erg = summerizer.Process(("text", "Russische Kräfte machen nach britischer Einschätzung leichte Fortschritte bei den Kämpfen um die ostukrainische Stadt Bachmut. Reguläre Truppen und Einheiten der Söldnergruppe Wagner hätten in den vergangenen vier Tagen taktische Vorstöße in die zehn Kilometer nördlich gelegene Kleinstadt Soledar gemacht und kontrollierten wahrscheinlich den größten Teil des Orts, teilt das Verteidigungsministerium in London mit. Bachmut bleibe das vorrangige Ziel der russischen Offensive. Der Vorstoß nach Soledar solle die Stadt von Norden her einschließen und ukrainische Kommunikationswege unterbrechen. Die Kämpfe konzentrierten sich auf Zugänge zu stillgelegten Salzminenstollen, die unter dem Gebiet verlaufen und insgesamt rund 200 Kilometer lang seien. Trotz des erhöhten Drucks auf Bachmut sei es unwahrscheinlich, dass Russland die Stadt bald einnimmt, da die ukrainischen Streitkräfte stabile Verteidigungsstellungen aufgebaut hätten und auch die Versorgungswege weiter kontrollierten.\r\n\r\n"));
Console.WriteLine(erg.Result);

```

See the SimpleSummerizer as inspiration for new chains. Its simple, like lego.

```csharp
    public class SimpleSummerizer:Chain
    {
        PrompTemplate SummerizerPromp = new PrompTemplate(
@"""Summerize the following text between >>> and <<< 

>>>
{text}
<<<

Summery:"""
        );

        ModelWithPromp Sumerizer;
        public SimpleSummerizer(LLM llm)
        {
            Sumerizer = new ModelWithPromp(llm, SummerizerPromp);
        }

        public override ChainOutput Process(ChainInput input)
        {
            return Sumerizer.Process(input);
        }
    }
 ´´´
 
