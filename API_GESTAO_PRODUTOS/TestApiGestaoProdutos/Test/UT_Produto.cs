using Entities.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApiGestaoProdutos.Common;
using TestApiGestaoProdutos.DTO;
using TestApiGestaoProdutos.Model;

namespace TestApiGestaoProdutos.Test
{
    internal class UT_Produto
    {
        [TestMethod]
        public void A_TestGetAll()
        {
            Util util = new Util();
            var result = util.ChamaApiGet("/Produto").Result;

            var listaProduto = JsonConvert.DeserializeObject<ResponseMV<DTOProduto>>(result);

            Assert.IsTrue(listaProduto.Data.Any());
        }

        [TestMethod]
        public void B_TestGetById()
        {
            Util util = new Util();
            int idProduto = 1;
            var result = util.ChamaApiGet("/Produto/GetById?id=" + idProduto).Result;

            var listaProduto = JsonConvert.DeserializeObject<ResponseMV<DTOProduto>>(result);

            Assert.IsTrue(listaProduto.Data.Any());
        }

        [TestMethod]
        public void C_TestAdd()
        {
            DTOProduto produto = new DTOProduto()
            {
                Id = 5,
                Descricao = "Produto Teste Unitario",
                Ativo = true,
                DataFabricacao = Convert.ToDateTime("2024-02-14T01:04:33"),
                DataValidade = Convert.ToDateTime("2024-05-15T01:04:33"),
                FornecedorId = 2,
                UserId = "5739c00e-64e3-48ee-85d9-be4d72720555"
            };

            Util util = new Util();
            var result = util.ChamaApiPost("/Produto", produto).Result;

            var listaProduto = JsonConvert.DeserializeObject<ResponseMV<DTOProduto>>(result);

            Assert.IsTrue(listaProduto.Data.Any());
        }

        [TestMethod]
        public void DTestUpdate()
        {
            DTOProduto produto = new DTOProduto()
            {
                Id = 5,
                Descricao = "Produto Teste Unitario",
                Ativo = true,
                DataFabricacao = Convert.ToDateTime("2024-02-14T01:04:33"),
                DataValidade = Convert.ToDateTime("2024-05-15T01:04:33"),
                FornecedorId = 2,
                UserId = "5739c00e-64e3-48ee-85d9-be4d72720555"
            };

            Util util = new Util();
            var result = util.ChamaApiPut("/Produto", produto).Result;

            var listaProduto = JsonConvert.DeserializeObject<ResponseMV<DTOProduto>>(result);

            Assert.IsTrue(listaProduto.Data.Any());
        }

        [TestMethod]
        public void E_TestDelete()
        {
            Util util = new Util();
            int idProduto = 5;
            var result = util.ChamaApiGet("/Produto/GetById?id=" + idProduto).Result;

            var listaProduto = JsonConvert.DeserializeObject<ResponseMV<DTOProduto>>(result);

            Assert.IsTrue(listaProduto.Data.Any());
        }
    }
}
