using AnekProvider.DataModels.Entities;
using AnekProvider.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnekProvider.DataModels.Services
{
    public class AnekService
    {
        private UnitOfWork _uof;
        public AnekService(UnitOfWork uof)
        {
            _uof = uof;
        }
        public BaseAnek Create(BaseAnek anek)
        {
            return _uof.Aneks.Create(anek);
        }


        #region SameOperations
        public BaseAnek FindByID(Guid id)
        {
            return _uof.Aneks.FindById(id);
        }

        public IEnumerable<BaseAnek> Get()
        {
            return _uof.Aneks.Get();
        }

        public IEnumerable<BaseAnek> Get(Func<BaseAnek, bool> predicate)
        {
            return _uof.Aneks.Get(predicate);
        }

        public void Remove(BaseAnek anek)
        {
            _uof.Aneks.Remove(anek);
        }

        public void Update(BaseAnek anek)
        {
            _uof.Aneks.Update(anek);
        }
        #endregion
    }
}
