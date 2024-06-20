using System.Text.RegularExpressions;

class Program
{
    //Declaración de variables

    //Grado del polinomio
    static int grado = 0;
    //Lista de listas
    static List<List<int>> tabla_de_diferencias = new List<List<int>>();
    //Diccionario para almacenar coeficientes y sus potencias
    static Dictionary<int, int> coeficientes = new Dictionary<int, int>();

    static void Main()
    {
        //Ingreso del polinomio por teclado, los polinomios tienen la siguiente notación:
        //n1xp1+n2xp2-n3xp3...+pn
        //Donde primero va el número que acompaña a la variable, y luego de la variable, el número que representa su
        //potencia, luego sigue el operador + o - y luego continúa el polinomio
        //pn representa el término constante.
        //Ejemplo: 2x3-3x2+1

        //Adicional: Cada potencia debe estar asociada a un único término, por ejemplo, si se presenta
        // algo así: 2x3+4x3, el programa presentará fallos.
        //Además, por practicidad se dejó el número máximo de la tabla en 1000 para facilidad y 
        // practicidad de este ejercicio

        Console.WriteLine("Ingrese el polinomio:");
        string polinomio = Console.ReadLine()!.Trim();

        //Verificar si la entrada no es null
        if (polinomio != null)
        {
            //Verificar el grado del polinomio

            // Dividir el polinomio en términos usando como separador los + y -
            string[] terminos = Regex.Split(polinomio, @"(?=[+-])");

            // Recorrer la lista de términos para determinar el grado del polinomio y almacenar coeficientes
            foreach (var termino in terminos)
            {
                // Extraer coeficiente y potencia
                Match match = Regex.Match(termino, @"([+-]?\d*)x?(\d*)");
                if (match.Success)
                {
                    string coefStr = match.Groups[1].Value;
                    string potenciaStr = match.Groups[2].Value;

                    int coeficiente;
                    int potencia;

                    if (string.IsNullOrEmpty(coefStr))
                    {
                        coeficiente = 1;
                    }
                    else if (coefStr == "+")
                    {
                        coeficiente = 1;
                    }
                    else if (coefStr == "-")
                    {
                        coeficiente = -1;
                    }
                    else
                    {
                        coeficiente = int.Parse(coefStr);
                    }

                    if (string.IsNullOrEmpty(potenciaStr)) {
                        potencia = termino.Contains('x') ? 1 : 0;
                    } else {
                        potencia = int.Parse(potenciaStr);
                    }

                    if (potencia > grado) {
                        grado = potencia;
                    }

                    coeficientes[potencia] = coeficiente;
                }

            }

            //Número hasta el cual se rellenará la tabla, por temas de practicidad.
            int x = 1000;

            //Número de listas necesarias
            int l = grado+1;

            //Crear las listas necesarias
            for(int i = 0; i < l; i++){
                tabla_de_diferencias.Add(new List<int>());
            }

            //Rellenar la tabla
            for(int i = 1; i <= x; i++){
                int valor = EvaluarPolinomio(i);
                tabla_de_diferencias[0].Add(valor);
                for(int j = 0; j < l; j++){
                    int tamaño = tabla_de_diferencias[j].Count;
                    if(tamaño < 2){
                        break;
                    }else{
                        int ultimo = tabla_de_diferencias[j][tamaño-1];
                        int penultimo = tabla_de_diferencias[j][tamaño-2];
                        int diferencia = ultimo - penultimo;
                        if(j+1 < l){
                            tabla_de_diferencias[j+1].Add(diferencia);
                        }
                    }
                }
            }

            //Ejecución
            while(true){
                Console.WriteLine($"Ingrese el valor (Recuerde que debe ser mayor a: {l}):");
                // Leer la entrada del usuario desde la consola
                string input = Console.ReadLine()?.Trim()!;

                // Intentar convertir el valor a un entero
                if (int.TryParse(input, out int valor))
                {
                    if(valor <= l){
                        Console.WriteLine($"Recuerde que debe ser mayor a: {l}");
                    }else{
                        int rta = ObtenerValor(valor);
                        Console.WriteLine($"La respuesta es: {rta}");
                    }
                }
                else
                {
                    Console.WriteLine("Entrada no válida. Debe ser un número entero.");
                }
            }
        }
        else
        {
            Console.WriteLine("El Polinomio es nulo.");
        }
    }

    //Función que resuelve el polinomio para un valor x utilizando los valores del diccionario
    static int EvaluarPolinomio(int x) {
        int resultado = 0;
        foreach (var par in coeficientes) {
            int potencia = par.Key;
            int coeficiente = par.Value;
            resultado += coeficiente * (int)Math.Pow(x, potencia);
        }
        return resultado;
    }

    //Función para obtener el valor
    static int ObtenerValor(int x){
        int x_relativa = x - 2;
        int valor = 0;
        int resto = 0;
        for(int i = 0; i < tabla_de_diferencias.Count(); i++){
            valor += tabla_de_diferencias[i][x_relativa-resto];
            resto += 1;
        }
        return valor;
    }
}