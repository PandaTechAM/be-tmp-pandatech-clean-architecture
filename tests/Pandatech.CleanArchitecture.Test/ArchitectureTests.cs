using NetArchTest.Rules;
using Pandatech.CleanArchitecture.Core;
using Xunit;

namespace Architecture.Tests;

public class ArchitectureTests
{
   private static readonly string? _coreName =
      typeof(AssemblyReference).Assembly.GetName().Name;

   private static readonly string? _applicationClientName =
      typeof(Pandatech.CleanArchitecture.Application.AssemblyReference).Assembly.GetName().Name;

   private static readonly string? _infrastructureName =
      typeof(Pandatech.CleanArchitecture.Infrastructure.AssemblyReference).Assembly.GetName().Name;

   private static readonly string? _webApiName =
      typeof(Pandatech.CleanArchitecture.Api.AssemblyReference).Assembly.GetName().Name;

   [Fact]
   public void Core_Should_Not_HaveDependency_On_OtherProjects()
   {
      // Arrange
      var assembly = typeof(AssemblyReference).Assembly;

      var otherProjects = new[] { _webApiName, _infrastructureName, _applicationClientName };

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

      var otherProjects = new[] { _webApiName, _infrastructureName };

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
         .HaveDependencyOnAny(_coreName)
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
         .HaveDependencyOnAny(_webApiName)
         .GetResult();

      // Assert
      Assert.True(testResult.IsSuccessful);
   }
}
