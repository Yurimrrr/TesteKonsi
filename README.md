
# Teste back-end Konsi

Essa documentação abrange a instalação e o funcionamento do projeto da Konsi onde foi construido uma API que efetua requisições à uma API externa para obter dados de beneficios de CPF's.

Foi utilizado RabbitMq para preencher a fila de CPF's que seriam pesquisados nessa API.

Foi utilizado o Redis também para que os dados recuperados sejam persistidos em cache para que, caso o CPF se repita, não haja necessidade da busca na API do terceiro, poupando processamento.

Esses dados recuperados são persistidos no ElasticSearch para serem exibidos no FrontEnd do projeto que utiliza um endpoint para acessar os dados persistidos no mesmo.



## Instalação

Para rodar o back-end é necessário o .NET 8 SDK, e Docker para construir o container dos serviços utilizados pela API.

Os comandos levam em conta que esteja na raiz do projeto e a navegação segue os passos.

### Criar os serviços
```bash
  docker-compose -f testekonsi-compose.yaml up -d
```

### Iniciar a API
```bash
  cd ./TesteKonsi.WebApi
  dotnet build
  dotnet run
```

Em outro terminal também partindo da raiz:
### Iniciar o Front-end
```bash
  cd ./TesteKonsi.FrontEnd/teste-konsi
  npm install
  npm start
```

Agora no campo de pesquisa disponibilizado na interface, fazer as pesquisas com os seguintes CPF's:
- 343.228.350-40
- 869.230.000-41
- 568.946.870-30
- 433.510.120-12
- 415.022.590-79
