using Tech.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Tech.Data;

public class AppDbSeed
{
    public AppDbSeed(ModelBuilder builder)
    {
        List<Banca> bancas = new() {

            new Banca { Id = 1, Nome = "Avança SP", Tipo = "Médio" },
            new Banca { Id = 2, Nome = "Avança SP", Tipo = "Superior" },
            new Banca { Id = 3, Nome = "Instituto Consulplan", Tipo = "Médio" },
            new Banca { Id = 4, Nome = "Instituto Consulplan", Tipo = "Superior" },
            new Banca { Id = 5, Nome = "UFRR", Tipo = "Superior" }
     };


        builder.Entity<Banca>().HasData(bancas);

        #region Populate Roles - Perfis de Usuário
        List<IdentityRole> roles = new()
        {
            new IdentityRole() {
               Id = "0b44ca04-f6b0-4a8f-a953-1f2330d30894",
               Name = "Administrador",
               NormalizedName = "ADMINISTRADOR"
            },
            new IdentityRole() {
               Id = "bec71b05-8f3d-4849-88bb-0e8d518d2de8",
               Name = "Funcionário",
               NormalizedName = "FUNCIONÁRIO"
            },
            new IdentityRole() {
               Id = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
               Name = "Cliente",
               NormalizedName = "CLIENTE"
            },
        };
        builder.Entity<IdentityRole>().HasData(roles);
        #endregion

        #region Populate Usuário
        List<Usuario> usuarios = new() {
            new Usuario(){
                Id = "ddf093a6-6cb5-4ff7-9a64-83da34aee005",
                Email = "bruuna.oliveiraa1@gmail.com",
                NormalizedEmail = "BRUUNA.OLIVEIRAA1@GMAIL.COM",
                UserName = "BrunaLopes",
                NormalizedUserName = "BRUNALOPES",
                LockoutEnabled = true,
                EmailConfirmed = true,
                Nome = "Bruna Maria Lopes de Oliveira",
                Foto = "/img/usuarios/ddf093a6-6cb5-4ff7-9a64-83da34aee005.png"
            }
        };
        foreach (var user in usuarios)
        {
            PasswordHasher<IdentityUser> pass = new();
            user.PasswordHash = pass.HashPassword(user, "123456");
        }
        builder.Entity<Usuario>().HasData(usuarios);

        #endregion        

        List<Questionario> questionario = new List<Questionario>()
        {
            new Questionario {
                Id = 1,
                Nome= "Teste",
                BancaId = 2,
            },

        };

        builder.Entity<Questionario>().HasData(questionario);

        List<Historico> historico = new List<Historico> // Falta preencher os historicos dos erros e acertos do usuário
        {
            new Historico {
                Id= 1,
                Erro= " " ,
                Acerto= " ",
                UsuarioId= usuarios[0].Id,
                QuestionarioId= 1
            },
        };
        builder.Entity<Historico>().HasData(historico);

        List<Questoes> questoes = new List<Questoes>
        {

            new Questoes
            {
                Id = 1,
                Questao = "O uso de teclas de atalho no Microsoft Word proporciona maior agilidade na edição e formatação de textos, permitindo que o usuário execute comandos de forma rápida e eficiente. Considerando esse recurso, qual combinação de teclas deve ser utilizada para excluir a palavra localizada à direita do cursor?",
                AlternativaA = "Ctrl + Delete",
                AlternativaB = "Ctrl + Backspace",
                AlternativaC = "Ctrl + Função",
                AlternativaD = "Ctrl + End",
                AlternativaE = "Ctrl + -",
                AlternativaCorreta = "A",
                QuestionarioId= 1
            },



            new Questoes
            {
                Id = 2,
                Questao = "Antes de realizar a impressão de um documento no Microsoft Word, o usuário tem a possibilidade de visualizar o conteúdo e definir quais páginas serão impressas, ajustando configurações conforme necessário. Considerando os recursos disponíveis na tela de impressão, a imagem apresentada refere-se a:",
                AlternativaA = "Uma ferramenta para ajustar a orientação da página.",
                AlternativaB = "Um recurso para selecionar o tipo de papel a ser utilizado.",
                AlternativaC = "Um controle deslizante de zoom no documento, permitindo ampliar ou reduzir a visualização para facilitar a leitura de textos pequenos.",
                AlternativaD = "Uma opção para configurar margens personalizadas.",
                AlternativaE = "Um contador para acompanhar a evolução da impressão.",
                AlternativaCorreta = "C",
                QuestionarioId= 2
            },




            new Questoes
            {
                Id = 3,
                Questao = "No que diz respeito a interação com programas e aplicativos do Windows 7, o Prompt de Comando no Windows 7 é uma interface de linha de comando que possibilita a execução de diversas operações no sistema, oferecendo maior controle sobre arquivos e diretórios. Para exibir todos os arquivos e pastas contidos em um diretório específico, qual comando deve ser utilizado no programa: ",
                AlternativaA = "Dir",
                AlternativaB = "Log",
                AlternativaC = "Conf",
                AlternativaD = "List",
                AlternativaE = "Inf",
                AlternativaCorreta = "A",
                QuestionarioId= 3
            },



            new Questoes
            {
                Id = 4,
                Questao = "Indique a alternativa que apresenta a implementação do .NET mais adequada para criação de aplicativos multiplataforma, que executam em Windows, Linux e macOS.",
                AlternativaA = "Xamarin",
                AlternativaB = "Mono",
                AlternativaC = "UWP",
                AlternativaD = ".NET 5+",
                AlternativaE = ".Net Framework",
                AlternativaCorreta = "D",
                QuestionarioId= 4
            },




            new Questoes
            {
                Id = 5,
                Questao = "Um funcionário de uma empresa relata ao departamento de TI que o mouse de seu computador Windows não está funcionando, mesmo tendo testado outros periféricos do mesmo tipo e religar seu computador. Um dos técnicos de suporte instrui o funcionário a atualizar o driver do dispositivo manualmente. Qual das seguintes ações apresenta um método mais eficiente da resolução do problema?",
                AlternativaA = "Reinicializar seu computador para que o sistema operacional realize a reinstalação automaticamente.",
                AlternativaB = "Acessar “Gerenciador de dispositivos”, localizar seu dispositivo e atualizá-lo.",
                AlternativaC = "Pesquisar na web um driver do fabricante do dispositivo.",
                AlternativaD = "Entrar em contato com o suporte da Microsoft e solicitar o driver do dispositivo.",
                AlternativaE = "Ativar a função de mouse virtual.",
                AlternativaCorreta = "B",
                QuestionarioId= 5
            },



            new Questoes
            {
                Id = 6,
                Questao = "No setor de planejamento estratégico da Câmara Municipal de Araraquara, um funcionário recebeu uma planilha no Excel M365 com dados de frequência de comparecimento de vereadores em sessões plenárias, organizados por nome, data e presença (indicado com “Presente” ou “Ausente”). O objetivo é destacar, em cada linha da planilha, as datas em que os vereadores estiveram ausentes. Para isso, foi solicitado o uso do recurso de formatação condicional no Excel M365. Assinale a afirmativa, a seguir, que descreve corretamente como configurar a formatação condicional para destacar as datas cujo valor da célula na coluna de presença seja igual a “Ausente”.",
                AlternativaA = "Inserir um filtro na coluna de Presença pelo menu 'Dados'; filtrar pela palavra “Ausente”; e aplicar manualmente a cor desejada nas células. ",
                AlternativaB = "Selecionar toda a tabela; acessar o menu “Fórmulas”; criar uma nova regra condicional personalizada baseada no texto “Ausente”; e configurar as opções de preenchimento. ",
                AlternativaC = "Selecionar toda a coluna da tabela; acessar o menu “Página Inicial”; clicar em “Classificar e Filtrar”; e configurar uma nova regra para destacar células onde o texto seja 'Ausente' ",
                AlternativaD = "Acessar o menu “Exibir”; ativar o modo de Visualização de Fórmulas; selecionar a coluna Data; e aplicar uma formatação condicional baseada na fórmula =B2='Ausente', ajustando a formatação de acordo com a necessidade. ",
                AlternativaE = "Selecionar as células da coluna de presença; acessar o menu “Página Inicial”; clicar em “Formatação Condicional”; escolher “Realçar Regras das Células”; selecionar a opção “É Igual a”; digitar “Ausente” no campo de texto; e definir a formatação desejada.",
                AlternativaCorreta = "A",
                QuestionarioId= 6
            },




            new Questoes
            {
                Id = 7,
                Questao = "O setor de Recursos Humanos da Câmara Municipal de Araraquara precisa enviar uma notificação individualizada a todos os servidores sobre uma nova política de benefícios. Para agilizar o processo, optou-se pelo uso da Mala Direta do Microsoft Word (versão 365), vinculada a uma planilha do Excel que contém os dados de cada servidor: nome, endereço, cargo, e-mail e data de admissão. Além do texto principal, deseja-se que seja exibida automaticamente, no corpo da carta, uma mensagem adicional apenas para os servidores que têm mais de cinco anos de serviço, calculados com base na data de admissão. Considerando a necessidade de inserir essa mensagem condicional na correspondência personalizada, assinale a afirmativa que descreve corretamente como configurar a Regra “Se...Então...Senão” (If...Then...Else) no Word 365, a fim de exibir a mensagem somente para os servidores com mais de cinco anos de serviço. ",
                AlternativaA = "Converter manualmente a data de admissão em cada carta gerada, usando a opção “Pesquisar e Substituir” para filtrar os servidores com mais de cinco anos, antes de efetivamente concluir a mala direta.",
                AlternativaB = "Configurar a mala direta para que todos os destinatários recebam a mesma mensagem de forma genérica e, posteriormente, ocultar o texto manualmente nas cartas geradas para aqueles com menos de cinco anos de serviço.",
                AlternativaC = "Ajustar a fonte da mensagem no próprio corpo do documento Word para cor branca sempre que o servidor tiver menos de cinco anos de serviço, tornando a mensagem invisível para esse grupo sem o uso de regras ou campos adicionais.",
                AlternativaD = "Inserir um campo IF no Word que compara diretamente a data de admissão do servidor ao dia atual, usando a função interna do Word para subtrair datas e avaliar se o resultado é maior que cinco anos, sem necessidade de cálculos prévios.",
                AlternativaE = "Criar, no Excel, uma coluna que calcule se o servidor já completou cinco anos de serviço (por exemplo, gerando “Sim” ou “Não”), e então usar o campo IF (Se...Então...Senão) no Word para inserir a mensagem somente quando o valor dessa coluna for “Sim”.",
                AlternativaCorreta = "E",
                QuestionarioId= 7
            },


            new Questoes
            {
                Id = 8,
                Questao = "Os gráficos são recursos visuais fundamentais no Microsoft Excel para representar e interpretar dados de maneira clara e objetiva. Entre os diversos tipos disponíveis, há um modelo em que as categorias são distribuídas de forma uniforme no eixo horizontal, enquanto os valores são organizados igualmente no eixo vertical. Esse tipo de gráfico é ideal para demonstrar a evolução de informações contínuas ao longo do tempo, facilitando a visualização de tendências em períodos regulares, como meses, trimestres ou anos fiscais.Com base nessa descrição, e na imagem a seguir, assinale a alternativa que identifica corretamente o tipo de gráfico mencionado:",
                AlternativaA = "Gráfico de Pizza.",
                AlternativaB = "Gráfico de Linhas.",
                AlternativaC = "Gráfico de Radar.",
                AlternativaD = "Gráfico de Árvore (Treemap).",
                AlternativaE = "Gráfico de Histograma.",
                AlternativaCorreta = "B",
                QuestionarioId= 8
            },



            new Questoes
            {
                Id = 9,
                Questao = "O Sistema Operacional (SO) precisa lidar constantemente com requisições por recursos, dependendo de um algoritmo para decidir qual processo consegue determinado recurso e quando. Considere a seguinte situação na qual muitos processos querem imprimir um documento: suponha que o algoritmo implementado no SO para decidir como alocar a impressora, cede esse dispositivo ao processo com o menor arquivo a ser impresso. Tal algoritmo busca maximizar o atendimento rápido a processos com pequenas tarefas a serem executadas. Ocorre que existem muitos processos requisitando a impressora, sendo que um desses processos possui um arquivo grande. Assim, a cada instante no qual a impressora está livre, o algoritmo do SO a disponibiliza para o próximo processo com arquivo pequeno para imprimir, de modo que o processo com arquivo grande é preterido indefinidamente, embora não esteja bloqueado. Essa situação leva o processo com arquivo grande a ser impresso a condição de _________.",
                AlternativaA = "Starvation.",
                AlternativaB = "Host.",
                AlternativaC = "Livelock.",
                AlternativaD = "Deadlock.",
                AlternativaE = "Checkpoint.",
                AlternativaCorreta = "A",
                QuestionarioId= 9
            },

            new Questoes
            {
                Id = 10,
                Questao = "A lista abaixo apresenta duas tecnologias amplamente utilizadas no contexto de servidores Web. Associe elas às suas características de acordo com sua numeração. 1. Apache Webserver. 2. Nginx ( ) Utiliza um modelo orientado em eventos, no qual um único processo principal coordena vários processos trabalhadores, cada um com um único thread. ( ) Sua configuração é orientada a blocos. ( ) Utiliza um modelo baseado em processos/threads, em que cada conexão é gerenciada por um processo ou thread distinto.  ( ) É nativamente mais estendido por módulos. A sequência CORRETA dessa associação é: ",
                AlternativaA = "(1) (2) (2) (2).",
                AlternativaB = "(2) (1) (1) (2).",
                AlternativaC = "(1) (1) (2) (2).",
                AlternativaD = "(2) (2) (1) (2).",
                AlternativaE = "(2) (1) (2) (1).",
                AlternativaCorreta = "D",
                QuestionarioId= 10
            },

        };
        builder.Entity<Questoes>().HasData(questoes);

    }

}