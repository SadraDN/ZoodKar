using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Data
{
    public interface IOrderRepository
    {

        #region "Queries"

        Task GetById(int id);
        Task GetAll();

        #endregion



        #region "Commands"

        Task<int> Add();
        Task Update();
        Task Delete();

        #endregion

    }
}
