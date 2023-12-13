# Health APP

## Descrição:
O Health APP consiste em uma REST API no contexto da saúde desenvolvida em C# .NET 6 utilizando a arquitetura DDD, IDENTITY e JWT para controle de usuários, SQL SERVER como DB, ENTITY como ORM implementado em conjunto com o AUTOMAPPER, THUNDER CLIENT em testes de unidade, LINQ em consultas e SWAGGER para documentação.

## Processo de desenvolvimento:
1. Construindo toda estrutura do projeto no padrão DDD.
2. Criando as Entities e os Enums com validação de campo.
3. Criando os elementos do Domain: Interfaces e Classe de mensagem de serviço.
4. Criando a Infra: Configuração do contexto do banco de dados, repositórios e métodos CRUD.
5. Criando as WebAPIs.
6. Criando os testes.
7. Dando o contexto do meio da saúde.
8. Criando e refinando as validações.

## Processo Criativo:
A escolha pelo .NET 6 foi devido às experiências que tenho com ele. Já a IDE Visual Studio além de experiência, também pela robustez e por ser mais bem indicada para a linguagem. Ao começar o projeto meu primeiro passo foi definir com clareza os requisitos, padrões e tecnologias a serem utilizados. Como já tenho conhecimento em REST APIs, não foi difícil começar o desenvolvimento.

Decidi criar primeiramente a base do projeto de maneira genérica para as entidades, métodos e a maior parte dos elementos, para então assim fazer seu backup e então modificá-la para o contexto desejado. Meu segundo passo foi começar o desenvolvimento organizando as pastas e arquivos do projeto seguindo o padrão DDD, a melhor maneira de manter um projeto organizado é começá-lo organizado.

Optei por não criar a camada "Application", pois muitas empresas estão deixando de usá-la pela repetição que pode causar, criando assim os controllers diretamente nas WebAPIs. Com tudo definido, o terceiro passo foi construir todos os elementos necessários em cada camada, começando pelas entidades, partindo para o domínio, seguindo para a infraestrutura e por fim as WebAPIs, em sua criação optei por não utilizar o Dapper, pois sou familiarizado com o Linq, então decidi usá-lo, pois o resultado seria o mesmo.

Próximo ao fim do projeto desenvolvi os testes unitários, e ajustei todo o contexto da API para o meio da Saúde, onde a principal entidade afetada pelos métodos CRUD é a de consultas médicas, e então refinei e criei mais validações de usuários, assim como dos métodos.

# Configurações para rodar o código

## NuGet Packages Versions:

### Versão 6.0.7:
- Microsoft.AspNetCore.Identity
- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Microsoft.AspNetCore.Identity.UI
- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.EntityFrameworkCore.Design

### Versão 11.0.1:
- AutoMapper

## Conectando ao banco de dados e ajustes necessários:

### Conectando-se:
1. Abra o SQL Server e copie o endereço do servidor.
2. No Visual Studio, vá para View (na barra superior da IDE) > Server Explorer > Connect to Database (símbolo de tomada com um cilindro ao lado).
3. Cole o endereço do servidor em "Server name".
4. Altere a opção "Encrypt" de "Mandatory" para "Optional".
5. Clique no botão OK.

### Arrumando o contexto:
1. Dentro do Visual Studio, vá para Solution 'HealthAPP' > 3 - Infra > Infrastructure > Configuration > ContextBase.
2. No código, encontre a função "ConnectionString" e siga as instruções comentadas.
3. Vá para Solution 'HealthAPP' > 1 – APIs > WebAPIs > appsettings.json.
4. Siga as instruções comentadas no arquivo.
5. Clique com o botão direito do mouse no arquivo WebAPIs e selecione "Set as Startup Project".

### Atualizando o DB:
1. Abra o console no Visual Studio: Tools (na barra superior da IDE) > NuGet Package Manager > Package Manager Console.
2. No console, altere o "Default project" para "3 – Infra\Infrastructure".
3. Adicione as migrations novamente no console: `Add-Migration Initial -Context ContextBase`.
4. Rode o código no console para atualizar seu banco de dados: `Update-Database -Context ContextBase`.

# Rodando o Projeto

Para rodar o projeto, utilize a tecla “F5” no teclado ou clique no ícone de play (▶ WebAPIs) na segunda barra superior de cima para baixo.

Caso seja solicitada a instalação do certificado, instale-o para indicar ao navegador que a conexão é segura. Pode ser necessário acessar o gerenciador de certificados no Windows, copiar o certificado gerado na área pessoal e colá-lo na área de certificados de confiança.

No Swagger é possível cadastrar um novo usuário com o tipo desejado e gerar um token de uma hora de duração, alguns métodos devem ser testados em um software de teste, como o Insomnia, ou o Thunder Client do Visual Studio Code, pois é necessário adicionar o Token gerado em sua aba de autorização. Os usuários padrão têm permissão apenas para agendar consultas, enquanto os administradores podem realizar todas as operações.
