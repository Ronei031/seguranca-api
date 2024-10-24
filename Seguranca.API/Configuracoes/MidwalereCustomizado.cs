public class AutorizacaoMiddlewareCustomizada
{
    private readonly RequestDelegate _next;

    public AutorizacaoMiddlewareCustomizada(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Passa a solicitação para o próximo middleware
        await _next(context);

        // Verifica se a resposta é 403 e personaliza a mensagem
        if (context.Response.StatusCode == StatusCodes.Status403Forbidden)
        {
            context.Response.ContentType = "application/json";
            var response = new
            {
                StatusCode = 403,
                Message = "Acesso negado. Você não tem permissão para acessar este recurso."
            };
            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }
        // Verifica se a resposta é 401 e personaliza a mensagem
        else if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
        {
            context.Response.ContentType = "application/json";
            var response = new
            {
                StatusCode = 401,
                Message = "Não autorizado. Você precisa estar autenticado para acessar este recurso."
            };
            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }
    }
}
