using HootPin.Repositories;

namespace HootPin.Persistence
{
    public interface IUnitOfWork
    {
        IHootRepository Hoots { get; }
        IAttendanceRepository Attendances { get; }
        IFollowingRepository Followings { get; }
        IGenreRepository Genres { get; }
        IUserRepository Users { get; }
        void Complete();
    }
}
