using AutoMapper;
using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Domain.Services;
using Entities.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOs;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProdutoController : ControllerBase
    {
        private readonly IMapper _IMapper;
        private readonly IProduto _IProduto;
        private readonly IServiceProduto _IServiceProduto;

        public ProdutoController(IMapper IMapper, IProduto IProduto, IServiceProduto IServiceProduto)
        {
            _IMapper = IMapper;
            _IProduto = IProduto;
            _IServiceProduto = IServiceProduto;
        }

        [HttpGet]
        public async Task<ActionResult> ListAll()
        {

            try
            {
                var produtos = await _IProduto.ListAll();
                var produtosMap = _IMapper.Map<List<ProdutoDTO>>(produtos);

                if (produtosMap == null)
                {
                    return BadRequest(new ResponseModelView(false, "A pesquisa não retornou resultados"));
                }

                return Ok(new ResponseModelView<ProdutoDTO>(true, "Consulta realizada com sucesso", produtosMap));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelView(false, "Erro: " + ex.Message));
            }
        }

        [HttpGet("GetById")]
        public async Task<ActionResult> GetById(int id)
        {

            try
            {
                Produto produto = await _IProduto.GetEntityById(id);
                var produtoMap = _IMapper.Map<ProdutoDTO>(produto);

                if (produtoMap == null)
                {
                    return BadRequest(new ResponseModelView(false, "A pesquisa não retornou resultados"));
                }

                return Ok(new ResponseModelView<ProdutoDTO>(true, "Consulta realizada com sucesso", _IMapper.Map<ProdutoDTO>(produtoMap)));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelView(false, "Erro: " + ex.Message));
            }
        }

        [HttpGet("ListarAtivos")]
        public async Task<ActionResult> ListarAtivos()
        {
            try
            {
                var produtos = await _IServiceProduto.ListarProdutosAtivos();
                var ProdutoMap = _IMapper.Map<List<ProdutoDTO>>(produtos);

                if (ProdutoMap == null)
                {
                    return BadRequest(new ResponseModelView(false, "A pesquisa não retornou resultados"));
                }

                return Ok(new ResponseModelView<ProdutoDTO>(true, "Consulta realizada com sucesso", ProdutoMap));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelView(false, "Erro: " + ex.Message));
            }
        }

        [HttpGet("ListarAtivosPagination")]
        public async Task<ActionResult> ListarAtivosPagination(int page = 1, int pageSize = 10)
        {
            try
            {
                var produtos = await _IServiceProduto.ListarProdutosAtivos();
                var ProdutoMap = _IMapper.Map<List<ProdutoDTO>>(produtos);

                if (ProdutoMap == null)
                {
                    return BadRequest(new ResponseModelView(false, "A pesquisa não retornou resultados"));
                }

                var totalCount = ProdutoMap.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                var produtosPorPagina = _IMapper.Map<List<ProdutoDTO>>(ProdutoMap.Skip((page - 1) * pageSize).Take(pageSize).ToList());

                return Ok(new ResponseModelView<ProdutoDTO>(true, "Consulta realizada com sucesso", produtosPorPagina));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelView(false, "Erro: " + ex.Message));
            }

        }

        [HttpPost]
        public async Task<ActionResult> Add(ProdutoDTO produto)
        {

            try
            {
                produto.UserId = User.FindFirst("idUsuario").Value;
                Produto produtoMap = _IMapper.Map<Produto>(produto);
                await _IServiceProduto.Adicionar(produtoMap);

                if (produtoMap.Notificacoes.Count > 0)
                {
                    return Ok(new ResponseModelView<Notifies>(false, "Falha na validação dos dados", produtoMap.Notificacoes));
                }

                return Ok(new ResponseModelView<ProdutoDTO>(true, "Produto adicionado com sucesso", _IMapper.Map<ProdutoDTO>(produtoMap)));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelView(false, "Erro: " + ex.Message));
            }

            
        }

        [HttpPut]
        public async Task<ActionResult> Update(ProdutoDTO produto)
        {
            
            try
            {
                Produto produtoMap = _IMapper.Map<Produto>(produto);
                await _IServiceProduto.Atualizar(produtoMap);


                if (produtoMap.Notificacoes.Count > 0)
                {
                    return Ok(new ResponseModelView<Notifies>(false, "Falha na validação dos dados", produtoMap.Notificacoes));
                }

                return Ok(new ResponseModelView<ProdutoDTO>(true, "Produto alterado com sucesso", _IMapper.Map<ProdutoDTO>(produtoMap)));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelView(false, "Erro: " + ex.Message));
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(ProdutoDTO produto)
        {
            
            try
            {
                var produtoMap = _IMapper.Map<Produto>(produto);
                await _IProduto.Delete(produtoMap);

                if (produtoMap.Notificacoes.Count > 0)
                {
                    return Ok(new ResponseModelView<Notifies>(false, "Falha na validação dos dados", produtoMap.Notificacoes));
                }

                return Ok(new ResponseModelView(true, "Produto deletado com sucesso"));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelView(false, "Erro: " + ex.Message));
            }
        }

        [HttpDelete("id={id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _IProduto.Delete(id);

                return Ok(new ResponseModelView(true, "Produto deletado com sucesso"));

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelView(false, "Erro: " + ex.Message));
            }


        }

        [HttpDelete("DeleteLogic")]
        public async Task<ActionResult> DeleteLogic(int id)
        {
            try
            {
                await _IServiceProduto.DeletarLogic(id);

                return Ok(new ResponseModelView(true, "Produto deletado com sucesso"));

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelView(false, "Erro: " + ex.Message));
            }


        }     
    }
}
