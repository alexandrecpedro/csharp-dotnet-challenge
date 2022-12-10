var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Mysql
#endregion

#region JWT
#endregion

#region Swagger
builder.Services.AddSwaggerGen();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

MapRoutes(app);

app.Run();

#region Rotas usando Minimal API

void MapRoutes(WebApplication app)
{
    app.MapGet("/", () => new {Mensagem = "Bem-vindo à API"});
    app.MapGet("/recebe-parametro", (HttpRequest request, HttpResponse response, string? nome) => 
    {
        response.StatusCode = 201;
        
        nome = $"""
        Alterando parâmetro recebido {nome}
        """;

        var objetoDeRetorno = new {
            ParametroPassado = nome,
            Mensagem = "Muito bem alunos! Passamos um parâmetro por query string"
        };

        return objetoDeRetorno;
    });
}

#endregion
