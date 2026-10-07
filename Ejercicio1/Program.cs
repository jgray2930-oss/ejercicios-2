
Console.WriteLine("=== La Real Calculadora ===");
Console.WriteLine();

Console.WriteLine("Inserte el primer número:");
double a = double.Parse(Console.ReadLine()!);

Console.Write("Inserte el segundo número: ");
double b = double.Parse(Console.ReadLine()!);

Console.WriteLine();
Console.WriteLine("=== Resultados ===");

Console.WriteLine($"Sumna: {a} + {b} = {a + b }");
Console.WriteLine($"Resta: {a} - {b} = {a - b }");
Console.WriteLine($"Multiplicación: {a} * {b} = {a * b }");

if (b != 0)
{
   Console.WriteLine($"Division: {a} / {b} = {a / b:F2}");
}
else
{
    Console.WriteLine("Division: No se puede dividir entre cero.");
}

if (a >= 0)
{
    Console.WriteLine($"Raíz cuadrada de {a} = {Math.Sqrt(a):F2}");
}
else
{
    Console.WriteLine($"Raíz cuadrada de {a}: no existe para números negativos");
}

if (b >= 0)
{
    Console.WriteLine($"Raíz cuadrada de {b} = {Math.Sqrt(b):F2}");
}
else
{
    Console.WriteLine($"Raíz cuadrada de {b}: no existe para números negativos");
}