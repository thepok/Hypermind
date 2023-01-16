# Hypermind
Welcome to Hypermind the .NET way of building minds.

Connect foundation models to enable emergent capabilities - with ease

## Community 

[![](https://dcbadge.vercel.app/api/server/6teaRCtD?style=flat)](https://discord.gg/6teaRCtD)
##  Installation: 
Add reference to HypermindLib
 Set the Enivormentvariable OPENAI_API_KEY to your OpenAI API Key or provide Key directly in Code
Example Code:

## News
* Recursive Summarizer - simply summarize a complete book
* Caching for LLMs implemented

## Examples

```csharp
using HypermindLib;

//Ask a LLM a question that it directly without help awnsers.
var askLLm = new AskLLM(new OpenAI());
var site = askLLm.Ask("What is the Url of the Wikipediasite about the element gold?");
Console.WriteLine(site);

//get text of an Website
Webrequest webrequest = new Webrequest();
var text = webrequest.Get(site);
Console.WriteLine(text);

//split a long text prety smartly
var splits = Textsplitter.SmartStringSplit(text, 4000);

//summarize a novel
text = webrequest.Get("https://www.gutenberg.org/cache/epub/69773/pg69773-images.html");
var recursivSummerizer = new RecursivSummarizer(new OpenAI(maxNewTokens:500));
var summery = recursivSummerizer.Summerize(text);
Console.WriteLine(summery);

//ask questions about a text
var answerer = new AnswerQuestionAboutText(new OpenAI());
var result = answerer.AskQuestion("the apple was red and the car yellow","what color was the apple?");
Console.WriteLine(result);

```

See the RecursiveSummerizer as inspiration for new chains. Its simple, like lego.

```csharp
    public class RecursivSummarizer : Chain
    {
        PrompTemplate SummerizerPromp = new PrompTemplate(
@"""Summerize the following text between >>> and <<< 

>>>
{text}
<<<

Summery:"""
        );

        ModelWithPromp Sumerizer;
        public RecursivSummarizer(LLM llm)
        {
            Sumerizer = new ModelWithPromp(llm, SummerizerPromp);
        }

        /// <summary>
        /// Simplified Call to Summerizer
        /// </summary>
        /// <param name="text">Text we want summerized</param>
        /// <returns></returns>
        public string Summerize(string text)
        {
            var splits = Textsplitter.SmartStringSplit(text, 8000);

            if (splits.Length == 1)
            {
                var input = new ChainInput("text", text);
                var output = Sumerizer.Process(input);
                return output.Result[0].Value;
            }
            else
            {
                var concatsummeries = splits.Select(s => Summerize(s)).Aggregate("", (current, next) => { return current +" "+ next; });
                var finamsummary = Summerize(concatsummeries);

                return finamsummary;
            }
        }

        public override ChainOutput Process(ChainInput input)
        {
            return Sumerizer.Process(input);
        }
    }
 ´´´
 
