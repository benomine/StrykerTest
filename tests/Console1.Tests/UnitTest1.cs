using System;
using Console1;
using Xunit;

namespace Console1.Tests;

public class UnitTest1
{
    private readonly Calculator _sut = new();

    [Theory]
    [InlineData(1, 2, 3)]
    [InlineData(0, 0, 0)]
    [InlineData(-1, 1, 0)]
    [InlineData(100, 200, 300)]
    public void Add_ReturnsSum(int a, int b, int expected)
    {
        var result = _sut.Add(a, b);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(5, 3, 2)]
    [InlineData(0, 5, -5)]
    [InlineData(-3, -2, -1)]
    [InlineData(10, 0, 10)]
    public void Substract_ReturnsDifference(int a, int b, int expected)
    {
        var result = _sut.Substract(a, b);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(3, 4, 12)]
    [InlineData(-3, 4, -12)]
    [InlineData(0, 12345, 0)]
    [InlineData(7, -2, -14)]
    public void Multiply_ReturnsProduct(int a, int b, int expected)
    {
        var result = _sut.Multiply(a, b);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(6, 3, 2)]
    [InlineData(7, 3, 2)]
    [InlineData(-7, 3, -2)]
    [InlineData(7, -3, -2)]
    public void Divide_ReturnsQuotient_IntegerDivisionBehavior(int a, int b, int expected)
    {
        var result = _sut.Divide(a, b);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Divide_ByZero_ThrowsDivideByZeroException()
    {
        Assert.Throws<DivideByZeroException>(() => _sut.Divide(1, 0));
    }

    [Theory]
    [InlineData(5, 7)]
    [InlineData(-2, 3)]
    [InlineData(0, 0)]
    public void Add_IsCommutative(int a, int b)
    {
        var left = _sut.Add(a, b);
        var right = _sut.Add(b, a);

        Assert.Equal(left, right);
    }

    [Theory]
    [InlineData(5, 3)]
    [InlineData(0, 5)]
    [InlineData(-3, -2)]
    public void Substract_IsNotCommutative(int a, int b)
    {
        var first = _sut.Substract(a, b);
        var swapped = _sut.Substract(b, a);

        Assert.NotEqual(first, swapped);
    }

    [Theory]
    [InlineData(2, 3, 6)]
    [InlineData(-2, -3, 6)]
    [InlineData(-2, 3, -6)]
    public void Multiply_RepeatedAddEquivalent(int a, int b, int expected)
    {
        var result = _sut.Multiply(a, b);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Add_WithLargeNumbers_WorksForTypicalCases()
    {
        var result = _sut.Add(int.MaxValue - 1, 1);

        Assert.Equal(int.MaxValue, result);
    }

    [Fact]
    public void Substract_WithNegativeResults_ReturnsNegative()
    {
        var result = _sut.Substract(0, 5);

        Assert.True(result < 0);
    }

    [Fact]
    public void Add_Overflow_WrapsAround()
    {
        var result = _sut.Add(int.MaxValue, 1);

        int expected = unchecked(int.MaxValue + 1);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Multiply_Overflow_WrapsAround()
    {
        var result = _sut.Multiply(int.MaxValue, 2);

        int expected = unchecked(int.MaxValue * 2);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Divide_MinValue_ByMinusOne_Behavior()
    {
        Assert.Throws<OverflowException>(() => _sut.Divide(int.MinValue, -1));
    }

    [Theory]
    [InlineData(0, 1, 1)]
    [InlineData(1, 0, 1)]
    [InlineData(123, 456, 579)]
    [InlineData(-1000, 500, -500)]
    [InlineData(214748364, 6, 214748370)]
    public void Add_ManyCombinations(int a, int b, int expected)
    {
        var result = _sut.Add(a, b);

        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(10, 2, 5)]
    [InlineData(9, 2, 4)]
    [InlineData(-9, 2, -4)]
    [InlineData(9, -2, -4)]
    [InlineData(0, 1, 0)]
    public void Divide_MultipleCases(int a, int b, int expected)
    {
        var result = _sut.Divide(a, b);

        Assert.Equal(expected, result);
    }
}
