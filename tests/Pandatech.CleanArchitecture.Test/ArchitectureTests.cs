using NetArchTest.Rules;
using Xunit;

namespace Architecture.Tests;

public class ArchitectureTests
{
   private readonly static string? CoreName = 
      typeof(Pandatech.CleanArchitecture.Core.AssemblyReference).Assembly.GetName().Name;
   private readonly static string? ApplicationClientName = 
      typeof(Pandatech.CleanArchitecture.Application.AssemblyReference).Assembly.GetName().Name;
   private readonly static string? InfrastructureName = 
      typeof(Pandatech.CleanArchitecture.Infrastructure.AssemblyReference).Assembly.GetName().Name;
   private readonly static string? WebApiName = 
      typeof(Pandatech.CleanArchitecture.Api.AssemblyReference).Assembly.GetName().Name;

   [Fact]
   public void Core_Should_Not_HaveDependency_On_OtherProjects()
   {
      // Arrange
      var assembly = typeof(Pandatech.CleanArchitecture.Core.AssemblyReference).Assembly;

      var otherProjects = new[]
      {
         WebApiName,
         InfrastructureName,
         ApplicationClientName,
      };

      // Act
      var testResult = Types
         .InAssembly(assembly)
         .ShouldNot()
         .HaveDependencyOnAny(otherProjects)
         .GetResult();

      // Assert
      Assert.True(testResult.IsSuccessful);
   }

   [Fact]
   public void Application_Should_Not_HaveDependency_On_OtherProjects()
   {
      // Arrange
      var assembly = typeof(Pandatech.CleanArchitecture.Application.AssemblyReference).Assembly;

      var otherProjects = new[]
      {
         WebApiName,
         InfrastructureName,
      };

      // Act
      var testResult = Types
         .InAssembly(assembly)
         .ShouldNot()
         .HaveDependencyOnAny(otherProjects)
         .GetResult();

      // Assert
      Assert.True(testResult.IsSuccessful);
   }

   [Fact]
   public void Handlers_Should_Have_Dependency_On_Core()
   {
      // Arrange
      var assembly = typeof(Pandatech.CleanArchitecture.Application.AssemblyReference).Assembly;

      // Act
      var testResult = Types
         .InAssembly(assembly)
         .That()
         .HaveNameEndingWith("Handler")
         .Should()
         .HaveDependencyOnAny(CoreName)
         .GetResult();

      // Assert
      Assert.True(testResult.IsSuccessful);
   }

   [Fact]
   public void Infrastructure_Should_Not_HaveDependency_On_OtherProjects()
   {
      // Arrange
      var assembly = typeof(Pandatech.CleanArchitecture.Infrastructure.AssemblyReference).Assembly;

      // Act
      var testResult = Types
         .InAssembly(assembly)
         .ShouldNot()
         .HaveDependencyOnAny(WebApiName)
         .GetResult();

      // Assert
      Assert.True(testResult.IsSuccessful);
   }
}
