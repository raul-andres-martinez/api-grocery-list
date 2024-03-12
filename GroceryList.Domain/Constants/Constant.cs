namespace GroceryList.Domain.Constants
{
    public struct Constant
    {
        public struct Queries 
        {
            public const string InsertGroceryList = @"
                        INSERT INTO GroceryLists (Id, Title, CreatedBy, CreatedAt, UpdatedAt)
                        VALUES (@Id, @Title, @CreatedBy, NOW(), NOW());
                        ";

            public const string InsertUserGroceryList = @"
                        INSERT INTO UserGroceryLists (Id, UserId, GroceryListId, CreatedAt, UpdatedAt)
                        VALUES (@Id, @UserId, @GroceryListId, NOW(), NOW());
                        ";


            public const string InsertUser = @"
                        INSERT INTO Users (Id, Name, Email, PasswordHash, PasswordSalt, CreatedAt, UpdatedAt)
                        VALUES (@Id, @Name, @Email, @PasswordHash, @PasswordSalt, NOW(), NOW());
                        ";

            public const string SelectLogin = @"
                        SELECT Id, PasswordSalt, PasswordHash FROM Users WHERE Email = @Email
                        ";
        }
    }
}