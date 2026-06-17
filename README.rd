# LeitorDeNotas.ClearArch

Este projeto foi criado para o envio e processamento de notas fiscais de Produto e Serviço.

## Objetivo
- Aplicar persistência de notas fiscais, produtos e valores separados por série e data.
- Calcular estimativas de imposto e lucro/prejuízo.
- Futuramente, evoluir para análise de relatórios de vendas de marketplaces, com taxas de comissão e frete.

## Estrutura da solução
- `src/LeitorDeNotas.ClearArch.Domain`: entidades e interfaces de domínio.
- `src/LeitorDeNotas.ClearArch.Application`: regras de negócio e serviços.
- `src/LeitorDeNotas.ClearArch.Infrastructure`: persistência e DB context.
- `src/LeitorDeNotas.ClearArch.Api`: API para importação de XML e consulta de notas fiscais.
- `src/LeitorDeNotas.ClearArch.WebApp`: aplicação MVC para visualização e notificações em tempo real.
- `src/LeitorDeNotas.ClearArch.IoC`: registro de serviços e dependências.
- `src/LeitorDeNotas.ClearArch.Commons`: classes genéricas e resultados de operação.

## Banco de dados
- O projeto foi preparado para PostgreSQL usando Entity Framework Core.
- Configure a connection string no `appsettings.json` do projeto API.

## Funcionalidades atuais
- Importação de XML de nota fiscal pela API (`POST /api/NotaFiscal/importar`).
- Persistência de notas fiscais e itens no PostgreSQL.
- Consulta de notas fiscais por período.
- WebApp MVC com SignalR para notificação de processamento em lote.

## Próximas evoluções
- Envio de relatório de vendas de marketplaces.
- Análise de taxas de comissão e frete.
- Relatórios consolidados de imposto e lucro/prejuízo.
