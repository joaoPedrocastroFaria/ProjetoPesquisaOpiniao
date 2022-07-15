namespace Model;
using Microsoft.EntityFrameworkCore;
public class Context : DbContext{
    public DbSet<Adm> Adm {get;set;}
    public DbSet<Pesquisa> Pesquisa {get;set;}
    public DbSet<Pergunta> Pergunta {get;set;}
    public DbSet<Resposta> Resposta {get;set;}
    public DbSet<Alternativas> Alternativas {get;set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
        // optionsBuilder.UseSqlServer("Data Source = JVLPC0581;" + "Initial Catalog = marketPlace; Integrated Security=True"); //bdjao
        optionsBuilder.UseSqlServer("Data Source = JVLPC0510\\SQLSERVER;" + "Initial Catalog = pesquisaOpinion; Integrated Security=True"); //bdluisao
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Adm>(entity =>
        {
            entity.HasKey(e=>e.Id);
            entity.Property(e=>e.Nome).IsRequired();
            entity.Property(e=>e.EDV).IsRequired();
            entity.Property(e=>e.Senha).IsRequired();
            entity.HasMany(e=>e.Pesquisas).WithOne(e=>e.Adm);
        });
        modelBuilder.Entity<Pesquisa>(entity =>
        {
            entity.HasKey(e=>e.Id);
            entity.Property(e=>e.Nome).IsRequired();
            entity.Property(e=>e.Codigo).IsRequired();
            entity.HasOne(e=>e.Adm).WithMany(e=>e.Pesquisas).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(e=>e.Perguntas).WithOne(e=>e.Pesquisa);
        });
        modelBuilder.Entity<Pergunta>(entity =>
        {
            entity.HasKey(e=>e.Id);
            entity.Property(e=>e.ConteudoPergunta).IsRequired();
            entity.Property(e=>e.TipoPergunta).IsRequired();
            entity.Property(e=>e.QtddAlternativas).IsRequired();
            entity.HasOne(e=>e.Pesquisa).WithMany(e=>e.Perguntas).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(e=>e.Alternativas).WithOne(e=>e.Pergunta);
            entity.HasMany(e=>e.Respostas).WithOne(e=>e.Pergunta);
        });
        modelBuilder.Entity<Resposta>(entity =>
        {
            entity.HasKey(e=>e.Id);
            entity.Property(e=>e.ConteudoResposta).IsRequired();
            entity.HasOne(e=>e.Pergunta).WithMany(e=>e.Respostas).OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<Alternativas>(entity =>
        {
            entity.HasKey(e=>e.Id);
            entity.Property(e=>e.ConteudoAlternativa).IsRequired();
            entity.HasOne(e=>e.Pergunta).WithMany(e=>e.Alternativas).OnDelete(DeleteBehavior.Cascade);
        });
    }
}    