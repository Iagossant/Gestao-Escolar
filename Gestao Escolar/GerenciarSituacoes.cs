using System;
using System.Linq;
using System.Collections.Generic;

public class GerenciarSituacao
{
    public static void Menu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Gerenciamento de Situações ===\n1 - Alterar Situação\n2 - Ver Situações\n3 - Remover Situação\n0 - Voltar");
            int.TryParse(Console.ReadLine(), out int opcao);

            switch (opcao)
            {
                case 1: AlterarSituacao(); break;
                case 2: VerSituacoes(); break;
                case 3: RemoverSituacao(); break;
                case 0: return;
                default: Console.WriteLine("Opção inválida."); break;
            }
        }
    }

    static void AlterarSituacao()
    {
        Console.Clear();
        Console.Write("=== Alterar Situação ===\nDigite a matrícula do aluno para alterar a situação: ");
        string matricula = Console.ReadLine();

        Aluno aluno = GerenciarAlunos.Alunos.Find(a => a.Matricula.Equals(matricula, StringComparison.OrdinalIgnoreCase));
        if (aluno == null)
        {
            Console.WriteLine("Aluno não encontrado!");
            return;
        }

        Console.Write("Digite a situação do aluno: ");
        aluno.Situacao = Console.ReadLine();
        SalvarCarregar.SalvarSituacoes();
        Console.WriteLine("Situação alterada com sucesso!");
        Console.ReadKey();
    }

    static void VerSituacoes()
    {
        Console.Clear();
        Console.WriteLine("=== Ver Situações ===\n1 - Ver situação de todos\n2 - Ver situação de aluno\n0 - Voltar");
        int.TryParse(Console.ReadLine(), out int opcao);

        switch (opcao)
        {
            case 1: VerSituacoesTodos(); break;
            case 2: VerSituacaoAluno(); break;
            case 0: return;
            default:
                Console.WriteLine("Opção inválida.");
                break;
        }
        Console.ReadKey();
    }

    static void VerSituacaoAluno()
    {
        Console.Clear();
        Console.Write("=== Ver Situação de Aluno ===\nDigite a matrícula do aluno: ");
        string matricula = Console.ReadLine();

        Aluno aluno = GerenciarAlunos.Alunos.Find(a => a.Matricula.Equals(matricula, StringComparison.OrdinalIgnoreCase));
        if (aluno == null)
        {
            Console.WriteLine("Aluno não encontrado!");
            return;
        }

        Console.WriteLine($"Matrícula: {aluno.Matricula}, Nome: {aluno.Nome}, Situação: {aluno.Situacao ?? "Sem situação definida"}");
    }

    static void VerSituacoesTodos()
    {
        Console.Clear();
        Console.WriteLine("Situações de todos os alunos:");
        if (!GerenciarAlunos.Alunos.Any())
        {
            Console.WriteLine("Nenhum aluno cadastrado.");
            return;
        }

        foreach (var aluno in GerenciarAlunos.Alunos)
        {
            Console.WriteLine($"Matrícula: {aluno.Matricula}, Nome: {aluno.Nome}, Situação: {aluno.Situacao ?? "Sem situação definida"}");
        }
    }

    static void RemoverSituacao()
    {
        Console.Clear();
        Console.Write("=== Remover Situação ===\nDigite a matrícula do aluno para remover a situação: ");
        string matricula = Console.ReadLine();

        Aluno aluno = GerenciarAlunos.Alunos.Find(a => a.Matricula.Equals(matricula, StringComparison.OrdinalIgnoreCase));
        if (aluno == null)
        {
            Console.Write($"Deseja realmente remover a situação do aluno {aluno.Nome}? (S/N): ");
            string opcao = Console.ReadLine().Substring(0, 1).ToUpper();

            if (opcao == "S")
            {
                aluno.Situacao = null;
                SalvarCarregar.SalvarSituacoes();
                Console.WriteLine("Situação removida com sucesso!");
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