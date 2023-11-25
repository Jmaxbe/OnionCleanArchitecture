﻿using FluentValidation.Results;

namespace StaffTimetable.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public ValidationException(IEnumerable<string> errors)
    : base(string.Join(";", errors))
    {
        Errors = new Dictionary<string, string[]>();
    }
    
    public IDictionary<string, string[]> Errors { get; }
}