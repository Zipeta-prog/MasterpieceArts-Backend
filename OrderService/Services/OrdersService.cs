using OrderService.Models;
using OrderService.Models.Dto;
using OrderService.Services.IService;
using OrderService.Data;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using Stripe;

namespace OrderService.Services
{
    public class OrdersService : IOrder
    {
        private readonly ApplicationDbContext _context;
        private readonly IUser _userService;
        private readonly IBid _bidService;
        //private readonly IMessageBus _messageBUs;

        public OrdersService(IUser userService, IBid bidService, ApplicationDbContext context)
        {
            _userService = userService;
            _context = context;
            _bidService = bidService;

        }

        public async Task<List<Orders>> GetAllOrders(Guid userId)
        {
            return await _context.Order.Where(b => b.BidderId == userId).ToListAsync();
        }

        public async Task<Orders> GetOrderById(Guid Id)
        {
            return await _context.Order.Where(b => b.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<StripeRequestDto> MakePayments(StripeRequestDto stripeRequestDto)
        {
            var order = await _context.Order.Where(x => x.Id == stripeRequestDto.OrderId).FirstOrDefaultAsync();
            var bid = await _bidService.GetBidById((order.BidId).ToString());

            var options = new SessionCreateOptions()
            {
                SuccessUrl = stripeRequestDto.ApprovedUrl,
                CancelUrl = stripeRequestDto.CancelUrl,
                Mode = "payment",
                LineItems = new List<SessionLineItemOptions>()
            };



            var item = new SessionLineItemOptions()
            {
                PriceData = new SessionLineItemPriceDataOptions()
                {
                    UnitAmount = (long)order.TotalAmount * 100,
                    Currency = "kes",

                    ProductData = new SessionLineItemPriceDataProductDataOptions()
                    {
                        Name = bid.ArtName,
                        Images = new List<string> { "https://imgs.search.brave.com/av4uh1BAXrv7q2gkJt-E709vrIz3mB1-wrcPDtDyZNI/rs:fit:500:0:0/g:ce/aHR0cHM6Ly93d3cu/ZXhwZXJ0YWZyaWNh/LmNvbS9pbWFnZXMv/YXJlYS8xODI5X2wu/anBn" }
                    }
                },
                Quantity = 1


            };

            var service = new SessionService();
            Session session = service.Create(options);

            // URL//ID

            stripeRequestDto.StripeSessionUrl = session.Url;
            stripeRequestDto.StripeSessionId = session.Id;

            //update Database =>status/ SessionId 

            order.StripeSessionId = session.Id;
            order.Status = "Ongoing";
            await _context.SaveChangesAsync();

            return stripeRequestDto;
        }

        public async Task<string> PlaceOrder(Orders order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();
            return "Order Placed Successfully";
        }

        public async Task saveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidatePayments(Guid OrderId)
        {
            var order = await _context.Order.Where(x => x.Id == OrderId).FirstOrDefaultAsync();

            var service = new SessionService();
            Session session = service.Get(order.StripeSessionId);

            PaymentIntentService paymentIntentService = new PaymentIntentService();

            PaymentIntent paymentIntent = paymentIntentService.Get(session.PaymentIntentId);

            if (paymentIntent.Status == "succeeded")
            {
                //the payment was success

                order.Status = "Paid";
                order.PaymentIntent = paymentIntent.Id;
                await _context.SaveChangesAsync();


                var user = await _userService.GetUserById(order.BidderId.ToString());

                //if (string.IsNullOrWhiteSpace(user.Email))
                //{
                //    return false;
                //}
                //else
                //{
                //    var email = new MailDto()
                //    {
                //        OrderId = order.Id,
                //        OrderAmount = order.TotalAmount,
                //        Name = user.Name,
                //        Email = user.Email

                //    };
                //    await _messageBus.PublishMessage(email, "orderplaced");
                //}

                // Send an Email to User
                //Reward the user with some Bonus Points 
                //return true;

            }
            return false;
        }
    }

}


