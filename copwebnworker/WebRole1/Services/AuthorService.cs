using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;
using WebRole1.Entities;

namespace WebRole1.Services
{
    public class AuthorService : TableService
    {
        private string tableName = "author";

        public AuthorService() : base() 
        {
        }

        public void InsertSeedData()
        {
            CloudTable authorTable = this.GetTableReference(tableName);
            if (authorTable == null) return;

            Author[] authors = new Author[] 
            {
                new Author(){ PartitionKey = "OdeToCode", RowKey = Guid.NewGuid().ToString(), Firstname = "Scott", Lastname = "Allen", Phone = "8790000801", Email = "OdeToCode@psmail.com" },
                new Author(){ PartitionKey = "troyhunt", RowKey = Guid.NewGuid().ToString(), Firstname = "Troy", Lastname = "Hunt", Phone = "8790000802", Email = "troyhunt@psmail.com" },
                new Author(){ PartitionKey = "danwahlin", RowKey = Guid.NewGuid().ToString(), Firstname = "Dan", Lastname = "Wahlin", Phone = "8790000803", Email = "danwahlin@psmail.com" },
                new Author(){ PartitionKey = "shawnwildermuth", RowKey = Guid.NewGuid().ToString(), Firstname = "Shawn", Lastname = "Wildermuth", Phone = "8790000804", Email = "shawnwildermuth@psmail.com" },
                new Author(){ PartitionKey = "housecor", RowKey = Guid.NewGuid().ToString(), Firstname = "Cory", Lastname = "House", Phone = "8790000805", Email = "housecor@psmail.com" },
                new Author(){ PartitionKey = "jeremybytes", RowKey = Guid.NewGuid().ToString(), Firstname = "Jeremy", Lastname = "Clark", Phone = "8790000806", Email = "jeremybytes@psmail.com" },
                new Author(){ PartitionKey = "john_papa", RowKey = Guid.NewGuid().ToString(), Firstname = "John", Lastname = "Papa", Phone = "8790000807", Email = "john_papa@psmail.com" },
                new Author(){ PartitionKey = "skonnard", RowKey = Guid.NewGuid().ToString(), Firstname = "Aaron", Lastname = "Skonnard", Phone = "8790000808", Email = "skonnard@psmail.com" },
                new Author(){ PartitionKey = "robertsjason", RowKey = Guid.NewGuid().ToString(), Firstname = "Jason", Lastname = "Roberts", Phone = "8790000809", Email = "robertsjason@psmail.com" },
                new Author(){ PartitionKey = "mcwoodring", RowKey = Guid.NewGuid().ToString(), Firstname = "Mike", Lastname = "Woodring", Phone = "8790000810", Email = "mcwoodring@psmail.com" },
                new Author(){ PartitionKey = "julielerman", RowKey = Guid.NewGuid().ToString(), Firstname = "Julie", Lastname = "Lerman", Phone = "8790000811", Email = "julielerman@psmail.com" },
                new Author(){ PartitionKey = "TheLoudSteve", RowKey = Guid.NewGuid().ToString(), Firstname = "Steve", Lastname = "Evans", Phone = "8790000812", Email = "TheLoudSteve@psmail.com" }
            };

            foreach(Author author in authors)
            {
                authorTable.Execute(TableOperation.Insert(author));
            }
        }

        public IEnumerable<Author> GetAuthors(string firstname, string lastname)
        {
            CloudTable authorTable = GetTableReference(tableName);
            if (authorTable == null) return new List<Author>();

            TableQuery<Author> query = default(TableQuery<Author>);

            if (!string.IsNullOrEmpty(firstname) && !string.IsNullOrEmpty(lastname))
            {
                string lastnameFilter = TableQuery.GenerateFilterCondition("Lastname", QueryComparisons.Equal, lastname);
                string firstnameFilter = TableQuery.GenerateFilterCondition("Firstname", QueryComparisons.Equal, firstname);
                string finalFilter = TableQuery.CombineFilters(lastnameFilter, TableOperators.And, firstnameFilter);

                query = new TableQuery<Author>().Where(finalFilter);
            }
            else if (!string.IsNullOrEmpty(firstname))
            {
                query = new TableQuery<Author>()
                    .Where(TableQuery.GenerateFilterCondition("Firstname", QueryComparisons.Equal, firstname));
            }
            else if (!string.IsNullOrEmpty(lastname))
            {
                query = new TableQuery<Author>()
                    .Where(TableQuery.GenerateFilterCondition("Lastname", QueryComparisons.Equal, lastname));
            }
            else 
            {
                query = new TableQuery<Author>();
            }


            return authorTable.ExecuteQuery<Author>(query);
        }
    }
}
