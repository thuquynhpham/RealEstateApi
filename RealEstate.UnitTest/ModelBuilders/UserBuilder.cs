using RealEstate.Domain.Models;

namespace RealEstate.UnitTest.ModelBuilders
{
    public class UserBuilder : ModelBuilderBase
    {
        private User _user;

        public UserBuilder(int id, string name, string email, string password, string phone)
        {
            _user = new User
            {
                UserId = id,
                Name = name,
                Email = email,
                Password = password,
                Phone = phone
            };
        }

        public User Build()
        {
            return _user;
        }
    }
}
