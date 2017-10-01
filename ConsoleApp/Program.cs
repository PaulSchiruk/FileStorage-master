using System;
using System.Linq;
using DependencyResolver;
using System.Data.Entity;
using BLL.Interfaces.Services;
using SimpleInjector;
using SimpleInjector.Lifestyles;


namespace ConsoleApp
{
    class Program
    {
        private static Container _resolver;
        static Program()
        {
            _resolver = new Container();
            _resolver.Options.DefaultScopedLifestyle = new ThreadScopedLifestyle();
            _resolver.ConfigureResolver();
        }

        static void Main(string[] args)
        {
            using (ThreadScopedLifestyle.BeginScope(_resolver))
            {
                var userService = _resolver.GetInstance<IUserService>();
                var fileService = _resolver.GetInstance<IFileService>();

                Console.WriteLine("Input email");
                var email = Console.ReadLine();

                Console.WriteLine("Input password");
                var password = Console.ReadLine();

                if (userService.ValidateUser(email, password)) 
                {
                    Console.WriteLine("Your files:");
                    foreach (var item in fileService.GetFilesOfUser(email, null))
                    {
                        Console.WriteLine(string.Format("Id:{0}, Name{1}, Size: {2} bytes, Date uploaded: {3}", item.Id, item.Name, item.Size, item.DateUploaded));
                    }
                }
                else Console.WriteLine("Incorrect login or password");
                Console.ReadKey();
            }
        }
    }
}
