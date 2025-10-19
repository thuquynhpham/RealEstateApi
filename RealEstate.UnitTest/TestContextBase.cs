using RealEstate.Api.Handlers._Shared;
using RealEstate.Domain.Repositories;

namespace RealEstate.UnitTest
{
    public abstract class TestContextBase
    {
        protected IUnitOfWork _unitOfWork = null!;

        protected ICategoryRepository? _categoryRepository;
        protected IUserRepository? _userRepository;
        protected IPropertyRepository? _propertyRepository;

        protected List<Domain.Models.Category> _categories = [];
        protected List<Domain.Models.User> _users = [];
        protected List<Domain.Models.Property> _properties = [];

        protected TimeProvider _timeProvider = null!;

        protected Exception? _exception;

        [OneTimeSetUp]
        public virtual void SetUp()
        {

        }

        [SetUp]
        public virtual Task RunHandlerAsync()
        {
            return Task.CompletedTask;
        }

        protected DataContext GetDataContext()
        {
            return new DataContext();
        }

        protected virtual void SetUpData()
        {

        }

        protected virtual void InitializeRepositories()
        {

        }

    }
}
