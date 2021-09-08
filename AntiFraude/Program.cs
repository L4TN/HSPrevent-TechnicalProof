using System;
/*Este programa é meramente simbólico e não implementa a totalidade dos processos mencionados no fluxograma KYC 
Este programa é orientado para equipes de segurança que tem os documentos e dados dos usuários em mãos.*/
namespace AntiFraude
{
    class Program
    {
        //Esta variável de instancia global armazenará os dados do usuário dentro de todos os processos(todas as funções)
        static usuario a = new usuario();

        static void Main(string[] args)
        {
            //Cabeçalho do programa e Entrada de Dados 
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("################ Programa do Fluxograma KYC ################");
            Console.ResetColor();
            Console.WriteLine("Know Your Customer - Baseado nas Leis 9.613/98 e 13.709/18");          
            Console.WriteLine("Digite o nome Completo do usuário");
            var nome = Console.ReadLine().ToUpper();
            Console.WriteLine("Digite o CPF do usuário:");
            var CPF = Console.ReadLine();
            
            //1. Processo de Validação e Cruzamento é executado
            ValidaçãoECruzamento(nome,CPF);
           
            //Condicional
            Console.WriteLine("Analisando os documentos fornecidos, existe algum potencial padrão suspeito ? S/N" );
            string option = Console.ReadLine().ToUpper();
            
            //Caso 1.2 Se o usuário não tem todas as informações válidas , vai para segunda a checagem de dados(pedir mais dados)
            if(option == "S")
            {
                //Esta segunda checagem serve para usuários que não tem as informações completas(ex:só Trabalharam Informalmente ou Nunca estudaram)
                //Então pedimos mais dados para verificar se este usuário é fraudulento ou não, pois não podemos descartar esses usuários somente por isso
                Console.WriteLine("É necessário um novo prazo para resposta, pois precisa-se de mais dados");
                Console.WriteLine("Sair do programa? S/N");
                option = Console.ReadLine().ToUpper();
                    if(option == "S")
                    {
                        Environment.Exit(0);
                    }
                    //Neste trecho o funcionário da Instituição Financeira já checou os novos documentos 
                    else if (option == "N")
                    {
                        //Neste trecho informamos o resultado dessa segunda checagem, se mesmo com os novos dados o ainda usuário pareça fraudulento
                        //Então vetamos o relacionamento com a Instituição Financeira, caso contrário avançamos para Processo de Conhecimento do Patrimônio
                        Console.WriteLine("Analisando os novos documentos fornecidos, existe algum potencial padrão suspeito ? S/N" );
                        option = Console.ReadLine().ToUpper();
                        //Caso 1.2.1
                        if(option == "S")
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("O relacionamento entre Cliente e Instituição financeira foi Vetado !");
                            Console.ResetColor();    
                            Console.ReadKey();                   
                            Environment.Exit(0);                        
                        }
                        //Caso 1.2.2
                        else if(option == "N")
                        {
                            //2. Processo de Conhecimento do Patrimônio é executado
                            ConhecimentoPatrimonio();
                        }
                        //Validação de entrada do código
                        else 
                        {
                            Console.WriteLine("Opção inválida ! Execute o programa novamente ");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }
                    }
                    else 
                    {
                        Console.WriteLine("Opção inválida ! Execute o programa novamente ");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }
            }
            //Caso 1.1 Se todas as informações são válidas e completas, avança para o Processo de Conhecimento do Patrimônio.
            else if(option == "N")
            {
                //Processo de Conhecimento do Patrimônio
                ConhecimentoPatrimonio();              
            }
            //Validação de entrada do código
            else 
            {
                Console.WriteLine("Opção inválida ! Execute o programa novamente ");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        //1. Processo de Validação e Cruzamento de Dados
        public static void ValidaçãoECruzamento(string nome, string CPF)
        {
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n Executando Processo: 1. VALIDAÇÃO E CRUZAMENTO DE DADOS");
            Console.ResetColor();
            
            //Pedimos esses dois dados para poder encontrar a pessoa física 
            a.Nome = nome;
            a.Cpf= CPF;
            while (string.IsNullOrEmpty(a.Nome) || a.Nome.Length <= 2)
            {
                
                Console.WriteLine("Digite um nome válido");
                a.Nome = Console.ReadLine();
                
            }
            Console.WriteLine(a.Nome);
            Console.WriteLine("Nome aceito!");

            //Essa Validação de nome e CPF  é simbólico para demonstrar a consulta do nome e CPF a um banco de dados externo
            while(ValidaCPF(a.Cpf) == false)
            {
                Console.WriteLine("CPF inválido! digite um CPF válido: ");
                a.Cpf= Console.ReadLine();
                
            }
            Console.WriteLine(CPF);
            Console.WriteLine("CPF válido");
            Console.ForegroundColor = ConsoleColor.Blue;
            //Este trecho é simbólico e representa a consulta da situação do CPF no Serasa e Registrato
            Console.WriteLine("Cruzando os dados com outros BD, a condição do CPF está Ok!");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n O Processo: 1. VALIDAÇÃO E CRUZAMENTO DE DADOS foi bem sucedido ✓");
            Console.ResetColor();
            //Fim do processo
        }

        //SubProcesso auxiliar para o Processo de Validação e Cruzamento de Dados
        public static bool ValidaCPF(string vrCPF)
        {
            string valor = vrCPF.Replace(".", "");
            valor = valor.Replace("-", "");

            if (valor.Length != 11)
                return false;

            bool igual = true;
            
            for (int i = 1; i < 11 && igual; i++)
                if (valor[i] != valor[0])
                    igual = false;

            if (igual || valor == "12345678909")
                return false;
            int[] numeros = new int[11];
            for (int i = 0; i < 11; i++)
              numeros[i] = int.Parse(
                valor[i].ToString());
            int soma = 0;
            for (int i = 0; i < 9; i++)
                soma += (10 - i) * numeros[i];
            int resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[9] != 0)
                    return false;
            }
            else if (numeros[9] != 11 - resultado)
                return false;

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += (11 - i) * numeros[i];
            resultado = soma % 11;
            if (resultado == 1 || resultado == 0)
            {
                if (numeros[10] != 0)
                    return false;
            }

            else
                if (numeros[10] != 11 - resultado)
                    return false;
            return true;
            //Fim do processo auxiliar de CPF
        }

        //2. Processo de Conhecimento do Patrimônio
        public static void ConhecimentoPatrimonio()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n Executando Processo: 2. CONHECIMENTO DO PATRIMONIO ");
            Console.ResetColor();
            //Este trecho é simbólico e representa todos os subprocessos do Due Diligence para conhecer melhor o usuário
            Console.WriteLine("Aplicando Due Diligence para Questões financeiras,legais e Aspectos envolvendo o negócio");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            //Neste trecho simbolicamente revisaríamos todas as informações das etapas anteriores
            Console.WriteLine("Fazendo re-verificação dos dados");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Tudo Ok");
            Console.ResetColor();
            //Representação Simbólica da análise do Due Diligence para determinar o perfil financeiro e a qualidade do portfólio do usuário para crédito
            Console.WriteLine("Verificação do perfil financeiro e a Qualidade do perfil para empréstimos ");
            //A informação de renda mensal auxilia a traçar um padrão de consumo e perfil financeiro
            Console.WriteLine("Quanto é a renda mensal do usuário ? (Insira um valor no mínimo de R$300)");
            double renda = double.Parse(Console.ReadLine());
            double perfil = (renda-200)/100;

            //Nesse trecho definimos perfis financeiros com a função (x-200)/100=y onde Renda(X)=Salário e Perfil(Y)=Pontuação
            //Traçando o perfil com base no salário,sendo o limite mínimo 300 reais:
            while(perfil < 1)
            {
                Console.WriteLine("Valor mínimo não atingido");
                renda = double.Parse(Console.ReadLine());
                perfil = (renda-200)/100;
            }
            if(perfil >= 1 && perfil <= 30)
            {
                Console.WriteLine(" Perfil de consumo A ");
            }
            else if(perfil >= 31 && perfil <= 60)
            {
                Console.WriteLine(" Perfil de consumo B ");
            }
            else
            {
                Console.WriteLine(" Perfil de consumo C ");
            }

            //Condicional do 2. Processo de Conhecimento do Patrimônio
            Console.WriteLine("Há padrão Suspeito ? S/N");
            var opcao = Console.ReadLine().ToUpper();
            //Segunda checagem para revisar dados ,evitando erros da equipe de segurança e constragimento do usuário
            if(opcao == "S")
            {
                Console.WriteLine("Uma revisão dos dados será executada");
                Console.WriteLine("Sair do programa ? S/N ");
                opcao = Console.ReadLine().ToUpper();
                if(opcao == "S") Environment.Exit(0);
                else if (opcao == "N")
                {
                    //Caso 2.1 Se ainda existir algum padrão suspeito , o resultado será veto da relação entre cliente e empresa.
                    Console.WriteLine("Ainda há padrão suspeito ?");
                    opcao = Console.ReadLine().ToUpper();
                    
                    if(opcao == "S")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("O relacionamento entre Cliente e Instituição financeira foi Vetado !");
                        Console.ResetColor();
                        Environment.Exit(0);
                    }
                    //Caso 2.2 Se tudo estiver correto nessa segunda checagem, então o processo avança para Processo de Resultado da solicitação de relacionamento.
                    else if (opcao == "N")
                    {
                        //3. Processo de Solicitação de Relacionamento é executado
                        ResultadoSolicRelac();
                    }
                    //Validação de entrada do código
                    else
                    {                          
                        Console.WriteLine("Opção inválida ! Execute o programa novamente ");
                        Console.ReadKey();
                        Environment.Exit(0);                         
                    }
                }
                //Validação de entrada do código
                else
                {  
                    Console.WriteLine("Opção inválida ! Execute o programa novamente ");
                    Console.ReadKey();
                    Environment.Exit(0);                     
                }      
            }
            //Quando não houve nenhuma suspeita de fraude , então o fluxo vai direto para o 3 Processo de Resultado do Relacionamento
            else if (opcao == "N")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n Processo: 2. CONHECIMENTO DO PATRIMONIO foi bem sucedido ✓");
                //3. Processo de Solicitação de Relacionamento é executado
                ResultadoSolicRelac();
            }
            //Validação de entrada do código
            else
            {         
                Console.WriteLine("Opção inválida ! Execute o programa novamente ");
                Console.ReadKey();
                Environment.Exit(0);             
            }   
            //Fim do processo   
        }

        //3. Processo de Resultado da Solicitação de Relacionamento
        public static void ResultadoSolicRelac()
        {
            Console.WriteLine("\n Executando Processo: 3. RESULTADO DA SOLICITAÇÃO DE RELACIONAMENTO ");
            Console.ResetColor();
            //Simbólicamente esse trecho representa o levantamento de dados das origens do patrimônio e verifica se há indícios de crimes contra a lei
            Console.WriteLine("Qual a origem do patrimonio do usuário? ");
            Console.WriteLine("1 - Ativos");  
            Console.WriteLine("2 - Passivos ");
            int aux= int.Parse(Console.ReadLine());
            if(aux == 1)
            {
                a.Patrimonio = "Ativos";
            }
            else if (aux == 2)
            {
                a.Patrimonio = "Passivos";
            }
            
            
            //Condicional do Processo de Resultado de Relacionamento
            Console.WriteLine("Há riscos ou suspeitas envolvidas ? S/N  (Risco geográfico, atividade ou de serviços) ");
            string opcao = Console.ReadLine();

            //Caso 3.1 Se houver indícios de ocorrência de crimes ou riscos, o resultado é veto na relação entre cliente e empresa
            //Simbolicamente os riscos analisados seriam:Localização geográfica,Tipo de atividade/profissão,Tipo de serviços/produtos contratados
                if (opcao == "S")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("O relacionamento entre Cliente e Instituição financeira foi Vetado !");
                    Console.ResetColor();
                    a.ResultRelac = "Reprovado"; 
                    Console.WriteLine($"O usuário {a.Nome} de CPF: {a.Cpf} e Patrimonio de {a.Patrimonio} e tem resultado ");
                    Console.ForegroundColor = ConsoleColor.Red;  
                    Console.Write(a.ResultRelac); 
                    Console.ResetColor();
                    Console.ReadKey();  
                    Environment.Exit(0);
                }
                else if (opcao == "N")
                {
                    
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("O relacionamento entre Cliente e Instituição foi APROVADO");
                    Console.ResetColor();
                    a.ResultRelac = "Aprovado";
                    Console.WriteLine($"O usuário {a.Nome} de CPF: {a.Cpf} e Patrimonio de {a.Patrimonio} tem resultado: ");
                    Console.ForegroundColor = ConsoleColor.Green;  
                    Console.WriteLine(a.ResultRelac); 
                    Console.ResetColor();
                    Console.ReadKey();            
                    Environment.Exit(0);
                }
        }     
    }
}


