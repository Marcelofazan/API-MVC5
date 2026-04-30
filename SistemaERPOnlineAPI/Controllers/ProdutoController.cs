using Google.Protobuf.WellKnownTypes;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using SistemaERPOnlineAPI.Models;
using SistemaERPOnlineAPI.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SistemaERPOnlineAPI.Controllers
{
    [RoutePrefix("api/Produto")]
    public class ProdutoController : ApiController
    {
        private readonly IProdutoRepository _repository;

        public ProdutoController()
        {
            _repository = new ProdutoRepository();
        }

        [HttpGet]
        [Route("ConsultaProdutos")]
        public IHttpActionResult ConsultaProdutos()
        {
            try
            {
                var listaProdutos = _repository.ConsultaProdutos();

                if (listaProdutos == null)
                {
                    return NotFound();
                }

                return Ok(listaProdutos);
            }
            catch (Exception)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }

        [HttpGet]
        [Route("ConsultaProduto/{idproduto}")]
        public IHttpActionResult ConsultaProduto(int IdProduto)
        {
            try
            {
                var Produto = _repository.ConsultaProduto(IdProduto);

                if (Produto == null)
                {
                    return NotFound();
                }

                return Ok(Produto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("AdicionarProduto")]
        [System.Web.Http.Description.ResponseType(typeof(List<ProdutoViewModel>))]
        public IHttpActionResult AdicionarProduto([FromBody] ProdutoViewModel produto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (produto == null)
            {
                return NotFound();
            }

            try
            {
                _repository.AdicionarProduto(produto);

                return Ok(new { mensagem = "Produto adicionado com sucesso", dados = produto });
            }
            catch (KeyNotFoundException ex)
            {
                return Content(HttpStatusCode.NotFound, new
                {
                    message = "O recurso solicitado não foi encontrado.",
                    detail = ex.Message
                });
            }
        }

        [HttpPut]
        [Route("AtualizarProduto/{idproduto}")]
        [System.Web.Http.Description.ResponseType(typeof(List<ProdutoViewModel>))]
        public IHttpActionResult AtualizarProduto(int IdProduto, [FromBody] ProdutoViewModel produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (IdProduto <= 0)
            {
                return BadRequest("Necessário informar Id ou Id válido.");
            }

            if (produto == null)
            {
                return NotFound();
            }

            try
            {
                _repository.AtualizarProduto(IdProduto, produto);

                return Ok(new { mensagem = "Produto alterado com sucesso", dados = produto });
            }
            catch (KeyNotFoundException ex)
            {
                return Content(HttpStatusCode.NotFound, new
                {
                    message = "O recurso solicitado não foi encontrado.",
                    detail = ex.Message
                });
            }
        }

        [HttpDelete]
        [Route("RemoverProduto/{idproduto}")]
        public IHttpActionResult RemoverProduto(int IdProduto)
        {
            try
            {
                if (IdProduto <= 0)
                {
                    return BadRequest("Necessário informar Id ou Id válido.");
                }

                _repository.RemoverProduto(IdProduto);

                return Ok(new { mensagem = "Produto excluído com sucesso" });
            }
            catch (KeyNotFoundException ex)
            {
                return Content(HttpStatusCode.NotFound, new
                {
                    message = "O recurso solicitado não foi encontrado.",
                    detail = ex.Message
                });
            }
        }
    }
}
