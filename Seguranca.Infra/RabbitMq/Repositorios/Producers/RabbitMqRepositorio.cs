using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Seguranca.Dominio.RabbitMq.Repositorios;
using Seguranca.Dominio.Utils.Excecoes;
using System.Text;

namespace Seguranca.Infra.RabbitMq.Repositorios.Producers
{
    public class RabbitMqRepositorio : IRabbitMqRepositorio
    {
        private readonly IConfiguration configuration;
        private readonly ConnectionFactory factory;
        private IConnection? conexao;
        private IChannel? canal;
        private CancellationTokenSource? cts;
        private bool isConsumerRunning;

        public RabbitMqRepositorio(IConfiguration configuration)
        {
            this.configuration = configuration;

            factory = new ConnectionFactory
            {
                HostName = configuration["RabbitMQ:Host"] ?? "",
                Port = int.Parse(configuration["RabbitMQ:Port"] ?? ""),
                UserName = configuration["RabbitMQ:User"] ?? "",
                Password = configuration["RabbitMQ:Password"] ?? "",
                AutomaticRecoveryEnabled = true,
                RequestedConnectionTimeout = TimeSpan.FromMilliseconds(3000),
                TopologyRecoveryEnabled = true
            };
        }

        public async Task EnviarMensagemAsync(string mensagem, string topico, CancellationToken cancellationToken = default)
        {
            try
            {
                using var conexao = await factory.CreateConnectionAsync(cancellationToken);
                using var canal = await conexao.CreateChannelAsync(null, cancellationToken);

                await canal.QueueDeclareAsync(
                    queue: topico,
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null,
                    noWait: true,
                    cancellationToken);

                byte[] body = Encoding.UTF8.GetBytes(mensagem);

                await canal.BasicPublishAsync(
                    exchange: "",
                    routingKey: topico,
                    body: body,
                    cancellationToken);
            }
            catch (Exception ex)
            {
                throw new RegraDeNegocioExcecao("Erro ao enviar mensagem para o RabbitMQ", ex);
            }
        }
    }
}
