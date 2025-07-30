using Infrastructure.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection
{
    public static class AddNotificationsDependencyInjection
    {
        public static void NotificationsDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<NotificationContext>();
        }
    }
}
