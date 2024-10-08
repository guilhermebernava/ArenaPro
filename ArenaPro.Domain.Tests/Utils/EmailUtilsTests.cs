﻿using ArenaPro.Domain.Utils;

namespace ArenaPro.Domain.Tests.Utils;
public class EmailUtilsTests
{
    [Theory]
    [InlineData("example@example.com", true)]
    [InlineData("user.name+tag+sorting@example.com", true)]
    [InlineData("invalid-email", false)]
    [InlineData("another.invalid@", false)]
    [InlineData("yet@another.invalid", true)]
    [InlineData("", false)]
    [InlineData(null, false)]
    public void ItShouldValidateEmailCorrectly(string email, bool expected)
    {
        // Act
        bool result = EmailUtils.IsValidEmail(email);

        // Assert
        Assert.Equal(expected, result);
    }
}