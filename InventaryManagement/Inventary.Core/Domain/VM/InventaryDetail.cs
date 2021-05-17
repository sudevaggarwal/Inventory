using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using FluentValidation;

namespace Inventary.Core.Domain.VM
{
    public class InventaryDetail
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? PricePerUnit { get; set; }
        public int? Quantity { get; set; }
    }
    public class InventaryDetailList
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class SpecificInventary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? PricePerUnit { get; set; }
        public int? Quantity { get; set; }
    }
    public class InventaryDetailValidator : AbstractValidator<InventaryDetail>
    {
        public InventaryDetailValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty()
                .WithMessage("Please Enter Name");
            RuleFor(c => c.PricePerUnit).GreaterThan(0)
                .When(c => c.PricePerUnit != null)
                .WithMessage("Price per unit must be greater then zero").NotNull().WithMessage("Please Enter Price Per Unit");
            RuleFor(c => c.Quantity).GreaterThan(0)
               .When(c => c.Quantity != null)
               .WithMessage("Quantity must be greater then zero")
               .NotNull().WithMessage("Please Enter Quantity");
        }
    }
}
