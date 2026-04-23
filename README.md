# 🧪 MinhasFinanças — Projeto de Testes

Repositório contendo a suíte completa de testes do sistema **MinhasFinanças**, um controle de gastos residenciais. Este projeto foi desenvolvido como parte de um teste técnico para a vaga de **Analista de Qualidade de Software**.

---

## 📐 Estratégia de Testes — Pirâmide

```
          /\
         /E2E\
        /------\
       /Integração\
      /------------\
     /   Unitários   \
    /------------------\
```

A pirâmide foi estruturada em três camadas:

- **Unitários** — Testes isolados das regras de negócio do domínio (C# + xUnit) e funções utilitárias do frontend (Vitest)
- **Integração** — Testes das APIs REST via HTTP (C# + xUnit + FluentAssertions)
- **E2E** — Testes de fluxo completo pelo navegador (Playwright + TypeScript)

---

## 🗂️ Estrutura do Repositório

```
ExameQA/
├── unit/
│   └── MinhasFinancas.Testes/
│       ├── Backend/
│       │   └── MinhasFinancas.Testes/
│       │       ├── CategoriaTestes.cs
│       │       ├── PessoaTestes.cs
│       │       └── TransacoesTestes.cs
│       └── FrontEnd/
│           └── tests/
│               ├── formatters.test.js
│               └── schemas.test.js
├── integration/
│   └── MinhasFinancas.IntegrationTestes/
│       ├── BaseIntegrationTest.cs
│       ├── CategoriasIntegrationTestes.cs
│       ├── PessoasIntegrationTestes.cs
│       ├── TotaisIntegrationTestes.cs
│       └── TransacoesIntegrationTestes.cs
├── e2e/
│   └── tests/e2e/
│       ├── dashboard.spec.ts
│       ├── pessoas.spec.ts
│       ├── categorias.spec.ts
│       └── transacao.spec.ts
└── README.md
```

---

## ⚙️ Pré-requisitos

Antes de rodar os testes, certifique-se de que a aplicação está rodando via Docker:

```bash
docker compose up -d
```

Verifique se os containers estão ativos:

```bash
docker ps
```

Você deverá ver:
- **Frontend** rodando em `http://localhost:5173`
- **API** rodando em `http://localhost:5000`

---

## 🚀 Como Rodar os Testes

### 🔵 Testes Unitários — Backend (C# + xUnit)

```bash
cd unit/MinhasFinancas.Testes/Backend/MinhasFinancas.Testes
dotnet test
```

### 🟡 Testes Unitários — Frontend (Vitest)

```bash
cd unit/MinhasFinancas.Testes/FrontEnd
npm install
npm test
```

### 🟠 Testes de Integração (C# + xUnit)

> ⚠️ A aplicação deve estar rodando antes de executar os testes de integração.

```bash
cd integration/MinhasFinancas.IntegrationTestes
dotnet test
```

### 🟢 Testes E2E (Playwright)

> ⚠️ A aplicação deve estar rodando antes de executar os testes E2E.

```bash
cd e2e
npx playwright test
```

Para rodar com o navegador visível:

```bash
npx playwright test --headed
```

Para rodar um arquivo específico:

```bash
npx playwright test dashboard.spec.ts --headed
```

Para ver o relatório HTML após os testes:

```bash
npx playwright show-report
```

---

## 📊 Cobertura de Testes

### Unitários — Backend

| Teste | Regra Validada |
|-------|---------------|
| `Categoria_Despesa_DevePermitirTipoDespesa` | Categoria Despesa aceita transação Despesa |
| `Categoria_Receita_DevePermitirTipoReceita` | Categoria Receita aceita transação Receita |
| `Categoria_Ambas_DevePermitirTipoDespesa` | Categoria Ambas aceita transação Despesa |
| `Categoria_Ambas_DevePermitirTipoReceita` | Categoria Ambas aceita transação Receita |
| `Categoria_Despesa_DeveRejeitarTipoReceita` | Categoria Despesa rejeita transação Receita |
| `Categoria_Receita_DeveRejeitarTipoDespesa` | Categoria Receita rejeita transação Despesa |
| `Pessoa_MenorDeIdade_DeveRetornarFalse` | Menor de 18 anos retorna falso para maioridade |
| `Pessoa_MaiorDeIdade_DeveRetornarTrue` | Maior de 18 anos retorna verdadeiro para maioridade |

### Unitários — Frontend

| Teste | Regra Validada |
|-------|---------------|
| `deve formatar valor positivo corretamente` | Formatação monetária positiva |
| `deve formatar valor zero corretamente` | Formatação monetária com zero |
| `deve formatar valor negativo corretamente` | Formatação monetária negativa |
| `deve rejeitar nome vazio` | Validação de nome obrigatório |
| `deve rejeitar nome com mais de 200 caracteres` | Validação de tamanho máximo |
| `deve rejeitar descrição vazia` | Validação de descrição obrigatória |
| `deve rejeitar valor negativo` | Validação de valor positivo |
| `deve rejeitar valor zero` | Validação de valor maior que zero |

### Integração

| Módulo | Testes | Passando |
|--------|--------|---------|
| Pessoas | 13 | 10 ✅ / 3 🔴 |
| Categorias | 8 | 7 ✅ / 1 🔴 |
| Transações | 12 | 9 ✅ / 3 🔴 |
| Totais | 4 | 4 ✅ |

### E2E

| Módulo | Testes |
|--------|--------|
| Dashboard | 4 ✅ |
| Pessoas | 9 ✅ |
| Categorias | 6 ✅ |
| Transações | 8 ✅ |

---

## 🐛 Bugs Encontrados

### Bug 01 — DELETE Pessoa com ID inexistente retorna 204
- **Módulo:** Pessoas
- **Endpoint:** `DELETE /api/v1/Pessoas/{id}`
- **Comportamento esperado:** `404 Not Found`
- **Comportamento encontrado:** `204 No Content`
- **Regra violada:** A API deve validar a existência do recurso antes de retornar sucesso
- **Severidade:** Alta
- **Detectado em:** Testes de Integração

### Bug 02 — Exclusão em cascata não funciona ao deletar pessoa
- **Módulo:** Pessoas / Transações
- **Endpoint:** `DELETE /api/v1/Pessoas/{id}`
- **Comportamento esperado:** Transações vinculadas à pessoa devem ser removidas
- **Comportamento encontrado:** Transações permanecem após exclusão da pessoa
- **Regra violada:** Exclusão em cascata de transações ao excluir pessoa
- **Severidade:** Alta
- **Detectado em:** Testes de Integração

### Bug 03 — DELETE Pessoa inexistente retorna 204
- **Módulo:** Pessoas
- **Endpoint:** `DELETE /api/v1/Pessoas/{id}`
- **Comportamento esperado:** `404 Not Found`
- **Comportamento encontrado:** `204 No Content`
- **Regra violada:** A API deve retornar 404 para recursos inexistentes
- **Severidade:** Alta
- **Detectado em:** Testes de Integração

### Bug 04 — Criar categoria sem finalidade retorna 201
- **Módulo:** Categorias
- **Endpoint:** `POST /api/v1/Categorias`
- **Comportamento esperado:** `400 Bad Request`
- **Comportamento encontrado:** `201 Created`
- **Regra violada:** Finalidade é campo obrigatório para categoria
- **Severidade:** Média
- **Detectado em:** Testes de Integração

### Bug 05 — Regras de negócio de transação retornam 500 em vez de 400
- **Módulo:** Transações
- **Endpoint:** `POST /api/v1/Transacoes`
- **Cenários afetados:**
  - Receita com categoria Despesa → retorna `500` (esperado `400`)
  - Despesa com categoria Receita → retorna `500` (esperado `400`)
  - Receita para menor de idade → retorna `500` (esperado `400`)
- **Regra violada:** A API deve tratar exceções de negócio e retornar 400, não expor erros internos com 500
- **Severidade:** Crítica
- **Detectado em:** Testes de Integração

---

## 📝 Bugs Encontrados Manualmente

### Bug 06 — Campos Categoria e Pessoa não exibidos na lista de Transações
- **Módulo:** Transações
- **Página:** `/transacoes`
- **Comportamento esperado:** Colunas Categoria e Pessoa exibem os dados vinculados à transação
- **Comportamento encontrado:** Colunas Categoria e Pessoa aparecem vazias na tabela
- **Severidade:** Alta
- **Detectado em:** Teste Manual

### Bug 07 — Valores monetários quebram o layout quando muito grandes
- **Módulo:** Dashboard
- **Página:** `/`
- **Comportamento esperado:** Valores monetários devem se adaptar ao espaço disponível no card
- **Comportamento encontrado:** Valores acima de 14 casas decimais ultrapassam os limites do card e quebram o layout
- **Severidade:** Média
- **Detectado em:** Teste Manual

### Bug 08 — Gráfico de Resumo Mensal não é exibido
- **Módulo:** Dashboard
- **Página:** `/`
- **Comportamento esperado:** Gráfico de pizza exibindo o resumo mensal por categoria
- **Comportamento encontrado:** Área do gráfico aparece vazia, apenas a legenda é exibida sem o gráfico
- **Severidade:** Alta
- **Detectado em:** Teste Manual

### Bug 09 — Logo "Minhas Finanças" não possui ação de navegação
- **Módulo:** Header
- **Página:** Todas
- **Comportamento esperado:** Clicar na logo redireciona para o Dashboard
- **Comportamento encontrado:** Logo possui efeito visual mas não executa nenhuma ação ao ser clicada
- **Severidade:** Baixa
- **Detectado em:** Teste Manual

### Bug 10 — Layout do Resumo Mensal quebrado
- **Módulo:** Dashboard
- **Página:** `/`
- **Comportamento esperado:** Seção de Resumo Mensal exibida corretamente com texto e gráfico
- **Comportamento encontrado:** Layout da seção está quebrado, elementos fora de posição
- **Severidade:** Média
- **Detectado em:** Teste Manual

### Bug 11 — Espaçamento inconsistente nas colunas das tabelas
- **Módulo:** Geral
- **Página:** Todas com tabelas
- **Comportamento esperado:** Colunas com espaçamento uniforme e consistente
- **Comportamento encontrado:** Espaçamento entre colunas é aleatório, algumas maiores que outras sem padrão
- **Severidade:** Baixa
- **Detectado em:** Teste Manual

### Bug 12 — Site não é responsivo para dispositivos mobile
- **Módulo:** Geral
- **Página:** Todas
- **Comportamento esperado:** Layout adaptado para diferentes tamanhos de tela incluindo mobile
- **Comportamento encontrado:** Site não é compatível com dispositivos móveis, layout quebrado em telas menores
- **Severidade:** Alta
- **Detectado em:** Teste Manual

### Bug 13 — Mensagens de erro persistem após correção dos campos na transação
- **Módulo:** Transações
- **Página:** `/transacoes`
- **Comportamento esperado:** Mensagens de erro desaparecem ao corrigir os campos inválidos
- **Comportamento encontrado:** Ao pressionar Enter sem preencher os campos, todos os alertas são disparados. Mesmo após preencher os dados corretamente as mensagens de erro permanecem visíveis
- **Severidade:** Média
- **Detectado em:** Teste Manual

---

## 🧠 Justificativa das Escolhas

### Por que essa pirâmide?

A pirâmide foi construída priorizando testes de menor custo e maior isolamento na base, e testes de maior cobertura de fluxo no topo:

- **Unitários no backend** testam diretamente as entidades do domínio (`Categoria.PermiteTipo`, `Pessoa.EhMaiorDeIdade`), garantindo que as regras de negócio mais críticas estão corretas de forma isolada e rápida.

- **Unitários no frontend** testam funções utilitárias (`formatCurrency`, `formatDate`) e schemas de validação (`pessoaSchema`, `categoriaSchema`, `transacaoSchema`), garantindo que o frontend valida os dados corretamente antes de enviar à API.

- **Testes de integração** validam os endpoints da API de ponta a ponta, incluindo as 3 regras de negócio principais, e foram onde a maioria dos bugs foi encontrada.

- **Testes E2E** garantem que o usuário consegue realizar os fluxos principais pelo navegador, cobrindo 27 cenários reais de uso.

### Limitação identificada

As propriedades `Categoria` e `Pessoa` da entidade `Transacao` possuem `internal set`, impedindo atribuição direta nos testes unitários fora do assembly. Por isso, as regras de negócio de transação foram cobertas exclusivamente nos testes de integração via API, onde os bugs 05 foram identificados.

---
Entendido! Apenas os testes que realmente implementamos:

---
Implementados
📋 Cenários de Testes Implementados

### 🔵 Back-End — Integração

#### 👤 Pessoas

| Cenário | Tipo | Resultado Esperado | Status |
|---------|------|-------------------|--------|
| [POST][201] Verificar o retorno ao criar pessoa com dados válidos | Sucesso | Retorna 201 com objeto da pessoa criada | ✅ Done |
| [GET][200] Verificar o retorno ao buscar todas as pessoas | Sucesso | Retorna 200 com lista de pessoas | ✅ Done |
| [GET][200] Verificar o retorno ao buscar pessoa por ID existente | Sucesso | Retorna 200 com dados da pessoa | ✅ Done |
| [PUT][204] Verificar o retorno ao atualizar pessoa com dados válidos | Sucesso | Retorna 204 com dados atualizados | ✅ Done |
| [PUT][400] Verificar o retorno ao atualizar com campo Nome vazio | Erro | Retorna 400 com campo obrigatório | ✅ Done |
| [PUT][400] Verificar o retorno ao atualizar com campo DataNascimento vazio | Erro | Retorna 400 com campo obrigatório | ✅ Done |
| [PUT][404] Verificar o retorno ao utilizar pessoa com Id Inválido | Erro | Retorna 404 Id Inválido | ✅ Done |
| [DEL][204] Verificar o retorno ao deletar pessoa existente | Sucesso | Retorna 204 e pessoa é removida | 🔴 Falhando |
| [DEL][200] Verificar se transações são removidas em cascata ao deletar pessoa | Alternativo | Retorna 200 e transações vinculadas são excluídas | 🔴 Falhando |
| [POST][400] Verificar o retorno ao criar pessoa com nome vazio | Erro | Retorna 400 com mensagem de validação | ✅ Done |
| [POST][400] Verificar o retorno ao criar pessoa sem data de nascimento | Erro | Retorna 400 com campo obrigatório | ✅ Done |
| [GET][404] Verificar o retorno ao buscar pessoa por ID inexistente | Erro | Retorna 404 | ✅ Done |
| [DEL][404] Verificar o retorno ao deletar pessoa inexistente | Erro | Retorna 404 | 🔴 Falhando |

#### 🏷️ Categorias

| Cenário | Tipo | Resultado Esperado | Status |
|---------|------|-------------------|--------|
| [POST][201] Verificar o retorno ao criar categoria do tipo Receita | Sucesso | Retorna 201 com categoria criada | ✅ Done |
| [POST][201] Verificar o retorno ao criar categoria do tipo Despesa | Sucesso | Retorna 201 com categoria criada | ✅ Done |
| [POST][201] Verificar o retorno ao criar categoria do tipo Ambas | Sucesso | Retorna 201 com categoria criada | ✅ Done |
| [GET][200] Verificar o retorno ao buscar todas as categorias | Sucesso | Retorna 200 com lista de categorias | ✅ Done |
| [GET][200] Verificar o retorno ao buscar categoria por ID existente | Sucesso | Retorna 200 com dados da categoria | ✅ Done |
| [POST][400] Verificar o retorno ao criar categoria sem descrição | Erro | Retorna 400 com mensagem de validação | ✅ Done |
| [POST][400] Verificar o retorno ao criar categoria sem finalidade | Erro | Retorna 400 com campo obrigatório | 🔴 Falhando |
| [GET][404] Verificar o retorno ao buscar categoria por ID inexistente | Erro | Retorna 404 | ✅ Done |

#### 💸 Transações

| Cenário | Tipo | Resultado Esperado | Status |
|---------|------|-------------------|--------|
| [POST][201] Verificar o retorno ao criar transação de Receita com categoria Receita | Sucesso | Retorna 201 com transação criada | ✅ Done |
| [POST][201] Verificar o retorno ao criar transação de Despesa com categoria Despesa | Sucesso | Retorna 201 com transação criada | ✅ Done |
| [POST][201] Verificar o retorno ao criar transação com categoria Ambas | Sucesso | Retorna 201 com transação criada | ✅ Done |
| [POST][201] Verificar o retorno ao criar Despesa para menor de idade | Alternativo | Retorna 201 — despesa permitida para menor | ✅ Done |
| [GET][200] Verificar o retorno ao buscar todas as transações | Sucesso | Retorna 200 com lista de transações | ✅ Done |
| [GET][200] Verificar o retorno ao buscar transação por ID existente | Sucesso | Retorna 200 com dados da transação | ✅ Done |
| [POST][400] Verificar o retorno ao criar Receita com categoria Despesa | Erro | Retorna 400 — incompatibilidade de categoria | 🔴 Falhando |
| [POST][400] Verificar o retorno ao criar Despesa com categoria Receita | Erro | Retorna 400 — incompatibilidade de categoria | 🔴 Falhando |
| [POST][400] Verificar o retorno ao criar Receita para menor de idade | Erro | Retorna 400 — menor não pode ter receita | 🔴 Falhando |
| [POST][400] Verificar o retorno ao criar transação sem valor | Erro | Retorna 400 com campo obrigatório | ✅ Done |
| [POST][400] Verificar o retorno ao criar transação sem pessoa vinculada | Erro | Retorna 400 com mensagem de validação | ✅ Done |
| [GET][404] Verificar o retorno ao buscar transação por ID inexistente | Erro | Retorna 404 | ✅ Done |

#### 📊 Totais

| Cenário | Tipo | Resultado Esperado | Status |
|---------|------|-------------------|--------|
| [GET][200] Verificar o retorno dos totais por pessoa com transações | Sucesso | Retorna 200 com receitas, despesas e saldo | ✅ Done |
| [GET][200] Verificar o retorno dos totais por categoria com transações | Sucesso | Retorna 200 com totais por categoria | ✅ Done |
| [GET][200] Verificar o retorno dos totais de pessoa sem transações | Alternativo | Retorna 200 com valores zerados | ✅ Done |
| [GET][200] Verificar o retorno do saldo negativo quando despesas superam receitas | Alternativo | Retorna 200 com saldo negativo correto | ✅ Done |

---

### 🟢 Front-End — E2E (Playwright)

#### 🏠 Dashboard

| Cenário | Tipo | Resultado Esperado | Status |
|---------|------|-------------------|--------|
| [WEB] Verificar se o Dashboard exibe Saldo Atual, Receitas e Despesas do Mês | Sucesso | Três cards com valores visíveis | ✅ Done |
| [WEB] Verificar se as últimas transações são exibidas na tabela | Sucesso | Tabela com Data, Descrição, Categoria e Valor | ✅ Done |
| [WEB] Verificar se o menu de navegação contém todos os links | Sucesso | Dashboard, Transações, Categorias, Pessoas, Relatórios | ✅ Done |
| [WEB] Verificar se o link 'Ver Todas' redireciona para Transações | Alternativo | Navega para /transacoes | ✅ Done |

#### 👤 Pessoas

| Cenário | Tipo | Resultado Esperado | Status |
|---------|------|-------------------|--------|
| [WEB] Verificar se a lista de pessoas é exibida corretamente | Sucesso | Tabela com Nome, Data de Nascimento, Idade e Ações | ✅ Done |
| [WEB] Verificar se o botão 'Adicionar Pessoa' abre o modal | Sucesso | Modal com campos Nome e Data de Nascimento | ✅ Done |
| [WEB] Verificar se é possível cadastrar uma pessoa com dados válidos | Sucesso | Pessoa aparece na lista após salvar | ✅ Done |
| [WEB] Verificar se o botão 'Editar' abre o modal com dados preenchidos | Alternativo | Modal abre com nome e data já preenchidos | ✅ Done |
| [WEB] Verificar se é possível deletar uma pessoa | Alternativo | Pessoa é removida da lista | ✅ Done |
| [WEB] Verificar se salvar com nome vazio exibe erro | Erro | Exibe mensagem de campo obrigatório | ✅ Done |
| [WEB] Verificar se o botão 'Cancelar' fecha o modal sem salvar | Alternativo | Modal fecha sem alterar a lista | ✅ Done |
| [WEB] Verificar paginação na lista de pessoas | Alternativo | Navega entre páginas corretamente | ✅ Done |
| [WEB] Verificar se salvar com data de nascimento vazia exibe erro | Erro | Exibe mensagem de campo obrigatório | ✅ Done |

#### 🏷️ Categorias

| Cenário | Tipo | Resultado Esperado | Status |
|---------|------|-------------------|--------|
| [WEB] Verificar se a lista de categorias exibe Descrição e Finalidade | Sucesso | Tabela com dados corretos | ✅ Done |
| [WEB] Verificar se o botão 'Adicionar Categoria' abre o modal | Sucesso | Modal com campos Descrição e Finalidade | ✅ Done |
| [WEB] Verificar se é possível criar categoria do tipo Receita | Sucesso | Categoria aparece na lista com finalidade Receita | ✅ Done |
| [WEB] Verificar se é possível criar categoria do tipo Despesa | Sucesso | Categoria aparece na lista com finalidade Despesa | ✅ Done |
| [WEB] Verificar se é possível criar categoria do tipo Ambas | Sucesso | Categoria aparece na lista com finalidade Ambas | ✅ Done |
| [WEB] Verificar se salvar sem descrição exibe erro | Erro | Exibe mensagem de campo obrigatório | ✅ Done |

#### 💸 Transações

| Cenário | Tipo | Resultado Esperado | Status |
|---------|------|-------------------|--------|
| [WEB] Verificar se a lista de transações é exibida corretamente | Sucesso | Tabela com Data, Descrição, Valor, Tipo, Categoria e Pessoa | ✅ Done |
| [WEB] Verificar se o botão 'Adicionar Transação' abre o modal | Sucesso | Modal com todos os campos disponíveis | ✅ Done |
| [WEB] Verificar se é possível criar uma transação de Despesa válida | Sucesso | Transação aparece na lista após salvar | ✅ Done |
| [WEB] Verificar se é possível criar uma transação de Receita válida | Sucesso | Transação aparece na lista após salvar | ✅ Done |
| [WEB] Verificar se o campo Pessoa pesquisa corretamente | Alternativo | Lista filtra conforme digitação | ✅ Done |
| [WEB] Verificar se o campo Categoria pesquisa corretamente | Alternativo | Lista filtra conforme digitação | ✅ Done |
| [WEB] Verificar se salvar sem valor exibe erro | Erro | Exibe mensagem de campo obrigatório | ✅ Done |
| [WEB] Verificar paginação na lista de transações | Alternativo | Navega entre páginas corretamente | ✅ Done |

---

### 🟡 Unitários — Backend

#### 🏷️ CategoriaTestes.cs

| Cenário | Tipo | Resultado Esperado | Status |
|---------|------|-------------------|--------|
| Categoria Despesa deve permitir tipo Despesa | Sucesso | Retorna true | ✅ Done |
| Categoria Receita deve permitir tipo Receita | Sucesso | Retorna true | ✅ Done |
| Categoria Ambas deve permitir tipo Despesa | Sucesso | Retorna true | ✅ Done |
| Categoria Ambas deve permitir tipo Receita | Sucesso | Retorna true | ✅ Done |
| Categoria Despesa deve rejeitar tipo Receita | Erro | Retorna false | ✅ Done |
| Categoria Receita deve rejeitar tipo Despesa | Erro | Retorna false | ✅ Done |

#### 👤 PessoaTestes.cs

| Cenário | Tipo | Resultado Esperado | Status |
|---------|------|-------------------|--------|
| Pessoa menor de idade deve retornar false | Erro | Retorna false | ✅ Done |
| Pessoa maior de idade deve retornar true | Sucesso | Retorna true | ✅ Done |

---

### 🟡 Unitários — Frontend

#### formatters.test.js

| Cenário | Tipo | Resultado Esperado | Status |
|---------|------|-------------------|--------|
| Deve formatar valor positivo corretamente | Sucesso | Contém 100,00 | ✅ Done |
| Deve formatar valor zero corretamente | Sucesso | Contém 0,00 | ✅ Done |
| Deve formatar valor negativo corretamente | Sucesso | Contém -R$ e 100,00 | ✅ Done |

#### schemas.test.js

| Cenário | Tipo | Resultado Esperado | Status |
|---------|------|-------------------|--------|
| Deve rejeitar nome vazio | Erro | success = false | ✅ Done |
| Deve rejeitar nome com mais de 200 caracteres | Erro | success = false | ✅ Done |
| Deve rejeitar descrição vazia | Erro | success = false | ✅ Done |
| Deve rejeitar valor negativo | Erro | success = false | ✅ Done |
| Deve rejeitar valor zero | Erro | success = false | ✅ Done |
---

## 🛠️ Tecnologias Utilizadas

| Camada | Tecnologia |
|--------|-----------|
| Unitários Backend | C# + .NET 9 + xUnit |
| Unitários Frontend | Vitest + JavaScript |
| Integração | C# + .NET 9 + xUnit + FluentAssertions |
| E2E | Playwright + TypeScript |
