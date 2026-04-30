using SistemaERPOnlineAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaERPOnlineAPI.Repositorio
{
    public interface IProdutoRepository
    {
        IEnumerable<ProdutoViewModel> ConsultaProdutos();
        ProdutoViewModel ConsultaProduto(int IdProduto);
        void AdicionarProduto(ProdutoViewModel produto);
        void AtualizarProduto(int IdProduto, ProdutoViewModel produto);
        void RemoverProduto(int IdProduto);

    }
}