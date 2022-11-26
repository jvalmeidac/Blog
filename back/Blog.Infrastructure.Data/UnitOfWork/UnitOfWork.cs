using Blog.Domain.CrossCutting;
using Blog.Infrastructure.Data.Interfaces;
using Dapper;
using System.Text;
using System.Transactions;

namespace Blog.Infrastructure.Data.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly DbSession _session;

        public UnitOfWork(DbSession session)
        {
            _session = session;
        }

        public async Task<T> GetById<T>(Guid id) where T : IEntity
        {
            var tableName = typeof(T).Name;
            return await _session.Connection.QueryFirstOrDefaultAsync<T>(
                $"SELECT * FROM [{tableName}] WHERE Id=@Id",
                new { Id = id }, 
                _session.Transaction);
        }

        public async Task<T> InsertAsync<T>(T item) where T : IEntity
        {
            var insertQuery = GetInsertQuery(typeof(T));

            BeginTransaction();

            var insertedId = await _session.Connection.QuerySingleAsync<Guid>(
                insertQuery, 
                item, 
                _session.Transaction);

            Commit();

            return await GetById<T>(insertedId);
        }

        public async Task InsertRangeAsync<T>(IList<T> items) where T : IEntity
        {
            BeginTransaction();

            var insertQuery = GetInsertQuery(typeof(T));

            var inserted = +await _session.Connection.ExecuteAsync(
                insertQuery, 
                items, 
                _session.Transaction);

            if (inserted != items.Count)
            {
                Rollback();
                throw new TransactionAbortedException();
            }

            Commit();
        }

        public async Task DeleteAsync<T>(Guid id) where T : IEntity
        {
            BeginTransaction();

            await _session.Connection.ExecuteAsync(
                $"DELETE FROM [{typeof(T).Name}] WHERE Id=@Id",
                new { Id = id },
                _session.Transaction);

            Commit();
        }

        public async Task UpdateAsync<T>(T item) where T : IEntity
        {
            BeginTransaction();

            var updateQuery = GetUpdateQuery(typeof(T));

            await _session.Connection.ExecuteAsync(updateQuery, item, _session.Transaction);

            Commit();
        }

        public void BeginTransaction()
        {
            _session.Transaction = _session?.Connection?.BeginTransaction();
        }

        public void Commit()
        {
            _session.Transaction?.Commit();
            Dispose();
        }

        public void Rollback()
        {
            _session.Transaction?.Rollback();
            Dispose();
        }

        public void Dispose() => _session.Transaction?.Dispose();

        private string GetInsertQuery(Type objectType)
        {
            var insertQuery = new StringBuilder($"INSERT INTO [{objectType.Name}]");

            insertQuery.Append('(');

            var columns = GetColumnsNames(objectType);

            foreach (var property in columns)
            {
                insertQuery.Append($"[{property}],");
            }

            insertQuery
               .Remove(insertQuery.Length - 1, 1)
               .Append(") output inserted.Id VALUES (");

            foreach (var property in columns)
            {
                insertQuery.Append($"@{property},");
            }

            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(')');

            return insertQuery.ToString();
        }

        private string GetUpdateQuery(Type objectType)
        {
            var updateQuery = new StringBuilder($"UPDATE [{objectType.Name}] SET ");
            var columns = GetColumnsNames(objectType);

            foreach (var property in columns)
            {
                if(property is nameof(IEntity.Id))
                {
                    continue;
                }

                updateQuery.Append($"{property}=@{property},");
            }

            updateQuery.Remove(updateQuery.Length - 1, 1);
            updateQuery.Append($" WHERE {nameof(IEntity.Id)}=@Id");

            return updateQuery.ToString();
        }

        private IEnumerable<string> GetColumnsNames(Type objectType)
        {
            var properties = objectType.GetProperties();
            foreach (var property in properties)
            {
                yield return property.Name;
            }
        }
    }
}
