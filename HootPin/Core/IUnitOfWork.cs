using HootPin.Core.Repositories;

namespace HootPin.Core
{
    public interface IUnitOfWork
    {
        IHootRepository Hoots { get; }
        IAttendanceRepository Attendances { get; }
        IFollowingRepository Followings { get; }
        IGenreRepository Genres { get; }
        IUserRepository Users { get; }
        IUserNotificationRepository UserNotifications { get; }
        void Complete();
    }
}
