using AtualizaDadosCopa.Models.Context;


namespace AtualizaDadosCopa
{
    class Program
    {
        static void Main(string[] args)
        {
            PLProjetoProvider myPLProjetoProvider = new PLProjetoProvider();
            myPLProjetoProvider.RODA_ATUALIZACAO_TABAUX_COPA();
        }
    }
}
