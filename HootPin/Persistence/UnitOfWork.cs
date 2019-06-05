using HootPin.Models;
using HootPin.Repositories;

namespace HootPin.Persistence
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public HootRepository Hoots { get; private set; }
        public AttendanceRepository Attendances { get; private set; }
        public FollowingRepository Followings { get; private set; }
        public GenreRepository Genres { get; private set; }
        public UserRepository Users { get; private set; }

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