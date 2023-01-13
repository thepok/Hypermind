﻿// See https://aka.ms/new-console-template for more information
using HypermindLib;
using OpenAILib;

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



