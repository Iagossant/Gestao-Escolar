using System;
using System.Linq;
using System.Collections.Generic;

public class Professor
{
    public string Codigo { get; set; }
    public string Nome { get; set; }
    public string Disciplina { get; set; }
    public string Turno { get; set; }
    public int Idade { get; set; }

    public Professor(string codigo, string nome, string disciplina, string turno, int idade)
    {
        Codigo = codigo;
        Nome = nome;
        Disciplina = disciplina;
        Turno = turno;
        Idade = idade;
    }
}

public class GerenciarProfessores
{
    public static List<Professor> Professores { get; set; } = new List<Professor>();

    public static void Menu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Gerenciamento de Professores ===\n1 - Cadastrar Professor\n2 - Alterar Professor\n3 - Ver Professores\n4 - Remover Professor\n0 - Voltar");
            int.TryParse(Console.ReadLine(), out int opcao);

            switch (opcao)
            {
                case 1: CadastrarProfessor(); break;
                case 2: AlterarProfessor(); break;
                case 3: VerProfessores(); break;
                case 4: RemoverProfessor(); break;
                case 0: return;
                default: Console.WriteLine("Opção inválida."); break;
            }
        }
    }

    static void CadastrarProfessor()
    {
        Console.Clear();
        Console.WriteLine("=== Cadastrar Novo Professor ===");
        string nome = Uteis.DefinirNome("Digite o nome do professor: ");
        if (Professores.Any(a => a.Nome == nome))
        {
            Console.WriteLine($"O professor ({nome}) já foi cadastrado.");
            Console.ReadKey();
            return;
        }
        int idade = Uteis.DefinirIdade("Digite a idade do professor: ");

        string disciplina = Uteis.DefinirNome("Digite a disciplina do professor: ");

        string turno = Uteis.DefinirTurno("Digite o turno do professor (Manhã ou Tarde): ");

        string codigoProfessor = Uteis.GerarCodigoUnico(nome);

        Professor novoProfessor = new Professor(codigoProfessor, nome, disciplina, turno, idade);
        Professores.Add(novoProfessor);
        SalvarCarregar.SalvarProfessores();
        Uteis.GerarComprovante(novoProfessor);
    }

    static void VerProfessores()
    {
        Console.Clear();
        Console.Write("=== Lista de Professores ===\n1 - Ver todos os professores\n2 - Pesquisar professor pelo nome\n0 - Voltar\nEscolha uma opção: ");
        int.TryParse(Console.ReadLine(), out int opcao);

        switch (opcao)
        {
            case 1: VerTodosProfessores(); break;
            case 2: PesquisarProfessorPorNome(); break;
            case 0: return;
            default: Console.WriteLine("Opção inválida."); break;
        }
        Console.ReadKey();
    }
    static void VerTodosProfessores()
    {
        Console.Clear();
        Console.WriteLine("=== Lista de Todos os Professores ===");

        if (!Professores.Any())
        {
            Console.WriteLine("Nenhum professor cadastrado.");
            return;
        }

        foreach (var professor in Professores)
        {
            Console.WriteLine($"Código: {professor.Codigo}, Nome: {professor.Nome}, Disciplina: {professor.Disciplina}, Turno: {professor.Turno}, Idade: {professor.Idade}");
        }
    }
    static void PesquisarProfessorPorNome()
    {
        Console.Clear();
        Console.Write("Digite o nome do professor: ");
        string nome = Console.ReadLine();

        var professoresEncontrados = Professores.Where(p => p.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase)).ToList();

        if (!professoresEncontrados.Any())
        {
            Console.WriteLine("Nenhum professor encontrado com esse nome.");
        }
        else
        {
            Console.WriteLine("Professores encontrados:");
            foreach (var professor in professoresEncontrados)
            {
                Console.WriteLine($"Código: {professor.Codigo}, Nome: {professor.Nome}, Disciplina: {professor.Disciplina}, Turno: {professor.Turno}, Idade: {professor.Idade}");
            }
        }
    }
    static void AlterarProfessor()
    {
        Console.Clear();
        Console.Write("=== Alterar Dados do Professor ===\nDigite o código do professor que deseja alterar:");
        string codigo = Console.ReadLine();
        var professor = Professores.Find(a => a.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase));
        if (professor == null)
        {
            Console.WriteLine("Professor não encontrado.");
            return;
        }

        professor.Nome = Uteis.DefinirNome($"Nome atual: {professor.Nome}.\nDigite o novo nome ou aperte Enter para manter: ", professor.Nome);

        professor.Disciplina = Uteis.DefinirNome($"Disciplina atual: {professor.Disciplina}.\nDigite a nova disciplina ou aperte Enter para manter: ", professor.Disciplina);

        professor.Turno = Uteis.DefinirTurno($"Turno atual: {professor.Turno}.\nDigite o novo turno ou aperte Enter para manter: ", professor.Turno);

        professor.Idade = Uteis.DefinirIdade($"Idade atual: {professor.Idade}.\nDigite a nova idade ou aperte Enter para manter: ", professor.Idade);

        SalvarCarregar.SalvarProfessores();
        Uteis.GerarComprovante(professor);
    }
    static void RemoverProfessor()
    {
        Console.Clear();
        Console.WriteLine("=== Remover Professor ===\nDigite o código do professor que deseja remover:");
        string codigo = Console.ReadLine();

        var professor = Professores.FirstOrDefault(p => p.Codigo == codigo);
        if (professor != null)
        {
            Console.Write($"Deseja realmente remover o professor {professor.Nome}? (S/N): ");
            string opcao = Console.ReadLine().Substring(0, 1).ToUpper();

            if (opcao == "S")
            {
                Professores.Remove(professor);
                SalvarCarregar.SalvarProfessores();
                Console.WriteLine("Professor removido com sucesso!");
            }
            else
            {
                Console.WriteLine("Operação cancelada.");
            }
        }
        else
        {
            Console.WriteLine("Professor não encontrado.");
        }
        Console.ReadKey();
    }
}