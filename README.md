# IFood.Produtos.Application

Este é o projeto do WebService principal utilizado pelo IFood.Produtos.FrontEnd

## Instalação

## Configuração da ConnectionString

Para definir uma Conexão com o Banco de Dados, será necessário acessar o arquivo de configuração <b>appsettings</b> disponível em <b>IFood.Produtos.Application/Infra/Environment</b>

## Inicializando o Banco de Dados via Migrations

Para criar as tabelas do Banco de Dados será necessário a abertura do <b>Packager Manage Console</b>. Quando o console for aberto selecione o Projeto <b>IFood.Produtos.Infra.Data</b> como Default, logo em seguida digite o seguinte comando:

```
:: Roda o Script de Migrations no BD configurado

Update-DataBase -Verbose
```



