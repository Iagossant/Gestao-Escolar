using System;
using System.Collections.Generic;
using System.Linq;

public class Uteis
{
    public static int ExtrairNumero(string idade)
    {
        if (int.TryParse(new string(idade.Where(char.IsDigit).ToArray()), out int numero))
            return numero;
        return -1;
    }
    public static string FormatarTexto(string texto)
    {
        if (string.IsNullOrWhiteSpace(texto)) return texto;
        return char.ToUpper(texto[0]) + texto.Substring(1).ToLower();
    }
    public static bool ValidarAnoEscolar(string anoEscolar)
    {
        return anoEscolar.EndsWith("Ano", StringComparison.OrdinalIgnoreCase) ||
               anoEscolar.EndsWith("Médio", StringComparison.OrdinalIgnoreCase);
    }
    public static string GerarCodigoUnico(string nome)
    {
        return $"{nome[0].ToString().ToUpper()}{new Random().Next(100, 999)}";
    }
    public static string DefinirNome(string mensagem, string nomeAtual = "")
    {
        while (true)
        {
            Console.Write(mensagem);
            string nome = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(nome))
            {
                if (nomeAtual != "") return nomeAtual;
                Console.WriteLine("Nome inválida.");
                continue;
            }
            string[] partesNome = nome.Split(' ');
            for (int i = 0; i < partesNome.Length; i++)
            {
                partesNome[i] = FormatarTexto(partesNome[i]);
            }
            return nome = string.Join(" ", partesNome);
        }
    }
    public static string DefinirTurno(string mensagem, string turnoAtual = "")
    {
        string turno;
        do
        {
            Console.Write(mensagem);
            turno = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(turno))
            {
                if (turnoAtual != "") return turnoAtual;
                Console.WriteLine("Turno inválida.");
                continue;
            }
            turno = FormatarTexto(turno);
            if (turno != "Manhã" && turno != "Tarde")
            {
                Console.WriteLine("Turno inválido. Tente novamente.");
            }
        } while (turno != "Manhã" && turno != "Tarde");
        return turno;
    }

    public static int DefinirIdade(string mensagem, int idadeAtual = 0)
    {
        int idade = -1;
        do
        {
            Console.Write(mensagem);
            string idadeStr = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(idadeStr))
            {
                if (idadeAtual != 0) return idadeAtual;
                Console.WriteLine("Idade inválida.");
                continue;
            }

            idade = Uteis.ExtrairNumero(idadeStr);
            if (idade < 0 || idade > 99)
            {
                Console.WriteLine("Idade inválida. Tente novamente.");
            }
        } while (idade < 0 || idade > 99);
        return idade;
    }
    public static string DefinirAno(string mensagem, string anoAtual = "")
    {
        string anoEscolar;
        do
        {
            Console.Write(mensagem);
            anoEscolar = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(anoEscolar))
            {
                if (anoAtual != "") return anoAtual;
                Console.WriteLine("Turno inválida.");
                continue;
            }
            string[] partesAnoEscolar = anoEscolar.Split(' ');
            partesAnoEscolar[1] = Uteis.FormatarTexto(partesAnoEscolar[1]);
            anoEscolar = $"{partesAnoEscolar[0]} {partesAnoEscolar[1]}";
            if (!ValidarAnoEscolar(anoEscolar))
            {
                Console.WriteLine("Formato inválido. Tente novamente.");
                Console.ReadKey();
            }
        } while (!ValidarAnoEscolar(anoEscolar));
        return anoEscolar;
    }
    public static Turma DefinirTurma(string mensagem, Turma turmaAtual = null)
    {
        do
        {
            Console.Write(mensagem);
            string codigo = Console.ReadLine();
            var turma = GerenciarTurmas.Turmas.Find(a => a.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase));
            if (string.IsNullOrWhiteSpace(codigo))
            {
                if (turmaAtual != null && turma != null) turmaAtual.RemoverAluno(); return turmaAtual;
                Console.WriteLine("Turno inválida.");
                continue;
            }
            if (turma == null || !turma.AdicionarAluno())
            {
                Console.WriteLine("Turma inválida ou lotada.");
            }
            else if (turma.AlunosCount < turma.CapacidadeMaxima)
            {
                return turma;
            }
        } while (true);
    }
    public static void GerarComprovante(object pessoa)
    {
        Console.Clear();

        Console.WriteLine("========= COMPROVANTE =========");
        if (pessoa is Professor professor)
        {
            Console.WriteLine(
                $"Código: {professor.Codigo}\n" +
                $"Nome: {professor.Nome}\n" +
                $"Idade: {professor.Idade}\n" +
                $"Disciplina: {professor.Disciplina}\n" +
                $"Turno: {professor.Turno}");
        }
        else if (pessoa is Aluno aluno)
        {
            Console.WriteLine(
                $"Matrícula: {aluno.Matricula}\n" +
                $"Nome: {aluno.Nome}\n" +
                $"Idade: {aluno.Idade}\n" +
                $"Ano Escolar: {aluno.AnoEscolar}\n" +
                $"Turno: {aluno.Turno}\n" +
                $"Turma: {aluno.Turma}");
        }
        else if (pessoa is Turma turma)
        {
            Console.WriteLine(
                $"Código: {turma.Codigo}\n" +
                $"Professor: {turma.Professor}\n" +
                $"Turno: {turma.Turno}\n" +
                $"Capacidade: {turma.CapacidadeMaxima}");
        }

        Console.WriteLine("===============================\nPressione qualquer tecla para voltar.");
        Console.ReadKey();
    }
}