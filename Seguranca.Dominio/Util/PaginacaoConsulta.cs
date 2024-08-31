using System.Diagnostics.CodeAnalysis;

namespace Seguranca.Dominio.Util;

[ExcludeFromCodeCoverage]
public class PaginacaoConsulta<T> where T : class
{
    public long Total { get; set; }
    public IEnumerable<T> Registros { get; set; }      
   
}
