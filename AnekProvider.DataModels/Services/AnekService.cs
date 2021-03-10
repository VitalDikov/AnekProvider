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
        public Anek Create(string url, string text)
        {
            var Out = new Anek()
            {
                Uri = url,
                Text = text
            };
            _uof.Aneks.Create(Out);
            return Out;
        }


        #region SameOperations
        public Anek FindByID(Guid id)
        {
            return _uof.Aneks.FindById(id);
        }

        public IEnumerable<Anek> Get()
        {
            return _uof.Aneks.Get();
        }

        public IEnumerable<Anek> Get(Func<Anek, bool> predicate)
        {
            return _uof.Aneks.Get(predicate);
        }

        public void Remove(Anek anek)
        {
            _uof.Aneks.Remove(anek);
        }

        public void Update(Anek anek)
        {
            _uof.Aneks.Update(anek);
        }
        #endregion
    }
}
