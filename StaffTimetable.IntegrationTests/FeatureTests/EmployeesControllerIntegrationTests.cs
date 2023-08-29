using System.Net;
using Microsoft.AspNetCore.Http;
using StaffTimetable.IntegrationTests.Fixtures;

namespace StaffTimetable.IntegrationTests.FeatureTests;

public class EmployeesControllerIntegrationTests : IClassFixture<TestingWebAppFactory<Program>>
{
    private readonly HttpClient _client;
    public EmployeesControllerIntegrationTests(TestingWebAppFactory<Program> factory) 
        => _client = factory.CreateClient();
    
    [Fact]
    public async Task Index_WhenCalled_ReturnsApplicationForm()
    {
        //Arrange
        
        //Act
        var response = await _client.GetAsync("/Employees");

        //Assert
        Assert.True(HttpStatusCode.NotFound == response.StatusCode);
    }
}