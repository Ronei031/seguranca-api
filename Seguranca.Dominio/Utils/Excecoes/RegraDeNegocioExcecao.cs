﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Seguranca.Dominio.Utils.Excecoes;

[Serializable]
[ExcludeFromCodeCoverage]
public class RegraDeNegocioExcecao : Exception
{
    public RegraDeNegocioExcecao()
    {
    }

    public RegraDeNegocioExcecao(string mensagem) : base(mensagem)
    {

    }

    public RegraDeNegocioExcecao(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected RegraDeNegocioExcecao(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}


