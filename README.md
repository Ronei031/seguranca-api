🔐 API de Segurança  
Este projeto é uma API de segurança robusta desenvolvida em C# e .NET, projetada para gerenciar autenticação, controle de acesso e auditoria em aplicações.

📊 Modelagem de Dados  
A modelagem de dados da API é baseada em várias entidades principais que garantem a segurança e a integridade dos dados:

Usuário: Representa os usuários da aplicação, com informações como nome de usuário, e-mail, senha (armazenada de forma segura), e data de criação. Relaciona-se com outras entidades para gerenciar funções, permissões e tokens de acesso.

Role: Define as funções (ou papéis) que podem ser atribuídas a um usuário, como "Admin", "User", etc. As roles são utilizadas para agrupar permissões específicas que controlam o que os usuários podem fazer na aplicação.

Permissão: Especifica as permissões que podem ser atribuídas às roles. Cada permissão representa uma ação ou conjunto de ações que podem ser executadas dentro do sistema.

Relacionamentos Usuário-Role e Role-Permissão: Usuários podem ter múltiplas roles, e roles podem ter múltiplas permissões, criando uma estrutura flexível e escalável para o controle de acesso.

Token de Acesso: Gerencia tokens de autenticação associados a usuários, incluindo informações como data de criação, data de expiração, e status de revogação. Esses tokens são essenciais para a implementação de autenticação baseada em tokens (como JWT).

Log de Auditoria: Mantém um registro detalhado das ações realizadas pelos usuários, incluindo informações sobre quem realizou a ação, o que foi feito, quando e de onde (IP), facilitando o rastreamento e a auditoria de eventos.

Configurações de Segurança: Armazena políticas de segurança, como requisitos de senha, tentativas de login falhas e configurações de autenticação de dois fatores, permitindo que a segurança da aplicação seja configurada e ajustada conforme necessário.

🔗 Relacionamentos e Considerações  
Usuário-Role: Um usuário pode ter várias roles, permitindo uma definição granular de acesso.
Role-Permissão: As roles podem ser compostas de múltiplas permissões, permitindo um controle detalhado sobre o que cada função pode fazer na aplicação.
Autenticação: Tokens de acesso são gerados e gerenciados para garantir que apenas usuários autenticados possam acessar recursos protegidos.
Auditoria: A API registra todas as ações críticas para facilitar a auditoria e a conformidade.
Configurações de Segurança: Configurações flexíveis para gerenciar políticas de segurança, garantindo que a aplicação esteja sempre protegida.

🛠️ Implementação  
Este projeto utiliza NHibernate para gerenciar o banco de dados e definir o modelo de dados. Os mapeamentos são configurados para garantir que o esquema do banco de dados seja criado e atualizado conforme necessário.

Passos para Configuração:
Clone o Repositório:

bash  
Copiar código  
[git clone https://github.com/seu-usuario/api-seguranca.git  ](https://github.com/Ronei031/seguranca-api.git)  
Configure as Dependências: Configure o arquivo nhibernate.config com a string de conexão do banco de dados e outras configurações necessárias.

Execute as Migrações: Aplique as migrações necessárias para criar o banco de dados:

bash
Copiar código
dotnet run
Inicie a Aplicação: Execute a aplicação e explore os endpoints disponíveis para gerenciar usuários, roles, permissões, e mais.

🚀 Instruções para Execução  
Após configurar a aplicação, você pode iniciar o servidor e testar os endpoints utilizando uma ferramenta como Postman ou diretamente via navegador para endpoints que suportam GET.

📄 Licença  
Este projeto está licenciado sob os termos da licença MIT. Veja o arquivo LICENSE para mais detalhes.

