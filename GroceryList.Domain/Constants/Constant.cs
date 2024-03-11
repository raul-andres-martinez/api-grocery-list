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
        }
    }
}