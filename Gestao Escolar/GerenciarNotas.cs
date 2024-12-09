using System;
using System.Linq;
using System.Collections.Generic;

public class Nota
{
    public double Semestre1 { get; set; }
    public double Semestre2 { get; set; }
    public double Semestre3 { get; set; }

    public Nota(double semestre1, double semestre2, double semestre3)
    {
        Semestre1 = semestre1;
        Semestre2 = semestre2;
        Semestre3 = semestre3;
    }

    public double CalcularMedia()
    {
        return (Semestre1 + Semestre2 + Semestre3) / 3.0;
    }
}

public class GerenciarNotas
{
    public static void Menu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Gerenciamento de Notas ===\n1 - Alterar Notas\n2 - Ver Notas\n0 - Voltar");
            int.TryParse(Console.ReadLine(), out int opcao);

            switch (opcao)
            {
                case 1: AlterarNota(); break;
                case 2: VerNotas(); break;
                case 0: return;
                default: Console.WriteLine("Opção inválida."); break;
            }
        }
    }
    static void AlterarNota()
    {
        Console.Clear();
        Console.Write("=== Alterar Notas ===\nDigite a matrícula do aluno para alterar as notas: ");
        string matricula = Console.ReadLine();

        Aluno aluno = GerenciarAlunos.Alunos.Find(a => a.Matricula.Equals(matricula, StringComparison.OrdinalIgnoreCase));
        if (aluno == null)
        {
            Console.WriteLine("Aluno não encontrado!");
            return;
        }

        var notaAtual = aluno.Notas.Last();

        Console.WriteLine($"\nNotas atuais do aluno:\n" +
                          $"1º Semestre: {notaAtual.Semestre1}\n" +
                          $"2º Semestre: {notaAtual.Semestre2}\n" +
                          $"3º Semestre: {notaAtual.Semestre3}\n");

        for (int i = 1; i <= 3; i++)
        {
            Console.Write($"Digite a nova nota para o {i}º semestre (0 a 10): ");
            if (double.TryParse(Console.ReadLine(), out double novaNota) && novaNota >= 0 && novaNota <= 10)
            {
                if (i == 1)
                    notaAtual.Semestre1 = novaNota;
                else if (i == 2)
                    notaAtual.Semestre2 = novaNota;
                else if (i == 3)
                    notaAtual.Semestre3 = novaNota;
            }
            else
            {
                Console.WriteLine("Nota inválida! Alteração para este semestre será ignorada.");
            }
        }

        aluno.Notas[aluno.Notas.IndexOf(notaAtual)] = notaAtual;

        double media = notaAtual.CalcularMedia();
        string situacao = media >= 6 ? "Aprovado" : "Reprovado";

        Console.WriteLine($"\nNotas atualizadas com sucesso!\n" +
                          $"1º Semestre: {notaAtual.Semestre1}\n" +
                          $"2º Semestre: {notaAtual.Semestre2}\n" +
                          $"3º Semestre: {notaAtual.Semestre3}\n" +
                          $"Nova média do aluno: {media:F2}\n" +
                          $"Situação atualizada: {situacao}");

        SalvarCarregar.SalvarAlunos();
        Console.ReadKey();
    }
    static void VerNotas()
    {
        Console.Clear();
        Console.Write("=== Ver Notas ===\n1- Ver todas as notas\n2 - Ver notas de um aluno\n0 - Voltar\nEscolha uma opção: ");
        int.TryParse(Console.ReadLine(), out int opcao);

        switch (opcao)
        {
            case 1: VerTodasNotas(); break;
            case 2: VerNotasAluno(); break;
            case 0: return;
            default:
                Console.WriteLine("Opção inválida.");
                Console.ReadKey();
                break;
        }
        Console.ReadKey();
    }
    static void VerNotasAluno()
    {
        Console.Clear();
        Console.Write("=== Ver Notas Aluno ===\nDigite a matrícula do aluno: ");
        string matricula = Console.ReadLine();

        Aluno aluno = GerenciarAlunos.Alunos.Find(a => a.Matricula.Equals(matricula, StringComparison.OrdinalIgnoreCase));
        if (aluno == null || aluno.Notas.Count == 0)
        {
            Console.WriteLine("Aluno não encontrado ou sem notas cadastradas!");
            return;
        }

        var nota = aluno.Notas.Last();
        double media = nota.CalcularMedia();
        string situacao = media >= 6 ? "Aprovado" : "Reprovado";

        Console.WriteLine($"Notas de {aluno.Nome} ({aluno.Matricula}):");
        Console.WriteLine($"1º Semestre: {nota.Semestre1}, 2º Semestre: {nota.Semestre2}, 3º Semestre: {nota.Semestre3}");
        Console.WriteLine($"Média: {media:F2} - Situação: {situacao}");
    }
    static void VerTodasNotas()
    {
        Console.Clear();
        Console.WriteLine("=== Notas de Todos os Alunos ===");

        if (!GerenciarAlunos.Alunos.Any())
        {
            Console.WriteLine("Nenhum aluno cadastrado.");
            return;
        }

        foreach (var aluno in GerenciarAlunos.Alunos)
        {
            var nota = aluno.Notas.Last();
            double media = nota.CalcularMedia();
            string situacao = media >= 6 ? "Aprovado" : "Reprovado";

            Console.WriteLine($"Aluno: {aluno.Nome} ({aluno.Matricula})");
            Console.WriteLine($"1º Semestre: {nota.Semestre1}, 2º Semestre: {nota.Semestre2}, 3º Semestre: {nota.Semestre3}");
            Console.WriteLine($"Média: {media:F2} - Situação: {situacao}\n");
        }
    }
}