List<Estudiante> estudiantes = new List<Estudiante>();
string respuesta;

Console.WriteLine("=== Registro de calificaciones ===");

do
{
    Console.WriteLine();
    Console.Write("Nombre: ");
    string nombre = Console.ReadLine()!;

    Console.Write("Apellido: ");
    string apellido = Console.ReadLine()!;

    double nota1 = PedirNota("Nota 1: ");
    double nota2 = PedirNota("Nota 2: ");
    double nota3 = PedirNota("Nota 3: ");
    double nota4 = PedirNota("Nota 4: ");

    estudiantes.Add(new Estudiante(nombre, apellido, nota1, nota2, nota3, nota4));

    Console.Write("¿Desea agregar otro estudiante? (s/n): ");
    respuesta = Console.ReadLine()!.Trim().ToLower();
}
while (respuesta == "s");

// ===== Reporte =====
Console.WriteLine();
Console.WriteLine("Colegio Dios es bueno.");
Console.WriteLine("Calificaciones del cuatrimestre");
Console.WriteLine(new string('=', 71));
Console.WriteLine($"{"Nombre",-12}{"Apellido",-12}{"Nota1",7}{"Nota2",7}{"Nota3",7}{"Nota4",7}{"Promedio",10}{"Literal",9}");
Console.WriteLine(new string('=', 71));

foreach (Estudiante e in estudiantes)
{
    Console.WriteLine($"{e.Nombre,-12}{e.Apellido,-12}{e.Nota1,7}{e.Nota2,7}{e.Nota3,7}{e.Nota4,7}{e.Promedio,10:F1}{e.Literal,9}");
}

// ===== Función para pedir una nota =====
double PedirNota(string mensaje)
{
    Console.Write(mensaje);
    return double.Parse(Console.ReadLine()!);
}

// ===== Clase Estudiante =====
class Estudiante
{
    public string Nombre { get; }
    public string Apellido { get; }
    public double Nota1 { get; }
    public double Nota2 { get; }
    public double Nota3 { get; }
    public double Nota4 { get; }

    public Estudiante(string nombre, string apellido, double nota1, double nota2, double nota3, double nota4)
    {
        Nombre = nombre;
        Apellido = apellido;
        Nota1 = nota1;
        Nota2 = nota2;
        Nota3 = nota3;
        Nota4 = nota4;
    }

    public double Promedio
    {
        get
        {
            return (Nota1 + Nota2 + Nota3 + Nota4) / 4;
        }
    }

    public string Literal
    {
        get
        {
            if (Promedio >= 90) return "A";
            if (Promedio >= 80) return "B";
            if (Promedio >= 70) return "C";
            return "D";
        }
    }
}
