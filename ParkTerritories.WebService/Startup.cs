using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ParkTerritories.ApplicationServices.GetParkListUseCase;
using ParkTerritories.ApplicationServices.Repositories;
using ParkTerritories.DomainObjects.Ports;
using ParkTerritories.DomainObjects;
using System.Collections.Generic;

namespace ParkTerritories.WebService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<InMemoryParkRepository>(x => new InMemoryParkRepository(
                new List<Park> {
                    new Park()
                    {
                        Id = 1, Name = "�������� ������-��������� ��������� � ����� ������ ���������� ����� � ��������� �� ������ ��.�������� �.17", 
                        Location = "������� ���� � ���� ���������� �� ������� ����� ��������, ������ ��������� " +
                        "���������, �����������������, � ��������� ����� �� 30 �, " +
                        "�������� � ������� �������� ������� � ���������, �� ��������� ���������� ���� � ������������ ���������", 
                        PlayGround = PlayGrounds.no, SportsGround = SportsGrounds.no, Water = "���"
                    },
                    new Park()
                    {
                        Id = 1, Name = "������� ���� �����", Location = "����� ������� ��������, ��� 9",
                        PlayGround = PlayGrounds.yes, SportsGround = SportsGrounds.no, Water = "���"
                    },
                    new Park()
                    {
                         Id = 1, Name = "��������-������������ ���� ����������� ���", Location = "��������� ����� ������ �������",
                        PlayGround = PlayGrounds.yes, SportsGround = SportsGrounds.yes, Water = "���"
                    }
            }));
            services.AddScoped<IReadOnlyParkRepository>(x => x.GetRequiredService<InMemoryParkRepository>());
            services.AddScoped<IParkRepository>(x => x.GetRequiredService<InMemoryParkRepository>());

            services.AddScoped<IGetParkListUseCase, GetParkListUseCase>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
