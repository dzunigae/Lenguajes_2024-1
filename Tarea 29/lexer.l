/* Código en C*/

%{ 
	/* Bibliotecas estándar */
	#include <stdio.h>
	#include <string.h>
	#include <stdlib.h>

	/* Definición de constantes manifiestas */
    #define RELOP 400
	#define LT 401
	#define LE 402
	#define EQ 403
	#define NE 404
	#define GT 405
	#define GE 406
    #define KW 500
	#define IF 501
	#define THEN 502
	#define ELSE 503
	#define ID 600
	#define NUMBER 700

	/* Variable que puede contener el valor de las constantes */
	int yylval;

	/* Estructura para la tabla de símbolos */
	typedef struct {
		char *name;
	} Symbol;

	/* Tabla de símbolos para los identificadores */
	Symbol idTable[100]; 
	int idCount = 0;

	/* Tabla de símbolos para los números */
	int numTable[100];
	int numCount = 0;

	int installID();
	int installNum();
%}

/* Definiciones regulares */
delim	[ \t\n]
ws	{delim}+
letter	[A-Za-z]
digit	[0-9]
id	{letter}({letter}|{digit})*
number	{digit}+(\.{digit}+)?(E[+-]?{digit}+)?

/* Retornos */
%%

{ws}		{/* Sin acción o retorno */}
if			{yylval = IF; printf("KW IF (%d)\n", yylval); return(KW);}
then		{yylval = THEN; printf("KW THEN (%d)\n", yylval); return(KW);}
else		{yylval = ELSE; printf("KW ELSE (%d)\n", yylval); return(KW);}
{id}		{yylval = installID(); printf("ID %s (%d)\n", yytext, yylval); return(ID);}
{number}	{yylval = installNum(); printf("NUMBER %s (%d)\n", yytext, yylval); return(NUMBER);}
"<"			{yylval = LT; printf("RELOP < (%d)\n", yylval); return(RELOP);}
"<="		{yylval = LE; printf("RELOP <= (%d)\n", yylval); return(RELOP);}
"="			{yylval = EQ; printf("RELOP = (%d)\n", yylval); return(RELOP);}
"<>"		{yylval = NE; printf("RELOP <> (%d)\n", yylval); return(RELOP);}
">"			{yylval = GT; printf("RELOP > (%d)\n", yylval); return(RELOP);}
">="		{yylval = GE; printf("RELOP >= (%d)\n", yylval); return(RELOP);}
. {printf("Caracter no reconocido: %s\n", yytext);}

%%

/* Funciones */
int installID() {
	idTable[idCount].name = strdup(yytext);
	return idCount++;
}

int installNum() {
	numTable[numCount] = atoi(yytext);
	return numCount++;
}

/* Main */
int main(int argc, char **argv) {
	yyin = stdin; // Leer por consola
	printf("Ingrese la expresión a evaluar (Presione Ctrl+D para salir):\n");

	while (!feof(yyin)) {
		yylex();
	}

	printf("\n");

	return 0;
}