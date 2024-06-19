using System;
using System.Text.RegularExpressions;

class Program{
    //Declaración de variables
    
    //Grado del polinomio
    static int grado = 0;

    static void Main(){
        //Ingreso del polinomio por teclado, los polinomios tienen la siguiente notación:
        //n1xp1+n2xp2-n3xp3...+pn
        //Donde primero va el número que acompaña a la variable, y luego de la variable, el número que representa su
        //potencia, luego sigue el operador + o - y luego continúa el polinomio
        //pn representa el término constante.
        //Ejemplo: 2x3-3x2+1
        Console.WriteLine("Ingrese el polinomio:");
        //string polinomio = Console.ReadLine()!.Trim();
        string polinomio = "2x3-3x2+1";

        //Verificar si la entrada no es null
        if(polinomio != null){
            //Verificar el grado del polinomio
            //Dividir el polinomio en términos usando como separador los + y -
            string[] terminos = polinomio.Split(new char[] { '+', '-' });
            //Recorrer la lista de términos para determinar el grado del polinomio
            foreach(var termino in terminos){
                //Sacar la potencia del término
                int potencia = 0;
                Match match = Regex.Match(termino, @"x\^?(\d*)");
                if (match.Success){
                    string potenciaStr = match.Groups[1].Value;
                    if (!string.IsNullOrEmpty(potenciaStr)){
                        potencia = int.Parse(potenciaStr);
                    }else{
                        potencia = 1;
                    }
                }else{
                    potencia = 0;
                }
                if(potencia > grado){
                    grado = potencia;
                }
            }
            
            //Crear la estructura de datos que contendrá las listas

        }else{
            Console.WriteLine("El Polinomio es nulo.");
        }
    }
}