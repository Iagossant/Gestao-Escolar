class Program
{
    static void Main(string[] args)
    {
        SalvarCarregar.CarregarProfessores();
        SalvarCarregar.CarregarTurmas();
        SalvarCarregar.CarregarAlunos();
        SalvarCarregar.CarregarSituacoes();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Gerenciamento Escolar ===\n1 - Gerenciar Alunos\n2 - Gerenciar Turmas\n3 - Gerenciar Professores\n4 - Gerenciar Notas\n5 - Gerenciar Situações\n6 - Ver Histórico\n0 - Sair");
            int.TryParse(Console.ReadLine(), out int opcao);

            switch (opcao)
            {
                case 1: GerenciarAlunos.Menu(); break;
                case 2: GerenciarTurmas.Menu(); break;
                case 3: GerenciarProfessores.Menu(); break;
                case 4: GerenciarNotas.Menu(); break;
                case 5: GerenciarSituacao.Menu(); break;
                case 6: GerenciarHistorico.VerHistorico(); break;
                case 0:
                    SalvarCarregar.SalvarAlunos();
                    SalvarCarregar.SalvarTurmas();
                    SalvarCarregar.SalvarProfessores();
                    SalvarCarregar.SalvarSituacoes();
                    Console.WriteLine("Saindo...");
                    return;
                default: Console.WriteLine("Opção inválida."); break;
            }
        }
    }
}