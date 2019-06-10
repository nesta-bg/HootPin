using HootPin.Core;
using HootPin.Core.Repositories;
using HootPin.Persistence.Repositories;

namespace HootPin.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IHootRepository Hoots { get; private set; }
        public IAttendanceRepository Attendances { get; private set; }
        public IFollowingRepository Followings { get; private set; }
        public IGenreRepository Genres { get; private set; }
        public IUserRepository Users { get; private set; }
        public IUserNotificationRepository UserNotifications { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Hoots = new HootRepository(_context);
            Attendances = new AttendanceRepository(_context);
            Followings = new FollowingRepository(_context);
            Genres = new GenreRepository(_context);
            Users = new UserRepository(_context);
            UserNotifications = new UserNotificationRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}