using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace YourProject.Repositories
{
    public class YourEntityRepository : IRepository<YourEntity>
    {
        private readonly IDbConnection _dbConnection;

        public YourEntityRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<YourEntity>> GetAllAsync()
        {
            return await _dbConnection.QueryAsync<YourEntity>("SELECT * FROM YourEntityTable");
        }

        public async Task<YourEntity> GetByIdAsync(int id)
        {
            return await _dbConnection.QueryFirstOrDefaultAsync<YourEntity>("SELECT * FROM YourEntityTable WHERE Id = @Id", new { Id = id });
        }

        public async Task AddAsync(YourEntity entity)
        {
            const string sql = @"INSERT INTO YourEntityTable (Prop1, Prop2, Prop3) VALUES (@Prop1, @Prop2, @Prop3);";
            await _dbConnection.ExecuteAsync(sql, entity);
        }

        public async Task UpdateAsync(YourEntity entity)
        {
            const string sql = @"UPDATE YourEntityTable SET Prop1 = @Prop1, Prop2 = @Prop2, Prop3 = @Prop3 WHERE Id = @Id;";
            await _dbConnection.ExecuteAsync(sql, entity);
        }

        public async Task DeleteAsync(YourEntity entity)
        {
            const string sql = @"DELETE FROM YourEntityTable WHERE Id = @Id;";
            await _dbConnection.ExecuteAsync(sql, entity);
        }
    }
}
