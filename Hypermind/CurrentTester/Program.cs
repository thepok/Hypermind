﻿// See https://aka.ms/new-console-template for more information
using HypermindLib;
using OpenAILib = OpenAI;
//Ask a LLM a question that it directly without help awnsers.
var askLLm = new AskLLM(new OAI());
var site = askLLm.Ask("What is the Url of the Wikipediasite about the element gold?");
Console.WriteLine(site);

//get text of an Website
Webrequest webrequest = new Webrequest();
var text = webrequest.Get(site);
Console.WriteLine(text);

//split a long text prety smartly
var splits = Textsplitter.SmartStringSplit(text, 4000);

//summarize a novel
Console.WriteLine("warning - summarizing a book with OpenAI davinci costs ~ 2$");
Console.ReadLine();
text = webrequest.Get("https://www.gutenberg.org/files/11/11-h/11-h.htm");
var recursivSummarizer = new RecursivSummarizer(new OAI(maxNewTokens:500));
var summery = recursivSummarizer.Summarize(text);
Console.WriteLine(summery);

//ask questions about a text
var answerer = new AnswerQuestionAboutText(new OAI());
var result = answerer.AskQuestion("the apple was red and the car yellow","what color was the apple?");
Console.WriteLine(result);



