using SistemaERPOnlineAPI.Models;
using SistemaERPOnlineAPI.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace SistemaERPOnlineAPI.Aplicacao
{
    public class ProdutoAplicacao
    {
        private readonly Contexto contexto;
        public ProdutoAplicacao()
        {
            contexto = new Contexto();
        }

        public List<dynamic> ConsultaProdutos()
        {
            var listaprodutos = new List<dynamic>();
            var commandText = "SELECT Produtos.IdProduto as PrIdProduto, Produtos.Descricao as PrDescricao, Produtos.AliquotaIcms as PrAliquotaIcms, Produtos.AliquotaIpi as PrAliquotaIpi, Produtos.ValorVenda as PrValorVenda, Produtos.Unidade as PrUnidade ";
            commandText += "FROM Produtos ";
            commandText += "WHERE 1 = 1";

            var rows = contexto.ExecutaComandoComRetorno(commandText, null);
            foreach (var row in rows)
            {
                var tempprodutos = new ProdutoViewModel
                {
                    IdProduto = int.Parse(!string.IsNullOrEmpty(row["PrIdProduto"]) ? row["PrIdProduto"] : "0"),
                    Descricao = row["PrDescricao"],
                    AliquotaIcms = decimal.Parse(!string.IsNullOrEmpty(row["PrAliquotaIcms"]) ? row["PrAliquotaIcms"] : "0"),
                    AliquotaIpi = decimal.Parse(!string.IsNullOrEmpty(row["PrAliquotaIpi"]) ? row["PrAliquotaIpi"] : "0"),
                    ValorVenda = decimal.Parse(!string.IsNullOrEmpty(row["PrValorVenda"]) ? row["PrValorVenda"] : "0"),
                    Unidade = row["PrUnidade"],
                };
                listaprodutos.Add(tempprodutos);
            }
            return listaprodutos;
        }

        public ProdutoViewModel ConsultaProduto(int IdProduto)
        {
            var produto = new List<dynamic>();
            var commandText = "SELECT Produtos.IdProduto as PrIdProduto, Produtos.Descricao as PrDescricao, Produtos.AliquotaIcms as PrAliquotaIcms, Produtos.AliquotaIpi as PrAliquotaIpi, Produtos.ValorVenda as PrValorVenda, Produtos.Unidade as PrUnidade ";
            commandText += "FROM Produtos ";
            commandText += "WHERE Produtos.IdProduto = " + IdProduto;

            var rows = contexto.ExecutaComandoComRetorno(commandText, null);
            foreach (var row in rows)
            {
                var tempproduto = new ProdutoViewModel
                {
                    IdProduto = int.Parse(!string.IsNullOrEmpty(row["PrIdProduto"]) ? row["PrIdProduto"] : "0"),
                    Descricao = row["PrDescricao"],
                    AliquotaIcms = decimal.Parse(!string.IsNullOrEmpty(row["PrAliquotaIcms"]) ? row["PrAliquotaIcms"] : "0"),
                    AliquotaIpi = decimal.Parse(!string.IsNullOrEmpty(row["PrAliquotaIpi"]) ? row["PrAliquotaIpi"] : "0"),
                    ValorVenda = decimal.Parse(!string.IsNullOrEmpty(row["PrValorVenda"]) ? row["PrValorVenda"] : "0"),
                    Unidade = row["PrUnidade"],
                };
                produto.Add(tempproduto);
            }

            return produto.FirstOrDefault(); ;
        }

        public void AdicionarProduto(ProdutoViewModel produto)
        {
            const string commandText = " INSERT INTO Produtos (Descricao, AliquotaIcms, AliquotaIpi, ValorVenda, Unidade) VALUES (@Descricao, @AliquotaIcms, @AliquotaIpi, @ValorVenda, @Unidade) ";

            var parameters = new Dictionary<string, object>
            {
                {"Descricao", produto.Descricao},
                {"AliquotaIcms", produto.AliquotaIcms},
                {"AliquotaIpi", produto.AliquotaIpi},
                {"ValorVenda", produto.ValorVenda},
                {"Unidade", produto.Unidade}
            };

            int linhasAfetadas = contexto.ExecutaComando(commandText, parameters); 

            if (linhasAfetadas == 0)
            {
                throw new KeyNotFoundException($"Nenhum produto foi adicionado.");
            }
        }

        public void AtualizarProduto(ProdutoViewModel produto)
        {

            var commandText = "UPDATE Produtos SET ";
            commandText += " Descricao = @Descricao, ";
            commandText += " AliquotaIcms = @AliquotaIcms, ";
            commandText += " AliquotaIpi = @AliquotaIpi, ";
            commandText += " ValorVenda = @ValorVenda, ";
            commandText += " Unidade = @Unidade ";
            commandText += " WHERE IdProduto = @IdProduto ";

            var parameters = new Dictionary<string, object>
            {
                {"IdProduto", produto.IdProduto},
                {"Descricao", produto.Descricao},
                {"AliquotaIcms", produto.AliquotaIcms},
                {"AliquotaIpi", produto.AliquotaIpi},
                {"ValorVenda", produto.ValorVenda},
                {"Unidade", produto.Unidade}
            };

            int linhasAfetadas = contexto.ExecutaComando(commandText, parameters);

            if (linhasAfetadas == 0)
            {
                throw new KeyNotFoundException($"Nenhum produto foi alterado.");
            }
        }

        public void RemoverProduto(int IdProduto)
        {
            const string commandText = "DELETE FROM Produtos WHERE IdProduto = @IdProduto";

            var parametros = new Dictionary<string, object>
            {
                {"IdProduto", IdProduto}
            };

            int linhasAfetadas = contexto.ExecutaComando(commandText, parametros);

            if (linhasAfetadas == 0)
            {
                throw new KeyNotFoundException($"Nenhum produto foi excluido.");
            }
        }
    }
}