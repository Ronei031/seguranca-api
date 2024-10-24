using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Seguranca.Dominio.Utils.Excecoes;

[ExcludeFromCodeCoverage]
public class AtributoInvalidoExcecao : RegraDeNegocioExcecao
{
    public AtributoInvalidoExcecao()
    {
    }
    public AtributoInvalidoExcecao(string atributo) : base(atributo + " inválido")
    {
    }

    protected AtributoInvalidoExcecao(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}


