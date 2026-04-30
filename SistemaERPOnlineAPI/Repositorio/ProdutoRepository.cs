using SistemaERPOnlineAPI.Aplicacao;
using SistemaERPOnlineAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaERPOnlineAPI.Repositorio
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ProdutoAplicacao _produtoAplicacao;

        public ProdutoRepository()
        {
            _produtoAplicacao = new ProdutoAplicacao();
        }

        public IEnumerable<ProdutoViewModel> ConsultaProdutos()
        {
            var listaDinamica = _produtoAplicacao.ConsultaProdutos();

            var listaPrecos = listaDinamica.Select(x => new ProdutoViewModel
            {
                IdProduto = (int)x.IdProduto,
                Descricao = (string)x.Descricao,
                ValorVenda = (decimal)x.ValorVenda,
                AliquotaIcms = (decimal)x.AliquotaIcms,
                AliquotaIpi = (decimal)x.AliquotaIpi,
                Unidade = (string)x.Unidade
            }).ToList();

            return listaPrecos;
        }

        public ProdutoViewModel ConsultaProduto(int IdProduto)
        {
            if (IdProduto <= 0) throw new KeyNotFoundException("Produto não encontrado"); // Ou sua exceção customizada

            var buscaproduto = _produtoAplicacao.ConsultaProduto(IdProduto);
            if (buscaproduto == null) throw new KeyNotFoundException("Produto não encontrado"); // Ou sua exceção customizada;

            var produto = new ProdutoViewModel();
            {
                produto.IdProduto = buscaproduto.IdProduto;
                produto.Descricao = buscaproduto.Descricao;
                produto.ValorVenda = buscaproduto.ValorVenda;
                produto.AliquotaIcms = buscaproduto.AliquotaIcms;
                produto.AliquotaIpi = buscaproduto.AliquotaIpi;
                produto.Unidade = buscaproduto.Unidade;
            }
            return produto;
        }

        public void AdicionarProduto(ProdutoViewModel produtoVm)
        {
            if (produtoVm == null) throw new KeyNotFoundException("Produto não encontrado");

            try
            {
                _produtoAplicacao.AdicionarProduto(produtoVm);
            }
            catch (Exception ex)
            {
                throw new KeyNotFoundException($"Erro ao adicionar o produto no banco de dados." + ex.Message);
            }
        }

        public void AtualizarProduto(int IdProduto, ProdutoViewModel produtoVm)
        {
            if (IdProduto <= 0) throw new KeyNotFoundException("Produto não encontrado");
            if (produtoVm == null) throw new KeyNotFoundException("Produto não encontrado");

            var buscaproduto = _produtoAplicacao.ConsultaProduto(IdProduto);
            if (buscaproduto == null) throw new KeyNotFoundException("Produto não encontrado");

            if (buscaproduto != null)
            {
                try
                {
                    _produtoAplicacao.AtualizarProduto(produtoVm);
                }
                catch (Exception ex)
                {
                    throw new KeyNotFoundException($"Erro ao atualizar o produto no banco de dados." + ex.Message);
                }
            }
        }

        public void RemoverProduto(int IdProduto)
        {
            if (IdProduto <= 0) throw new KeyNotFoundException("Produto não encontrado");

            var buscaproduto = _produtoAplicacao.ConsultaProduto(IdProduto);
            if (buscaproduto == null) throw new KeyNotFoundException("Produto não encontrado");

            if (buscaproduto != null)
            {
                try
                {
                    _produtoAplicacao.RemoverProduto(IdProduto);
                }
                catch (Exception ex)
                {
                    throw new KeyNotFoundException($"Erro ao excluir o produto no banco de dados." + ex.Message);
                }
            }
        }
    }
}