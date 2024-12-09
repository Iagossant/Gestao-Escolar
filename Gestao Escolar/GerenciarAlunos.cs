using System;
using System.Linq;
using System.Collections.Generic;

public class Aluno
{
    public string Matricula { get; set; }
    public string Nome { get; set; }
    public int Idade { get; set; }
    public string AnoEscolar { get; set; }
    public string Turno { get; set; }
    public Turma Turma { get; set; }
    public List<Nota> Notas { get; set; } = new List<Nota>();
    public string Situacao { get; set; } = " ";

    public Aluno(string matricula, string nome, int idade, string anoEscolar, string turno, Turma turma)
    {
        Matricula = matricula;
        Nome = nome;
        Idade = idade;
        AnoEscolar = anoEscolar;
        Turno = turno;
        Turma = turma;
    }
    public void AdicionarNota(double semestre1, double semestre2, double semestre3)
    {
        Notas.Add(new Nota(semestre1, semestre2, semestre3));
    }
}

public class GerenciarAlunos
{
    public static List<Aluno> Alunos = new List<Aluno>();

    public static void Menu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Gerenciamento de Alunos ===\n1 - Cadastrar Aluno\n2 - Ver Alunos\n3 - Alterar Aluno\n4 - Remover Aluno\n0 - Voltar");
            int.TryParse(Console.ReadLine(), out int opcao);

            switch (opcao)
            {
                case 1: CadastrarAluno(); break;
                case 2: VerAlunos(); break;
                case 3: AlterarAluno(); break;
                case 4: RemoverAluno(); break;
                case 0: return;
                default: Console.WriteLine("Opção inválida."); break;
            }
        }
    }
    static void CadastrarAluno()
    {
        Console.Clear();
        Console.WriteLine("=== Cadastrar Novo Aluno ===");
        string nome = Uteis.DefinirNome("Digite o nome do aluno: ");
        if (Alunos.Any(a => a.Nome == nome))
        {
            Console.WriteLine($"O aluno ({nome}) já foi cadastrado.");
            Console.ReadKey();
            return;
        }
        int idade = Uteis.DefinirIdade("Digite a idade do aluno: ");

        string anoEscolar = Uteis.DefinirAno("Ano Escolar e Nível (Ex: '3 Ano' ou '3 Médio'): ");

        string turno = Uteis.DefinirTurno("Turno (Manhã ou Tarde): ");

        Turma turma = Uteis.DefinirTurma("Digite a turma do aluno: ");

        string matricula = Uteis.GerarCodigoUnico(nome);

        Aluno novoAluno = new Aluno(matricula, nome, idade, anoEscolar, turno, turma);
        Alunos.Add(novoAluno);
        novoAluno.AdicionarNota(0, 0, 0);
        SalvarCarregar.SalvarAlunos();
        SalvarCarregar.SalvarTurmas();
        Uteis.GerarComprovante(novoAluno);
    }
    static void VerAlunos()
    {
        Console.Clear();
        Console.Write("=== Lista de Alunos ===\n1 - Ver todos os Alunos\n2 - Pesquisar aluno pelo nome\n0 - Voltar\nEscolha uma opção: ");
        int.TryParse(Console.ReadLine(), out int opcao);

        switch (opcao)
        {
            case 1: VerTodosAlunos(); break;
            case 2: PesquisarMatriculaPorNome(); break;
            case 0: return;
            default:
                Console.WriteLine("Opção inválida.");
                break;
        }
        Console.ReadKey();
    }
    static void VerTodosAlunos()
    {
        Console.Clear();
        Console.WriteLine("=== Lista de Alunos ===");

        if (!Alunos.Any())
        {
            Console.WriteLine("Nenhum aluno cadastrado.");
            return;
        }

        foreach (var aluno in Alunos)
        {
            Console.WriteLine($"Matrícula: {aluno.Matricula}, Nome: {aluno.Nome}, Turma: {aluno.Turma}");
        }
    }
    static void PesquisarMatriculaPorNome()
    {
        Console.Clear();
        Console.Write("=== Pesquisar Aluno pelo Nome ===\nDigite o nome do aluno: ");
        string nome = Console.ReadLine();

        var alunosEncontrados = Alunos.Where(a => a.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase)).ToList();
        if (!alunosEncontrados.Any())
        {
            Console.WriteLine("Nenhum aluno encontrado com esse nome.");
        }
        else
        {
            Console.WriteLine("Alunos encontrados:");
            foreach (var aluno in alunosEncontrados)
            {
                Console.WriteLine($"Matrícula: {aluno.Matricula}, Nome: {aluno.Nome}, Turma: {aluno.Turma}");
            }
        }
    }
    static void AlterarAluno()
    {
        Console.Clear();
        Console.Write("Digite a matrícula do aluno que deseja alterar: ");
        string matricula = Console.ReadLine();
        var aluno = Alunos.Find(a => a.Matricula.Equals(matricula, StringComparison.OrdinalIgnoreCase));
        if (aluno == null)
        {
            Console.WriteLine("Aluno não encontrado.");
            return;
        }

        aluno.Nome = Uteis.DefinirNome($"Nome atual: {aluno.Nome}.\nDigite o novo nome ou aperte Enter para manter: ", aluno.Nome);

        aluno.Idade = Uteis.DefinirIdade($"Idade atual: {aluno.Idade}.\nDigite a nova idade ou aperte Enter para manter: ", aluno.Idade);

        aluno.AnoEscolar = Uteis.DefinirAno($"Ano Escolar atual: {aluno.AnoEscolar}.\nDigite o novo ano ou aperte Enter para manter: ", aluno.AnoEscolar);

        aluno.Turno = Uteis.DefinirTurno($"Turno atual: {aluno.Turno}.\nDigite o novo turno ou aperte Enter para manter: ", aluno.Turno);

        aluno.Turma = Uteis.DefinirTurma($"Turma atual: {aluno.Turma}.\nDigite a nova turma ou aperte Enter para manter: ", aluno.Turma);

        SalvarCarregar.SalvarAlunos();
        SalvarCarregar.SalvarTurmas();
        Uteis.GerarComprovante(aluno);
    }
    static void RemoverAluno()
    {
        Console.Clear();
        Console.Write("Digite a matrícula do aluno que deseja remover: ");
        string matricula = Console.ReadLine();
        var aluno = Alunos.Find(a => a.Matricula.Equals(matricula, StringComparison.OrdinalIgnoreCase));
        if (aluno != null)
        {
            Console.Write($"Deseja realmente remover o aluno {aluno.Nome}? (S/N): ");
            string opcao = Console.ReadLine().Substring(0, 1).ToUpper();

            if (opcao == "S")
            {
                var turma = GerenciarTurmas.Turmas.FirstOrDefault(a => a == aluno.Turma);
                turma?.RemoverAluno();
                Alunos.Remove(aluno);
                SalvarCarregar.SalvarAlunos();
                SalvarCarregar.SalvarTurmas();
                Console.WriteLine("Aluno removido com sucesso!");
            }
            else
            {
                Console.WriteLine("Operação cancelada.");
            }
        }
        else
        {
            Console.WriteLine("Aluno não encontrado.");
        }
        Console.ReadKey();
    }
}