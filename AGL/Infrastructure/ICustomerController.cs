using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AGL.Models;

namespace AGL.Infrastructure
{
    public interface ICustomerController
    {
        //CancellationToken parameter is used to signal when the request has been cancelled
        Task<List<RootObject>> GetPet(CancellationToken cToken);
        Task<IEnumerable<CategoryViewModel>> GetPet(CancellationToken cToke,string category);

    }
}
