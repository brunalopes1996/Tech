using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tech.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "banca",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Tipo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_banca", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "perfil",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_perfil", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Foto = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: true),
                    UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "questionario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", maxLength: 300000, nullable: false),
                    BancaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questionario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_questionario_banca_BancaId",
                        column: x => x.BancaId,
                        principalTable: "banca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "perfil_regra",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_perfil_regra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_perfil_regra_perfil_RoleId",
                        column: x => x.RoleId,
                        principalTable: "perfil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_usuario_UserId",
                        column: x => x.UserId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuario_login",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(255)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario_login", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_usuario_login_usuario_UserId",
                        column: x => x.UserId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuario_perfil",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario_perfil", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_usuario_perfil_perfil_RoleId",
                        column: x => x.RoleId,
                        principalTable: "perfil",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_usuario_perfil_usuario_UserId",
                        column: x => x.UserId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuario_token",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario_token", x => x.Id);
                    table.ForeignKey(
                        name: "FK_usuario_token_usuario_UserId",
                        column: x => x.UserId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "questoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Questao = table.Column<string>(type: "varchar(5000)", maxLength: 5000, nullable: false),
                    AlternativaA = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    AlternativaB = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    AlternativaC = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    AlternativaD = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    AlternativaE = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    AlternativaCorreta = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: false),
                    Imagem = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true),
                    QuestionarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_questoes_questionario_QuestionarioId",
                        column: x => x.QuestionarioId,
                        principalTable: "questionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "historico",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Acertou = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Erro = table.Column<string>(type: "longtext", nullable: true),
                    Acerto = table.Column<string>(type: "longtext", nullable: true),
                    DataResposta = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UsuarioId = table.Column<string>(type: "varchar(255)", nullable: false),
                    QuestionarioId = table.Column<int>(type: "int", nullable: false),
                    QuestaoId = table.Column<int>(type: "int", nullable: false),
                    AlternativaSelecionada = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_historico", x => x.Id);
                    table.ForeignKey(
                        name: "FK_historico_questionario_QuestionarioId",
                        column: x => x.QuestionarioId,
                        principalTable: "questionario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_historico_questoes_QuestaoId",
                        column: x => x.QuestaoId,
                        principalTable: "questoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_historico_usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "banca",
                columns: new[] { "Id", "Nome", "Tipo" },
                values: new object[,]
                {
                    { 1, "Avança SP", "medio" },
                    { 2, "Avança SP", "superior" },
                    { 3, "Instituto Consulplan", "medio" },
                    { 4, "Instituto Consulplan", "superior" },
                    { 5, "UFRR", "superior" }
                });

            migrationBuilder.InsertData(
                table: "perfil",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0b44ca04-f6b0-4a8f-a953-1f2330d30894", null, "Administrador", "ADMINISTRADOR" },
                    { "bec71b05-8f3d-4849-88bb-0e8d518d2de8", null, "Funcionário", "FUNCIONÁRIO" },
                    { "ddf093a6-6cb5-4ff7-9a64-83da34aee005", null, "Cliente", "CLIENTE" }
                });

            migrationBuilder.InsertData(
                table: "usuario",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Foto", "LockoutEnabled", "LockoutEnd", "Nome", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ddf093a6-6cb5-4ff7-9a64-83da34aee005", 0, "39fd4d92-0c5b-4de5-a309-89ddceb51775", "bruuna.oliveiraa1@gmail.com", true, "/img/IconeMasculino.png", true, null, "Bruna Maria Lopes de Oliveira", "BRUUNA.OLIVEIRAA1@GMAIL.COM", "BRUNALOPES", "AQAAAAIAAYagAAAAEGgt8SmlaMYSpx5IXavo+aRKuljmVCojeawjcGWuY+jk0XH7MRl/UKwDOUtxYuqi9A==", null, false, "e265f229-2d71-458d-bae0-57efab605b88", false, "BrunaLopes" });

            migrationBuilder.InsertData(
                table: "questionario",
                columns: new[] { "Id", "BancaId", "Nome" },
                values: new object[] { 1, 2, "Questionario" });

            migrationBuilder.InsertData(
                table: "questoes",
                columns: new[] { "Id", "AlternativaA", "AlternativaB", "AlternativaC", "AlternativaCorreta", "AlternativaD", "AlternativaE", "Imagem", "Questao", "QuestionarioId" },
                values: new object[,]
                {
                    { 1, "Ctrl + Delete", "Ctrl + Backspace", "Ctrl + Função", "A", "Ctrl + End", "Ctrl + -", null, "O uso de teclas de atalho no Microsoft Word proporciona maior agilidade na edição e formatação de textos, permitindo que o usuário execute comandos de forma rápida e eficiente. Considerando esse recurso, qual combinação de teclas deve ser utilizada para excluir a palavra localizada à direita do cursor?", 1 },
                    { 2, "Uma ferramenta para ajustar a orientação da página.", "Um recurso para selecionar o tipo de papel a ser utilizado.", "Um controle deslizante de zoom no documento, permitindo ampliar ou reduzir a visualização para facilitar a leitura de textos pequenos.", "C", "Uma opção para configurar margens personalizadas.", "Um contador para acompanhar a evolução da impressão.", "../img/QuestaoEM.png", "Antes de realizar a impressão de um documento no Microsoft Word, o usuário tem a possibilidade de visualizar o conteúdo e definir quais páginas serão impressas, ajustando configurações conforme necessário. Considerando os recursos disponíveis na tela de impressão, a imagem apresentada refere-se a:", 1 },
                    { 3, "Dir", "Log", "Conf", "A", "List", "Inf", null, "No que diz respeito a interação com programas e aplicativos do Windows 7, o Prompt de Comando no Windows 7 é uma interface de linha de comando que possibilita a execução de diversas operações no sistema, oferecendo maior controle sobre arquivos e diretórios. Para exibir todos os arquivos e pastas contidos em um diretório específico, qual comando deve ser utilizado no programa: ", 1 },
                    { 4, "Xamarin", "Mono", "UWP", "D", ".NET 5+", ".Net Framework", null, "Indique a alternativa que apresenta a implementação do .NET mais adequada para criação de aplicativos multiplataforma, que executam em Windows, Linux e macOS.", 1 },
                    { 5, "Reinicializar seu computador para que o sistema operacional realize a reinstalação automaticamente.", "Acessar “Gerenciador de dispositivos”, localizar seu dispositivo e atualizá-lo.", "Pesquisar na web um driver do fabricante do dispositivo.", "B", "Entrar em contato com o suporte da Microsoft e solicitar o driver do dispositivo.", "Ativar a função de mouse virtual.", null, "Um funcionário de uma empresa relata ao departamento de TI que o mouse de seu computador Windows não está funcionando, mesmo tendo testado outros periféricos do mesmo tipo e religar seu computador. Um dos técnicos de suporte instrui o funcionário a atualizar o driver do dispositivo manualmente. Qual das seguintes ações apresenta um método mais eficiente da resolução do problema?", 1 },
                    { 6, "Inserir um filtro na coluna de Presença pelo menu 'Dados'; filtrar pela palavra “Ausente”; e aplicar manualmente a cor desejada nas células. ", "Selecionar toda a tabela; acessar o menu “Fórmulas”; criar uma nova regra condicional personalizada baseada no texto “Ausente”; e configurar as opções de preenchimento. ", "Selecionar toda a coluna da tabela; acessar o menu “Página Inicial”; clicar em “Classificar e Filtrar”; e configurar uma nova regra para destacar células onde o texto seja 'Ausente' ", "A", "Acessar o menu “Exibir”; ativar o modo de Visualização de Fórmulas; selecionar a coluna Data; e aplicar uma formatação condicional baseada na fórmula =B2='Ausente', ajustando a formatação de acordo com a necessidade. ", "Selecionar as células da coluna de presença; acessar o menu “Página Inicial”; clicar em “Formatação Condicional”; escolher “Realçar Regras das Células”; selecionar a opção “É Igual a”; digitar “Ausente” no campo de texto; e definir a formatação desejada.", null, "No setor de planejamento estratégico da Câmara Municipal de Araraquara, um funcionário recebeu uma planilha no Excel M365 com dados de frequência de comparecimento de vereadores em sessões plenárias, organizados por nome, data e presença (indicado com “Presente” ou “Ausente”). O objetivo é destacar, em cada linha da planilha, as datas em que os vereadores estiveram ausentes. Para isso, foi solicitado o uso do recurso de formatação condicional no Excel M365. Assinale a afirmativa, a seguir, que descreve corretamente como configurar a formatação condicional para destacar as datas cujo valor da célula na coluna de presença seja igual a “Ausente”.", 1 },
                    { 7, "Converter manualmente a data de admissão em cada carta gerada, usando a opção “Pesquisar e Substituir” para filtrar os servidores com mais de cinco anos, antes de efetivamente concluir a mala direta.", "Configurar a mala direta para que todos os destinatários recebam a mesma mensagem de forma genérica e, posteriormente, ocultar o texto manualmente nas cartas geradas para aqueles com menos de cinco anos de serviço.", "Ajustar a fonte da mensagem no próprio corpo do documento Word para cor branca sempre que o servidor tiver menos de cinco anos de serviço, tornando a mensagem invisível para esse grupo sem o uso de regras ou campos adicionais.", "E", "Inserir um campo IF no Word que compara diretamente a data de admissão do servidor ao dia atual, usando a função interna do Word para subtrair datas e avaliar se o resultado é maior que cinco anos, sem necessidade de cálculos prévios.", "Criar, no Excel, uma coluna que calcule se o servidor já completou cinco anos de serviço (por exemplo, gerando “Sim” ou “Não”), e então usar o campo IF (Se...Então...Senão) no Word para inserir a mensagem somente quando o valor dessa coluna for “Sim”.", null, "O setor de Recursos Humanos da Câmara Municipal de Araraquara precisa enviar uma notificação individualizada a todos os servidores sobre uma nova política de benefícios. Para agilizar o processo, optou-se pelo uso da Mala Direta do Microsoft Word (versão 365), vinculada a uma planilha do Excel que contém os dados de cada servidor: nome, endereço, cargo, e-mail e data de admissão. Além do texto principal, deseja-se que seja exibida automaticamente, no corpo da carta, uma mensagem adicional apenas para os servidores que têm mais de cinco anos de serviço, calculados com base na data de admissão. Considerando a necessidade de inserir essa mensagem condicional na correspondência personalizada, assinale a afirmativa que descreve corretamente como configurar a Regra “Se...Então...Senão” (If...Then...Else) no Word 365, a fim de exibir a mensagem somente para os servidores com mais de cinco anos de serviço. ", 1 },
                    { 8, "Gráfico de Pizza.", "Gráfico de Linhas.", "Gráfico de Radar.", "B", "Gráfico de Árvore (Treemap).", "Gráfico de Histograma.", "../img/QuestaoES.png", "Os gráficos são recursos visuais fundamentais no Microsoft Excel para representar e interpretar dados de maneira clara e objetiva. Entre os diversos tipos disponíveis, há um modelo em que as categorias são distribuídas de forma uniforme no eixo horizontal, enquanto os valores são organizados igualmente no eixo vertical. Esse tipo de gráfico é ideal para demonstrar a evolução de informações contínuas ao longo do tempo, facilitando a visualização de tendências em períodos regulares, como meses, trimestres ou anos fiscais.Com base nessa descrição, e na imagem a seguir, assinale a alternativa que identifica corretamente o tipo de gráfico mencionado:", 1 },
                    { 9, "Starvation.", "Host.", "Livelock.", "A", "Deadlock.", "Checkpoint.", null, "O Sistema Operacional (SO) precisa lidar constantemente com requisições por recursos, dependendo de um algoritmo para decidir qual processo consegue determinado recurso e quando. Considere a seguinte situação na qual muitos processos querem imprimir um documento: suponha que o algoritmo implementado no SO para decidir como alocar a impressora, cede esse dispositivo ao processo com o menor arquivo a ser impresso. Tal algoritmo busca maximizar o atendimento rápido a processos com pequenas tarefas a serem executadas. Ocorre que existem muitos processos requisitando a impressora, sendo que um desses processos possui um arquivo grande. Assim, a cada instante no qual a impressora está livre, o algoritmo do SO a disponibiliza para o próximo processo com arquivo pequeno para imprimir, de modo que o processo com arquivo grande é preterido indefinidamente, embora não esteja bloqueado. Essa situação leva o processo com arquivo grande a ser impresso a condição de _________.", 1 },
                    { 10, "(1) (2) (2) (2).", "(2) (1) (1) (2).", "(1) (1) (2) (2).", "D", "(2) (2) (1) (2).", "(2) (1) (2) (1).", null, "A lista abaixo apresenta duas tecnologias amplamente utilizadas no contexto de servidores Web. Associe elas às suas características de acordo com sua numeração.<br>1. Apache Webserver.<br>2. Nginx<br>( ) Utiliza um modelo orientado em eventos, no qual um único processo principal coordena vários processos trabalhadores, cada um com um único thread.<br>( ) Sua configuração é orientada a blocos. <br>( ) Utiliza um modelo baseado em processos/threads, em que cada conexão é gerenciada por um processo ou thread distinto.<br>( ) É nativamente mais estendido por módulos. <br>A sequência CORRETA dessa associação é: ", 1 }
                });

            migrationBuilder.InsertData(
                table: "historico",
                columns: new[] { "Id", "Acerto", "Acertou", "AlternativaSelecionada", "DataResposta", "Erro", "QuestaoId", "QuestionarioId", "UsuarioId" },
                values: new object[] { 1, " ", false, "A", new DateTime(2025, 5, 20, 20, 13, 23, 693, DateTimeKind.Local).AddTicks(5581), " ", 1, 1, "ddf093a6-6cb5-4ff7-9a64-83da34aee005" });

            migrationBuilder.CreateIndex(
                name: "IX_historico_QuestaoId",
                table: "historico",
                column: "QuestaoId");

            migrationBuilder.CreateIndex(
                name: "IX_historico_QuestionarioId",
                table: "historico",
                column: "QuestionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_historico_UsuarioId",
                table: "historico",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "perfil",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_perfil_regra_RoleId",
                table: "perfil_regra",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_questionario_BancaId",
                table: "questionario",
                column: "BancaId");

            migrationBuilder.CreateIndex(
                name: "IX_questoes_QuestionarioId",
                table: "questoes",
                column: "QuestionarioId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "usuario",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "usuario",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_usuario_login_UserId",
                table: "usuario_login",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_perfil_RoleId",
                table: "usuario_perfil",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_token_UserId",
                table: "usuario_token",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "historico");

            migrationBuilder.DropTable(
                name: "perfil_regra");

            migrationBuilder.DropTable(
                name: "usuario_login");

            migrationBuilder.DropTable(
                name: "usuario_perfil");

            migrationBuilder.DropTable(
                name: "usuario_token");

            migrationBuilder.DropTable(
                name: "questoes");

            migrationBuilder.DropTable(
                name: "perfil");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "questionario");

            migrationBuilder.DropTable(
                name: "banca");
        }
    }
}
