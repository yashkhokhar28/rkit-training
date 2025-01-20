using StockPortfolioAPI.Models.ENUM;
using StockPortfolioAPI.Models.POCO;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockPortfolioAPI.BL
{
    public interface IDataHandlerService<T> where T : class
    {

        EnmEntryType Type { get; set; }

        void PreSave(T objDTO);

        Response Validation();

        Response Save();
        Object PreDelete(int id);

        Response ValidationOnDelete(Object objEMP01);
        int Delete(int id);
    }
}
