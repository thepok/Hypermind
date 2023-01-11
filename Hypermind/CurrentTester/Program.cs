// See https://aka.ms/new-console-template for more information
using HypermindLib;
using OpenAILib;


var answerer = new AnswerQuestionWithText(new OpenAI());
var result = answerer.AskQuestion("the apple was red and the car yellow","what color was the apple?");
Console.WriteLine(result);

var summerizer = new SimpleSummerizer(new OpenAI(OpenAI.STRONGEST, maxNewTokens: 100));
var longText = "Russische Kräfte machen nach britischer Einschätzung leichte Fortschritte bei den Kämpfen um die ostukrainische Stadt Bachmut. Reguläre Truppen und Einheiten der Söldnergruppe Wagner hätten in den vergangenen vier Tagen taktische Vorstöße in die zehn Kilometer nördlich gelegene Kleinstadt Soledar gemacht und kontrollierten wahrscheinlich den größten Teil des Orts, teilt das Verteidigungsministerium in London mit. Bachmut bleibe das vorrangige Ziel der russischen Offensive. Der Vorstoß nach Soledar solle die Stadt von Norden her einschließen und ukrainische Kommunikationswege unterbrechen. Die Kämpfe konzentrierten sich auf Zugänge zu stillgelegten Salzminenstollen, die unter dem Gebiet verlaufen und insgesamt rund 200 Kilometer lang seien. Trotz des erhöhten Drucks auf Bachmut sei es unwahrscheinlich, dass Russland die Stadt bald einnimmt, da die ukrainischen Streitkräfte stabile Verteidigungsstellungen aufgebaut hätten und auch die Versorgungswege weiter kontrollierten.\r\n\r\n";
var erg = summerizer.Summerize(longText);
Console.WriteLine(erg);


