using CursoLinq;
using System.Threading.Channels;

LinqQueries queries = new LinqQueries();

//Toda la coleccion 
//ImprimirValores(queries.TodaLaColeccion());

// Libros despues del 2000
//ImprimirValores(queries.LibrosDespuesdel2000());

// Libros con mas de 250 paginas y que contengan en el titulo "in action"
//ImprimirValores(queries.LibrosMas250pagsYtitulo());

// Todos los libros tienen Status 
//Console.WriteLine($" Todos los libros tienen status? - {queries.TodosLosLibrosTienenStatus()}");

// Preguntar si algun libro fue publicado en 2005
//Console.WriteLine($" Algun libro fue publicado en 2005? - {queries.SiAlgunLibroFuePublicado2005()}");

// Libros de Pyhton que se encuentran en la coleccion
//ImprimirValores(queries.LibrosQuePertenzcanCatPython());

// Elementos que sean de la cat java ordenados por su nombre
//ImprimirValores(queries.LibrosdeJavaPorNombreAscendente());

// Libros que tengan mas de 450 pags, ordenados por nro de pags descendente 
//ImprimirValores(queries.LibrosConMasde450pagsOrdenadosDescendentemente());

// Primeros 3 libros con fecha de public mas reciente categorizados en Java
//ImprimirValores(queries.PrimerosTresLibrosMasRecientes());

// Tercer y cuarto libro de los que tengan mas de 400 pags
//ImprimirValores(queries.TerceryCuartoLibroConMas400pags());

// Seleccionar titulo y nro de pag de los primeros 3 libros de la coleccion
//ImprimirValores(queries.TresPrimerosLibrosDeLaColeccion());

// Nro de libros que tengan entre 200 y 500 pags
//Console.WriteLine($" Cantidad de libros que tienen entre 200 y 500 paginas: {queries.CantidadDeLibrosEntre200y500pags()}"); 

// Retornar la menor fecha de publicacion de la lista de libros
//Console.WriteLine($"Fecha de publicacion menor de un libro: {queries.FechaDePublicacionMenor().ToShortDateString()}");

// Retornar la cant de pags del libro con mayor nro de pags de la coleccion
//Console.WriteLine($"Cantidad de paginas del libro con mas paginas: {queries.CantidadDePagsDelLibroConMasPags()}");

// Retorna el libro que tenga la menor cantidad de paginas mayor a 0
//var libroMenorPag = queries.LibroConMenorNroDePagina();
//Console.WriteLine($"Libro con menor numero de paginas: Titulo - {libroMenorPag.Title} - Paginas - {libroMenorPag.PageCount}");

// Retorna el libro con la fecha de publicacion mas reciente
//var libroMasReciente = queries.LibroConFechaDePublicacionMasReciente();
//Console.WriteLine($"Libro con fecha de publicacion mas reciente: Titulo - {libroMasReciente.Title} - Paginas - {libroMasReciente.PublishedDate.ToShortDateString()}");

// Retorna la suma de la cant de pags de todos los libros que tengan entre 0 y 500 pags
//Console.WriteLine($"Suma total de paginas: {queries.SumaDeTodasLasPaginasLibrosEntre0y500()}");

// Retornar el titulo de los libros que tienen fecha de publicacion posterior a 2015
//Console.WriteLine(queries.TitulosLibrosDespuesDel2015Concatenados());

// Promedio de caracteres que tienen los titulos de la coleccion
//Console.WriteLine($"Promedio de caracteres que tienen los titulos de los libros: {queries.PromedioCaracteresTitulo()}");

// Promedio de nro de pags de los libros y filtrar libros que tienen 0 paginas.
//Console.WriteLine($"Promedio de paginas de los libros: {queries.PromedioCantPagsExcpPagsCon0()}");

// Libros que fueron publicados a partir del 2000, agrupados x año
//ImprimirGrupo(queries.LibrosPublicadosAPartirDel2000AgrupadosXAño());

// Retorna un diccionario que permita consultar los libros de acuerdo a la letra con la que inicia el titulo del libro
//var diccionarioLookup = queries.DiccionarioDeLibrosPorLetra();
//ImprimirDiccionario(diccionarioLookup, 'A');

// Obtener coleccion que tenga todos los libros con mas de 500 pags y otra que contenga los libros publicados dsp del 2005. Retornar los libros que esten en ambas colecciones
ImprimirValores(queries.LibrosDespuesDel2005ConMas500Pags());

void ImprimirValores(IEnumerable<Book> listaDeLibros)
{
    Console.WriteLine("{0, -60} {1, 15} {2, 15}\n", "Titulo", "N. pagina", "Fecha de publicacion");
    foreach (var item in listaDeLibros)
    {
        Console.WriteLine("{0, -60} {1, 15} {2, 15}", item.Title, item.PageCount, item.PublishedDate.ToShortDateString());
    }
}

void ImprimirGrupo(IEnumerable<IGrouping<int, Book>> ListadeLibros)
{
    foreach (var grupo in ListadeLibros)
    {
        Console.WriteLine("");
        Console.WriteLine($"Grupo: {grupo.Key}");
        Console.WriteLine("{0,-60} {1, 15} {2, 15}\n", "Titulo", "N. Paginas", "Fecha publicacion");
        foreach (var item in grupo)
        {
            Console.WriteLine("{0,-60} {1, 15} {2, 15}", item.Title, item.PageCount, item.PublishedDate.Date.ToShortDateString());
        }
    }
}

void ImprimirDiccionario(ILookup<char, Book> ListadeLibros, char letra)
{
    Console.WriteLine("{0,-60} {1, 15} {2, 15}\n", "Titulo", "N. Paginas", "Fecha publicacion");
    foreach (var item in ListadeLibros[letra])
    {
        Console.WriteLine("{0,-60} {1, 15} {2, 15}", item.Title, item.PageCount, item.PublishedDate.Date.ToShortDateString());
    }
}

/*
List<Animal> animales = new List<Animal>();
animales.Add(new Animal() { Nombre = "Hormiga", Color = "Rojo" });
animales.Add(new Animal() { Nombre = "Lobo", Color = "Gris" });
animales.Add(new Animal() { Nombre = "Elefante", Color = "Gris" });
animales.Add(new Animal() { Nombre = "Pantegra", Color = "Negro" });
animales.Add(new Animal() { Nombre = "Gato", Color = "Negro" });
animales.Add(new Animal() { Nombre = "Iguana", Color = "Verde" });
animales.Add(new Animal() { Nombre = "Sapo", Color = "Verde" });
animales.Add(new Animal() { Nombre = "Camaleon", Color = "Verde" });
animales.Add(new Animal() { Nombre = "Gallina", Color = "Blanco" });

// filtra todos los animales que sean de color verde que su nombre inicie con una vocal

//List<char> vocales = new List<char>() { 'a', 'e', 'i', 'o', 'u' };

//List<Animal> resultados = animales.Where(p => p.Color.ToLower().Equals("verde") && vocales.Contains(p.Nombre.ToLower()[0])).ToList();

//if (resultados.Any())
//    resultados.ForEach(p => Console.WriteLine(p.Nombre));

// Retorna los elementos de la colección animal ordenados por nombre
//animales.OrderBy(p => p.Nombre).ToList().ForEach(p => Console.WriteLine(p.Nombre));

// Retorna los datos de la colleción Animales agrupada por color
//var animalesAgrupadosColor = animales.GroupBy(x => x.Color);

//foreach (var grupo in animalesAgrupadosColor)
//{
//    Console.WriteLine("");
//    Console.WriteLine($"Grupo: {grupo.Key}");
//    Console.WriteLine("{0, 15} {1, 15}\n", "Nombre", "Color");
//    foreach (var item in grupo)
//    {
//        Console.WriteLine("{0, 15} {1, 15}", item.Nombre, item.Color);
//    }
//}

//IEnumerable<IGrouping<string, Animal>> animalesGroup = animales.GroupBy(p => p.Color);

//foreach (var grupo in animalesGroup)
//{
//    Console.WriteLine("");
//    Console.WriteLine($"Grupo: {grupo.Key}");
//    Console.WriteLine("{0,-60}{1,15}", "Name", "Color");
//    foreach (var item in grupo)
//    {
//        Console.WriteLine("{0,-60}{1,15}", item.Nombre, item.Color);
//    }
//}
public class Animal
{
    public string Nombre { get; set; }
    public string Color { get; set; }
}

*/

