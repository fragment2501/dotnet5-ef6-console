using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace bsr_dotnet_ef6
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("Hello World!");

      IConfiguration config =  new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

      var connectionString = config.GetConnectionString("blogdb");

      using (var db = new BloggingContext(connectionString))
      {
        BlogInitialTests(db);
        //InsertTestData(db);
        //ListBlogs(db);
      }
    }

    public static void ListBlogs(IBloggingContext db){
      foreach( var blog in db.Blogs.OrderBy(b => b.BlogId)){
        Console.WriteLine($"Blog: {blog.BlogId} {blog.Url}");
        foreach( var post in blog.Posts.OrderBy(p => p.PostId)){
          Console.WriteLine($" - Post: {post.PostId} {post.Content}");
        }
      }
    }

    public static void InsertTestData(IBloggingContext db){

      db.Blogs.RemoveRange(db.Blogs);

      // Create
      Console.WriteLine("Inserting a new blog");
      var newBlog = db.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
      newBlog.Entity.Posts.Add( new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });

      Console.WriteLine("Inserting a new post");
      var newBlog2 = db.Blogs.Add(new Blog { Url = "http://google.com" });
      newBlog2.Entity.Posts.Add( new Post { Title = "About google", Content = "Google stuff" });
      newBlog2.Entity.Posts.Add( new Post { Title = "About google2", Content = "Google stuff2" });

      db.SaveChanges();
    }

    public static void BlogInitialTests(BloggingContext db){
      // Note: This sample requires the database to be created before running.
      Console.WriteLine($"Connection String: {db.ConnectionString}.");

      // Create
      Console.WriteLine("Inserting a new blog");
      db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
      db.SaveChanges();

      // Read
      Console.WriteLine("Querying for a blog");
      var blog = db.Blogs
          .OrderBy(b => b.BlogId)
          .First();

      // Update
      Console.WriteLine("Updating the blog and adding a post");
      blog.Url = "https://devblogs.microsoft.com/dotnet";
      blog.Posts.Add(
          new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
      db.SaveChanges();

      // Delete
      Console.WriteLine("Delete the blog");
      db.Remove(blog);
      db.SaveChanges();
    }

  }
}
