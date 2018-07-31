namespace Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Data.Models;
    using System.Text;
    using DBContext;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    internal sealed class Configuration : DbMigrationsConfiguration<Data.DBContext.MyShopDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Data.DBContext.MyShopDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            // Code khi chạy DB sẽ tự chạy
            CreateUser(context);
            CreateColorAndSize(context);
        }
        private string Encryptdata(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }

        private string Decryptdata(string encryptpwd)
        {
            string decryptpwd = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpwd = new String(decoded_char);
            return decryptpwd;
        }
        private void CreateUser(MyShopDBContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new MyShopDBContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new MyShopDBContext()));

            var user = new ApplicationUser()
            {
                UserName = "Administrator",
                Email = "tiepnv022093@gmail.com",
                EmailConfirmed = true,
                FirstName = "Nguyen Van",
                LastName = "Tiep",
                Level = 1,
                JoinDate = DateTime.Now
            };
            if (manager.Users.Count(x => x.UserName == "Administrator") == 0)
            {
                manager.Create(user, "Ab@123456");

                if (!roleManager.Roles.Any())
                {
                    roleManager.Create(new IdentityRole { Name = "Admin" });
                    roleManager.Create(new IdentityRole { Name = "User" });
                }

                var adminUser = manager.FindByEmail("tiepnv022093@gmail.com.com");

                //     manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
            }

            //if (context.ApplicationGroups.Count() == 0)
            //{
            //    ApplicationGroup group = new ApplicationGroup()
            //    {
            //        Name = "Administrator",
            //        Description = "Nhóm quản trị hệ thống"
            //    };
            //    context.ApplicationGroups.Add(group);
            //    context.ApplicationGroups.Add(new ApplicationGroup()
            //    {
            //        Name = "Users",
            //        Description = "Nhóm người dùng"
            //    });
            //    context.SaveChanges();
            //    var adminUser = manager.FindByEmail("nguyenthe675@gmail.com");
            //    context.ApplicationUserGroups.Add(new ApplicationUserGroup()
            //    {
            //        UserId = adminUser.Id,
            //        GroupId = group.ID
            //    });
        }
        private void CreateColorAndSize(MyShopDBContext context)
        {
            if (context.Size.Count() == 0)
            {
                List<Size> ListSize = new List<Size>()  {
                new Size() { SizeName="XS",SizeValue="XS" },
                new Size() { SizeName="S",SizeValue="M"},
                new Size() { SizeName="L",SizeValue="L" },
                new Size() { SizeName="XL ",SizeValue="#ffffff" },
                new Size() { SizeName="XXL",SizeValue="XXL" },
            };
                context.Size.AddRange(ListSize);
            }

        }
    }
}
