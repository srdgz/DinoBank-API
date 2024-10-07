using DinoBank.Domain.User;

namespace DinoBank.Persistence.Database
{
    public interface IDatabaseService
    {
        List<UserEntity> GetAll();
        bool Create(UserEntity user);
        bool Update(UserEntity user);
        bool Delete(int id);
    }
}
