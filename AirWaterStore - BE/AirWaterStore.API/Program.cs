using AirWaterStore.Business.Interfaces;
using AirWaterStore.Business.Services;
using AirWaterStore.Data.Repositories;
using AirWaterStore.Data;
using Microsoft.EntityFrameworkCore;

namespace AirWaterStore.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Configure DbContext
            builder.Services.AddDbContext<AirWaterStoreContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register repositories	
            builder.Services.AddScoped<IGameRepository, GameRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
            builder.Services.AddScoped<IMessageRepository, MessageRepository>();

            // Register services	
            builder.Services.AddScoped<IGameService, GameService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
            builder.Services.AddScoped<IReviewService, ReviewService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IChatRoomService, ChatRoomService>();
            builder.Services.AddScoped<IMessageService, MessageService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
