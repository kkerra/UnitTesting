using Moq;
using TestingLib.Shop;

namespace UnitTesting.kerra
{
    public class ShopServiceTest
    {
        private readonly Mock<ICustomerRepository> mockCustomerRepository;
        private readonly Mock<IOrderRepository> mockOrderRepository;
        private readonly Mock<INotificationService> mockNotificationService;
        private readonly ShopService shopService;

        public ShopServiceTest()
        {
            mockCustomerRepository = new Mock<ICustomerRepository>();
            mockOrderRepository = new Mock<IOrderRepository>();
            mockNotificationService = new Mock<INotificationService>();
            shopService = new ShopService(mockCustomerRepository.Object, mockOrderRepository.Object, mockNotificationService.Object);
        }

        [Fact]
        public void CreateOrder_ShouldAddOrderAndSendNotification()
        {
            var customer = new Customer { Id = 1, Name = "ааа", Email = "ааааа" };
            var order = new Order { Id = 1, Amount = 100, Customer = customer };

            shopService.CreateOrder(order);

            mockOrderRepository.Verify(repo => repo.AddOrder(order), Times.Once);
            mockNotificationService.Verify(service => service.SendNotification(
                order.Customer.Email,
                $"Order {order.Id} created for customer {order.Customer.Name} total price {order.Amount}"), Times.Once);

        }

        [Fact]
        public void GetCustomerInfo_ShouldReturnCorrectInfo()
        {
            var customer = new Customer { Id = 1, Name = "ааа" };
            var orders = new List<Order> { new Order { Customer = customer }, new Order { Customer = customer } };

            mockCustomerRepository.Setup(repo => repo.GetCustomerById(1)).Returns(customer);
            mockOrderRepository.Setup(repo => repo.GetOrders()).Returns(orders);

            var result = shopService.GetCustomerInfo(1);

            Assert.Equal("Customer ааа has 2 orders", result);
        }
    }
}
