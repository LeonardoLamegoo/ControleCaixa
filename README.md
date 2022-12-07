# ControleCaixa
Aplicação para controle de caixa

# Requisitos
  * Visual Studio 2022
  * Asp Net Core
  * .Net 6
  * SQL Server
  
# Passos para a execução da API
  1º Passo: Restaure o backup da base de dados que está na pasta "DataBase-ControleCaixa" dentro da solução, para o SQL Server.<br />
  2º Passo: Dentro do projeto, abra o arquivo appsettings.json, em "ConnectionStrings", altere o "Data Source" para o seu servidor do SQL Server.<br />
  
# EndPoints
  1º Passo: Para utilizar a API, depure o projeto "ControleCaixaAPI", após isso se abrirá a interface do Swagger.
  2º Passo: Para utilizar um EndPoint clique sobre o mesmo, clique em "Try it out", por fim insira os parametros (Caso exista, como descrito abaixo) e clique em "Execute".
  
  Lancamento: EndPoint para inserir um lançamento na base de dados, utilize o padrão a seguir:
  
  "valor" : "DIGITE UM VALOR DECIMAL REFERENTE AO VALOR DO LANÇAMENTO"
  "tipoPagamento" : "DIGITE UM NUMERO INTEIRO QUE TERÁ DE SER  0 = Crédito, 1 = Débito e 2 = Dinheiro"
  "data" : "DIGITE A DATA DO LANCAMENTO NO MODELO dd/MM/yyyy"
  
  DownloadRelatorioDiario: EndPoint para baixar um relátorio de todos os lançamentos cuja data é referente ao mesmo dia que o EndPoint é solicitado.
  
  
# Algumas Bibliotecas utilizadas:

  Autofac : Injeção de dependências
  XUnit : Teste unitário
  NPOI : Auxilio na geração do relatório
  
