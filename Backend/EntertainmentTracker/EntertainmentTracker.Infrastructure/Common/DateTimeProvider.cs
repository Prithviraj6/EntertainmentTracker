using EntertainmentTracker.Application.Common.Interfaces;

namespace EntertainmentTracker.Infrastructure.Common
{
    public sealed class DateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
