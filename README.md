# POC - Azure Form Recognizer

## Resumo

Este projeto é uma aplicação ASP.NET Core destinada a demonstrar as capacidades do Azure Form Recognizer. O serviço de Form Recognizer é utilizado para analisar formulários e documentos diversos. Após a análise, os resultados são processados e exportados em JSON. A aplicação também fornece uma API RESTful, cuja documentação é gerada pelo Swagger, facilitando o teste e a integração.

## Instalação

### Dependências

#### .NET

- SDK do .NET 6.0

#### Python

- Python 3.10
- Biblioteca pandas

Você pode instalar a biblioteca usando pip:

```bash
pip install pandas
```

#### Swagger

O Swagger é utilizado para documentação da API e é uma dependência do projeto .NET.

### Configuração da API Key do Azure via Variáveis de Ambiente

Para utilizar o Azure Form Recognizer, você precisará de uma chave de API válida. A chave é gerenciada através de variáveis de ambiente, então certifique-se de configurá-la em seu ambiente de desenvolvimento e produção.

No sistema operacional, defina a variável de ambiente, como por exemplo Windows:

```
setx FormRecognizerApiKey "sua_chave_aqui"
setx FormRecognizerEndpoint "seu_endpoint_aqui"
```

### Passos para Instalação

1. Clone o repositório.
2. Navegue até o diretório do projeto e execute o comando para restaurar os pacotes NuGet:

```bash
dotnet restore
```

3. Execute o projeto:

```bash
dotnet run
```

## Testando o Projeto

Para testar a API, acesse a documentação gerada pelo Swagger na URL:

```
https://localhost:7009/swagger/index.html
```

Você encontrará todas as endpoints disponíveis e poderá testá-las diretamente pelo Swagger.

## Estrutura do Projeto

- Controllers: Contém o `FormRecognizerController`, que lida com as operações da API.
- Services: Contém serviços como `FormRecognizerService` e `ExcelService`, que implementam a lógica de negócios.
- Utils: Scripts auxiliares em `PythonScripts`.
- Python: Script `ExcelConverter.py` para processamento do JSON usando Python.

## Gestão de Pacotes NuGet

A gestão dos pacotes NuGet é feita automaticamente pelo .NET. O arquivo `*.csproj` mantém um registro de todas as dependências, que são restauradas com o comando `dotnet restore`.

---
