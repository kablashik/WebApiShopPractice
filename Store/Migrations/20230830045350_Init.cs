using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplicationL5.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    first_name = table.Column<string>(type: "longtext", nullable: true),
                    last_name = table.Column<string>(type: "longtext", nullable: true),
                    age = table.Column<int>(type: "int", nullable: false),
                    country = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.id);
                    table.CheckConstraint("age", "age > 0");
                })
                .Annotation("MySQL:Charset", "utf8mb4");
            migrationBuilder.Sql(@"USE usersdb3;
INSERT INTO Customers (id, first_name, last_name, age, country)
VALUES
    (1, 'Иван', 'Петров', 22, 'Россия' ),
    (2, 'Елена', 'Сидорова', 30, 'Украина'),
    (3, 'Алексей', 'Иванов', 28, 'Канада' ),
    (4, 'Мария', 'Смирнова', 25, 'Казахстан' ),
    (5, 'Андрей', 'Козлов', 40, 'Россия' ),
    (6, 'Ольга', 'Морозова', 35, 'Украина' ),
    (7, 'Дмитрий', 'Лебедев', 29, 'США' ),
    (8, 'Екатерина', 'Соколова', 27, 'Румыния'), 
    (9, 'Сергей', 'Новиков', 33, 'Беларусь' ),
    (10, 'Анна', 'Зайцева', 31, 'Польша' );");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    customer_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    count = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.CheckConstraint("count", "count >= 0");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.Sql(@"USE usersdb3;
INSERT INTO Orders (id, customer_id, product_id, count, created_at)
VALUES
    (1, 3, 8, 2, '2023-01-05 10:30:00'),
    (2, 5, 2, 1, '2023-02-15 11:15:00'),
    (3, 9, 12, 3, '2023-03-20 12:00:00'),
    (4, 2, 6, 2, '2023-04-10 13:45:00'),
    (5, 7, 1, 1, '2023-05-18 14:30:00'),
    (6, 11, 10, 1, '2023-06-25 15:15:00'),
    (7, 6, 14, 2, '2023-07-08 16:00:00'),
    (8, 1, 3, 1, '2023-01-31 16:45:00'),
    (9, 13, 4, 3, '2023-02-12 17:30:00'),
    (10, 8, 9, 2, '2023-03-05 18:15:00');");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    Description = table.Column<string>(type: "longtext", nullable: true),
                    Price = table.Column<double>(type: "double", nullable: false),
                    product_type = table.Column<Enum>(type: "ENUM('Книга', 'Диск', 'Продукт')", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.CheckConstraint("Price", "Price > 0");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.Sql(@"USE usersdb3;
INSERT INTO Products (id, name, description, price, product_type)
VALUES
    (1, 'Путешествие Европа', 'Замечательное путешествие.', 49.99, 'Книга'),
    (2, 'Хиты 2023', 'Сборник популярных песен.', 14.99, 'Диск'),
    (3, 'Фруктовый салат', 'Свежие фрукты.', 6.99, 'Продукт'),
    (4, 'История Рима', 'Увлекательная история.', 32.50, 'Книга'),
    (5, 'Хиты 80-х', 'Сборник музыки из 80-х.', 12.99, 'Диск'),
    (6, 'Органический авокадо', 'Органическое авокадо.', 2.49, 'Продукт'),
    (7, 'Мастер и Маргарита', 'Шедевр Михаила Булгакова.', 19.75, 'Книга'),
    (8, 'Классика рока', 'Сборник хитов классического рока.', 17.50, 'Диск'),
    (9, 'Греческий йогурт', 'Плотный йогурт.', 3.99, 'Продукт'),
    (10, 'Преступление и наказание', 'Роман Фёдора Достоевского.', 27.00, 'Книга');");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
