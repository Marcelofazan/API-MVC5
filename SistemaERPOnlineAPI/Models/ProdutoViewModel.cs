using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SistemaERPOnlineAPI.Models
{
    public class ProdutoViewModel
    {
        public int IdProduto { get; set; }

        [Required(ErrorMessage = "O campo Descrição é obrigatório.")]
        [StringLength(200, ErrorMessage = "A Descricao deve ter no máximo 200 caracteres.")]
        public string Descricao { get; set; }

        [Range(0, 999.99, ErrorMessage = "O valor deve estar entre 0,01 e 999,99.")]
        public decimal AliquotaIcms { get; set; }

        [Range(0, 999.99, ErrorMessage = "O valor deve estar entre 0,01 e 999,99.")]
        public decimal AliquotaIpi { get; set; }

        [Range(0, 9999999999999999.99, ErrorMessage = "O valor máximo permitido é 18 dígitos.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ValorVenda { get; set; }

        [StringLength(4, ErrorMessage = "A Unidade deve ter no máximo 4 caracteres.")]
        public string Unidade { get; set; }
    }
}