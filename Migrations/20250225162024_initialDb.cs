using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace olympo_webapi.Migrations
{
    /// <inheritdoc />
    public partial class initialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CPF = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    ImagePath = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ImagePath = table.Column<string>(type: "text", nullable: true),
                    VideoPath = table.Column<string>(type: "text", nullable: true),
                    Day = table.Column<int>(type: "integer", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Repetitions = table.Column<int>(type: "integer", nullable: false),
                    Series = table.Column<int>(type: "integer", nullable: false),
                    Breaks = table.Column<double>(type: "double precision", nullable: false),
                    Time = table.Column<double>(type: "double precision", nullable: false),
                    ExerciseId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CPF", "Email", "ImagePath", "Name", "Password", "Type" },
                values: new object[,]
                {
                    { 1, "123.456.789-01", "adm@gmail.com", "defaultphoto.jpg", "Admin", "AQAAAAIAAYagAAAAEObbl94OOgivuB9hl2yNRdyqmZDc1zhksnBTQsMNAUl05d+ekcUHUZP5VeHfL53Lpg==", "Administrador" },
                    { 2, "987.654.321-09", "jose@gmail.com", "defaultphoto.jpg", "José", "AQAAAAIAAYagAAAAEI0E/z+kTrGJvlbiYunG/A6usyz0SQtMYkqht7Sg5GItMa3MjkkwfbUhLlvV3cmPlw==", "Professor" },
                    { 3, "123.456.789-01", "maria@gmail.com", "defaultphoto.jpg", "Maria", "AQAAAAIAAYagAAAAEKwD4UAJZq3JXJL600d8hlyhyXIS72tzWhOA0ClRNtRTPxIRK9UWzKRg4qM4+FJUGw==", "Aluno" },
                    { 4, "123.456.789-01", "joao@gmail.com", "defaultphoto.jpg", "João", "AQAAAAIAAYagAAAAEOJK3uYuItjVg7Glpre4mvw0SDnhU+ohbTAg03BqG6UDIYWn4fnVahH7lpjpjjUg2w==", "Aluno" }
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "Day", "Description", "ImagePath", "Name", "UserId", "VideoPath" },
                values: new object[,]
                {
                    { 1, 5, "Use uma pegada pronada, com as palmas das mãos voltadas para o corpo, para segurar a barra. Mantenha os joelhos flexionados na posição de agachamento, a coluna ereta e alinhada, e as pernas abertas com os pés apontados para fora.", "images/exe2.png", "Agachamento Terra", 3, "videos/execucao.mp4" },
                    { 2, 5, "Sente-se em um banco e incline-se levemente, mantendo o peito erguido. Flexione o braço para levantar o halter até o ombro, pause por um segundo no topo e estenda lentamente o braço para retornar à posição inicial.", "images/exe.png", "Rosca Concentrada", 4, "videos/execucao.mp4" },
                    { 3, 1, "Deite-se em um banco plano, segure a barra com uma pegada média e abaixe-a até tocar levemente o peito. Empurre a barra para cima até que os braços estejam completamente estendidos.", "images/exe3.png", "Supino Reto", 3, "videos/execucao.mp4" },
                    { 4, 3, "Sente-se no aparelho de puxada e segure a barra com uma pegada ampla. Puxe a barra em direção ao peito enquanto mantém a coluna reta, contraindo os músculos das costas. Retorne à posição inicial de forma controlada.", "images/exe4.png", "Puxada Aberta", 3, "videos/execucao.mp4" },
                    { 5, 5, "Fique em pé com os pés na largura dos ombros, segure a barra com uma pegada mista e mantenha a coluna reta. Levante a barra do chão até a altura do quadril, mantendo o controle, e abaixe-a lentamente.", "images/exe5.png", "Levantamento Terra", 4, "videos/execucao.mp4" }
                });

            migrationBuilder.InsertData(
                table: "Sessions",
                columns: new[] { "Id", "Breaks", "ExerciseId", "Repetitions", "Series", "Time" },
                values: new object[,]
                {
                    { 1, 60.0, 1, 10, 3, 5.0 },
                    { 2, 45.0, 2, 12, 4, 6.0 },
                    { 3, 90.0, 3, 8, 5, 7.5 },
                    { 4, 30.0, 4, 15, 3, 4.5 },
                    { 5, 120.0, 5, 6, 6, 10.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_UserId",
                table: "Exercises",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ExerciseId",
                table: "Sessions",
                column: "ExerciseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
