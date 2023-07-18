using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace CursoLinq
{
    internal class LinqQueries
    {

        private List<Book> librosCollection = new List<Book>();
        public LinqQueries()
        {
            using (StreamReader reader = new StreamReader("books.json")) // Si estariamos buscando dentro de las carpetas de windows, deberiamos poner c:/ y demas, como se encuentra dentro del proyecto no hace falta
            {
                string json = reader.ReadToEnd(); // guardamos dentro de un json la lectura de todo el archivo y lo guarda de manera temporal en la sig coleccion
                this.librosCollection = System.Text.Json.JsonSerializer.Deserialize<List<Book>>(json, new System.Text.Json.JsonSerializerOptions() { PropertyNameCaseInsensitive = true }); // Convierte todo el json a un tipo List Book
            }
        }

        public IEnumerable<Book> TodaLaColeccion()
        {
            return librosCollection;
            // Usamos IEnumerable pq solo queremos imprimir la coleccion completa sin ningun filtro
        }

        public IEnumerable<Book> LibrosDespuesdel2000()
        {
            // extension method
            // return librosCollection.Where(p => p.PublishedDate.Year > 2000);

            // query expresion
            return from l in librosCollection where l.PublishedDate.Year > 2000 select l;
        }

        public IEnumerable<Book> LibrosMas250pagsYtitulo()
        {
            // extension method
            //return librosCollection.Where(p => p.PageCount > 250 && p.Title.Contains("in Action"));

            // query expresion
            return from l in librosCollection where l.PageCount > 250 && l.Title.Contains("in Action") select l;
        }

        public bool TodosLosLibrosTienenStatus()
        {
            return librosCollection.All(p => p.Status != string.Empty);
        }

        public bool SiAlgunLibroFuePublicado2005()
        {
            return librosCollection.Any(p => p.PublishedDate.Year == 2005);
        }

        public IEnumerable<Book> LibrosQuePertenzcanCatPython()
        {
            return librosCollection.Where(p => p.Categories.Contains("Python"));
        }

        public IEnumerable<Book> LibrosdeJavaPorNombreAscendente()
        {
            return librosCollection.Where(p => p.Categories.Contains("Java")).OrderBy(p => p.Title);
        }

        public IEnumerable<Book> LibrosConMasde450pagsOrdenadosDescendentemente()
        {
            return librosCollection.Where(p => p.PageCount > 450).OrderByDescending(p => p.PageCount);
        }

        public IEnumerable<Book> PrimerosTresLibrosMasRecientes()
        {
            //return librosCollection
            //    .Where(p => p.Categories.Contains("Java"))
            //    .OrderByDescending(p => p.PublishedDate).Take(3); 
            return librosCollection
                .Where(p => p.Categories.Contains("Java"))
                .OrderBy(p => p.PublishedDate).TakeLast(3);
        }

        public IEnumerable<Book> TerceryCuartoLibroConMas400pags()
        {
            //return librosCollection
            //    .Where(p => p.PageCount > 400)
            //    .OrderByDescending(p => p.PageCount)
            //    .Take(4).Skip(2);
            return librosCollection
                .Where(p => p.PageCount > 400)
                .Take(4).Skip(2);
        }

        public IEnumerable<Book> TresPrimerosLibrosDeLaColeccion()
        {
            return librosCollection.Take(3)
             .Select(p => new Book() { Title = p.Title, PageCount = p.PageCount });
        }

        public int CantidadDeLibrosEntre200y500pags()
        {
            //return librosCollection.Where(p => p.PageCount >= 200 && p.PageCount <= 500).Count();
            return librosCollection.Count(p => p.PageCount >= 200 && p.PageCount <= 500);
        }

        public long CantidadDeLibrosEntre200y500pagsConLong()
        {
            //return librosCollection.Where(p => p.PageCount >= 200 && p.PageCount <= 500).LongCount();
            // Hacer asi con Count como LongCount es una mala practica porque Count y Long Count pueden recibir una condicion.
            return librosCollection.LongCount(p => p.PageCount >= 200 && p.PageCount <= 500);
        }

        public DateTime FechaDePublicacionMenor()
        {
            return librosCollection.Min(p => p.PublishedDate);
        }

        public int CantidadDePagsDelLibroConMasPags()
        {
            return librosCollection.Max(p => p.PageCount);
        }

        public Book LibroConMenorNroDePagina()
        {
            return librosCollection.Where(p => p.PageCount > 0).MinBy(p => p.PageCount);
        }

        public Book LibroConFechaDePublicacionMasReciente()
        {
            return librosCollection.MaxBy(p => p.PublishedDate);
        }

        public int SumaDeTodasLasPaginasLibrosEntre0y500()
        {
            return librosCollection.Where(p => p.PageCount >= 0 && p.PageCount <= 500).Sum(p => p.PageCount);   
        }

        public string TitulosLibrosDespuesDel2015Concatenados()
        {
            return librosCollection
                .Where(p => p.PublishedDate.Year > 2015)
                .Aggregate("", (TitulosLibros, next) =>
                {
                    if (TitulosLibros != string.Empty)
                        TitulosLibros += " - " + next.Title;
                    else
                        TitulosLibros += next.Title;
                    return TitulosLibros;
                });

// El "" vacio significa que va a ir un string en TitulosLibros (si pongo un entero va a dar error pq va a esperar un int),
// next es el q va a ir guardando y tomando el valor de cada uno de los libros que fueron filtrados por el where
        }

        public double PromedioCaracteresTitulo()
        {
            return librosCollection.Average(p => p.Title.Length);  
        }

        public double PromedioCantPagsExcpPagsCon0()
        {
            return librosCollection.Where(p => p.PageCount > 0).Average(p => p.PageCount);
        }

        public IEnumerable<IGrouping<int, Book>> LibrosPublicadosAPartirDel2000AgrupadosXAño()
        {
            return librosCollection.Where(p => p.PublishedDate.Year >= 2000).GroupBy(p => p.PublishedDate.Year);
        }

        public ILookup<char, Book> DiccionarioDeLibrosPorLetra()
        {
            return librosCollection.ToLookup(p => p.Title[0], p => p);
        }

        public IEnumerable<Book> LibrosDespuesDel2005ConMas500Pags()
        {
            var LibrosDespuesDel2005 = librosCollection.Where(p => p.PublishedDate.Year > 2005);

            var LibrosConMasDe500pags = librosCollection.Where(p => p.PageCount > 500);

            return LibrosDespuesDel2005.Join(LibrosConMasDe500pags, p => p.Title, x => x.Title, (p, x) => p);
            // Aca estamos comparando por titulo pero se deberia comparar por ID
        }
    }
}
