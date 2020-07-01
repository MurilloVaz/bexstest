# Bexs test

### Instalação

A execução do projeto requer docker e bash.

Para facilitar a revisão do projeto, publiquei os containers necessários para execução.

Comandos necessários:


```sh
docker network create bexsnetwork

docker run -p 44380:80 --net bexsnetwork --name bexsrest murillovaz/bexsrest 

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Mumu(#)12345" --name sqlserverbexs --net bexsnetwork -p 1433:1433 murillovaz/sqlserver-bexs
```
É possível executar o projeto a partir de ferramentas de depuração, nesse caso, recomendo usar o VisualStudio com as extensões de dados instaladas.

## Solução

Com meu entendimento do enunciado, vi a obrigatoriedade de persistir/consultar os dados no arquivo input-file.txt. 
Porém, usar um arquivo diretamente desse jeito é extremamente prejudicial para qualquer solução que envolva informações em tempo real ou consumo simultâneo por vários clients. 
Sendo assim, concluí que seria melhor persistir/consultar os dados em uma database e criar um processo de atualização periódico do arquivo txt. 
Por ter mais afinidade com os pacotes ETL do SSIS, decidi usar o SqlServer para o desenvolvimento desse processo (e já que tomei essa decisão, coloquei na prática minha biblioteca open-source, [Noty](https://github.com/MurilloVaz/Noty), para fazer a conexão com o banco de dados).

 - A interface de consulta/inserção rest foi desenvolvida em  .NET Core 3.1, seguindo a Repository-Service Pattern, acredito que tornará a leitura e entendimento do código fonte mais fácil.

 - Também adicionei o Swagger, me poupando o esforço de documentar os endpoints manualmente (levando em consideração que a documentação solicitada foi definida como "simples"). Normalmente, eu mesmo escreveria o yaml ou usaria raml.

 - A interface de consulta por linha de comando, assim como o "start" da aplicação, foram feitos em scripts bash, dado sua pequena complexidade. O arquivo ShellInterface.sh faz requisições para a web API para realizar a consulta das rotas e o Start.sh faz upload do arquivo para um endpoint da API, a API então dá inicio ao pacote ETL que faz o processamento do arquivo.

 - Os testes unitários foram implementados usando XUnit e Mock.

Na realização das consultas, a API busca as informações das rotas da database, cria um lifecycle de cache para essas informações e através da recursividade, busca nessas informações a melhor rota possível (levando em consideração apenas o valor total da viagem).

Diagrama que representa a comunicação entre os módulos da aplicação.

![N|Solid](https://raw.githubusercontent.com/MurilloVaz/bexstest/dev/docs/diagram.png?token=AJKNTWU7MU6MYRIQLB4V6N27AUHS2)

### Importante:

O container de SQLServer não suporta o SSIS atualmente, impossibilitando que os pacotes ETL executem, visto essa adversidade, gravei um [curto vídeo](https://www.youtube.com/watch?v=8Etn6tQXxo4) mostrando a execução dos pacotes (também já deixei a imagem do SqlServer com as tabelas populadas).
