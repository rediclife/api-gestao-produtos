using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public class ServiceFornecedor : IServiceFornecedor
    {
        private readonly IFornecedor _IFornecedor;

        public ServiceFornecedor(IFornecedor IFornecedor)
        {
            _IFornecedor = IFornecedor;
        }
    }
}
