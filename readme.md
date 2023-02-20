# First ASP.NET API

Projeto para revisão de conceitos e aprendizado.

Uma aplicação Web é um EXE/DLL que fica executando no computador. Enquanto a aplicação estiver em execução, ela fica ouvindo uma porta e responde as possíveis requisições a essa porta.

## Requisitos

- Dotnet versão 7
- VS Code Thunder Client

## Primeiros passos

1. Crie um novo projeto com o comando **dotnet new web -o MinhaAPI**
1. Habilite o HTTPS com o comando **dotnet dev-certs https --trust**
1. Remova o profile http do do arquivo Properties -> launchSettings.json
1. Adicione ao Program.cs a instrução app.UseHttpsRedirection();

## Métodos HTTP

Toda vez que acessamos uma pagina web, estamos fazendo uma requisição.
Essa requisição é chamada de GET, que faz o retorno da informação ao solicitante.

Além desse método, o protocolo de comunicação Http possui mais métodos para cada tipo de interação:

GET: usado para recuperar recursos do servidor.
POST: usado para enviar dados para o servidor e criar um novo recurso.
PUT: usado para atualizar um recurso existente no servidor.
DELETE: usado para excluir um recurso do servidor.
PATCH: usado para fazer uma atualização parcial em um recurso existente no servidor.

## Request

Uma requisição é divida em 4 partes: Método HTTP, URI, Headers e Body.

- Método HTTP: o método utilizado na solicitação, como GET, POST, PUT, DELETE, etc.

- URI (Uniform Resource Identifier): o identificador exclusivo do recurso solicitado.

- Headers (Cabeçalho): informações adicionais que a solicitação pode incluir, como o tipo de dados aceitos, informações de autenticação, etc.

- Body (Corpo da solicitação): os dados adicionais que podem ser enviados juntamente com a solicitação, como parâmetros de consulta ou dados de formulário.
  Essas são as partes básicas de uma solicitação HTTP. O servidor processa a solicitação e retorna uma resposta ao cliente, que também é composta por várias partes, incluindo o código de status HTTP, cabeçalhos e o corpo da resposta.

## Response

A resposta é o retorno da requisição. Cada retorno possui uma faixa com codigos informando seu status, os principais são:

- 1xx (Respostas Informativas): Indica que a solicitação foi recebida e o servidor está continuando a processá-la.

- 2xx (Respostas de Sucesso): Indica que a solicitação foi recebida, compreendida e aceita com sucesso pelo servidor.

- 3xx (Respostas de Redirecionamento): Indica que a solicitação precisa ser redirecionada para outra localização para ser concluída.

- 4xx (Respostas de Erro do Cliente): Indica que a solicitação do cliente não pôde ser atendida ou compreendida pelo servidor devido a erros do cliente.

- 5xx (Respostas de Erro do Servidor): Indica que o servidor não pôde concluir a solicitação do cliente devido a um erro interno do servidor.

Essas são as principais faixas de códigos de resposta HTTP. Cada código de status específico dentro dessas faixas fornece informações mais detalhadas sobre a natureza da resposta do servidor.

## Estrutura da aplicação

A estrutura basica do App é parecida com essa:

```Csharp
// Cria a aplicação web para ficar ouvindo uma porta e responder as requisições.
var builder = WebApplication.CreateBuilder(args);

// Constrói a aplicação
var app = builder.Build();

// Rotas com endereços para acesso via URL
app.MapGet("/", () => "Hello World!");
app.MapGet("/produtos", () => "Todos os produtos estão aqui!");

// Configuração de redirecionamento
app.UseHttpsRedirection();

// Executa a aplicação
app.Run();
```

A função anônima representada por "( ) =>" visa reduzir a escrita de código. Sem ela, o codigo ficaria dessa forma:

```Csharp
// Método mais descritivo
app.MapGet("/", RetornaHelloWorld);
Void RetornaHelloWorld(){
  return "Hello World!";
}

// 2ª forma descritiva
app.MapGet("/", () => {
  return "Hello World!";
});

// Método simplificado
app.MapGet("/", () => "Hello World!");
```

## Passagem de valores

A url pode ser útil para receber parametros, com limite de 2000 caracteres aprox.
Obs: Não devemos passar informações sensíveis aqui por segurança.

```Csharp
// Rota que recebe o nome por parametro e exibe o nome na resposta
app.MapGet("/nome/{nome}", (string nome) =>
{
    return Results.Ok($"Olá {nome}");
});
```

Uma forma comum e padrão de mercado é trabalhar com informações no formato JSON.
JSON é a notação Javascript para objetos e o .NET faz a conversão automática (Serialização).

```Javascript
{
  "id": 1,
  "name": "Thiago"
}
```

```Csharp
app.MapPost("/", (User user) =>
{
    return Results.Ok(user);
});

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
}
```

Na maioria dos casos de integração entre sistemas modernos, as informações consumidas e enviadas atendem a esse padrão.
