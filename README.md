# TextInterpreter
C# text interpreter for a text adventure I/O

Using a custom XML schema for holding responses and keyword linking to reponses the C# code uses LINQ queries to take a human input, 
picks out keywords, and provides responses to the user based on the XML file. The goal is for the reuse of this code by only 
changing the XML document based on the established schema for other uses of this type of input output interaction.
