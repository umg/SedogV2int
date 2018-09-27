using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class NRIProjetos360ViewModel
    {
        public long ID_PROJNRI { get; set; }
        public string ID_ARTISTA { get; set; }
        public string ARTISTA { get; set; }
        public string PROJETO { get; set; }
        public string BU { get; set; }
        public DateTime DATA_INICIO { get; set; }
        public DateTime DATA_EXPIRACAO { get; set; }
        public string OBS { get; set; }
        public List<NRIProjetosContrato> CONTRATOS { get; set; }
        public List<NRIProjetosReceitas> RECEITAS { get; set; }
        public List<NRIProjetosAnexosViewModel> ANEXOS { get; set; }
    }
    public class NRIProjetosAnexosViewModel
    {
        public long ID_PROJNRI { get; set; }
        public string NOME_ARQUIVO { get; set; }
        public string OBS { get; set; }
    }
    public class NRIProjetosContrato
    {
        public long ID_PROJNRI { get; set; }
        public long ID_TIPO_CONTRATO { get; set; }
        public string TIPO_CONTRATO { get; set; }
        public string DESCRICAO { get; set; }
        public string DESCRICAO_TIPO_CONTRATO { get; set; }
    }
    public class NRIProjetosReceitas
    {
        public long ID_PROJNRI { get; set; }
        public long ID_SEQ { get; set; }
        public long ID_TIPO_RECEITA { get; set; }
        public string TIPO_RECEITA { get; set; }
        public long ID_RESPONSAVEL { get; set; }
        public string RESPONSAVEL { get; set; }
        public decimal PERCENT { get; set; }
        public string ACORDO { get; set; }
        public int QUANTIDADE { get; set; }
        public int SALDO { get; set; }
        public DateTime DATA_EXPIRACAO { get; set; }
        public string OBS_RECEITA { get; set; }
        public string DESCRICAO { get; set; }
        public int EMAIL_COBRANCA { get; set; }
    }
    public class NRITipoReceita
    {
        public long ID_TIPO_RECEITA { get; set; }
        public string TIPO_RECEITA { get; set; }
        public string DESCRICAO { get; set; }
    }
    public class NRITipoContrato
    {
        public long ID_TIPO_CONTRATO { get; set; }
        public string TIPO_CONTRATO { get; set; }
        public string DESCRICAO_TIPO_CONTRATO { get; set; }
    }
    public class NRIResponsavel
    {
        public long ID_RESPONSAVEL { get; set; }
        public string RESPONSAVEL { get; set; }
    }
}