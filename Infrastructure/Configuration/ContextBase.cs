﻿using Entities.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configuration
{
    // Criando a classe de contexto do banco de dados
    public class ContextBase : IdentityDbContext<ApplicationUser>
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {
        }

        public DbSet<Message> Message { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        // Utilizando a string de conexão para conectar com o DB caso ela não estiver configurada
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString());
                base.OnConfiguring(optionsBuilder);
            }
        }

        // Mapeando a primary key da tabela de usuário
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);
            base.OnModelCreating(builder);
        }

        /* Definindo a string de conexão. Para conectar ao seu banco de dados caso seja necessário id e senha, insira após 
         * "Integrated Security = true" o seguinte código com id e senha no lugar dos caracteres "X":: User ID = XXXX; Password = XXXX; */
        public string ConnectionString()
        {
            return "Data Source = DESKTOP - H2S9AJ9\\SQLEXPRESS; Initial Catalog = HEALTH_APP; Integrated Security = True; Encrypt = False";
        }
    }
}
