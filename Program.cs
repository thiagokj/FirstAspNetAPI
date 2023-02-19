var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Executando...");

app.MapGet("/nome/{nome}", (string nome) =>
{
    return Results.Ok($"Olá {nome}");
});

app.MapGet("/produtos", () => "Todos os produtos estão aqui!");

app.MapPost("/", (User user) =>
{
    return Results.Ok(user);
});

app.UseHttpsRedirection();
app.Run();
