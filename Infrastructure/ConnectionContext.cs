using Microsoft.EntityFrameworkCore;
using olympo_webapi.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;
using olympo_webapi.Services;

namespace olympo_webapi.Infrastructure
{
    public class ConnectionContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Session> Sessions { get; set; }

        public ConnectionContext(DbContextOptions<ConnectionContext> options)
        : base(options)
        {
        }

        public ConnectionContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=olympo;Username=postgres;Password=password");
            }
            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Name).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Password).IsRequired();
                entity.Property(u => u.Type)
                    .HasConversion<string>();

                entity.HasMany(u => u.Exercise)
                      .WithOne(e => e.User)
                      .HasForeignKey(e => e.UserId);
            });

            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);

                entity.HasMany(e => e.Sessions)
                      .WithOne(s => s.Exercise)
                      .HasForeignKey(s => s.ExerciseId);
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Repetitions).IsRequired();
                entity.Property(s => s.Series).IsRequired();
                entity.Property(s => s.Time).IsRequired();
            });                

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    CPF = "123.456.789-01",
                    Name = "Admin",
                    Type = UserType.Administrador,
                    Email = "adm@gmail.com",
                    ImagePath = "defaultphoto.jpg",
                    Password = HashService.HashPassword("password"),
                },
                new User
                {
                    Id = 2,
                    CPF = "987.654.321-09",
                    Name = "José",
                    Type = UserType.Professor,
                    Email = "jose@gmail.com",
                    ImagePath = "defaultphoto.jpg",
                    Password = HashService.HashPassword("password")
                },
                new User
                {
                    Id = 3,
                    CPF = "123.456.789-01",
                    Name = "Maria",
                    Type = UserType.Aluno,
                    Email = "maria@gmail.com",
                    ImagePath = "defaultphoto.jpg",
                    Password = HashService.HashPassword("password")
                },
                new User
                {
                    Id = 4,
                    CPF = "123.456.789-01",
                    Name = "João",
                    Type = UserType.Aluno,
                    Email = "joao@gmail.com",
                    ImagePath = "defaultphoto.jpg",
                    Password = HashService.HashPassword("password")
                }
                
            );

            modelBuilder.Entity<Exercise>().HasData(
            new Exercise
            {
                Id = 1,
                Name = "Agachamento Terra",
                Description = "Use uma pegada pronada, com as palmas das mãos voltadas para o corpo, para segurar a barra. Mantenha os joelhos flexionados na posição de agachamento, a coluna ereta e alinhada, e as pernas abertas com os pés apontados para fora.",
                Day = (int?)Day.Sexta,
                ImagePath = "images/exe2.png",
                VideoPath = "videos/execucao.mp4",
                UserId = 3 
            },
            new Exercise
            {
                Id = 2,
                Name = "Rosca Concentrada",
                Description = "Sente-se em um banco e incline-se levemente, mantendo o peito erguido. Flexione o braço para levantar o halter até o ombro, pause por um segundo no topo e estenda lentamente o braço para retornar à posição inicial.",
                Day = (int?)Day.Sexta,
                ImagePath = "images/exe.png",
                VideoPath = "videos/execucao.mp4",
                UserId = 4 
            },
            new Exercise
            {
                Id = 3,
                Name = "Supino Reto",
                Description = "Deite-se em um banco plano, segure a barra com uma pegada média e abaixe-a até tocar levemente o peito. Empurre a barra para cima até que os braços estejam completamente estendidos.",
                Day = (int?)Day.Segunda,
                ImagePath = "images/exe3.png",
                VideoPath = "videos/execucao.mp4",
                UserId = 3 
            },
            new Exercise
            {
                Id = 4,
                Name = "Puxada Aberta",
                Description = "Sente-se no aparelho de puxada e segure a barra com uma pegada ampla. Puxe a barra em direção ao peito enquanto mantém a coluna reta, contraindo os músculos das costas. Retorne à posição inicial de forma controlada.",
                Day = (int?)Day.Quarta,
                ImagePath = "images/exe4.png",
                VideoPath = "videos/execucao.mp4",
                UserId = 3 
            },
            new Exercise
            {
                Id = 5,
                Name = "Levantamento Terra",
                Description = "Fique em pé com os pés na largura dos ombros, segure a barra com uma pegada mista e mantenha a coluna reta. Levante a barra do chão até a altura do quadril, mantendo o controle, e abaixe-a lentamente.",
                Day = (int?)Day.Sexta,
                ImagePath = "images/exe5.png",
                VideoPath = "videos/execucao.mp4",
                UserId = 4 
            }
        );

        modelBuilder.Entity<Session>().HasData(
            new Session
            {
                Id = 1,
                Repetitions = 10,
                Series = 3,
                Breaks = 60.0,
                Time = 5.0,
                ExerciseId = 1
            },
            new Session
            {
                Id = 2,
                Repetitions = 12,
                Series = 4,
                Breaks = 45.0,
                Time = 6.0,
                ExerciseId = 2
            },
            new Session
            {
                Id = 3,
                Repetitions = 8,
                Series = 5,
                Breaks = 90.0,
                Time = 7.5,
                ExerciseId = 3
            },
            new Session
            {
                Id = 4,
                Repetitions = 15,
                Series = 3,
                Breaks = 30.0,
                Time = 4.5,
                ExerciseId = 4
            },
            new Session
            {
                Id = 5,
                Repetitions = 6,
                Series = 6,
                Breaks = 120.0,
                Time = 10.0,
                ExerciseId = 5
            }
        );

            base.OnModelCreating(modelBuilder);
        }
    }
}