%{ 
	/* definitions of manifest constants */
	#define LT 401
	#define LE 402
	#define EQ 403
	#define NE 404
	#define GT 405
	#define GE 406
	#define IF 501
	#define THEN 502
	#define ELSE 503
	#define KW 500
	#define ID 600
	#define NUMBER 700
	#define RELOP 400

	int yylval;
	int installID();
	int installNum();
%}

/* regular definitions */
delim	[ \t\n]
ws	{delim}+
letter	[A-Za-z]
digit	[0-9]
id	{letter}({letter}|{digit})*
number	{digit}+(\.{digit}+)?(E[+-]?{digit}+)?

%%

{ws}	{/* no action and no return */}
if	{yylval = IF; return(KW);}
then	{yylval = THEN; return(KW);}
else	{yylval = ELSE; return(KW);}
{id}	{yylval = (int) installID(); return(ID);}
{number} {yylval = (int) installNum(); return(NUMBER);}
"<"	{yylval = LT; return(RELOP);}
"<="	{yylval = LE; return(RELOP);}
"="	{yylval = EQ; return(RELOP);}
"<>"	{yylval = NE; return(RELOP);}
">"	{yylval = GT; return(RELOP);}
">="	{yylval = GE; return(RELOP);}

%%

int installID() {
	/* function to install the lexeme, whose
	   first character is pointed to by yytext,
	   and whose length is yyleng, into the
	   symbol table and return a pointer thereto */
	static int id = 1;
	return id++;  // Simple ID installation simulation
}

int installNum() {
	/* similar to installID, but puts numerical
	   constants into a separate table */
	static int num = 100;
	return num++;  // Simple number installation simulation
}
