# Documentação para o Projeto de Conclusão

## 1. Introdução

* **Nome do Projeto:** Plataforma da Comunidade de Jogos Indie
* **Breve descrição do projeto:** A aplicação é uma plataforma web desenvolvida para centralizar o catálogo de jogos (como *The Binding of Isaac*, *Hollow Knight*, *Terraria*) e permitir que os jogadores consultem, criem e gerenciem guias e tutoriais. Ela resolve o problema da dispersão de informações e dicas de jogos em fóruns genéricos, oferecendo um espaço focado e intuitivo tanto para jogadores casuais quanto para criadores de conteúdo.
* **Motivação para a escolha do projeto:** Unir o interesse pelo ecossistema de jogos independentes ao desenvolvimento de uma aplicação full-stack moderna, aplicando padrões de arquitetura no backend (.NET 9) e boas práticas de interface de usuário no frontend (React).

---

## 2. Tecnologias Utilizadas

* **Linguagens de programação:** C# (.NET 9), JavaScript (ES6+), HTML5, CSS3.
* **Frameworks e bibliotecas:**
  * **Backend:** ASP.NET Core Web API, Entity Framework Core (EF Core), JWT (JSON Web Token) para autenticação.
  * **Frontend:** React, React Router DOM (navegação), Axios (integração de APIs), CSS Modules (estilização modular).
* **Banco de dados:** SQLite (`app.db`), com persistência auxiliar no cliente via `localStorage` (para a sessão de autenticação e lista rápida de "Guias Salvos").
* **Ferramentas de desenvolvimento:** Visual Studio Code, Swagger UI (documentação e testes da API), DB Browser / SQLite Viewer, Git.

---

## 3. Requisitos do Sistema

### Requisitos Funcionais (RF):

* **RF01 - Autenticação de Usuários:** O sistema deve permitir que os usuários se cadastrem, realizem login e recebam um token JWT.
* **RF02 - Visualização do Catálogo de Jogos:** O sistema deve exibir uma lista de jogos na página principal com título, descrição e capa.
* **RF03 - Detalhes do Jogo e Listagem de Guias:** O sistema deve exibir as informações detalhadas de um jogo e listar todos os guias associados a ele.
* **RF04 - Publicação de Guias:** Usuários autenticados devem conseguir criar e publicar novos guias vinculados a um jogo específico.
* **RF05 - Salvamento Local de Guias:** O usuário deve conseguir salvar guias para acesso rápido via `localStorage`.
* **RF06 - Perfil do Usuário:** O sistema deve exibir o perfil do usuário logado, dividindo a visualização entre "Meus Guias Publicados" (carregados via API) e "Guias Salvos" (carregados via `localStorage`).

---

## 4. Histórias de Usuário

* **US01:** Como **jogador**, eu quero **visualizar o catálogo de jogos na página inicial** para que eu possa **encontrar os jogos sobre os quais quero ler guias**.
* **US02:** Como **jogador**, eu quero **filtrar e ler guias específicos de um jogo** para que eu possa **superar desafios no jogo**.
* **US03:** Como **criador de conteúdo**, eu quero **criar e publicar um guia** para que eu possa **compartilhar minhas estratégias com a comunidade**.
* **US04:** Como **usuário registrado**, eu quero **salvar guias do meu interesse** para que eu possa **consultá-los rapidamente na minha página de perfil**.
* **US05:** Como **usuário**, eu quero **fazer login com segurança na plataforma** para que minhas **informações e conteúdos publicados fiquem vinculados à minha conta**.

---

## 5. Plano de Desenvolvimento

* **Etapa 1 - Modelagem e Backend (.NET 9):**
  * Configuração do projeto .NET 9 Web API e banco SQLite (`app.db`).
  * Criação das entidades (`Usuario`, `Jogo`, `Guia`), DTOs (`GuiaCriacaoDTO`, `GuiaRespostaDTO`) e Controllers com suporte a JWT.
* **Etapa 2 - Frontend e Integração (React):**
  * Estruturação dos componentes de UI (`TopBar`, `JogoDetalhes`, `Perfil`).
  * Configuração do Axios com interceptors de token e gerenciamento de estado.
* **Etapa 3 - Estilização e Refinamento de UX:**
  * Implementação de layouts modernos em CSS Modules (Glassmorphism e cards compactos).
  * Otimização do salvamento de guias e persistência de sessão com `localStorage`.
* **Etapa 4 - Testes e População do Banco:**
  * Testes end-to-end de integração API/Frontend e inserção do catálogo inicial de jogos e guias.

---

## 6. Conclusão e Próximos Passos

A aplicação atingiu com sucesso seu objetivo principal, entregando uma plataforma estável, responsiva e funcional com separação clara de responsabilidades entre cliente e servidor.

* **Melhorias Futuras:**
  1. **Sistema de Comentários e Avaliações:** Implementar a funcionalidade de comentários e curtidas em cada guia (mapeadas conceitualmente no backend).
  2. **Persistência de Guias Salvos no Banco:** Migrar o armazenamento de "Guias Salvos" do `localStorage` para uma tabela relacional no SQLite com suporte a sincronização entre múltiplos dispositivos.
  3. **Mídia e Imagens:** Suporte para upload direto de imagens e vídeos nos guias de usuários.