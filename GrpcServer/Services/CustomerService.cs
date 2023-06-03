using Grpc.Core;

namespace GrpcServer.Services
{
    public class CustomerService : Customer.CustomerBase
    {
        private readonly ILogger<CustomerService> _logger;   
        public CustomerService(ILogger<CustomerService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel outPut = new CustomerModel();

            if (request.UserId == 1)
            {
                outPut.FirstName = "ali";
                outPut.LastName = "hoseini";
            }
            else if (request.UserId == 2)
            {
                outPut.FirstName = "amir";
                outPut.LastName = "ostad";
            }
            else
            {
                outPut.FirstName = "hosein";
                outPut.LastName = "haghani";
            }

            return Task.FromResult(outPut);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = new List<CustomerModel> {
                new CustomerModel
                {
                    FirstName = "amir",
                    LastName = "ostad",
                    Age = 28,
                    EmailAddress = "something@gmail.com",
                    IsActice = true
                } ,
                new CustomerModel
                {
                    FirstName = "hosein",
                    LastName = "ostad",
                    Age = 25,
                    EmailAddress = "something2@gmail.com",
                    IsActice = false
                }
            };

            foreach (var item in customers)
            {
                await responseStream.WriteAsync(item);
            }
        }
    }
}
