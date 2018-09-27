using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Models
{
    public class ManutencaoProjetoViewModel
    {
        public long idProjSedog { get; set; }
        public string Projeto { get; set; }
        public string Artista { get; set; }
        public string IdArtista { get; set; }
        public DateTime LancamentoProduto { get; set; }
        public string Obs { get; set; }
        public int IdTipoRelease { get; set; }
        public int IdTipoContrato { get; set; }
        public int IdTipoProcesso { get; set; }
        public string Nacionalidade { get; set; }
        public string Responsavel { get; set; }
        public decimal EBITDAProjetado { get; set; }
        public decimal RECEITAProjetado { get; set; }
        public int IdGeneroMusical { get; set; }
        public int IdMedidorKPI { get; set; }
        public List<PL_Projeto_Sedog> plProjetoSedog { get; set; }
        public List<PL_Projeto> plProjeto { get; set; }
        public List<PL_Produto> plProduto { get; set; }
        public List<PL_Produto_BUS> plProdutoBus { get; set; }
        public List<PL_Receitas_Extras> plReceitasExtras { get; set; }
        public PL_Parametros_Direitos plParametrosDireitos { get; set; }
        public List<TipoRelease> _TipoRelease { get; set; }
        public List<TipoContrato> _TipoContrato { get; set; }
        public List<TipoProcesso> _TipoProcesso { get; set; }
        public List<TipoReceita> _TipoReceita { get; set; }
        public List<Usuario> _Usuarios { get; set; }
        public List<Genero_Musical> _TipoGeneroMusical { get; set; }
        public List<Medidor_KPI> _MedidorKPI { get; set; }


    }
    public class PL_Projeto_Sedog
    {
        public long IDPROJ_SEDOG { get; set; }
        public string PROJETO { get; set; }
        public string IDARTISTA { get; set; }
        public string ARTISTA { get; set; }
        public string OBS { get; set; }
        public int ID_TIPO_CONTRATO { get; set; }
        public int ID_TIPO_PROCESSO { get; set; }
        public int ID_TIPO_RELEASE { get; set; }
        public DateTime DATA_LANCAMENTO { get; set; }
        public string NACIONALIDADE { get; set; }
        public string COMPONENTES { get; set; }
        public decimal PERC_EXECPUBLICA { get; set; }
        public string LOGIN_RESPONSAVEL { get; set; }
        public decimal EBITIDA_PROJETADO { get; set; }
        public decimal RECEITA_PROJETADA { get; set; }
        public int ID_GENERO_MUSICAL { get; set; }
        public int ID_MEDIDOR_KPI { get; set; }
    }
    public class PL_Projeto
    {
        public long IDPROJ_SEDOG { get; set; }
        public string IDPROJ { get; set; }
        public string PROJECTNAME { get; set; }
    }
    public class PL_Produto_BUS
    {
        public long IDPROJ_SEDOG { get; set; }
        public string COD_PRODUTO { get; set; }
        public string PACKING { get; set; }
        public string BU { get; set; }
    }
    public class PL_Produto
    {
        public long IDPROJ_SEDOG { get; set; }
        public string COD_PRODUTO { get; set; }
        public string PACKING { get; set; }
        public string COD_BARRAS { get; set; }
        public string MIDIA { get; set; }
        public int DATA_LANCAMENTO { get; set; }
        public string COD_ARTISTA { get; set; }
        public string TITULO_PRODUTO { get; set; }
        public string ARTISTA { get; set; }
        public string COD_SELO { get; set; }
        public string SELO { get; set; }
        public int CBNSET { get; set; }
        public string OBS { get; set; }
    }
    public class PL_Parametros_Direitos
    {
        public long IDPROJ_SEDOG { get; set; }
        public decimal PERCENT_ARTISTICO_DIG { get; set; }
        public decimal PERCENT_AUTORAL_DIG { get; set; }
    }
    public class PL_Receitas_Extras
    {
        public long IDPROJ_SEDOG { get; set; }
        public int ID_TIPO_RECEITA { get; set; }
        public string TIPO_RECEITA { get; set; }
        public decimal VALOR { get; set; }
        public decimal DESPESA { get; set; }
        public int ANOMES { get; set; }
    }

    public class Genero_Musical
    {
        public int ID_GENERO_MUSICAL { get; set; }
        public string GENERO { get; set; }
        public string TIPO { get; set; }
    }
    public class Medidor_KPI
    {
        public int ID_MEDIDOR_KPI { get; set; }           
        public string MEDIDOR_KPI { get; set; }
    }
}