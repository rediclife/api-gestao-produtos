using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceProduto : IServiceProduto
    {
        private readonly IProduto _IProduto;

        public ServiceProduto(IProduto IProduto)
        {
            _IProduto = IProduto;
        }
        public async Task<List<Produto>> ListarProdutosAtivos()
        {
            return await _IProduto.ListarProdutos(p => p.Ativo);
        }

        public async Task Adicionar(Produto Objeto)
        {
            var validaDescricao = Objeto.ValidarPropriedadeString(Objeto.Descricao, "Descricao");
            if (validaDescricao)
            {
                if(Objeto.DataFabricacao >= Objeto.DataValidade)
                {
                    Objeto.AdicionarNotificacao("Data de fabricação não pode ser maior ou igual a data de validade", "DataFabricacao");
                }
                else
                {
                    Objeto.Ativo = true;
                    await _IProduto.Add(Objeto);
                }
            }
        }

        public async Task Atualizar(Produto Objeto)
        {
            var validaDescricao = Objeto.ValidarPropriedadeString(Objeto.Descricao, "Descricao");
            if (validaDescricao)
            {
                if (Objeto.DataFabricacao >= Objeto.DataValidade)
                {
                    Objeto.AdicionarNotificacao("Data de fabricação não pode ser maior ou igual a data de validade", "DataFabricacao");
                }
                else
                {
                    Objeto.Ativo = true;
                    await _IProduto.Update(Objeto);
                }
            }
        }

        public async Task DeletarLogic(int Id)
        {
            try
            {
                var Objeto = await _IProduto.GetEntityById(Id);
                Objeto.Ativo = false;

                await _IProduto.Update(Objeto);
            }
            catch (Exception ex)
            {
                throw ex;
            }         
        }        
    }
}
