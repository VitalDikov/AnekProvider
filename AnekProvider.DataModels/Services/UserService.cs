using AnekProvider.DataModels.Entities;
using AnekProvider.DataModels.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnekProvider.DataModels.Services
{
    public class UserService
    {
        private UnitOfWork _uof;
        public UserService(UnitOfWork uof)
        {
            _uof = uof;
        }
        public VkUser Create(string userPage)
        {
            var Out = new VkUser()
            {
                UserPage = userPage,
            };
            _uof.Users.Create(Out);
            return Out;
        }


        #region SameOperations
        public VkUser FindByID(Guid id)
        {
            return _uof.Users.FindById(id);
        }

        public IEnumerable<VkUser> Get()
        {
            return _uof.Users.Get();
        }

        public IEnumerable<VkUser> Get(Func<VkUser, bool> predicate)
        {
            return _uof.Users.Get(predicate);
        }

        public void Remove(VkUser user)
        {
            _uof.Users.Remove(user);
        }

        public void Update(VkUser user)
        {
            _uof.Users.Update(user);
        }
        #endregion
    }
}