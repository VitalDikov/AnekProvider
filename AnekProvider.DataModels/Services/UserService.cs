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
        public User Create(string profileID)
        {
            var Out = new User()
            {
                UserProfile = profileID
            };
            _uof.Users.Create(Out);
            return Out;
        }


        #region SameOperations
        public User FindByID(Guid id)
        {
            return _uof.Users.FindById(id);
        }

        public IEnumerable<User> Get()
        {
            return _uof.Users.Get();
        }

        public IEnumerable<User> Get(Func<User, bool> predicate)
        {
            return _uof.Users.Get(predicate);
        }

        public void Remove(User user)
        {
            _uof.Users.Remove(user);
        }

        public void Update(User user)
        {
            _uof.Users.Update(user);
        }
        #endregion
    }
}