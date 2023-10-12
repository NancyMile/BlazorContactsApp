using BlazorApp2.Shared;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Data
{
    public class ContactRepository : IContactRepository
    {
        //inject db dependency
        private readonly IDbConnection _dbConnection;
        public ContactRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task Delete(int id)
        {
            var sql = @" DELETE FROM Contacts WHERE Id = @Id ";
            await _dbConnection.ExecuteAsync(sql,
                new
                {
                    Id = id
                });
        }

        public async Task<IEnumerable<Contact>> GetAll()
        {
            var sql = @" SELECT Id
                                ,FirstName
                                ,LastName
                                ,Phone
                                ,Address
                         FROM Contacts ";
            return await _dbConnection.QueryAsync<Contact>(sql, new { });
        }

        public async Task<Contact> GetDetails(int id)
        {
            var sql = @" SELECT Id
                                ,FirstName
                                ,LastName
                                ,Phone
                                ,Address
                         FROM Contacts
                         WHERE Id = @Id ";

            return await _dbConnection.QueryFirstOrDefaultAsync<Contact>(sql, new { Id = id });
        }

        public async Task Insert(Contact contact)
        {
            var sql = @" INSERT INTO Contacts
                               (
                               Firstname
                               ,LastName
                               ,Phone
                               ,Address)
                         VALUES( @FirstName, @LastName, @Phone, @Address )";

            await _dbConnection.ExecuteAsync(sql,
            new
            {
                contact.FirstName,
                contact.LastName,
                contact.Phone,
                contact.Address
            });
        }

        public async Task Update(Contact contact)
        {
            var sql = @" UPDATE Contacts
                            SET FirstName = @FirstName
                               ,LastName = @LastName
                               ,Phone = @Phone
                               ,Address = @Address
                          WHERE Id = @Id ";

            await _dbConnection.ExecuteAsync(sql,
            new
            {
                contact.FirstName,
                contact.LastName,
                contact.Phone,
                contact.Address,
                contact.Id
            });
        }
    }
}
