# ASoftwareAPI
Este repositório é voltado para o back end do projeto ninal da Pós em Desenvolvimento Web FullStask.

O projeto é uma API em C# com Net 8.

Configuração da conexão com o banco de dados:
- Importe as tabelas do banco de dados asoftwarevfisio_usuarios e afoftwarevfisio_clientes;
- Configure a string de conexão no DefaultConnection no arquivo appsettings.js;
  
Build:
- Baixar o Visual Studio 2022;
- Clone o projeto
- No Visual Studio vá em Build faça um Clean e depois um Rebuild;
- Clique no botão play "Start Without Debugging" ou Ctrl + F5;

Documentação:
- Rotas GET:
  * http://localhost:5228/Cliente/TabelaSimples, retorna a lista de clientes com algumas propriedades;
  * http://localhost:5228/Cliente/TabelaDetalhada, retorna a lista de cliente e todos os seus dados;
  * http://localhost:5228/Cliente/2, retorna um cliente por id onde esse id é passado como parâmetro da requisição;
  * http://localhost:5228/Usuario, retorna todos os usuário cadastrados;
  * http://localhost:5228/Usuario/Clientes, retorna todos os usuários com seus clientes;
  * http://localhost:5228/Usuario/4, retorna o usuário por id onde esse id é passado como parâmetro da requisição;
 
- Rotas POST:
  * http://localhost:5228/Cliente/AdicionarCliente, adiciona um novo cliente.
    Exemplo do corpo da requisição:
     {
        "nome": "Jurere",
        "telefone": "83998905014",
        "dataDaConsulta": "2025-01-25T22:00:10.866Z",
        "categoria": "Sessão",
        "valorDaSessao": 500,
        "quantidadeDeSessao": 50,
        "vencimento": "15",
        "situacaoFinanceira": "pago",
        "dataDoCadastro": "2024-09-24T03:38:10.866Z",
        "usuarioId": 1
    }
  * http://localhost:5228/Usuario, adicione um novo usuário.
    Exemplo do corpo da requisição:
      {
        "usuarioId": 0,
        "login": "string",
        "senha": "string",
        "dataDoCadastro": "2024-09-28T18:47:46.145Z",
        "ultimaAtualizacao": "2024-09-28T18:47:46.145Z"
      }
- Rotas PUT:
  * http://localhost:5228/Cliente/AtualizarCliente, rota que atualiza todos os dados do cliente passando o id pelo parêmetro da requisição e os dados no corpo da requisição.
    Exemplo do corpo da requisição:
      {
        "nome": "Jurere Update",
        "telefone": "83998905014",
        "dataDaConsulta": "2025-09-25T02:00:10.866Z",
        "categoria": "Mensal",
        "valorDaSessao": 500,
        "quantidadeDeSessao": 50,
        "desconto": 50,
        "vencimento": "15",
        "situacaoFinanceira": "pago",
        "dataDoCadastro": "2024-09-24T03:38:10.866Z",
        "usuarioId": 1
      }
  * http://localhost:5228/Cliente/AtualizarClienteDataConsulta, rota que atualiza todos os dados do cliente mas com validação de horário, passando o id pelo parêmetro da requisição e os dados no corpo da requisição.
    Exemplo do corpo da requisição:
      {
        "nome": "Jurere Update 2",
        "telefone": "83998905014",
        "dataDaConsulta": "2020-09-25T02:00:10.866Z",
        "categoria": "Mensal",
        "valorDaSessao": 500,
        "quantidadeDeSessao": 50,
        "desconto": 50,
        "vencimento": "15",
        "situacaoFinanceira": "pago",
        "dataDoCadastro": "2024-09-24T03:38:10.866Z",
        "usuarioId": 1
      }
  * http://localhost:5228/Usuario/2, rota que atualiza todos os dados do usuário passando o id pelo parêmetro da requisição e os dados no corpo da requisição.
    Exemplo do corpo da requisição:
      {
        "usuarioId": 0,
        "login": "string",
        "senha": "string",
        "dataDoCadastro": "2024-09-28T18:47:46.145Z",
        "ultimaAtualizacao": "2024-09-28T18:47:46.145Z"
      }
- Rotas DELETE:
  * http://localhost:5228/Cliente/RemoverCliente?id=2, rota que deleta o cliente passando o id como parâmetro da requisição;
  * http://localhost:5228/Usuario/1, rota que deleta o usuário passando o id como parâmetro da requisição.
 
Observação: Para as rotas POST e PUT de cliente há um serviço PaymentService.cs onde é calculado o Valor pago pelo cliente com disconto ou não e o serviço TimeControlService.cs que faz a validação de horários.
Também há um serviço AuthService.cs que valida o login do usuário.

A API está sendo exposta via Swagger.
