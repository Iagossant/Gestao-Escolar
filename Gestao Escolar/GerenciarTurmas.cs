using System;
using System.Collections.Generic;

public class Turma
{
    public string Codigo { get; set; }
    public Professor Professor { get; set; }
    public string Turno { get; set; }
    public int CapacidadeMaxima { get; set; }
    public int AlunosCount { get; set; } = 0;
    public bool Ativa { get; set; } = true;

    public Turma(string codigo, Professor professor, string turno, int capacidadeMaxima)
    {
        Codigo = codigo;
        Professor = professor;
        Turno = turno;
        CapacidadeMaxima = capacidadeMaxima;
    }
    public Turma(string codigo)
    {
        Codigo = codigo;
        Ativa = false;
    }
    public string Capacidade => $"{AlunosCount}/{CapacidadeMaxima}";
    public bool AdicionarAluno()
    {
        if (AlunosCount < CapacidadeMaxima)
        {
            AlunosCount++;
            return true;
        }
        return false;
    }
    public void RemoverAluno()
    {
        if (AlunosCount > 0)
        {
            AlunosCount--;
        }
    }
    public void Remover()
    {
        Codigo = "N/A";
        Ativa = false;
    }
    public override string ToString()
    {
        return Codigo;
    }
}
public class GerenciarTurmas
{
    public static List<Turma> Turmas { get; set; } = new List<Turma>();
    public static void Menu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Gerenciamento de Turmas ===\n1 - Cadastrar Turma\n2 - Alterar Turma\n3 - Ver Turmas\n4 - Remover Turma\n0 - Voltar");
            int.TryParse(Console.ReadLine(), out int opcao);

            switch (opcao)
            {
                case 1: CadastrarTurma(); break;
                case 2: AlterarTurma(); break;
                case 3: VerTurmas(); break;
                case 4: RemoverTurma(); break;
                case 0: return;
                default: Console.WriteLine("Opção inválida."); break;
            }
        }
    }
    static string GerarCodigoTurma()
    {
        Random rnd = new Random();
        char letraAleatoria = (char)rnd.Next('A', 'Z' + 1);
        int numeroAleatorio = rnd.Next(0, 10);
        return $"{letraAleatoria}{numeroAleatorio:D2}";
    }
    static void CadastrarTurma()
    {
        Console.Clear();
        Console.WriteLine("=== Cadastrar Nova Turma ===");
        string codigo = GerarCodigoTurma();

        Console.Write("Digite o código do professor responsável: ");
        string codigoProfessor = Console.ReadLine();
        var professor = GerenciarProfessores.Professores.Find(a => a.Codigo.Equals(codigoProfessor, StringComparison.OrdinalIgnoreCase));
        if (professor == null)
        {
            Console.WriteLine("Professor não encontrado.");
            return;
        }

        string turno = Uteis.DefinirTurno("Digite o turno da turma (Manhã ou Tarde): ");

        int capacidadeMaxima = Uteis.DefinirIdade("Digite a capacidade máxima da turma: ");

        Turma novaTurma = new Turma(codigo, professor, turno, capacidadeMaxima);
        Turmas.Add(novaTurma);
        SalvarCarregar.SalvarTurmas();
        Uteis.GerarComprovante(novaTurma);
    }
    static void VerTurmas()
    {
        Console.Clear();
        Console.WriteLine("Lista de Turmas:");
        var turmasAtivas = Turmas.FindAll(t => t.Ativa);
        if (turmasAtivas.Count == 0)
        {
            Console.WriteLine("Nenhuma turma cadastrada ou ativa.");
        }
        else
        {
            foreach (var turma in turmasAtivas)
            {
                Console.WriteLine($"Código: {turma.Codigo}, Professor: {turma.Professor.Nome}, Turno: {turma.Turno}, Capacidade: {turma.Capacidade}");
            }
        }
        Console.ReadKey();
    }
    static void AlterarTurma()
    {
        Console.Clear();
        Console.Write("=== Alterar Informações da Turma ===\nDigite o código da turma: ");
        string codigo = Console.ReadLine();

        var turma = Turmas.Find(t => t.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase) && t.Ativa);
        if (turma == null)
        {
            Console.WriteLine("Turma não encontrada ou inativa.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine($"Informações atuais da turma:\nCódigo: {turma.Codigo}\nProfessor: {turma.Professor.Nome}\nTurno: {turma.Turno}\nCapacidade: {turma.Capacidade}");

        Console.Write("Digite o código do novo professor: ");
        string codigoProfessor = Console.ReadLine();
        var professor = GerenciarProfessores.Professores.Find(a => a.Codigo.Equals(codigoProfessor, StringComparison.OrdinalIgnoreCase));
        if (professor == null)
        {
            Console.WriteLine("Professor não encontrado.");
            return;
        }
        turma.Professor = professor;

        turma.Turno = Uteis.DefinirTurno("Digite o novo turno (Manhã ou Tarde): ", turma.Turno);

        turma.CapacidadeMaxima = Uteis.DefinirIdade("Digite o novo turno (Manhã ou Tarde): ", turma.CapacidadeMaxima);

        SalvarCarregar.SalvarTurmas();
        Uteis.GerarComprovante(turma);
    }
    static void RemoverTurma()
    {
        Console.Clear();
        Console.Write("=== Remover Turma ===\nDigite o código da turma que deseja remover: ");
        string codigo = Console.ReadLine();

        var turma = Turmas.Find(t => t.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase) && t.Ativa);
        if (turma == null)
        {
            Console.WriteLine("Turma não encontrada ou já inativa.");
            Console.ReadKey();
            return;
        }

        Console.Write($"Deseja realmente remover a turma {turma.Codigo}? (S/N): ");
        string opcao = Console.ReadLine().Substring(0, 1).ToUpper();

        if (opcao == "S")
        {
            turma.Remover();
            SalvarCarregar.SalvarTurmas();
            Console.WriteLine("Turma removida com sucesso!");
        }
        else
        {
            Console.WriteLine("Operação cancelada.");
        }
        Console.ReadKey();
    }
}