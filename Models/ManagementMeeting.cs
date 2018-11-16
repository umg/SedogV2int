using System;
using System.Collections.Generic;

namespace SEDOGv2.Models
{
    public class ManagementMeeting
    {
        public int ManagementMeetingId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
        public List<string> ModelErros { get; set; }

        public ManagementMeeting()
        {
            ModelErros = new List<string>();
        }
        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Nome))
            {
                ModelErros.Add("Nome do arquivo vazio.");
            }
            if (string.IsNullOrEmpty(FilePath))
            {
                ModelErros.Add("Nenhum arquivo selecionado.");
            }
            return ModelErros.Count == 0 ? true : false;
        }
    }
}