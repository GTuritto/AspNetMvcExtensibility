namespace System.Web.Mvc.Extensibility.Tests
{
    using Collections.Generic;
    using Linq;

    using Microsoft.Practices.ServiceLocation;

    using Moq;
    using Xunit;

    public class FilterRegistryTests
    {
        private readonly Mock<FakeServiceLocator> serviceLocator;
        private readonly FilterRegistryTestDouble registry;

        public FilterRegistryTests()
        {
            serviceLocator = new Mock<FakeServiceLocator>();

            serviceLocator.Setup(sl => sl.GetInstance<DummyFilter1>()).Returns(new DummyFilter1());
            serviceLocator.Setup(sl => sl.GetInstance<DummyFilter2>()).Returns(new DummyFilter2());
            serviceLocator.Setup(sl => sl.GetInstance<DummyFilter3>()).Returns(new DummyFilter3());
            serviceLocator.Setup(sl => sl.GetInstance<DummyFilter4>()).Returns(new DummyFilter4());

            registry = new FilterRegistryTestDouble(serviceLocator.Object);
        }

        [Fact]
        public void Should_be_able_Register_filter_for_controller()
        {
            registry.Register<FakeController, DummyFilter1>();

            Assert.Equal(1, registry.PublicItems[0].Filters.Count());
        }

        [Fact]
        public void Should_be_able_Register_and_configure_filter_for_controller()
        {
            registry.Register<FakeController, DummyFilter2>(filter =>
                                                            {
                                                                filter.IntegerProperty = 10;
                                                                filter.StringProperty = "foo";
                                                            });

            var item = (FilterRegistryControllerItem<FakeController>) registry.PublicItems[0];

            Assert.Equal(1, item.Filters.Count());
            Assert.Equal(10, ((DummyFilter2) item.Filters.ToList()[0]).IntegerProperty);
            Assert.Equal("foo", ((DummyFilter2)item.Filters.ToList()[0]).StringProperty);
        }

        [Fact]
        public void Should_be_able_Register_two_filters_for_controller()
        {
            registry.Register<FakeController, DummyFilter1, DummyFilter2>();

            Assert.Equal(2, registry.PublicItems[0].Filters.Count());
        }

        [Fact]
        public void Should_be_able_Register_three_filters_for_controller()
        {
            registry.Register<FakeController, DummyFilter1, DummyFilter2, DummyFilter3>();

            Assert.Equal(3, registry.PublicItems[0].Filters.Count());
        }

        [Fact]
        public void Should_be_able_Register_four_filters_for_controller()
        {
            registry.Register<FakeController, DummyFilter1, DummyFilter2, DummyFilter3, DummyFilter4>();

            Assert.Equal(4, registry.PublicItems[0].Filters.Count());
        }

        [Fact]
        public void Should_be_able_Register_filter_for_action()
        {
            registry.Register<FakeController, DummyFilter1>(c => c.Index());

            Assert.Equal(1, registry.PublicItems[0].Filters.Count());
        }

        [Fact]
        public void Should_be_able_Register_and_configure_filter_for_action()
        {
            registry.Register<FakeController, DummyFilter3>(c => c.Index(),
                                                            filter =>
                                                            {
                                                                filter.LongProperty = 10;
                                                                filter.DecimalProperty = 100;
                                                            });

            var item = (FilterRegistryActionItem<FakeController>)registry.PublicItems[0];

            Assert.Equal(1, item.Filters.Count());
            Assert.Equal(10, ((DummyFilter3)item.Filters.ToList()[0]).LongProperty);
            Assert.Equal(100, ((DummyFilter3)item.Filters.ToList()[0]).DecimalProperty);
        }

        [Fact]
        public void Should_be_able_Register_two_filters_for_action()
        {
            registry.Register<FakeController, DummyFilter1, DummyFilter2>(c => c.Index());

            Assert.Equal(2, registry.PublicItems[0].Filters.Count());
        }

        [Fact]
        public void Should_be_able_Register_three_filters_for_action()
        {
            registry.Register<FakeController, DummyFilter1, DummyFilter2, DummyFilter3>(c => c.Index());

            Assert.Equal(3, registry.PublicItems[0].Filters.Count());
        }

        [Fact]
        public void Should_be_able_Register_four_filters_for_action()
        {
            registry.Register<FakeController, DummyFilter1, DummyFilter2, DummyFilter3, DummyFilter4>(c => c.Index());

            Assert.Equal(4, registry.PublicItems[0].Filters.Count());
        }

        [Fact]
        public void Matching_should_return_matched_filters()
        {
            var controllerContext = new ControllerContext
                                        {
                                            Controller = new FakeController()
                                        };

            var controllerDescriptor = new Mock<ControllerDescriptor>();
            controllerDescriptor.SetupGet(cd => cd.ControllerName).Returns("Fake");

            var actionDescriptor = new Mock<ActionDescriptor>();
            actionDescriptor.SetupGet(ad => ad.ControllerDescriptor).Returns(controllerDescriptor.Object);
            actionDescriptor.SetupGet(ad => ad.ActionName).Returns("Index");

            registry.Register<FakeController, DummyFilter1, DummyFilter4>();
            registry.Register<FakeController, DummyFilter2, DummyFilter3>(c => c.Index());

            var filters = registry.Matching(controllerContext, actionDescriptor.Object);

            Assert.IsType<DummyFilter1>(filters.AuthorizationFilters[0]);
            Assert.IsType<DummyFilter2>(filters.ActionFilters[0]);
            Assert.IsType<DummyFilter3>(filters.ResultFilters[0]);
            Assert.IsType<DummyFilter4>(filters.ExceptionFilters[0]);
        }

        private sealed class FilterRegistryTestDouble : FilterRegistry
        {
            public FilterRegistryTestDouble(IServiceLocator serviceLocator) : base(serviceLocator)
            {
            }

            public IList<FilterRegistryItemBase> PublicItems
            {
                get
                {
                    return Items;
                }
            }
        }

        // ReSharper disable ClassNeverInstantiated.Local
        private sealed class FakeController : Controller
        // ReSharper restore ClassNeverInstantiated.Local
        {
            public ActionResult Index()
            {
                return View();
            }
        }

        private sealed class DummyFilter1 : FilterAttribute, IAuthorizationFilter
        {
            public void OnAuthorization(AuthorizationContext filterContext)
            {
                throw new NotImplementedException();
            }
        }

        private sealed class DummyFilter2 : FilterAttribute, IActionFilter
        {
            public int IntegerProperty
            {
                get;
                set;
            }

            public string StringProperty
            {
                get;
                set;
            }

            public void OnActionExecuting(ActionExecutingContext filterContext)
            {
                throw new NotImplementedException();
            }

            public void OnActionExecuted(ActionExecutedContext filterContext)
            {
                throw new NotImplementedException();
            }
        }

        private sealed class DummyFilter3 : FilterAttribute, IResultFilter
        {
            public long LongProperty
            {
                get;
                set;
            }

            public Decimal DecimalProperty
            {
                get;
                set;
            }

            public void OnResultExecuting(ResultExecutingContext filterContext)
            {
                throw new NotImplementedException();
            }

            public void OnResultExecuted(ResultExecutedContext filterContext)
            {
                throw new NotImplementedException();
            }
        }

        private sealed class DummyFilter4 : FilterAttribute, IExceptionFilter
        {
            public void OnException(ExceptionContext filterContext)
            {
                throw new NotImplementedException();
            }
        }
    }
}