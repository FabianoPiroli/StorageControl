# Documentação: Sistema de Controle de Estoque

Esta documentação tem como objetivo explicar a arquitetura, as tecnologias e o funcionamento do projeto de Controle de Estoque desenvolvido para a disciplina de Programação 3.

## 1. Visão Geral e Tecnologias
O sistema foi desenvolvido utilizando as seguintes tecnologias:
- **C# com ASP.NET Core 10:** Framework principal para o desenvolvimento da aplicação web.
- **Arquitetura MVC (Model-View-Controller):** Padrão de projeto que separa a aplicação em três camadas principais: Modelos (Dados), Visões (Telas) e Controladores (Lógica).
- **Entity Framework Core (EF Core):** ORM (Object-Relational Mapper) responsável por mapear as classes em C# para as tabelas no banco de dados.
- **Supabase (PostgreSQL):** Banco de dados em nuvem. Utilizado para facilitar a apresentação do projeto sem a necessidade de um banco local rodando na máquina.
- **Bootstrap 5:** Framework de CSS utilizado nas `Views` para deixar as telas responsivas e com design moderno de forma rápida.

---

## 2. Estrutura de Pastas e Padrão MVC

O projeto está dentro da pasta `ControleEstoque`. Abaixo está a explicação de cada camada:

### Models (Modelos)
Ficam na pasta `/Models`. Eles representam as tabelas no banco de dados e possuem regras de validação (ex: `[Required]`).
- `Categoria.cs`: Usado para agrupar produtos (ex: Eletrônicos, Alimentos).
- `Fornecedor.cs`: Mantém os dados de quem vende os produtos para a sua empresa (Nome, CNPJ, Email, Telefone).
- `Produto.cs`: A entidade principal. Possui informações como Nome, SKU, Preços, Estoque e tem relacionamento obrigatório com `Categoria` e `Fornecedor`.
- `MovimentacaoEstoque.cs`: Registra cada entrada ou saída de um produto.

### Data (Contexto do Banco)
Fica na pasta `/Data`.
- `EstoqueContext.cs`: É a ponte entre a aplicação e o Supabase. É aqui que dizemos ao EF Core quais `Models` devem virar tabelas (usando `DbSet`).

### Controllers (Controladores)
Ficam na pasta `/Controllers`. Eles são os "maestros" do sistema. Quando o usuário clica em um botão, a requisição cai num Controller.
- Exemplo (`ProdutosController.cs`): Tem uma ação (Action) para listar produtos, outra para salvar um novo produto no banco de dados, etc.
- `HomeController.cs`: Responsável pela página inicial (Dashboard), onde ele puxa do banco o total de produtos e alertas de estoque baixo.

### Views (Telas)
Ficam na pasta `/Views`. São as telas escritas em **Razor (HTML + C#)**.
- Elas estão separadas por pastas com o mesmo nome dos Controllers (ex: `/Views/Produtos`).
- **_Layout.cshtml** (dentro de `/Views/Shared`): É o "esqueleto" visual do site. Contém a barra de navegação (menu) e o rodapé. Todas as outras páginas são injetadas dentro dele.

---

## 3. Segurança e Banco de Dados (O Arquivo .env)

Para evitar que a senha do banco de dados fique visível no código-fonte do GitHub, adotamos uma boa prática de segurança:
- A *Connection String* do Supabase está guardada em um arquivo oculto chamado `.env`.
- Esse arquivo foi colocado no `.gitignore`, ou seja, o Git nunca vai enviá-lo para a internet.
- No arquivo `Program.cs`, usamos a biblioteca `DotNetEnv` para ler esse `.env` na hora que o programa inicia e injetar a conexão no sistema.

---

## 4. Fluxo Prático: Como as coisas funcionam juntas?

Se você for testar ou apresentar o projeto, entenda a ordem de dependência dos dados:

1. **Criar Categorias e Fornecedores:**
   Você não consegue criar um Produto se não existir uma Categoria e um Fornecedor antes, pois o Banco de Dados exige esse relacionamento. Comece cadastrando eles.
2. **Criar um Produto:**
   Ao criar um Produto, você informará uma quantidade inicial, um Estoque Mínimo e vinculará a uma Categoria e Fornecedor através de uma caixa de seleção (Dropdown).
3. **Movimentar o Estoque:**
   Sempre que precisar tirar ou colocar mais de um produto no estoque, você deve usar a tela de **Movimentações**. Lá você escolhe o produto, o tipo de movimentação (Entrada/Saída) e a quantidade. 

---

## 5. Como rodar o projeto
Para rodar este projeto na sua máquina ou apresentar para o professor:
1. Abra um terminal (Prompt de Comando ou PowerShell).
2. Entre na pasta principal (onde está o `.sln` ou `.csproj`):
   ```bash
   cd ControleEstoque
   ```
3. Digite o comando de execução:
   ```bash
   dotnet run
   ```
4. O terminal mostrará um endereço, normalmente `http://localhost:5xxx`. Basta clicar ou copiar esse endereço no seu navegador.
