﻿using CleanArchMVC.Domain.Entities;
using CleanArchMVC.Domain.Validation;
using FluentAssertions;

namespace CleanArchMvc.Domain.Tests
{
    public class ProductUnitTest1
    {
        [Fact]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product(1, "Product name", "Product Description", 9.99m, 99, "product image");
            action
                .Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Product name", "Product Description", 9.99m, 99, "product image");
            action
                .Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid Id value");
        }

        [Fact]
        public void CreateProduct_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Product(1, "Pr", "Product Description", 9.99m, 99, "product image");
            action
                .Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid name, too short, minimum 3 characters");
        }

        [Fact]
        public void CreateProduct_LongImageName_DomainExceptionLongImageName()
        {
            Action action = () => new Product(1, "Product name", "Product Description", 9.99m, 99,
                "product image toooooooooooooooooooooooooooooooooooooooooooo " +
                "loooooooooooooooooooooooooooooooooooooooooooooooooooooooooo" +
                "ooooooooooooooooooooooooooooooooggggggggggggggggggggggggggg" +
                "ggggggggggggggggggggggggggggggggggggggggggggggggggggggggggg" +
                "gggggggggggggggggggg");
            action
            .Should()
            .Throw<DomainExceptionValidation>()
            .WithMessage("Invalid image name, too long, maximum 250 characters");
        }

        [Fact]
        public void CreateProduct_WithNullImageName_NoDomainException()
        {
            Action action = () => new Product(1, "Product", "Product Description", 9.99m, 99, null);
            action
                .Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Fact]
        public void CreateProduct_WithEmptyImageName_NoDomainException()
        {
            Action action = () => new Product(1, "Product", "Product Description", 9.99m, 99, "");
            action
                .Should()
                .NotThrow<DomainExceptionValidation>();
        }

        [Theory]
        [InlineData(-5)]
        public void CreateProduct_InvalidStockValue_ExceptionDomainNegativeValue(int value)
        {
            Action action = () => new Product(1, "Pro", "Product Description", 9.99m, value, "product img");
            action
                .Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid stock value");
        }

        [Theory]
        [InlineData(-5)]
        public void CreateProduct_InvalidPriceValue_ExceptionDomain(int value)
        {
            Action action = () => new Product(1, "Pro", "Product Description", -9.99m, value, "product img");
            action
                .Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid price value");
        }

    }
}
