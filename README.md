## API-AspNet-MVC5-MySQL

Exemplo de criação de API Asp.Net MVC5 utilizando banco de dados MySQL.

### O que você vai encontrar neste projeto

- **CRUD** - Conjunto de quatro operações essenciais que permitem a manipulação de dados persistentes (criar, ler, atualizar e excluir).
- **Dicionário de Dados** - Armazenamento de coleções de pares (chave-valor), permitindo busca e recuperação de dados.
- **Swagger** - Ele automatiza a geração da documentação OpenAPI.
  
#### Execução da aplicação

Para executar a aplicação é necessário a execução do Script do MySQL.

##### String de conexão do banco

Modifique a string de conexão no arquivo **Web.config**, no trecho indicado:

```json
...
	connectionString="server=127.0.0.1;userid=root;password=SUASENHA;database=apimvc5;pooling=true;includesecurityasserts=true;AllowUserVariables=True;"
...

```

O script para criação da tabela do exemplo encontra-se na pasta **Database**.
