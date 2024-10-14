using Moq;
using TestingLib.Shop;

namespace UnitTesting.kerra
{
    public class ShopServiceTest
    {
        private readonly Mock<ICustomerRepository> _mockCustomerRepository;
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly Mock<INotificationService> _mockNotificationService;
        private readonly ShopService _shopService;

        public ShopServiceTest()
        {
            _mockCustomerRepository = new Mock<ICustomerRepository>();
            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockNotificationService = new Mock<INotificationService>();
            _shopService = new ShopService(_mockCustomerRepository.Object, _mockOrderRepository.Object, _mockNotificationService.Object);
        }

        [Fact]
        public void CreateOrder_ShouldAddOrder()
        {
            var customer = new Customer { Id = 1, Name = "ааа", Email = "ааааа" };
            var order = new Order { Id = 1, Amount = 100, Customer = customer };

            _shopService.CreateOrder(order);

            _mockOrderRepository.Verify(repo => repo.AddOrder(order), Times.Once);
        }

        [Fact]
        public void SendNotification_ShouldSendCorrectNotification()
        {
            var customer = new Customer { Id = 1, Name = "ааа", Email = "ааааа" };
            var order = new Order { Id = 1, Amount = 100, Customer = customer };

            _shopService.CreateOrder(order);

            _mockNotificationService.Verify(service => service.SendNotification(
                order.Customer.Email,
                $"Order {order.Id} created for customer {order.Customer.Name} total price {order.Amount}"), Times.Once);
        }

        [Fact]
        public void GetCustomerInfo_ShouldReturnCorrectInfo()
        {
            var customer = new Customer { Id = 1, Name = "ааа" };
            var orders = new List<Order> { new Order { Customer = customer }, new Order { Customer = customer } };

            _mockCustomerRepository.Setup(repo => repo.GetCustomerById(1)).Returns(customer);
            _mockOrderRepository.Setup(repo => repo.GetOrders()).Returns(orders);

            var result = _shopService.GetCustomerInfo(1);

            Assert.Equal("Customer ааа has 2 orders", result);
        }

        [Fact]
        public void CreateOrder_ArgumentException()
        {
            var existingOrder = new Order { Id = 1, Customer = new Customer { Email = "test@example.com", Name = "John Doe" }, Amount = 100 };
            _mockOrderRepository.Setup(repo => repo.GetOrderById(existingOrder.Id)).Returns(existingOrder);

            var exception = Assert.Throws<ArgumentException>(() => _shopService.CreateOrder(existingOrder));
            Assert.Equal("Order with current id already exists", exception.Message);
        }

        [Fact]
        public void GetCustomerInfo_ShouldReturnCorrectInfoWhenCustomerHasNoOrders()
        {
            var customer = new Customer { Id = 1, Name = "Jane Doe" };
            var orders = new List<Order>();

            _mockCustomerRepository.Setup(repo => repo.GetCustomerById(1)).Returns(customer);
            _mockOrderRepository.Setup(repo => repo.GetOrders()).Returns(new List<Order>());

            var result = _shopService.GetCustomerInfo(1);

            Assert.Equal("Customer Jane Doe has 0 orders", result);
        }
    }
}
