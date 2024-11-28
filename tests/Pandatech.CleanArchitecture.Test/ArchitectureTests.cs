using NetArchTest.Rules;
using Pandatech.CleanArchitecture.Core;
using Xunit;

namespace Pandatech.CleanArchitecture.Test;

public class ArchitectureTests
{
   private static readonly string? CoreName =
      typeof(AssemblyReference).Assembly.GetName()
                               .Name;

   private static readonly string? ApplicationClientName =
      typeof(Application.AssemblyReference).Assembly.GetName()
                                           .Name;

   private static readonly string? InfrastructureName =
      typeof(Infrastructure.AssemblyReference).Assembly.GetName()
                                              .Name;

   private static readonly string? WebApiName =
      typeof(Api.AssemblyReference).Assembly.GetName()
                                   .Name;

   [Fact]
   public void Core_Should_Not_HaveDependency_On_OtherProjects()
   {
      // Arrange
      var assembly = typeof(AssemblyReference).Assembly;

      var otherProjects = new[]
      {
         WebApiName,
         InfrastructureName,
         ApplicationClientName
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
      var assembly = typeof(Application.AssemblyReference).Assembly;

      var otherProjects = new[]
      {
         WebApiName,
         InfrastructureName
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
      var assembly = typeof(Application.AssemblyReference).Assembly;

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
      var assembly = typeof(Infrastructure.AssemblyReference).Assembly;

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