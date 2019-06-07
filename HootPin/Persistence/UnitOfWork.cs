using HootPin.Models;
using HootPin.Repositories;

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

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Hoots = new HootRepository(_context);
            Attendances = new AttendanceRepository(_context);
            Followings = new FollowingRepository(_context);
            Genres = new GenreRepository(_context);
            Users = new UserRepository(_context);
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}