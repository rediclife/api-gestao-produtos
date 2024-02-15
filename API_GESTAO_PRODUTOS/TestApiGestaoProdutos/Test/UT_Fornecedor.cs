using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Entities.Entities;
using TestApiGestaoProdutos.DTO;
using TestApiGestaoProdutos.Model;
using TestApiGestaoProdutos.Common;



namespace TestApiGestaoProdutos.Test
{
    [TestClass]
    public class UT_Fornecedor
    {
        [TestMethod]
        public void A_TestGetAll()
        {
            Util util = new Util();
            var result = util.ChamaApiGet("/Fornecedor").Result;

            var listaFornecedor = JsonConvert.DeserializeObject<ResponseMV<DTOFornecedor>>(result);

            Assert.IsTrue(listaFornecedor.Data.Any());
        }

        [TestMethod]
        public void B_TestGetById()
        {
            Util util = new Util();
            int idFornecedor = 1;
            var result = util.ChamaApiGet("/Fornecedor/GetById?id=" + idFornecedor).Result;

            var listaFornecedor = JsonConvert.DeserializeObject<ResponseMV<DTOFornecedor>>(result);

            Assert.IsTrue(listaFornecedor.Data.Any());
        }

        [TestMethod]
        public void C_TestAdd()
        {
            DTOFornecedor fornecedor = new DTOFornecedor()
            {
                Id = 0,
                Descricao = "Fornecedor Teste Unitário",
                Cnpj = "56233333006880",
                UserId = "5739c00e-64e3-48ee-85d9-be4d72720555"
            };

            Util util = new Util();
            var result = util.ChamaApiPost("/Fornecedor", fornecedor).Result;

            var listaFornecedor = JsonConvert.DeserializeObject<ResponseMV<DTOFornecedor>>(result);

            Assert.IsTrue(listaFornecedor.Data.Any());
        }

        [TestMethod]
        public void D_TestUpdate()
        {
            DTOFornecedor fornecedor = new DTOFornecedor()
            {
                Id = 1,
                Descricao = "Fornecedor Teste Unitário Alterar",
                Cnpj = "10000000000001",
                UserId = "5739c00e-64e3-48ee-85d9-be4d72720555"
            };

            Util util = new Util();
            var result = util.ChamaApiPut("/Fornecedor", fornecedor).Result;

            var listaFornecedor = JsonConvert.DeserializeObject<ResponseMV<DTOFornecedor>>(result);

            Assert.IsTrue(listaFornecedor.Data.Any());
        }

        [TestMethod]
        public void E_TestDelete()
        {
            Util util = new Util();
            int idFornecedor = 1;
            var result = util.ChamaApiGet("/Fornecedor/GetById?id=" + idFornecedor).Result;

            var listaFornecedor = JsonConvert.DeserializeObject<ResponseMV<DTOFornecedor>>(result);

            Assert.IsTrue(listaFornecedor.Data.Any());
        }
    }
}
