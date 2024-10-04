using Fiap.Web.Donation3.Data;
using Fiap.Web.Donation3.Models;

namespace Fiap.Web.Donation3.Repository
{
    public class TrocaRepository
    {

        private readonly DataContext _dataContext;

        public TrocaRepository(DataContext context)
        {
            this._dataContext = context;
        }


        public Guid Insert(TrocaModel trocaModel)
        {
            _dataContext.Troca.Add(trocaModel);
            _dataContext.SaveChanges();

            return trocaModel.TrocaId;
        }


    }
}
