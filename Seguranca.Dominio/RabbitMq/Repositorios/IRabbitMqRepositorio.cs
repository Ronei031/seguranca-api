namespace Seguranca.Dominio.RabbitMq.Repositorios
{
    public interface IRabbitMqRepositorio
    {
        Task EnviarMensagemAsync(string mensagem, string topico, CancellationToken cancellationToken = default);
    }
}
