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
    public class FornecedorController : ControllerBase
    {
        private readonly IMapper _IMapper;
        private readonly IFornecedor _IFornecedor;
        private readonly IServiceFornecedor _IServiceFornecedor;

        public FornecedorController(IMapper IMapper, IFornecedor IFornecedor, IServiceFornecedor IServiceFornecedor)
        {
            _IMapper = IMapper;
            _IFornecedor = IFornecedor;
            _IServiceFornecedor = IServiceFornecedor;
        }

        [HttpGet]
        public async Task<ActionResult> ListAll()
        {
            try
            {
                var fornecedores = await _IFornecedor.ListAll();
                var fornecedoresMap = _IMapper.Map<List<FornecedorDTO>>(fornecedores);

                if (fornecedoresMap == null)
                {
                    return BadRequest(new ResponseModelView(false, "A pesquisa não retornou resultados"));
                }

                return Ok(new ResponseModelView<FornecedorDTO>(true, "Consulta realizada com sucesso", fornecedoresMap));
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
                Fornecedor fornecedor = await _IFornecedor.GetEntityById(id);
                var fornecedorMap = _IMapper.Map<FornecedorDTO>(fornecedor);

                if (fornecedor == null)
                {
                    return BadRequest(new ResponseModelView(false, "A pesquisa não retornou resultados"));
                }
                else if (fornecedor.Notificacoes.Count > 0)
                {
                    return BadRequest(new ResponseModelView<Notifies>(false, "Falha na validação dos dados", fornecedor.Notificacoes));
                }


                return Ok(new ResponseModelView<FornecedorDTO>(true, "Consulta realizada com sucesso", _IMapper.Map<FornecedorDTO>(fornecedorMap)));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelView(false, "Erro: " + ex.Message));
            }


        }


        [HttpGet("GetAllPagination")]
        public async Task<ActionResult> GetAllPagination(int page = 1, int pageSize = 10)
        {
            try
            {
                var fornecedores = await _IFornecedor.ListAll();
                var fornecedoresMap = _IMapper.Map<List<FornecedorDTO>>(fornecedores);

                if (fornecedoresMap == null)
                {
                    return BadRequest(new ResponseModelView(false, "A pesquisa não retornou resultados"));
                }

                var totalCount = fornecedoresMap.Count;


                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                var fornecedoresPorPagina = _IMapper.Map<List<FornecedorDTO>>(fornecedoresMap.Skip((page - 1) * pageSize).Take(pageSize).ToList());

                return Ok(new ResponseModelView<FornecedorDTO>(true, "Consulta realizada com sucesso", fornecedoresPorPagina));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelView(false, "Erro: " + ex.Message));
            }

        }

        [HttpPost]
        public async Task<ActionResult> Add(FornecedorDTO fornecedor)
        {
            try
            {
                fornecedor.UserId = User.FindFirst("idUsuario").Value;
                Fornecedor fornecedorMap = _IMapper.Map<Fornecedor>(fornecedor);
                await _IFornecedor.Add(fornecedorMap);

                if (fornecedorMap.Notificacoes.Count > 0)
                {
                    return Ok(new ResponseModelView<Notifies>(false, "Falha na validação dos dados", fornecedorMap.Notificacoes));
                }
                                
                return Ok( new ResponseModelView<FornecedorDTO>(true, "Fornecedor adicionado com sucesso", _IMapper.Map<FornecedorDTO>(fornecedorMap)));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelView(false, "Erro: " + ex.Message));
            }
            
        }

        [HttpPut]
        public async Task<ActionResult> Update(FornecedorDTO fornecedor)
        {
            try
            {
                var fornecedorMap = _IMapper.Map<Fornecedor>(fornecedor);
                await _IFornecedor.Update(fornecedorMap);

                if (fornecedorMap.Notificacoes.Count > 0)
                {
                    return Ok(new ResponseModelView<Notifies>(false, "Falha na validação dos dados", fornecedorMap.Notificacoes));
                }

                return Ok(new ResponseModelView<FornecedorDTO>(true, "Fornecedor alterado com sucesso", _IMapper.Map<FornecedorDTO>(fornecedorMap)));
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelView(false, "Erro: " + ex.Message));
            }
            
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(FornecedorDTO fornecedor)
        {
            try
            {
                var fornecedorMap = _IMapper.Map<Fornecedor>(fornecedor);
                await _IFornecedor.Delete(fornecedorMap);

                if (fornecedorMap.Notificacoes.Count > 0)
                {
                    return Ok(new ResponseModelView<Notifies>(false, "Falha na validação dos dados", fornecedorMap.Notificacoes));
                }

                return Ok(new ResponseModelView(true, "Fornecedor deletado com sucesso"));

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
                await _IFornecedor.Delete(id);
                
                return Ok(new ResponseModelView(true, "Fornecedor deletado com sucesso"));

            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseModelView(false, "Erro: " + ex.Message));
            }


        }
    }
}
