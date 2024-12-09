public class GerenciarHistorico
{
    public static void VerHistorico()
    {
        Console.Clear();
        if (GerenciarAlunos.Alunos.Count == 0)
        {
            Console.WriteLine("Nenhum aluno cadastrado.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Histórico de todos os alunos:");
        Console.WriteLine("------------------------------------------------------------");
        Console.WriteLine($"{"Matrícula",-10} {"Nome",-20} {"Idade",-5} {"Ano Escolar",-15} {"Turma",-10} {"Média",-6} {"Situação",-10}");
        Console.WriteLine("------------------------------------------------------------");

        foreach (var aluno in GerenciarAlunos.Alunos)
        {
            Console.WriteLine(aluno.Notas.Last());
            double media = aluno.Notas.Last().CalcularMedia();
            string situacao = media >= 6 ? "Aprovado" : "Reprovado";
            Console.WriteLine($"{aluno.Matricula,-10} {aluno.Nome,-20} {aluno.Idade,-5} {aluno.AnoEscolar,-15} {aluno.Turma,-10} {media:F1} {situacao,-10}");
        }

        Console.WriteLine("------------------------------------------------------------");
        Console.ReadKey();
    }
}