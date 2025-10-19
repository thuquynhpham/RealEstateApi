using Microsoft.Extensions.Time.Testing;
using NSubstitute;
using RealEstate.Api.Handlers._Shared;
using RealEstate.Core;
using RealEstate.Domain.Repositories;
using RealEstate.UnitTest.Fakes;

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

        protected DateTimeOffset _now;
        protected TimeProvider _timeProvider = null!;

        protected IRequestContextProvider _requestContextProvider = null!;

        protected Exception? _exception;

        protected string _userId = "user@test.com";

        [OneTimeSetUp]
        public virtual void SetUp()
        {
            InitializeProviders();
            SetUpData();
            InitializeRepositories();
            InitializeUnitOfWork();
        }

        [SetUp]
        public virtual Task RunHandlerAsync()
        {
            return Task.CompletedTask;
        }

        [TearDown]
        public virtual void TearDown()
        {
            _unitOfWork?.Dispose();
        }

        [OneTimeTearDown]
        public virtual void OneTimeTearDown()
        {
            _unitOfWork?.Dispose();
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
            _categoryRepository = new FakeCategoryRepository(_categories, _requestContextProvider);
            _userRepository = new FakeUserRepository(_users, _requestContextProvider);
            _propertyRepository = new FakePropertyRepository(_properties, _requestContextProvider);
        }

        protected virtual void InitializeUnitOfWork()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _unitOfWork.Categories.Returns(_categoryRepository);
            _unitOfWork.Users.Returns(_userRepository);
            _unitOfWork.Properties.Returns(_propertyRepository);
        }

        protected virtual void InitializeProviders()
        {
            _requestContextProvider = Substitute.For<IRequestContextProvider>();
            _requestContextProvider.GetUserEmail().Returns(_userId);

            _now = TimeProvider.System.GetUtcNow();
            _timeProvider = new FakeTimeProvider(_now);
        }

    }
}
