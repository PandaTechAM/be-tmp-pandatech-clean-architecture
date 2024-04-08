using NetArchTest.Rules;
using Pandatech.CleanArchitecture.Core;
using Xunit;

namespace Architecture.Tests;

public class ArchitectureTests
{
   private const string CoreNamespace = "Pandatech.CleanArchitecture.Core";
   private const string ApplicationNamespace = "Pandatech.CleanArchitecture.Application";
   private const string InfrastructureNamespace = "Pandatech.CleanArchitecture.Infrastructure";
   private const string PresentationNamespace = "Presentation";
   private const string WebApiNamespace = "Pandatech.CleanArchitecture.Api";

   [Fact]
   public void Domain_Should_Not_HaveDependencyOnOtherProjects()
   {
      // Arrange
      var assembly = typeof(DependencyInjection).Assembly;

      var otherProjects = new[]
      {
         ApplicationNamespace, InfrastructureNamespace, PresentationNamespace, WebApiNamespace
      };

      // Act
      var testResult = Types
         .InAssembly(assembly)
         .ShouldNot()
         .HaveDependencyOnAll(otherProjects)
         .GetResult();

      // Assert
      Assert.True(testResult.IsSuccessful);
   }

   [Fact]
   public void Application_Should_Not_HaveDependencyOnOtherProjects()
   {
      // Arrange
      var assembly = typeof(Pandatech.CleanArchitecture.Application.DependencyInjection).Assembly;

      var otherProjects = new[] { InfrastructureNamespace, PresentationNamespace, WebApiNamespace };

      // Act
      var testResult = Types
         .InAssembly(assembly)
         .ShouldNot()
         .HaveDependencyOnAll(otherProjects)
         .GetResult();

      // Assert
      Assert.True(testResult.IsSuccessful);
   }

   [Fact]
   public void Handlers_Should_Have_DependencyOnDomain()
   {
      // Arrange
      var assembly = typeof(Pandatech.CleanArchitecture.Application.DependencyInjection).Assembly;

      // Act
      var testResult = Types
         .InAssembly(assembly)
         .That()
         .HaveNameEndingWith("Handler")
         .Should()
         .HaveDependencyOn(CoreNamespace)
         .GetResult();

      // Assert
      Assert.True(testResult.IsSuccessful);
   }

   [Fact]
   public void Infrastructure_Should_Not_HaveDependencyOnOtherProjects()
   {
      // Arrange
      var assembly = typeof(Pandatech.CleanArchitecture.Infrastructure.DependencyInjection).Assembly;

      var otherProjects = new[] { PresentationNamespace, WebApiNamespace };

      // Act
      var testResult = Types
         .InAssembly(assembly)
         .ShouldNot()
         .HaveDependencyOnAll(otherProjects)
         .GetResult();

      // Assert
      Assert.True(testResult.IsSuccessful);
   }

   [Fact]
   public void Presentation_Should_Not_HaveDependencyOnOtherProjects()
   {
      // Arrange
      var assembly = typeof(Pandatech.CleanArchitecture.Infrastructure.DependencyInjection).Assembly;

      var otherProjects = new[] { InfrastructureNamespace, WebApiNamespace };

      // Act
      var testResult = Types
         .InAssembly(assembly)
         .ShouldNot()
         .HaveDependencyOnAll(otherProjects)
         .GetResult();

      // Assert
      Assert.True(testResult.IsSuccessful);
   }
}
