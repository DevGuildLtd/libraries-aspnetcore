using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevGuild.AspNetCore.Services.Data.Relational;

namespace DevGuild.AspNetCore.Services.EntitySequences
{
    /// <summary>
    /// Default implementation of the service for entity-base sequence number generation.
    /// </summary>
    /// <seealso cref="IEntitySequenceService" />
    public class EntitySequenceService : IEntitySequenceService
    {
        private readonly IDbConnectionFactory connectionFactory;

        public EntitySequenceService(IDbConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        /// <summary>
        /// Asynchronously peeks the next number in the sequence of the specified entity.
        /// </summary>
        /// <param name="key">The key of the entity.</param>
        /// <returns>
        /// The next value to be returned by the sequence.
        /// </returns>
        public async Task<Int64> PeekNextNumberAsync(String key)
        {
            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandType = CommandType.Text;
                            command.CommandText = @"select [Id], [NextSequenceId] from [EntitySequences] where [Id] = @key";

                            command.AddParameter("key", DbType.String, key);

                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                if (await reader.ReadAsync())
                                {
                                    return reader.GetInt64(reader.GetOrdinal("NextSequenceId"));
                                }
                                else
                                {
                                    return 1;
                                }
                            }
                        }
                    }
                    finally
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        /// <summary>
        /// Asynchronously takes the next number from the sequence of the specified entity.
        /// </summary>
        /// <param name="key">The key of the entity.</param>
        /// <returns>
        /// The next value of the sequence.
        /// </returns>
        public async Task<Int64> TakeNextNumberAsync(String key)
        {
            async Task<(Boolean Success, Int64 Payload)> TryUpdate()
            {
                try
                {
                    using (var connection = this.connectionFactory.CreateConnection())
                    {
                        await connection.OpenAsync();
                        using (var transaction = connection.BeginTransaction())
                        {
                            var success = false;
                            try
                            {
                                using (var command = connection.CreateCommand())
                                {
                                    command.Transaction = transaction;
                                    command.CommandType = CommandType.Text;
                                    command.CommandText = @"update [EntitySequences] set [NextSequenceId] = [NextSequenceId] + 1
  output deleted.[NextSequenceId] as [PreviousId]
  where [Id] = @key";
                                    command.AddParameter("key", DbType.String, key);

                                    using (var reader = await command.ExecuteReaderAsync())
                                    {
                                        if (await reader.ReadAsync())
                                        {
                                            success = true;
                                            return (true, reader.GetInt64(0));
                                        }
                                        else
                                        {
                                            throw new InvalidOperationException();
                                        }
                                    }
                                }
                            }
                            finally
                            {
                                if (success)
                                {
                                    transaction.Commit();
                                }
                                else
                                {
                                    transaction.Rollback();
                                }
                            }
                        }
                    }
                }
                catch
                {
                    return (false, 0);
                }
            }

            async Task<Boolean> TryInsert()
            {
                try
                {
                    using (var connection = this.connectionFactory.CreateConnection())
                    {
                        await connection.OpenAsync();
                        using (var transaction = connection.BeginTransaction())
                        using (var command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandType = CommandType.Text;
                            command.CommandText = @"insert [EntitySequences] ([Id], [NextSequenceId]) values (@key, 2)";
                            command.AddParameter("key", DbType.String, key);

                            var changed = await command.ExecuteNonQueryAsync();
                            if (changed > 0)
                            {
                                transaction.Commit();
                                return true;
                            }
                            else
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                    }
                }
                catch
                {
                    return false;
                }
            }

            var delay = 1;
            while (true)
            {
                var (success, payload) = await TryUpdate();
                if (success)
                {
                    return payload;
                }

                success = await TryInsert();
                if (success)
                {
                    return 1;
                }

                await Task.Delay(50 * delay);
                delay *= 2;
            }
        }

        /// <summary>
        /// Asynchronously Takes the multiple numbers from the sequence of the specified entity.
        /// </summary>
        /// <param name="key">The key of the entity.</param>
        /// <param name="count">The number of numbers to take.</param>
        /// <returns>
        /// An array of taken numbers.
        /// </returns>
        public async Task<Int64[]> TakeMultipleNumbersAsync(String key, Int64 count)
        {
            async Task<(Boolean Success, Int64 Payload)> TryUpdate()
            {
                try
                {
                    using (var connection = this.connectionFactory.CreateConnection())
                    {
                        await connection.OpenAsync();
                        using (var transaction = connection.BeginTransaction())
                        {
                            var success = false;
                            try
                            {
                                using (var command = connection.CreateCommand())
                                {
                                    command.Transaction = transaction;
                                    command.CommandType = CommandType.Text;
                                    command.CommandText = @"update [EntitySequences] set [NextSequenceId] = [NextSequenceId] + @count
  output deleted.[NextSequenceId] as [PreviousId]
  where [Id] = @key";
                                    command.AddParameter("count", DbType.Int64, count);
                                    command.AddParameter("key", DbType.String, key);

                                    using (var reader = await command.ExecuteReaderAsync())
                                    {
                                        if (await reader.ReadAsync())
                                        {
                                            success = true;
                                            return (true, reader.GetInt64(0));
                                        }
                                        else
                                        {
                                            throw new InvalidOperationException();
                                        }
                                    }
                                }

                            }
                            finally
                            {
                                if (success)
                                {
                                    transaction.Commit();
                                }
                                else
                                {
                                    transaction.Rollback();
                                }
                            }
                        }
                    }
                }
                catch
                {
                    return (false, 0);
                }
            }

            async Task<Boolean> TryInsert()
            {
                try
                {
                    using (var connection = this.connectionFactory.CreateConnection())
                    {
                        await connection.OpenAsync();
                        using (var transaction = connection.BeginTransaction())
                        using (var command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandType = CommandType.Text;
                            command.CommandText = @"insert [EntitySequences] ([Id], [NextSequenceId]) values (@key, @count + 1)";
                            command.AddParameter("key", DbType.String, key);
                            command.AddParameter("count", DbType.Int64, count);

                            var changed = await command.ExecuteNonQueryAsync();
                            if (changed > 0)
                            {
                                transaction.Commit();
                                return true;
                            }
                            else
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                    }
                }
                catch
                {
                    return false;
                }
            }

            var delay = 1;
            while (true)
            {
                var (success, payload) = await TryUpdate();
                if (success)
                {
                    return Enumerable.Range(0, (Int32)count).Select(x => payload + x).ToArray();
                }

                success = await TryInsert();
                if (success)
                {
                    return Enumerable.Range(0, (Int32)count).Select(x => 1L + x).ToArray();
                }

                await Task.Delay(50 * delay);
                delay *= 2;
            }
        }
    }
}
