using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class SalvarCarregar
{
    public static void SalvarAlunos()
    {
        using (StreamWriter sw = new StreamWriter("alunos.txt"))
        {
            foreach (var aluno in GerenciarAlunos.Alunos)
            {
                sw.WriteLine($"{aluno.Matricula}|{aluno.Nome}|{aluno.Idade}|{aluno.AnoEscolar}|{aluno.Turno}|{string.Join(",", aluno.Notas.Select(n => $"{n.Semestre1}-{n.Semestre2}-{n.Semestre3}"))}|{aluno.Situacao}|{aluno.Turma}");
            }
        }
    }
    public static void CarregarAlunos()
    {
        if (File.Exists("alunos.txt"))
        {
            var linhas = File.ReadAllLines("alunos.txt");
            foreach (var linha in linhas)
            {
                string[] partes = linha.Split('|');
                var turma = GerenciarTurmas.Turmas.Find(p => p.Codigo.Equals(partes[7], StringComparison.OrdinalIgnoreCase));
                if (turma is null) turma = new("Sem Turma");
                var aluno = new Aluno(partes[0], partes[1], int.Parse(partes[2]), partes[3], partes[4], turma);

                if (!string.IsNullOrEmpty(partes[6]))
                {
                    string[] valores = partes[5].Split('-');
                    aluno.AdicionarNota(double.Parse(valores[0]), double.Parse(valores[1]), double.Parse(valores[2]));
                }

                aluno.Situacao = partes[6];
                GerenciarAlunos.Alunos.Add(aluno);
            }
        }
    }
    public static void SalvarProfessores()
    {
        using (StreamWriter sw = new StreamWriter("professores.txt"))
        {
            foreach (var professor in GerenciarProfessores.Professores)
            {
                sw.WriteLine($"{professor.Codigo}|{professor.Nome}|{professor.Disciplina}|{professor.Turno}|{professor.Idade}");
            }
        }
    }
    public static void CarregarProfessores()
    {
        if (File.Exists("professores.txt"))
        {
            var linhas = File.ReadAllLines("professores.txt");
            foreach (var linha in linhas)
            {
                var partes = linha.Split('|');
                GerenciarProfessores.Professores.Add(new Professor(partes[0], partes[1], partes[2], partes[3], int.Parse(partes[4])));
            }
        }
    }
    public static void SalvarTurmas()
    {
        using (StreamWriter sw = new StreamWriter("turmas.txt"))
        {
            foreach (var turma in GerenciarTurmas.Turmas)
            {
                sw.WriteLine($"{turma.Codigo}|{turma.Professor.Codigo}|{turma.Turno}|{turma.CapacidadeMaxima}|{turma.AlunosCount}|{turma.Ativa}");
            }
        }
    }
    public static void CarregarTurmas()
    {
        if (File.Exists("turmas.txt"))
        {
            var linhas = File.ReadAllLines("turmas.txt");
            foreach (var linha in linhas)
            {
                var partes = linha.Split('|');
                var professor = GerenciarProfessores.Professores.Find(p => p.Codigo.Equals(partes[1], StringComparison.OrdinalIgnoreCase));
                Turma turma = new Turma(partes[0], professor, partes[2], int.Parse(partes[3]))
                {
                    AlunosCount = int.Parse(partes[4]),
                    Ativa = bool.Parse(partes[5])
                };

                GerenciarTurmas.Turmas.Add(turma);
            }
        }
    }
    public static void SalvarSituacoes()
    {
        using (StreamWriter sw = new StreamWriter("situacoes.txt"))
        {
            foreach (var aluno in GerenciarAlunos.Alunos)
            {
                if (aluno.Situacao != null)
                    sw.WriteLine($"{aluno.Matricula}|{aluno.Situacao}");
            }
        }
    }
    public static void CarregarSituacoes()
    {
        if (File.Exists("situacoes.txt"))
        {
            var linhas = File.ReadAllLines("situacoes.txt");
            foreach (var linha in linhas)
            {
                var partes = linha.Split('|');
                var aluno = GerenciarAlunos.Alunos.FirstOrDefault(a => a.Matricula == partes[0]);
                if (aluno != null)
                {
                    aluno.Situacao = partes[1];
                }
            }
        }
    }
}