using System.Globalization;

// El curso guarda la lista de estudiantes y sabe leer/escribir el archivo
Curso curso = new Curso("Calificaciones del cuatrimestre", "estudiantes.txt");
curso.CargarDesdeArchivo();

string opcion;

do
{
    Console.WriteLine();
    Console.WriteLine("=== Colegio Dios es bueno ===");
    Console.WriteLine("1. Agregar un estudiante");
    Console.WriteLine("2. Eliminar un estudiante");
    Console.WriteLine("3. Reportes");
    Console.WriteLine("4. Salir");
    Console.Write("Elija una opción: ");
    opcion = Console.ReadLine()!.Trim();

    switch (opcion)
    {
        case "1":
            AgregarEstudiante();
            break;
        case "2":
            EliminarEstudiante();
            break;
        case "3":
            curso.MostrarReporte();
            break;
        case "4":
            Console.WriteLine("¡Hasta luego!");
            break;
        default:
            Console.WriteLine("Opción no válida. Intente de nuevo.");
            break;
    }
}
while (opcion != "4");

// ===== Opción 1: Agregar =====
void AgregarEstudiante()
{
    Console.WriteLine();
    Console.Write("Nombre: ");
    string nombre = Console.ReadLine()!.Trim();

    Console.Write("Apellido: ");
    string apellido = Console.ReadLine()!.Trim();

    double nota1 = PedirNota("Nota 1: ");
    double nota2 = PedirNota("Nota 2: ");
    double nota3 = PedirNota("Nota 3: ");
    double nota4 = PedirNota("Nota 4: ");

    curso.Agregar(new Estudiante(nombre, apellido, nota1, nota2, nota3, nota4));
    curso.GuardarEnArchivo();

    Console.WriteLine("Estudiante agregado y guardado.");
}

// ===== Opción 2: Eliminar =====
void EliminarEstudiante()
{
    if (curso.Estudiantes.Count == 0)
    {
        Console.WriteLine("No hay estudiantes registrados.");
        return;
    }

    Console.WriteLine();
    for (int i = 0; i < curso.Estudiantes.Count; i++)
    {
        Estudiante e = curso.Estudiantes[i];
        Console.WriteLine($"{i + 1}. {e.Nombre} {e.Apellido}");
    }

    Console.Write("Número del estudiante a eliminar: ");
    if (int.TryParse(Console.ReadLine(), out int numero) &&
        numero >= 1 && numero <= curso.Estudiantes.Count)
    {
        Estudiante eliminado = curso.Estudiantes[numero - 1];
        curso.Eliminar(numero - 1);
        curso.GuardarEnArchivo();
        Console.WriteLine($"Se eliminó a {eliminado.Nombre} {eliminado.Apellido}.");
    }
    else
    {
        Console.WriteLine("Número no válido.");
    }
}

// ===== Pedir una nota (repite hasta que sea válida) =====
double PedirNota(string mensaje)
{
    while (true)
    {
        Console.Write(mensaje);
        if (double.TryParse(Console.ReadLine(), out double nota) && nota >= 0 && nota <= 100)
        {
            return nota;
        }
        Console.WriteLine("Nota inválida. Debe ser un número entre 0 y 100.");
    }
}

// ===== Clase Curso =====
class Curso
{
    public string Nombre { get; }
    public string RutaArchivo { get; }
    public List<Estudiante> Estudiantes { get; } = new List<Estudiante>();

    public Curso(string nombre, string rutaArchivo)
    {
        Nombre = nombre;
        RutaArchivo = rutaArchivo;
    }

    public void Agregar(Estudiante estudiante)
    {
        Estudiantes.Add(estudiante);
    }

    public void Eliminar(int indice)
    {
        Estudiantes.RemoveAt(indice);
    }

    // Escribe una línea por estudiante en el txt
    public void GuardarEnArchivo()
    {
        List<string> lineas = new List<string>();
        foreach (Estudiante e in Estudiantes)
        {
            lineas.Add(e.ALineaTexto());
        }
        File.WriteAllLines(RutaArchivo, lineas);
    }

    // Lee el txt al iniciar el programa (si existe)
    public void CargarDesdeArchivo()
    {
        if (!File.Exists(RutaArchivo))
        {
            return;
        }

        foreach (string linea in File.ReadAllLines(RutaArchivo))
        {
            if (!string.IsNullOrWhiteSpace(linea))
            {
                Estudiantes.Add(Estudiante.DesdeLineaTexto(linea));
            }
        }
    }

    public void MostrarReporte()
    {
        if (Estudiantes.Count == 0)
        {
            Console.WriteLine("No hay estudiantes registrados.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Colegio Dios es bueno.");
        Console.WriteLine(Nombre);
        Console.WriteLine(new string('=', 71));
        Console.WriteLine($"{"Nombre",-12}{"Apellido",-12}{"Nota1",7}{"Nota2",7}{"Nota3",7}{"Nota4",7}{"Promedio",10}{"Literal",9}");
        Console.WriteLine(new string('=', 71));

        foreach (Estudiante e in Estudiantes)
        {
            Console.WriteLine($"{e.Nombre,-12}{e.Apellido,-12}{e.Nota1,7}{e.Nota2,7}{e.Nota3,7}{e.Nota4,7}{e.Promedio,10:F1}{e.Literal,9}");
        }

        Console.WriteLine(new string('=', 71));
        Console.WriteLine($"Total de estudiantes: {Estudiantes.Count}");
    }
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

    // Convierte el estudiante en una línea: Juan;Pérez;90;85;78;92
    public string ALineaTexto()
    {
        CultureInfo c = CultureInfo.InvariantCulture;
        return $"{Nombre};{Apellido};{Nota1.ToString(c)};{Nota2.ToString(c)};{Nota3.ToString(c)};{Nota4.ToString(c)}";
    }

    // Hace lo contrario: de una línea del txt crea un Estudiante
    public static Estudiante DesdeLineaTexto(string linea)
    {
        CultureInfo c = CultureInfo.InvariantCulture;
        string[] partes = linea.Split(';');
        return new Estudiante(
            partes[0],
            partes[1],
            double.Parse(partes[2], c),
            double.Parse(partes[3], c),
            double.Parse(partes[4], c),
            double.Parse(partes[5], c));
    }
}
