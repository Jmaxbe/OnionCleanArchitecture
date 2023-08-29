using Application.Common.Exceptions;
using FluentValidation.Results;

namespace Application.UnitTests.Common.Exceptions;

public class ValidationExceptionTests
{
    [Fact]
    public void DefaultConstructorCreatesAnEmptyErrorDictionary()
    {
        //Arrange
        var validation = new ValidationException();

        //Act
        var actual = validation.Errors;
        
        //Assert
        Assert.Empty(actual);
    }
    
    [Fact]
    public void SingleValidationFailureCreatesASingleElementErrorDictionary()
    {
        //Arrange
        var failures = new List<ValidationFailure>
        {
            new ValidationFailure("Age", "must be over 18"),
        };
        var validator = new ValidationException(failures);

        //Act
        var actual = validator.Errors;

        //Assert
        Assert.Equivalent(new string[]{ "Age" }, actual.Keys);
        Assert.Equivalent(new string[] { "must be over 18" }, actual["Age"]);
    }
    
    [Fact]
    public void MulitpleValidationFailureForMultiplePropertiesCreatesAMultipleElementErrorDictionaryEachWithMultipleValues()
    {
        //Arrange
        var failures = new List<ValidationFailure>
        {
            new ValidationFailure("Age", "must be 18 or older"),
            new ValidationFailure("Age", "must be 25 or younger"),
            new ValidationFailure("Password", "must contain at least 8 characters"),
            new ValidationFailure("Password", "must contain a digit"),
            new ValidationFailure("Password", "must contain upper case letter"),
            new ValidationFailure("Password", "must contain lower case letter"),
        };
        var validator = new ValidationException(failures);

        //Act
        var actual = validator.Errors;

        //Assert
        Assert.Equivalent(new string[] { "Password", "Age" }, actual.Keys);
        
        Assert.Equivalent(new string[]
        {
            "must be 25 or younger",
            "must be 18 or older",
        }, actual["Age"]);

        Assert.Equivalent(new string[]
        {
            "must contain lower case letter",
            "must contain upper case letter",
            "must contain at least 8 characters",
            "must contain a digit",
        }, actual["Password"]);
    }
}