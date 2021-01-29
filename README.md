# IFood.Produtos.Application

Este é o projeto do WebService principal utilizado pelo IFood.Produtos.FrontEnd

## Instalação

## Configuração da ConnectionString

Para definir uma Conexão com o Banco de Dados, será necessário acessar o arquivo de configuração <b>appsettings</b> disponível em IFood.Produtos.Application/Infra/Environment

## Inicializando o Banco de Dados via Migrations

Para criar as tabelas do Banco de Dados será necessário a abertura do Packager Manage Console. Quando o console for aberto selecione o Projeto IFood.Produtos.Infra.Data como Default, logo em seguida digite o seguinte comando

```
:: Roda o Script de Migrations no BD configurado

Update-DataBase -Verbose
```



